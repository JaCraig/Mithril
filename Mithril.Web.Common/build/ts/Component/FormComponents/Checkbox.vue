<template>
    <div>
        Checkbox
        <input :id="getFieldID()"
                type="checkbox"
                :checked="internalModel"
                @click="changed($event.target.checked)"
                :disabled="schema.disabled"
                :dirname="schema.dirname"
                :name="schema.inputName || getFieldID()"
                :readonly="schema.readonly"
                :required="schema.required"
                :data-error-message-value-missing="schema.errorMessageValueMissing"
                :class="schema.inputClasses"
                />
        <label :for="getFieldID()" v-if="!schema.label" :class="schema.labelClasses">
            {{ $filters.capitalize(schema.model) }}
            <span class="error clear-background" v-if="schema.required">*</span>
        </label>
        <label :for="getFieldID()" v-if="schema.label" :class="schema.labelClasses">
            {{ schema.label }}
            <span class="error clear-background" v-if="schema.required">*</span>
        </label>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import "../../Framework/Extensions/String";

export default Vue.defineComponent({
    data: function() {
        return {
            internalModel: this.model
        };
    },
    props: {
        model: Object,
        schema: Object,
        idSuffix: String,
    },
    methods: {
        getFieldID: function() {
            let result = "";
            if (this.schema.id) {
                result = this.schema.id;
            } else {
                result = this.schema.model.slugify();
            }
            if (this.idSuffix!==undefined) {
                result += this.idSuffix;
            }
            return result;
        },
        changed: function(newValue: any) {
            this.$emit("changed", newValue, this.schema);
        },
    },
    watch: {
        model: function(newModel, oldModel) {
            if (oldModel === newModel) {
                return;
            }
            this.internalModel = newModel;
        }
    }
});

</script>