<template>
    <div>
        <label :for="getFieldID()" v-if="internalSchema.displayName">
            {{ internalSchema.displayName }}
            <span class="error clear-background" v-if="internalSchema.metadata.required">*</span>
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
            }
        },
        methods: {
            willValidate: function() {
                return this.internalSchema.metadata.required || this.internalSchema.metadata.maxlength || this.internalSchema.metadata.minlength || this.internalSchema.metadata.pattern || this.internalSchema.metadata.min || this.internalSchema.metadata.max;
            },
            revalidate: async function () {
                let result = await new InputElementValidationRule().validate(document.getElementById(this.getFieldID()) as HTMLInputElement);
                this.errorMessage = result.errorMessage;
                return result.isValid;
            },
            convertToDate: function (value: string) {
                if (this.schema.metadata.isUTC) {
                    return moment.utc(value || new Date()).local();
                }
                return moment(value || new Date());
            },
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
            getFieldID: function () {
                return this.internalSchema.propertyName.slugify() + this.internalSchema.key;
            },
            changed: function (newValue: any) {
                let that = this;
                this.updateDataList();
                this.revalidate();
                //if(that.schema.datalistUrl) {
                //    if(that.timer !== 0) {
                //        clearTimeout(that.timer);
                //    }
                //    that.timer = setTimeout(function() {
                //    Request.post(that.schema.datalistUrl,{ value: newValue, queryCount: ++that.count })
                //            .onSuccess(function(ev:any){
                //                if(!ev){
                //                    return;
                //                }else if(ev.queryCount && ev.queryCount == that.count) {
                //                    that.schema.datalist = ev.value;
                //                } else if(!ev.queryCount) {
                //                    that.schema.datalist = ev;
                //                }
                //            })
                //            .onError(function (x) {
                //                that.$emit("error", x);
                //            })
                //            .send();
                //    }, 100);
                //}
                this.$emit("changed", newValue, this.schema);
            },
            getList: function () {
                if (this.schema.metadata.datalist) {
                    return this.getFieldID() + "-list";
                }
                return null;
            },
            updateDataList: function () {
                if (!this.internalSchema.queryType) {
                    return;
                }
                let that = this;

                debounce(() => {

                    Request.post('/api/query', {
                        query: that.datalistQuery,
                        variables: {
                            queryType: that.internalSchema.queryType,
                            queryFilter: that.internalModel
                        }
                    })
                        .onSuccess((data: any) => {
                            that.internalSchema.metadata.datalist = data.data.dropDown;
                        })
                        .withStorageMode(StorageMode.StorageAndUpdate)
                        .send();
                }, 300);
            }
        },
        created: function () {
            this.updateDataList();
        },
        watch: {
            model: function (newModel, oldModel) {
                if (oldModel === newModel) {
                    return;
                }
                this.internalModel = newModel;
                this.$nextTick(function () {
                    this.revalidate();
                });
            },
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
        mounted: function() {
            this.$nextTick(function () {
                this.revalidate();
            });
        }
    });
</script>