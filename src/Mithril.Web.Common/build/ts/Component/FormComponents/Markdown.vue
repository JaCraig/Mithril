<style type="text/less" scoped>
    textarea {
        min-height:500px;
    }

    nav.menu h1,
    nav.menu h2,
    nav.menu h3,
    nav.menu h4,
    nav.menu h5,
    nav.menu h6 {
        margin: 0 1em;
    }
</style>
<template>
    <div class="flex row">
        <div class="flex-item flex justify-space-between column wrap" id="editorRawInput">
            <nav class="menu pill flex-item">
                <ul>
                    <li class="item has-children">
                        <a href="#!">Header</a>
                        <ul>
                            <li><h1><a href="#!" @click.stop.prevent="tag('#')">Header 1</a></h1></li>
                            <li><h2><a href="#!" @click.stop.prevent="tag('##')">Header 2</a></h2></li>
                            <li><h3><a href="#!" @click.stop.prevent="tag('###')">Header 3</a></h3></li>
                            <li><h4><a href="#!" @click.stop.prevent="tag('####')">Header 4</a></h4></li>
                            <li><h5><a href="#!" @click.stop.prevent="tag('#####')">Header 5</a></h5></li>
                            <li><h6><a href="#!" @click.stop.prevent="tag('######')">Header 6</a></h6></li>
                        </ul>
                    </li>
                    <li class="item has-children">
                        <a href="#!">Format</a>
                        <ul>
                            <li><strong><a href="#!" @click.stop.prevent="symmetricTag('**')">Bold</a></strong></li>
                            <li><em><a href="#!" @click.stop.prevent="symmetricTag('*')">Italics</a></em></li>
                            <li><del><a href="#!" @click.stop.prevent="symmetricTag('~~')">Strikethrough</a></del></li>
                            <li><u><a href="#!" @click.stop.prevent="symmetricTag('__')">Underline</a></u></li>
                            <li><a href="#!" @click.stop.prevent="tag('>')">Quote</a></li>
                        </ul>
                    </li>
                    <li><a href="#!" @click.stop.prevent="tag('\n\n-------\n\n')">HR</a></li>
                    <li class="item has-children">
                        <a href="#!">List</a>
                        <ul>
                            <li><a href="#!" @click.stop.prevent="tag('1. ')" class="fa-list-ol">Ordered</a></li>
                            <li><a href="#!" @click.stop.prevent="tag('* ')" class="fa-list-ul">Unordered</a></li>
                        </ul>
                    </li>
                    <li class="item has-children">
                        <a href="#!">File</a>
                        <ul>
                            <li><a href="#!" @click.stop.prevent="showFrame('image')" class="fa-image"></a></li>
                            <li><a href="#!" @click.stop.prevent="showFrame('file')" class="fa-link"></a></li>
                        </ul>
                    </li>
                </ul>
            </nav>
            <textarea v-model="internalModel" id="editorInput" name="editorInput" class="flex-item" @keyup="keyup"></textarea><br />
        </div>
        <div class="flex-item" id="editorCompiledInput" v-if="displayPreview">
            <h1>{{ currentTitle }}</h1>
            <div v-html="compiledMarkdown"></div>
        </div>
        <div class="iframe-holder z-depth-2" v-if="displayFrame">
            <iframe :src="iframeSrc"></iframe>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import showdown from 'showdown';
    import moment from 'moment';
    import "../../Framework/Extensions/String";
    import { BrowserUtils } from '../../Framework/Browser/BrowserUtils';
    import { Request, StorageMode } from '../../Framework/Request';
    import debounce from "../../Framework/Utils/Debounce";
    import PropertySchema from '../DataTypes/PropertySchema';
    import { TextAreaElementValidationRule } from '../../Framework/Validation';
    import { Logger } from "../../Framework/Logging";

    export default Vue.defineComponent({
        name: "form-field-markdown",
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
            showFrame: function (type: string) {
                this.displayFrame = true;
                if (type === "image") {
                    this.iframeSrc = this.iframeImageSrc;
                } else {
                    this.iframeSrc = this.iframeFileSrc;
                }
            },
            markdown: function (content: string) {
                if (!content) {
                    return content;
                }
                var converter = new showdown.Converter({ tables: true, strikethrough: true, emoji: true, underline: true, ghMentions: true, ghMentionsLink: BrowserUtils.domain + "People/{u}" });
                return converter.makeHtml(content);
            },
            keyup: function () {
                this.$emit("keyup", {
                    content: this.currentContent
                });
            },
            cancel: function () {
                this.$emit("cancel");
            },
            tag: function (tag: string) {
                let editor = <HTMLTextAreaElement>document.getElementById('editorInput');
                if (!editor) {
                    return;
                }
                let start = editor.selectionStart;
                let end = editor.selectionEnd;
                let tagText = tag + this.currentContent.substring(start, end);
                this.currentContent = this.currentContent.substring(0, start) + tagText + this.currentContent.substring(end, this.currentContent.length);
                editor.focus();
            },
            symmetricTag: function (tag: string) {
                let editor = <HTMLTextAreaElement>document.getElementById('editorInput');
                if (!editor) {
                    return;
                }
                let start = editor.selectionStart;
                let end = editor.selectionEnd;
                let tagText = tag + this.currentContent.substring(start, end) + tag;
                this.currentContent = this.currentContent.substring(0, start) + tagText + this.currentContent.substring(end, this.currentContent.length);
                editor.focus();
            },
            linkTag: function (href: string) {
                let editor = <HTMLTextAreaElement>document.getElementById('editorInput');
                if (!editor) {
                    return;
                }
                let start = editor.selectionStart;
                let end = editor.selectionEnd;
                let tagText = "[" + this.currentContent.substring(start, end) + "](" + href + ")";
                this.currentContent = this.currentContent.substring(0, start) + tagText + this.currentContent.substring(end, this.currentContent.length);
                editor.focus();
            },
            imageTag: function (href: string) {
                let editor = <HTMLTextAreaElement>document.getElementById('editorInput');
                if (!editor) {
                    return;
                }
                let start = editor.selectionStart;
                let end = editor.selectionEnd;
                let tagText = "![" + this.currentContent.substring(start, end) + "](" + href + ")";
                this.currentContent = this.currentContent.substring(0, start) + tagText + this.currentContent.substring(end, this.currentContent.length);
                editor.focus();
            }
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
        // mounted is called when the component is loaded
        // this is where we setup the validation rules and the event listeners
        mounted: function () {
            let that = this;
            window.addEventListener('message', function (message) {
                if (typeof message.data !== 'string') return;

                let command = JSON.parse(message.data);
                that.displayFrame = false;
                if (command.href) {
                    if (that.iframeSrc == that.iframeImageSrc) {
                        that.imageTag(command.href);
                    } else {
                        that.linkTag(command.href);
                    }
                }
            });
            this.$nextTick(function () {
                this.revalidate();
            });
        },
    });
</script>