<template>
    <div>
        Select
        <label :for="getFieldID()" v-if="!schema.label && label" :class="schema.labelClasses">
            {{ $filters.capitalize(schema.model) }}
            <span class="error clear-background" v-if="schema.required">*</span>
            <span class="error clear-background" v-if="errorMessage">{{errorMessage}}</span>
            <span class="success clear-background fas fa-check-circle" v-if="!errorMessage && willValidate()"></span>
        </label>
        <label :for="getFieldID()" v-if="schema.label && label" :class="schema.labelClasses">
            {{ schema.label }}
            <span class="error clear-background" v-if="schema.required">*</span>
            <span class="error clear-background" v-if="errorMessage">{{errorMessage}}</span>
            <span class="success clear-background fas fa-check-circle" v-if="!errorMessage && willValidate()"></span>
        </label>
        <div v-if="internalSchema.metadata.subtitle">{{internalSchema.metadata.subtitle}}</div>
        <select v-model="internalModel"
                :disabled="schema.disabled"
                :name="schema.inputName || getFieldID()"
                :height="schema.height"
                :id="getFieldID()"
                @change="changed(internalModel)"
                :readonly="schema.readonly"
                :required="schema.required"
                :multiple="schema.multiple"
                :class="schema.inputClasses"
                :width="schema.width"
                :data-error-message-value-missing="schema.errorMessageValueMissing">
            <option v-for="value in schema.options" :value="value.key" :selected="isSelected(value)" v-bind:key="value.key">
                {{ value.value }}
            </option>
        </select>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import "../../Framework/Extensions/String";
    import { Request, StorageMode } from "../../Framework/Request";
    import { InputElementValidationRule } from '../../Framework/Validation';

    export default Vue.defineComponent({
        data: function () {
            return {
                internalModel: this.model,
                errorMessage: "",
            };
        },
        props: {
            model: Object,
            schema: Object,
            label: {
                default: true,
                type: Boolean,
            },
            idSuffix: String,
        },
        methods: {
            willValidate: function() {
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
            changed: function (newValue: any) {
                this.revalidate();
                this.$emit("changed", newValue, this.schema);
            },
            isSelected: function (value: any) {
                return this.internalModel === value.key;
            },
            getValues: function (data: any) {
                if (!data) {
                    return null;
                }
                let itemToCheck = data;
                if (Array.isArray(data)) {
                    itemToCheck = data[0];
                }
                let propertyNames = Object.getOwnPropertyNames(itemToCheck);
                for (let x = 0; x < propertyNames.length; ++x) {
                    if (propertyNames[x] === "key") {
                        return data;
                    }
                    let tempData = this.getValues(itemToCheck[propertyNames[x]]);
                    if (tempData) {
                        return tempData;
                    }
                }
                return null;
            }
        },
        beforeMount: function () {
            if (!this.schema.optionsQuery) {
                return;
            }
            let that = this;

            Request.post('/api/query', {
                query: that.schema.optionsQuery
            })
                .onSuccess((data: any) => {
                    that.schema.options = data.data.dropDown;
                    that.changed(that.internalModel);
                })
                .withStorageMode(StorageMode.StorageAndUpdate)
                .send();
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
        mounted: function() {
            this.$nextTick(function () {
                this.revalidate();
            });
        }
    });
</script>