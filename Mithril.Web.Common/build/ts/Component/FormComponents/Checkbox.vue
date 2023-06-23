<template>
    <div>
        <label :for="getFieldID()">
            <span v-if="internalSchema.displayName">
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
            }
        },
        methods: {
            willValidate: function() {
                return this.internalSchema.metadata.required;
            },
            revalidate: async function () {
                let result = await new InputElementValidationRule().validate(document.getElementById(this.getFieldID()) as HTMLInputElement);
                this.errorMessage = result.errorMessage;
                return result.isValid;
            },
            getFieldID: function () {
                return this.internalSchema.propertyName.slugify() + this.internalSchema.key;
            },
            changed: function(newValue: any) {
                this.revalidate();
                this.$emit("changed", newValue, this.schema);
            },
        },
        watch: {
            model: function(newModel, oldModel) {
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