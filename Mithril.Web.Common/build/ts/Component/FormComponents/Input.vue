<template>
    <div>
        <label :for="getFieldID()" v-if="showLabel && internalSchema.displayName">
            {{ internalSchema.displayName }}
            <span class="error clear-background" v-if="internalSchema.metadata.required && errorMessage">*</span>
            <i class="clear-background active no-border small" v-if="internalSchema.metadata.hint"><span class="fas fa-info-circle"></span>{{ internalSchema.metadata.hint }}</i>
            <span class="error clear-background" v-if="errorMessage">{{errorMessage}}</span>
            <span class="success clear-background fas fa-check-circle" v-if="!errorMessage && willValidate()"></span>
        </label>
        <div v-if="internalSchema.metadata.subtitle">{{internalSchema.metadata.subtitle}}</div>
        <input :id="getFieldID()"
               :type="internalSchema.metadata.inputType"
               v-model="internalModel"
               @input="changed($event.target.value)"
               :disabled="internalSchema.metadata.disabled"
               :accept="internalSchema.metadata.accept"
               :alt="internalSchema.metadata.alt"
               :autocomplete="internalSchema.metadata.autocomplete"
               :checked="internalSchema.metadata.checked"
               :dirname="internalSchema.metadata.dirname"
               :formaction="internalSchema.metadata.formaction"
               :formenctype="internalSchema.metadata.formenctype"
               :formmethod="internalSchema.metadata.formmethod"
               :formnovalidate="internalSchema.metadata.formnovalidate"
               :formtarget="internalSchema.metadata.formtarget"
               :height="internalSchema.metadata.height"
               :list="getList()"
               :max="internalSchema.metadata.max"
               :maxlength="internalSchema.metadata.maxlength"
               :min="internalSchema.metadata.min"
               :minlength="internalSchema.metadata.minlength"
               :multiple="internalSchema.metadata.multiple"
               :name="getFieldID()"
               :pattern="internalSchema.metadata.pattern"
               :placeholder="internalSchema.metadata.placeholder"
               :title="internalSchema.metadata.placeholder"
               :readonly="internalSchema.metadata.readonly"
               :required="internalSchema.metadata.required"
               :size="internalSchema.metadata.size"
               :src="internalSchema.metadata.src"
               :step="internalSchema.metadata.step"
               :width="internalSchema.metadata.width"
               :files="internalSchema.metadata.files"
               :data-error-message-value-missing="internalSchema.metadata.errorMessageValueMissing"
               :data-error-message-pattern-mismatch="internalSchema.metadata.errorMessagePatternMismatch"
               :data-error-message-range-overflow="internalSchema.metadata.errorMessageRangeOverflow"
               :data-error-message-range-underflow="internalSchema.metadata.errorMessageRangeUnderflow"
               :data-error-message-step-mismatch="internalSchema.metadata.errorMessageStepMismatch"
               :data-error-message-too-long="internalSchema.metadata.errorMessageTooLong"
               :data-error-message-too-short="internalSchema.metadata.errorMessageTooShort"
               :data-error-message-bad-input="internalSchema.metadata.errorMessageBadInput"
               :data-error-message-type-mismatch="internalSchema.metadata.errorMessageTypeMismatch" />
        <div class="text-center" v-if="internalSchema.metadata.inputType === 'color' || internalSchema.metadata.inputType === 'range'">{{ internalModel }}</div>
        <datalist v-if="internalSchema.metadata.datalist" :id="getList()">
            <option v-for="(item) in internalSchema.metadata.datalist" :value="item.value" v-bind:key="item.key" />
        </datalist>
    </div>
</template>

