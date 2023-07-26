
<template>
    <div>
        <div v-for="(item) in internalSchema.fields" v-bind:key="generateGuid(item)" v-if="internalModel">
            <component :is="'form-field-' + item.propertyType"
                       :schema="item"
                       :model="internalModel[item.propertyName]"
                       @changed="setModelValue"
                       @click="buttonClicked"
                       @error="error"
                       @exception="exception">
            </component>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import FormFieldInput from './Input.vue';
    import FormFieldSelect from './Select.vue';
    import FormFieldCheckbox from './Checkbox.vue';
    import FormFieldRadio from './Radio.vue';
    import FormFieldTextarea from './TextArea.vue';
    import FormFieldText from './Text.vue';
    import FormFieldUpload from './Upload.vue';
    import FormFieldButtons from './Buttons.vue';
    import FormFieldComplexConditional from './ComplexConditional.vue';
    import FormFieldComplexList from './ComplexList.vue';
    import FormFieldComplexTabs from './ComplexTabs.vue'; 
    import FormFieldRepeater from './ComplexRepeater.vue';

    export default Vue.defineComponent({
        name: "form-field-complex",
        components: {
            'form-field-complex-conditional': FormFieldComplexConditional,
            'form-field-complex-list': FormFieldComplexList,
            'form-field-complex-tabs': FormFieldComplexTabs,
            'form-field-input': FormFieldInput,
            'form-field-select': FormFieldSelect,
            'form-field-checkbox': FormFieldCheckbox,
            'form-field-radio': FormFieldRadio,
            'form-field-text-area': FormFieldTextarea,
            'form-field-text': FormFieldText,
            'form-field-upload': FormFieldUpload,
            'form-field-buttons': FormFieldButtons,
            'form-field-complex-repeater': FormFieldRepeater,
        },
        data: function() {
            return {
                internalModel: this.model,
                internalSchema: this.schema
            };
        },
        props: {
            model: {
                type: Object,
                default: {}
            },
            schema: {
                type: Object,
                default: {
                    fields: []
                }
            }
        },
        methods: {
            // When an error occurs, emit the error event
            // errorCode: contains the error code
            error: function(errorCode: any){
                this.$emit("error", errorCode);
            },
            // When an exception occurs, emit the exception event
            // errorCode: contains the error code
            exception: function(errorCode: any){
                this.$emit("exception", errorCode);
            },
            // When a field is changed, emit the changed event
            // newValue: contains the new value of the field
            // field: contains the field object
            setModelValue: function(newValue: any, field: any) {
                this.internalModel[field.propertyName] = newValue;
                this.$emit("changed", this.internalModel, this.schema);
            },
            // button clicked event
            // event: contains the event object
            // field: contains the field object
            buttonClicked: function(event: any, field: any) {
                this.$emit("click", event, field);
            },
            // generate a guid
            // item: contains the object
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
            // watch for changes in the model
            // newModel: contains the new model
            // oldModel: contains the old model
            model: function(newModel, oldModel) {
                if (oldModel === newModel) {
                    return;
                }
                this.internalModel = newModel;
            },
            // watch for changes in the schema
            // newSchema: contains the new schema
            // oldSchema: contains the old schema
            schema: function (newSchema, oldSchema) {
                if (oldSchema === newSchema) {
                    return;
                }
                this.internalSchema = newSchema;
            }
        }
    });

</script>