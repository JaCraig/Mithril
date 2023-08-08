
<template>
    <div>
        Upload
        <label :for="getFieldID()" v-if="!schema.label" :class="schema.labelClasses">
            {{ $filters.capitalize(schema.model) }}
            <span class="error clear-background" v-if="schema.required && errorMessage">*</span>
            <i class="clear-background active no-border small" v-if="schema.hint"><span class="fas fa-info-circle"></span>{{ schema.hint }}</i>
            <span class="error clear-background" v-if="errorMessage">{{errorMessage}}</span>
            <span class="success clear-background fas fa-check-circle" v-if="!errorMessage && willValidate()"></span>
        </label>
        <label :for="getFieldID()" v-if="schema.label" :class="schema.labelClasses">
            {{ schema.label }}
            <span class="error clear-background" v-if="schema.required && errorMessage">*</span>
            <i class="clear-background active no-border small" v-if="schema.hint"><span class="fas fa-info-circle"></span>{{ schema.hint }}</i>
            <span class="error clear-background" v-if="errorMessage">{{errorMessage}}</span>
            <span class="success clear-background fas fa-check-circle" v-if="!errorMessage && willValidate()"></span>
        </label>
        <div v-if="internalSchema.metadata.subtitle">{{internalSchema.metadata.subtitle}}</div>
        <div class="file-upload" :class="schema.inputClasses">
            {{ schema.placeholder }}
            <input :accept="schema.accept"
                   :id="getFieldID()"
                   @change="changed($event)"
                   :multiple="schema.multiple"
                   :name="schema.inputName || getFieldID()"
                   :required="schema.required"
                   type="file"
                   :data-error-message-value-missing="schema.errorMessageValueMissing" />
        </div>
        <div class="flex" v-if="schema.uploadPreview">
            <div class="upload-preview panel" v-for="(file) in files" :class="schema.previewClasses" v-bind:key="generateGuid(file)">
                <header><div class="header" @click="removeFile(file)">×</div>&nbsp;</header>
                <div class="body">
                    {{ file.filename }}
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
    import "../../Framework/Extensions/String";
    import { InputElementValidationRule } from '../../Framework/Validation';

    export default Vue.defineComponent({
        data() {
            return {
                files: [],
                ready: false,
                filesAdded: 0,
                internalModel: this.model,
                errorMessage: ""
            };
        },
        props: {
            model: Object,
            schema: Object,
            idSuffix: String,
        },
        methods: {
            willValidate: function () {
                return this.internalSchema.metadata.required || this.internalSchema.metadata.maxlength || this.internalSchema.metadata.minlength || this.internalSchema.metadata.pattern || this.internalSchema.metadata.min || this.internalSchema.metadata.max;
            },
            revalidate: async function () {
                let result = await new InputElementValidationRule().validate(document.getElementById(this.getFieldID()) as HTMLInputElement);
                this.errorMessage = result.errorMessage;
                return result.isValid;
            },
            getFieldID: function () {
                let result = "";
                if (this.schema.id) {
                    result = this.schema.id;
                } else {
                    result = this.schema.model.slugify();
                }
                if (this.idSuffix !== undefined) {
                    result += this.idSuffix;
                }
                return result;
            },
            changed: function (event: any) {
                this.revalidate();
                let that = this;
                that.filesAdded = that.filesAdded + event.target.files.length;
                that.ready = false;
                for (let x = 0; x < event.target.files.length; ++x) {
                    let reader = new FileReader();
                    reader.onload = (function (file) {
                        return function (readerEvent: any) {
                            that.files = that.files.concat({ filename: file, data: that.base64ArrayBuffer(reader.result) });
                            if (that.files.length === that.filesAdded) {
                                that.ready = true;
                            }
                        };
                    })(event.target.files[x].name);
                    reader.readAsArrayBuffer(event.target.files[x]);
                }
                this.check();
            },
            base64ArrayBuffer: function (buffer: ArrayBuffer) {
                let binary = "";
                let bytes = new Uint8Array(buffer);
                let len = bytes.byteLength;
                for (let x = 0; x < len; ++x) {
                    binary += String.fromCharCode(bytes[x]);
                }
                return window.btoa(binary);
            },
            check: function () {
                if (this.ready === true) {
                    this.$emit("changed", this.files, this.schema);
                    return;
                }
                setTimeout(this.check, 100);
            },
            removeFile: function (file: any) {
                let index = this.files.indexOf(file);
                this.files.splice(index, 1);
                this.filesAdded--;
            },
            generateGuid: function (item: any) {
                let Key = item.key;
                if (Key) {
                    return Key;
                }
                let result = ''
                for (let j = 0; j < 32; j++) {
                    let i = Math.floor(Math.random() * 16).toString(16).toUpperCase();
                    result = result + i;
                }
                item.key = result;
                return item.key;
            },
        },
        watch: {
            model: function (newModel, oldModel) {
                if (oldModel === newModel) {
                    return;
                }
                this.internalModel = newModel;
                this.$nextTick(function () {
                    this.revalidate();
                });
            },
        },
        mounted: function () {
            this.$nextTick(function () {
                this.revalidate();
            });
        }
    });

</script>