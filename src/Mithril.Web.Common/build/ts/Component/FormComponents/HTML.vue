<style type="text/less" scoped>
    .editor {
        padding-bottom: 10px;
    }

    div[role=presentation].tox-form__group {
        height: 500px;
    }
</style>
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
        <div class="editor">
            <textarea :id="getFieldID()"
                        v-model="internalModel"
                        :disabled="internalSchema.metadata.disabled"
                        :height="internalSchema.metadata.height"
                        :maxlength="internalSchema.metadata.maxlength"
                        :minlength="internalSchema.metadata.minlength"
                        :name="getFieldID()"
                        :placeholder="internalSchema.metadata.placeholder"
                        :title="internalSchema.metadata.placeholder"
                        :readonly="internalSchema.metadata.readonly"
                        :class="internalSchema.inputClasses"
                        :required="internalSchema.metadata.required"
                        :width="internalSchema.metadata.width"
                        :rows="internalSchema.metadata.rows || 3"
                        :data-error-message-value-missing="internalSchema.metadata.errorMessageValueMissing"
                        :data-error-message-too-long="internalSchema.metadata.errorMessageTooLong"
                        :data-error-message-too-short="internalSchema.metadata.errorMessageTooShort">
            </textarea>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue'
    declare var tinymce: any;
    import "../../Framework/Extensions/String";
    import { BrowserUtils } from '../../Framework/Browser/BrowserUtils';
    import { Request, StorageMode } from '../../Framework/Request';
    import debounce from "../../Framework/Utils/Debounce";
    import PropertySchema from '../DataTypes/PropertySchema';
    import { TextAreaElementValidationRule } from '../../Framework/Validation';
    import { Logger } from "../../Framework/Logging";

    export default Vue.defineComponent({
        name: "form-field-html",
        data: function () {
            return {
                internalModel: this.model,
                internalSchema: this.schema,
                errorMessage: "",
                currentCallback: null
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
            compiledMarkdown: function () {
                return this.markdown(this.currentContent);
            },
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
            imageFilePicker: function (callback: any, value: any, meta: any) {
                this.currentCallback = callback;
                tinymce.activeEditor.windowManager.open({
                    body: {
                        type: 'panel',
                        items: [{
                            type: 'htmlpanel',
                            html: '<iframe id="media-browser" src="/services/filebrowser/'+meta.filetype+'" style="width: 100%; height: 565px; border: 0;"></iframe>',
                            flex: 1
                        }]
                    },
                    title: meta.filetype.charAt(0).toUpperCase() + meta.filetype.substr(1).toLowerCase() + ' Picker',
                    size: 'large',
                    //buttons: [{
                    //    type: 'cancel',
                    //    text: 'Close',
                    //    onclick: 'close'
                    //}],
                });
            },
            processEditOperation: function (operation: any) {
                this.text = tinymce.get(this.getFieldID()).getContent();
                this.changed(this.text);
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
        unmounted: function () {
            tinymce.remove();
        },
        mounted: function () {
            let that = this;
            window.addEventListener('message', function (message) {
                if (typeof message.data !== 'string'|| !that.currentCallback) {
                    return;
                }

                let command = JSON.parse(message.data);
                if (command.href) {
                    that.currentCallback(command.href, { title: command.href });
                }
                that.currentCallback = null;
                tinymce.activeEditor.windowManager.close();
            });
            let tempID = this.getFieldID();
            tinymce.init({
                selector: '#' + tempID,
                height: '500px',
                skin: 'oxide-dark',
                plugins: [
                  'advlist', 'autolink', 'link', 'image', 'lists', 'charmap', 'preview', 'anchor', 'pagebreak',
                  'searchreplace', 'wordcount', 'visualblocks', 'visualchars', 'code', 'fullscreen', 'insertdatetime',
                  'media', 'table', 'emoticons', 'template'
                ],
                menu: {
                    file: { title: 'File', items: 'newdocument restoredraft | preview | export print | deleteallconversations' },
                    edit: { title: 'Edit', items: 'undo redo | cut copy paste pastetext | selectall | searchreplace' },
                    view: { title: 'View', items: 'code | visualaid visualchars visualblocks | spellchecker | preview fullscreen | showcomments' },
                    insert: { title: 'Insert', items: 'image link media addcomment pageembed template codesample inserttable | charmap emoticons hr | pagebreak nonbreaking anchor tableofcontents | insertdatetime' },
                    format: { title: 'Format', items: 'bold italic underline strikethrough superscript subscript codeformat | styles blocks fontfamily fontsize align lineheight | forecolor backcolor | language | removeformat' },
                    tools: { title: 'Tools', items: 'spellchecker spellcheckerlanguage | a11ycheck code wordcount' },
                    table: { title: 'Table', items: 'inserttable | cell row column | advtablesort | tableprops deletetable' }
                },
                textpattern_patterns: [
                    { start: '*', end: '*', format: 'italic' },
                    { start: '**', end: '**', format: 'bold' },
                    { start: '#', format: 'h1' },
                    { start: '##', format: 'h2' },
                    { start: '###', format: 'h3' },
                    { start: '####', format: 'h4' },
                    { start: '#####', format: 'h5' },
                    { start: '######', format: 'h6' },
                    { start: '1. ', cmd: 'InsertOrderedList' },
                    { start: '* ', cmd: 'InsertUnorderedList' },
                    { start: '- ', cmd: 'InsertUnorderedList' }
                ],
                relative_urls: false,
                a11y_advanced_options: true,
                file_picker_callback: function (callback: any, value: any, meta: any) {
                    that.imageFilePicker(callback, value, meta);
                },
                setup: function (ed: any) {
                    Logger.debug('TinyMCE setup');
                    ed.on('change', function (e: any) {
                        that.innerModel = ed.getContent();
                        that.changed(that.innerModel);
                    });
                    ed.on('KeyUp', function (e: any) {
                        that.innerModel = ed.getContent();
                        that.changed(that.innerModel);
                    });
                }
            });
        },
    });
</script>