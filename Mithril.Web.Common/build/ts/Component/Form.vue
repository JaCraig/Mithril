<style scoped>
    .input-group input[type=submit] {
        margin-right: 10px;
    }
</style>
<template>
    <form :action="action" class="stacked" @reset.stop.prevent="reset" @submit="submit" method="post" v-cloak :enctype="encoding" :id="formID">
        <div class="panel error" v-if="!valid">
            <header>Oops, some form elements have problems</header>
        </div>
        <form-field-complex :schema="internalSchema" :model="internalModel" @changed="setModelValue" @click="buttonClicked" @error="error"
                            @exception="exception">
        </form-field-complex>
        <div>
            <div class="input-group">
                <input type="submit" :disabled="submitting" :value="submitting ? 'Submitting...' : 'Submit'" />
                <input type="reset" :disabled="submitting" value="Reset" />
            </div>
        </div>

        <pre v-if="debug">
            {{ internalModel }}
        </pre>

        <pre v-if="debug">
            {{ internalSchema }}
        </pre>
    </form>
</template>

<script lang="ts">
    import { Request } from '../Framework/Request';
    import Vue from 'vue';
    import FormFieldComplex from './FormComponents/Complex.vue';
    import PropertySchema from "./DataTypes/PropertySchema";
    import { Logger } from '../Framework/Logging';
    import { Validation } from '../Framework/Validation';

    export default Vue.defineComponent({
        name: "mithril-form",
        components: {
            "form-field-complex": FormFieldComplex
        },
        data: function () {
            return {
                submitting: false,
                internalModel: JSON.parse(JSON.stringify(this.model)),
                internalSchema: { fields: this.schema },
                formID: "mithril-form-" + Math.floor(Math.random() * 1000000000),
                valid: false
            };
        },
        props: {
            schema: {
                type: Array<PropertySchema>,
                default: []
            },
            model: {
                type: Object,
                default: {}
            },
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
            revalidate: async function () {
                this.valid = await Validation.validateForm(document.getElementById(this.formID) as HTMLFormElement)
                return this.valid;
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
                Logger.debug("Resetting form to state:", that.model);
                that.internalModel = JSON.parse(JSON.stringify(that.model));
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
                Logger.debug("Submitting form to location: ", { "location": that.ajaxAction });
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
            }
        },
        watch: {
            schema: function (newSchema: Array<PropertySchema>, oldSchema: Array<PropertySchema>) {
                if (oldSchema === newSchema) {
                    return;
                }
                this.internalSchema = newSchema;
                if (this.internalModel !== null) {
                    this.$nextTick(() => {
                        this.revalidate();
                    });
                }
            },
            model: function (newModel: Object, oldModel: Object) {
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