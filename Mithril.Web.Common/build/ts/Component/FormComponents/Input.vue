
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
        <input :id="getFieldID()"
                :type="schema.inputType"
                v-model="internalModel"
                @input="changed($event.target.value)"
                :disabled="schema.disabled"
                :accept="schema.accept"
                :alt="schema.alt"
                :autocomplete="schema.autocomplete"
                :checked="schema.checked"
                :dirname="schema.dirname"
                :formaction="schema.formaction"
                :formenctype="schema.formenctype"
                :formmethod="schema.formmethod"
                :formnovalidate="schema.formnovalidate"
                :formtarget="schema.formtarget"
                :height="schema.height"
                :list="getList()"
                :max="schema.max"
                :maxlength="schema.maxlength"
                :min="schema.min"
                :minlength="schema.minlength"
                :multiple="schema.multiple"
                :name="schema.inputName || getFieldID()"
                :pattern="schema.pattern"
                :placeholder="schema.placeholder"
                :title="schema.placeholder"
                :readonly="schema.readonly"
                :required="schema.required"
                :size="schema.size"
                :src="schema.src"
                :step="schema.step"
                :width="schema.width"
                :files="schema.files"
                :class="schema.inputClasses"
                :data-error-message-value-missing="schema.errorMessageValueMissing"
                :data-error-message-pattern-mismatch="schema.errorMessagePatternMismatch"
                :data-error-message-range-overflow="schema.errorMessageRangeOverflow"
                :data-error-message-range-underflow="schema.errorMessageRangeUnderflow"
                :data-error-message-step-mismatch="schema.errorMessageStepMismatch"
                :data-error-message-too-long="schema.errorMessageTooLong"
                :data-error-message-too-short="schema.errorMessageTooShort"
                :data-error-message-bad-input="schema.errorMessageBadInput"
                :data-error-message-type-mismatch="schema.errorMessageTypeMismatch"
                />
        <div class="text-center" v-if="schema.inputType === 'color' || schema.inputType === 'range'">{{ internalModel }}</div>
        <datalist v-if="schema.datalist" :id="getList()">
            <option v-for="(item) in schema.datalist" :value="item.value" v-bind:key="item.key" />
        </datalist>
    </div>
</template>

<script lang="ts">
import { Request } from '../../Framework/AJAX/Request';
import { StorageMode } from "../../Framework/AJAX/Request";
import "../../Framework/Extensions/String";
import Vue from 'vue';
import moment from 'moment';

export default Vue.defineComponent({
    data: function() {
        let returnedModel: any;
        if (this.schema.inputType === "date"
            || this.schema.inputType === "datetime-local" 
            || this.schema.inputType === "datetime"
            || this.schema.inputType === "month") {

            let tempDate = moment(this.model||new Date());
            if (this.schema.isUTC) {
                tempDate = moment.utc(this.model||new Date()).local();
            }

            if (this.schema.inputType === "date") {
                returnedModel= tempDate.format('YYYY-MM-DD');
            }
            else if (this.schema.inputType === "datetime-local" || this.schema.inputType === "datetime") {
                returnedModel= tempDate.format('YYYY-MM-DDTHH:mm');
            }
            else if (this.schema.inputType === "month") {
                returnedModel= tempDate.format('YYYY-MM');
            }
        }
        else {
            returnedModel = this.model;
        }
        return {
            count: 0,
            timer: 0,
            internalModel: returnedModel
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
            let that = this;
            if(that.schema.datalistUrl) {
                if(that.timer !== 0) {
                    clearTimeout(that.timer);
                }
                that.timer = setTimeout(function() {
                Request.post(that.schema.datalistUrl,{ value: newValue, queryCount: ++that.count })
                        .onSuccess(function(ev:any){
                            if(!ev){
                                return;
                            }else if(ev.queryCount && ev.queryCount == that.count) {
                                that.schema.datalist = ev.value;
                            } else if(!ev.queryCount) {
                                that.schema.datalist = ev;
                            }
                        })
                        .onError(function (x) {
                            that.$emit("error", x);
                        })
                        .send();
                }, 100);
            }
            this.$emit("changed", newValue, this.schema);
        },
        getList: function() {
            if (this.schema.datalist !== undefined) {
                return this.getFieldID() + "-list";
            } else {
                return null;
            }
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
    beforeMount: function() {
        if(!this.schema.datalistQuery) {
            return;
        }
        let that = this;
        
        Request.post('/api/query', {
            query: that.schema.datalistQuery
        })
        .onSuccess((data: any)=>{
            that.schema.datalist = data.data.dropDown;
        })
        .setMode(StorageMode.StorageAndUpdate)
        .send();
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