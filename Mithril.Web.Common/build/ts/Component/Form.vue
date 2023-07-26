<style scoped>
    .input-group input[type=submit] {
        margin-right: 10px;
    }
</style>
<template>
    <form :action="action" class="stacked" @reset.stop.prevent="reset" @submit="submit" method="post" v-cloak :enctype="encoding" :id="formID">
        <div class="panel error" v-if="!valid">
            <header>Oops, some form elements have problems</header>
            <div class="body">
                We're sorry but some of the fields are having issues.
            </div>
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

        <div v-if="debug" class="flex row no-wrap">
            <div class="panel">
                <header>Form Schema</header>
                <div class="body">
                    <pre>
                        {{internalSchema}}
                    </pre>
                </div>
            </div>
            <div class="panel">
                <header>Form Model</header>
                <div class="body">
                    <pre>
                        {{internalModel}}
                    </pre>
                </div>
            </div>
        </div>
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
            // revalidate the input field
            revalidate: async function () {
                this.valid = await Validation.validateForm(document.getElementById(this.formID) as HTMLFormElement);
                return this.valid;
            },
            // set the model value and revalidate
            // newValue: the new value to set
            // field: the field to set
            setModelValue: function (newValue: any, field: any) {
                this.internalModel = newValue;
                this.revalidate();
                this.$emit("changed", this.internalModel);
            },
            // emit an error event
            // errorCode: the error code to emit
            error: function (errorCode: any) {
                this.$emit("error", errorCode);
            },
            // emit an exception event
            // errorCode: the error code to emit
            exception: function (errorCode: any) {
                this.$emit("exception", errorCode);
            },
            // emit a click event on a button click
            // event: the event that was clicked
            // field: the field that was clicked
            buttonClicked: function (event: any, field: any) {
                this.revalidate();
                this.$emit("click", event, field);
            },
            // reset the form to the original state and revalidate
            reset: function () {
                let that = this;
                Logger.debug("Resetting form to state:", that.model);
                that.internalModel = JSON.parse(JSON.stringify(that.model));
                setTimeout(function () {
                    that.revalidate();
                }, 100);
            },
            // submit the form to the server
            // event: the event that was submitted
            submit: function (event: any) {
                let that = this;
                if (!that.revalidate() || that.submitting) {
                    event.preventDefault();
                    return false;
                }
                that.submitting = true;
                if (that.ajaxAction) {

                    Logger.debug("Submitting form to location: ", { "location": that.ajaxAction });

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
                this.$emit("submit", event, that.internalModel); //{ event: event, model: that.internalModel });
                event.preventDefault();
                return false;
            },
            // get the ID suffix for the form
            getIDSuffix: function () {
                return "";
            }
        },
        watch: {
            // watch for changes to the schema
            // newSchema: the new schema
            // oldSchema: the old schema
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
            // watch for changes to the model
            // newModel: the new model
            // oldModel: the old model
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
        },
        // mounted event handler for the component (revalidate the form)
        mounted: function () {
            this.$nextTick(function () {
                this.revalidate();
            });
        }
    });
</script>