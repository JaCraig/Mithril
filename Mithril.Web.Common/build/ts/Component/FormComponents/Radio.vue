
<template>
    <div>
        <label :for="getFieldID()" v-if="internalSchema.displayName">
            {{ internalSchema.displayName }}
            <span class="error clear-background" v-if="internalSchema.metadata.required && errorMessage">*</span>
            <i class="clear-background active no-border small" v-if="internalSchema.metadata.hint"><span class="fas fa-info-circle"></span>{{ internalSchema.metadata.hint }}</i>
            <span class="error clear-background" v-if="errorMessage">{{errorMessage}}</span>
            <span class="success clear-background fas fa-check-circle" v-if="!errorMessage && willValidate()"></span>
        </label>
        <div v-if="internalSchema.metadata.subtitle">{{internalSchema.metadata.subtitle}}</div>
        <div class="flex row text-center">
            <div v-for="(value) in internalSchema.metadata.options" v-bind:key="value.key">
                <label>
                    <input :id="getFieldID(value.key)"
                           type="radio"
                           :checked="isItemChecked(value.key)"
                           @click="changed(value.key)"
                           :disabled="internalSchema.metadata.disabled"
                           :name="internalSchema.metadata.inputName || getFieldName()"
                           :readonly="internalSchema.metadata.readonly"
                           :class="internalSchema.metadata.inputClasses"
                           :value="value.key" />
                    {{ $filters.capitalize(value.value) }}
                </label>
            </div>
        </div>
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
        name: "form-field-radio",
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
            }
        },
        methods: {
            // determines if the field will be validated
            willValidate: function () {
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
                if (this.internalSchema == null) {
                    return;
                }
                return this.internalSchema.propertyName.slugify() + this.internalSchema.key;
            },
            // get the field name
            getFieldName: function () {
                if (this.internalSchema.id) {
                    return this.internalSchema.id;
                }
                return this.internalSchema.propertyName.slugify();
            },
            // called when the value changes
            // emits the changed event
            // newValue: the new value
            changed: function (newValue: any) {
                this.revalidate();
                this.$emit("changed", newValue, this.schema);
            },
            // Is the item checked?
            // item: item to check
            isItemChecked: function (item: any) {
                return item === this.internalModel;
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
        mounted: function () {
            this.$nextTick(function () {
                this.revalidate();
            });
        }
    });
</script>