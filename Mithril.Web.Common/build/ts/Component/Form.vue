<template>
    <form :action="action" class="stacked" @reset="reset" @submit="submit" method="post" v-cloak :enctype="encoding">
        <form-validator ref="validation">
            <slot name="validationHeader">The following errors were found</slot>
        </form-validator>
        <form-field-complex :schema="internalSchema" :model="internalModel" @changed="setModelValue" @click="buttonClicked" @error="error"
            @exception="exception">
        </form-field-complex>

        <pre v-if="debug">
            {{ internalModel }}
        </pre>

        <pre v-if="debug">
            {{ internalSchema }}
        </pre>
    </form>
</template>

<script lang="ts">
import { Request } from '../Framework/AJAX/Request';
import Vue from 'vue';
import FormValidator from './FormValidator.vue';
import FormFieldComplex from './FormComponents/Complex.vue';

    export default Vue.defineComponent({
        name: "mithril-form",
        components: {
            "form-field-complex": FormFieldComplex,
            "form-validator": FormValidator
        },
        data: function () {
            return {
                submitting: false,
                internalModel: this.model,
                internalSchema: { fields: this.schema }
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