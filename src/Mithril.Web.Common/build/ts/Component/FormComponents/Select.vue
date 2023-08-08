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
        <select v-model="internalModel"
                :disabled="internalSchema.metadata.disabled"
                :name="internalSchema.metadata.inputName || getFieldID()"
                :height="internalSchema.metadata.height"
                :id="getFieldID()"
                @change="changed(internalModel)"
                :readonly="internalSchema.metadata.readonly"
                :required="internalSchema.metadata.required"
                :multiple="internalSchema.metadata.multiple"
                :class="internalSchema.metadata.inputClasses"
                :width="internalSchema.metadata.width"
                :data-error-message-value-missing="internalSchema.metadata.errorMessageValueMissing">
            <option v-for="value in internalSchema.metadata.options" :value="value.key" :selected="isSelected(value)" v-bind:key="value.key">
                {{ value.value }}
            </option>
        </select>
    </div>
</template>

<script lang="ts">
    import "../../Framework/Extensions/String";
    import Vue from 'vue';
    import PropertySchema from '../DataTypes/PropertySchema';
    import { SelectElementValidationRule } from '../../Framework/Validation';
    import { Request, StorageMode } from "../../Framework/Request";
    import { Logger } from "../../Framework/Logging";

    export default Vue.defineComponent({
        name: "form-field-select",
        data: function () {
            return {
                internalModel: this.model,
                internalSchema: this.schema,
                optionsQuery: `query($queryType: String!) { dropDown(type: $queryType) { key, value } }`,
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
            }
        },
        methods: {
            // determines if the field will be validated
            willValidate: function() {
                return this.internalSchema.metadata.required;
            },
            // revalidate the input field
            revalidate: async function () {
                let result = await new SelectElementValidationRule().validate(document.getElementById(this.getFieldID()) as HTMLSelectElement);
                this.errorMessage = result.errorMessage;
                return result.isValid;
            },
            // gets the field id
            getFieldID: function () {
                return this.internalSchema.propertyName.slugify() + this.internalSchema.key;
            },
            // called when the value changes
            // emits the changed event
            // newValue: the new value
            changed: function (newValue: any) {
                this.revalidate();
                this.$emit("changed", newValue, this.schema);
            },
            // determines if the value is selected
            // value: the value to check
            isSelected: function (value: any) {
                return this.internalModel === value.key;
            },
            // gets the values for the select element
            getValues: async function () {
                if (!this.internalSchema.metadata.queryType) {
                    return;
                }
                let that = this;
                Logger.debug("Select: Getting values for " + this.internalSchema.metadata.queryType);

                await Request.post('/api/query', {
                    query: that.optionsQuery,
                    variables: {
                        queryType: that.internalSchema.metadata.queryType
                    }
                })
                    .onSuccess((data: any) => {
                        Logger.debug("Select: " + that.internalSchema.metadata.queryType + " loaded successfully.", data.data.dropDown);
                        that.internalSchema.metadata.options = data.data.dropDown;
                    })
                    .onError((error: any) => {
                        Logger.error("Select: " + that.internalSchema.metadata.queryType + " failed to load.", error);
                    })
                    .withStorageMode(StorageMode.StorageAndUpdate)
                    .send();
            },
        },
        // created event, gets the values for the select element
        created: function () {
            this.getValues();
        },
        watch: {
            // watches the model and revalidates when it changes
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
            // watches the schema and revalidates when it changes
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
        // mounted event, revalidates the field
        mounted: function() {
            this.$nextTick(function () {
                this.revalidate();
            });
        }
    });
</script>