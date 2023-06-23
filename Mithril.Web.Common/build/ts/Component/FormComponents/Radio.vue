
<template>
    <div>
        Radio
        <div v-if="!schema.label" :class="schema.labelClasses">
            {{ $filters.capitalize(schema.model) }}
            <span class="error clear-background" v-if="schema.required && errorMessage">*</span>
            <span class="error clear-background" v-if="errorMessage">{{errorMessage}}</span>
            <span class="success clear-background fas fa-check-circle" v-if="!errorMessage && willValidate()"></span>
        </div>
        <div v-if="schema.label" :class="schema.labelClasses">
            {{ schema.label }}
            <span class="error clear-background" v-if="schema.required">*</span>
            <span class="error clear-background" v-if="errorMessage">{{errorMessage}}</span>
            <span class="success clear-background fas fa-check-circle" v-if="!errorMessage && willValidate()"></span>
        </div>
        <div v-if="internalSchema.metadata.subtitle">{{internalSchema.metadata.subtitle}}</div>
        <div class="flex row text-center">
            <div v-for="(value) in schema.options" v-bind:key="value.key">
                <input :id="getFieldID(value.key)"
                       type="radio"
                       :checked="isItemChecked(value.key)"
                       @click="changed(value.key)"
                       :disabled="schema.disabled"
                       :name="schema.inputName || getFieldName()"
                       :readonly="schema.readonly"
                       :class="schema.inputClasses"
                       :value="value.key" />
                <label :for="getFieldID(value.key)">
                    {{ $filters.capitalize(value.value) }}
                </label>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import Vue from 'vue'
    import "../../Framework/Extensions/String";
    import { InputElementValidationRule } from '../../Framework/Validation';

export default Vue.defineComponent({
    data: function() {
        return {
            internalModel: this.model,
            errorMessage: "",
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
        getFieldID: function(value: any) {
            let result = "";
            if (this.schema.id) {
                result = this.schema.id;
            } else {
                result = this.schema.model.slugify();
            }
            result += "-" + value;
            if (this.idSuffix !== undefined) {
                result += this.idSuffix;
            }
            return result;
        },
        getFieldName: function() {
            if (this.schema.id) {
                return this.schema.id;
            }
            return this.schema.model.slugify();
        },
        changed: function (newValue: any) {
            this.revalidate();
            this.internalModel = newValue;
            this.$emit("changed", newValue, this.schema);
        },
        isItemChecked: function(item: any) {
            return this.getItemValue(item) === this.internalModel;
        },
        getItemValue: function(item: any) {
            return item;
        },
        generateGuid: function (item: any) {
            let Key = item.key;
            if(Key) {
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
        model: function(newModel, oldModel) {
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