
<template>
    <div>
        <label :for="getFieldID()" v-if="!schema.label && label" :class="schema.labelClasses">
            {{ $filters.capitalize(schema.model) }}
            <span class="error clear-background" v-if="schema.required">*</span>
            <i class="clear-background active no-border small" v-if="schema.hint"><span class="fas fa-info-circle"></span>{{ schema.hint }}</i>
        </label>
        <label :for="getFieldID()" v-if="schema.label && label" :class="schema.labelClasses">
            {{ schema.label }}
            <span class="error clear-background" v-if="schema.required">*</span>
            <i class="clear-background active no-border small" v-if="schema.hint"><span class="fas fa-info-circle"></span>{{ schema.hint }}</i>
        </label>
        <textarea :id="getFieldID()"
                v-model="internalModel"
                @input="changed($event.target.value)"
                :disabled="schema.disabled"
                :height="schema.height"
                :maxlength="schema.maxlength"
                :minlength="schema.minlength"
                :name="schema.inputName || getFieldID()"
                :placeholder="schema.placeholder"
                :title="schema.placeholder"
                :readonly="schema.readonly"
                :class="schema.inputClasses"
                :required="schema.required"
                :width="schema.width"
                :rows="schema.rows || 3"
                :data-error-message-value-missing="schema.errorMessageValueMissing"
                :data-error-message-too-long="schema.errorMessageTooLong"
                :data-error-message-too-short="schema.errorMessageTooShort">
        </textarea>
        <div class="clear-background active right small" v-if="schema.maxlength"><span class="fas fa-info-circle"></span>
            {{ charactersRemaining }} characters remaining ({{ schema.maxlength }} max)
        </div>
        <br class="clear" />
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
        label: {
            default: true,
            type: Boolean,
        },
    },
    computed: {
        charactersRemaining: function() {
            if (this.internalModel == null) {
                return 0;
            }
            return this.schema.maxlength - this.internalModel.length;
        }
    },
    methods: {
        getFieldID: function() {
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
        },
    }
});

</script>