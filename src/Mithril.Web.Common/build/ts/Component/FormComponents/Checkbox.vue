<template>
    <div>
        <label :for="getFieldID()">
            <span v-if="showLabel && internalSchema.displayName">
                {{ internalSchema.displayName }}
            </span>
            <span class="error clear-background" v-if="internalSchema.metadata.required && errorMessage">*</span>
            <span class="error clear-background" v-if="errorMessage">{{errorMessage}}</span>
            <span class="success clear-background fas fa-check-circle" v-if="!errorMessage && willValidate()"></span>
            <div v-if="internalSchema.metadata.subtitle">{{internalSchema.metadata.subtitle}}</div>
            <input :id="getFieldID()"
                   type="checkbox"
                   :checked="internalModel"
                   @click="changed($event.target.checked)"
                   :disabled="internalSchema.metadata.disabled"
                   :dirname="internalSchema.metadata.dirname"
                   :name="getFieldID()"
                   :readonly="internalSchema.metadata.readonly"
                   :required="internalSchema.metadata.required" />
        </label>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import "../../Framework/Extensions/String";
import PropertySchema from '../DataTypes/PropertySchema';
import { InputElementValidationRule } from '../../Framework/Validation';

    export default Vue.defineComponent({
        name: "form-field-checkbox",
        data: function() {
            return {
                internalModel: this.model,
                internalSchema: this.schema,
                errorMessage: ""
            };
        },
        props: {
            model: {
                type: Boolean,
                required: true,
                default: false
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
                let item = document.getElementById(this.getFieldID()) as HTMLInputElement;
                if (item == null) {
                    return;
                }
                let result = await new InputElementValidationRule().validate(item);
                this.errorMessage = result.errorMessage;
                return result.isValid;
            },
            // gets the field id
            getFieldID: function () {
                return this.internalSchema.propertyName.slugify() + this.internalSchema.key;
            },
            // called when the value changes
            // newValue: the new value
            // emits the changed event
            changed: function(newValue: any) {
                this.revalidate();
                this.$emit("changed", newValue, this.schema);
            },
        },
        watch: {
            // watches the model for changes
            // newModel: the new model
            // oldModel: the old model
            model: function(newModel, oldModel) {
                if (oldModel === newModel) {
                    return;
                }
                this.internalModel = newModel;
                this.$nextTick(function () {
                    this.revalidate();
                });
            },
            // watches the schema for changes
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
        // mounted event handler, revalidates the field
        mounted: function() {
            this.$nextTick(function () {
                this.revalidate();
            });
        }
    });

</script>