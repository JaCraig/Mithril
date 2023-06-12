<template>
    <div>
        <label :for="getFieldID()">
            <span v-if="internalSchema.displayName">
                {{ internalSchema.displayName }}
            </span>
            <span class="error clear-background" v-if="internalSchema.metadata.required">*</span>
            <input :id="getFieldID()"
                   type="checkbox"
                   :checked="internalModel"
                   @click="changed($event.target.checked)"
                   :disabled="internalSchema.metadata.disabled"
                   :dirname="internalSchema.metadata.dirname"
                   :name="getFieldID()"
                   :readonly="internalSchema.metadata.readonly"
                   :required="internalSchema.metadata.required"
                   :data-error-message-value-missing="internalSchema.metadata.errorMessageValueMissing" />
        </label>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import "../../Framework/Extensions/String";
import PropertySchema from '../DataTypes/PropertySchema';

    export default Vue.defineComponent({
        name: "form-field-checkbox",
        data: function() {
            return {
                internalModel: this.model,
                internalSchema: this.schema,
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
            getFieldID: function () {
                return this.internalSchema.propertyName.slugify() + this.internalSchema.key;
            },
            changed: function(newValue: any) {
                this.$emit("changed", newValue, this.schema);
            },
        },
        watch: {
            model: function(newModel, oldModel) {
                if (oldModel === newModel) {
                    return;
                }
                this.internalModel = newModel;
            },
            schema: function (newSchema, oldSchema) {
                if (oldSchema === newSchema) {
                   return;            
                }
                this.internalSchema = newSchema;
            }
        }
    });

</script>