<script lang="ts">
    import { Request, StorageMode } from '../../Framework/Request';
    import "../../Framework/Extensions/String";
    import debounce from "../../Framework/Utils/Debounce";
    import Vue from 'vue';
    import moment from 'moment';
    import PropertySchema from '../DataTypes/PropertySchema';
    import { InputElementValidationRule } from '../../Framework/Validation';
    import { Logger } from "../../Framework/Logging";

    export default Vue.defineComponent({
        name: "form-field-input",
        data: function () {
            return {
                internalModel: this.formatValue(this.model),
                internalSchema: this.schema,
                dataListQuery: `query($queryType: String!, $queryFilter: String!) { dropDown(type: $queryType, filter: $queryFilter) { key, value } }`,
                errorMessage: ""
            };
        },
        props: {
            model: {
                type: String,
                required: true,
                default: ""
            },
            schema: {
                type: PropertySchema,
                required: true,
                default: () => new PropertySchema()
            },
            showLabel: {
                type: Boolean,
                required: false,
                default: true
            },
        },
        methods: {
            // determines if the field should be validated
            willValidate: function() {
                return this.internalSchema.metadata.required || this.internalSchema.metadata.maxlength || this.internalSchema.metadata.minlength || this.internalSchema.metadata.pattern || this.internalSchema.metadata.min || this.internalSchema.metadata.max;
            },
            // revalidates the field
            revalidate: async function () {
                let item = document.getElementById(this.getFieldID()) as HTMLInputElement;
                if (item == null) {
                    return;
                }
                let result = await new InputElementValidationRule().validate(item);
                this.errorMessage = result.errorMessage;
                return result.isValid;
            },
            // converts the value to a date
            // value: the value to convert
            convertToDate: function (value: string) {
                if (this.schema.metadata.isUTC) {
                    return moment.utc(value || new Date()).local();
                }
                return moment(value || new Date());
            },
            // formats the value to the correct format
            // value: the value to format
            formatValue: function (value: string) {
                if (!value) {
                    return value;
                }
                if (this.schema.metadata.inputType === "date") {
                    return this.convertToDate(value).format('YYYY-MM-DD');
                }
                if (this.schema.metadata.inputType === "datetime-local" || this.schema.metadata.inputType === "datetime") {
                    return this.convertToDate(value).format('YYYY-MM-DDTHH:mm');
                }
                if (this.schema.metadata.inputType === "month") {
                    return this.convertToDate(value).format('YYYY-MM');
                }
                return value;
            },
            // gets the field id
            getFieldID: function () {
                return this.internalSchema.propertyName.slugify() + this.internalSchema.key;
            },
            // Called when the model changes
            // newValue: the new value
            // emits a changed event
            changed: function (newValue: any) {
                let that = this;
                this.updateDataList();
                this.revalidate();
                this.$emit("changed", newValue, this.schema);
            },
            // Gets the datalist id
            getList: function () {
                if (this.schema.metadata.datalist) {
                    return this.getFieldID() + "-list";
                }
                return null;
            },
            // Updates the datalist for the field
            updateDataList: debounce(async function() {
                if (!this.internalSchema.queryType) {
                    return;
                }
                let that = this;

                Request.post('/api/query', {
                    query: that.datalistQuery,
                    variables: {
                        queryType: that.internalSchema.metadata.queryType,
                        queryFilter: that.internalModel
                    }
                })
                    .onSuccess((data: any) => {
                        Logger.debug("Select: " + that.internalSchema.metadata.queryType + " loaded successfully.", data.data.dropDown);
                        that.internalSchema.metadata.datalist = data.data.dropDown;
                    })
                    .onError((error: any) => {
                        Logger.error("Select: " + that.internalSchema.metadata.queryType + " failed to load.", error);
                    })
                    .withStorageMode(StorageMode.StorageAndUpdate)
                    .send();
            }, 300),
        },
        // created method, updates the datalist values
        created: function () {
            this.updateDataList();
        },
        watch: {
            // watches the model and updates the internal model
            // newModel: the new model
            // oldModel: the old model
            model: function (newModel, oldModel) {
                if (oldModel === newModel) {
                    return;
                }
                this.internalModel = newModel;
                this.$nextTick(function () {
                    this.revalidate();
                });
            },
            // watches the schema and updates the internal schema
            // newSchema: the new schema
            // oldSchema: the old schema
            schema: function (newSchema, oldSchema) {
                if (oldSchema === newSchema) {
                    return;
                }
                this.internalSchema = newSchema;
                this.$nextTick(function () {
                    this.revalidate();
                });
            }
        },
        // mounted method, revalidates the field
        mounted: function() {
            this.$nextTick(function () {
                this.revalidate();
            });
        }
    });
</script>