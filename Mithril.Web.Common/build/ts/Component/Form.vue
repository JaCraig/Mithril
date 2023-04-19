<template>
    <form :action="action" class="stacked" @reset="reset" @submit="submit" method="post" v-cloak :enctype="encoding">
        <form-validator ref="validation">
            <slot name="validationHeader">The following errors were found</slot>
        </form-validator>
        <form-field-complex :schema="schema" :model="model" @changed="setModelValue" @click="buttonClicked" @error="error"
            @exception="exception">
        </form-field-complex>

        <pre v-if="debug">
            {{ internalModel }}
        </pre>

        <pre v-if="debug">
            {{ schema }}
        </pre>
    </form>
</template>

<script lang="ts">
import { Request } from '../Framework/AJAX/Request';
import Vue from 'vue';
import FormValidator from './FormValidator.vue';
import FormFieldInput from './FormComponents/Input.vue';
import FormFieldSelect from './FormComponents/Select.vue';
import FormFieldCheckbox from './FormComponents/Checkbox.vue';
import FormFieldRadio from './FormComponents/Radio.vue';
import FormFieldTextarea from './FormComponents/TextArea.vue';
import FormFieldText from './FormComponents/Text.vue';
import FormFieldUpload from './FormComponents/Upload.vue';
import FormFieldButtons from './FormComponents/Buttons.vue';
import FormFieldComplexConditional from './FormComponents/ComplexConditional.vue';
import FormFieldComplexList from './FormComponents/ComplexList.vue';
import FormFieldComplexTabs from './FormComponents/ComplexTabs.vue';
import FormFieldComplex from './FormComponents/Complex.vue';
import FormFieldRepeater from './FormComponents/ComplexRepeater.vue';

export default Vue.defineComponent({
    components: {
        "form-field-complex": FormFieldComplex,
        'form-field-complex-conditional': FormFieldComplexConditional,
        'form-field-complex-list': FormFieldComplexList,
        'form-field-complex-tabs': FormFieldComplexTabs,
        'form-field-input': FormFieldInput,
        'form-field-select': FormFieldSelect,
        'form-field-checkbox': FormFieldCheckbox,
        'form-field-radio-list': FormFieldRadio,
        'form-field-text-area': FormFieldTextarea,
        'form-field-text': FormFieldText,
        'form-field-upload': FormFieldUpload,
        'form-field-buttons': FormFieldButtons,
        'form-field-complex-repeater': FormFieldRepeater,
        'form-validator': FormValidator
    },
    data: function () {
        return {
            submitting: false,
            internalModel: this.model
        };
    },
    props: {
        schema: Object,
        model: Object,
        action: {
            default: "",
            type: String,
        },
        ajaxAction: {
            default: "",
            type: String,
        },
        encoding: {
            default: "multipart/form-data",
            type: String
        },
        debug: {
            default: true,
            type: Boolean
        }
    },
    methods: {
        revalidate: function () {
            return this.$refs.validation.revalidate();
        },
        setModelValue: function (newValue: any, field: any) {
            this.internalModel = newValue;
            this.revalidate();
            this.$emit("changed", this.internalModel);
        },
        error: function (errorCode: any) {
            this.$emit("error", errorCode);
        },
        exception: function (errorCode: any) {
            this.$emit("exception", errorCode);
        },
        buttonClicked: function (event: any, field: any) {
            this.revalidate();
            this.$emit("click", event, field);
        },
        reset: function () {
            let that = this;
            setTimeout(function () {
                that.revalidate();
            }, 100);
        },
        submit: function (event: any) {
            let that = this;
            if (!that.revalidate() || that.submitting) {
                event.preventDefault();
                return false;
            }
            that.submitting = true;
            if (that.ajaxAction) {
                Request.post(that.ajaxAction, that.internalModel)
                    .onSuccess(function (x: any) {
                        that.submitting = false;
                        that.$emit("success", x);
                    })
                    .onError(function (x: any) {
                        that.submitting = false;
                        that.$emit("error", x);
                    })
                    .send();
                event.preventDefault();
                return false;
            }
            return true;
        },
        getIDSuffix: function () {
            return "";
        },
    },
    watch: {
        model: function (newModel, oldModel) {
            if (oldModel === newModel) {
                return;
            }
            this.internalModel = newModel;
            if (newModel !== null) {
                this.$nextTick(() => {
                    this.revalidate();
                });
            }
        },
    }
});
</script>