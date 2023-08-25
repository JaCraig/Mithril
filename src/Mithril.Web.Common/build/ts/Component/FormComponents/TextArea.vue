
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
        <textarea :id="getFieldID()"
            v-model="internalModel"
            @input="changed($event.target.value)"
            :height="internalSchema.metadata.height"
            :maxlength="internalSchema.metadata.maxlength"
            :minlength="internalSchema.metadata.minlength"
            :name="getFieldID()"
            :placeholder="internalSchema.metadata.placeholder"
            :title="internalSchema.metadata.placeholder"
            :readonly="internalSchema.metadata.readonly"
            :required="internalSchema.metadata.required"
            :width="internalSchema.metadata.width"
            :rows="internalSchema.metadata.rows || 3"
            :disabled="internalSchema.metadata.disabled"
            :data-error-message-value-missing="internalSchema.metadata.errorMessageValueMissing"
            :data-error-message-pattern-mismatch="internalSchema.metadata.errorMessagePatternMismatch"
            :data-error-message-range-overflow="internalSchema.metadata.errorMessageRangeOverflow"
            :data-error-message-range-underflow="internalSchema.metadata.errorMessageRangeUnderflow"
            :data-error-message-step-mismatch="internalSchema.metadata.errorMessageStepMismatch"
            :data-error-message-too-long="internalSchema.metadata.errorMessageTooLong"
            :data-error-message-too-short="internalSchema.metadata.errorMessageTooShort"
            :data-error-message-bad-input="internalSchema.metadata.errorMessageBadInput"
            :data-error-message-type-mismatch="internalSchema.metadata.errorMessageTypeMismatch">
        </textarea>
        <div class="clear-background active right small" v-if="internalSchema.metadata.maxlength">
            <span class="fas fa-info-circle"></span>
            {{ charactersRemaining }} characters remaining ({{ internalSchema.metadata.maxlength }} max)
        </div>
        <br class="clear" />
    </div>
</template>

<script lang="ts">
    import "../../Framework/Extensions/String";
    import Vue from 'vue';
    import PropertySchema from '../DataTypes/PropertySchema';
    import { TextAreaElementValidationRule } from '../../Framework/Validation';

    export default Vue.defineComponent({
        name: "form-field-textarea",
        data: function () {
            return {
                internalModel: this.model,
                internalSchema: this.schema,
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
        computed: {
            charactersRemaining: function () {
                return this.internalSchema.metadata.maxlength - this.internalModel.length;
            }
        },
        methods: {
            // determines if the field should be validated
            willValidate: function () {
                return this.internalSchema.metadata.required || this.internalSchema.metadata.maxlength || this.internalSchema.metadata.minlength || this.internalSchema.metadata.pattern || this.internalSchema.metadata.min || this.internalSchema.metadata.max;
            },
            // revalidates the field
            revalidate: async function () {
                let item = document.getElementById(this.getFieldID()) as HTMLTextAreaElement;
                if (item == null) {
                    return;
                }
                let result = await new TextAreaElementValidationRule().validate(item);
                this.errorMessage = result.errorMessage;
                return result.isValid;
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
                this.revalidate();
                this.$emit("changed", newValue, this.schema);
            },
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
        mounted: function () {
            this.$nextTick(function () {
                this.revalidate();
            });
        }
    });

</script>