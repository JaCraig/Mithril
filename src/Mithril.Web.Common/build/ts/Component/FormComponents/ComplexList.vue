
<template>
    <div>
        <h2 v-if="internalSchema.displayName">
            {{ $filters.capitalize(internalSchema.displayName) }}
        </h2>
        <table class="form-table">
            <thead>
                <tr>
                    <th v-for="(item) in internalSchema.metadata.fields" v-bind:key="generateGuid(item)">
                        {{ $filters.capitalize(item.displayName) }}
                        <span v-if="item.metadata.hint"
                            :data-tooltip="item.metadata.hint"
                            data-tooltip-size="extra-large">
                            <span class="fas fa-info-circle no-border small"></span>
                        </span>
                    </th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(item, index) in internalModel"  v-bind:key="generateGuid(item)">
                    <td v-for="(field) in internalSchema.metadata.fields"  v-bind:key="generateGuid(field)">
                        <component :is="getFieldType(field)"
                                    :schema="field"
                                    :model="getModelValue(field,item)"
                                    :label="false"
                                    @changed="newValue => setModelValue(field,item,newValue)"
                                    @click="buttonClicked"
                                    @error="error"
                                    @exception="exception">
                        </component>
                    </td>
                    <td>
                        <a class="fas fa-minus-circle" @click.stop.prevent="removeItem(item)"></a>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td :colspan="internalSchema.metadata.fields.length + 1">
                        <a class="fas fa-plus-circle" @click.stop.prevent="addItem">Add More</a>
                    </td>
                </tr>
            </tfoot>
        </table>
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

    export default Vue.defineComponent({
        name: "form-field-complex-list",
        components: {
            'form-field-input': FormFieldInput,
            'form-field-select': FormFieldSelect,
            'form-field-checkbox': FormFieldCheckbox,
            'form-field-radio': FormFieldRadio,
            'form-field-text-area': FormFieldTextarea,
            'form-field-text': FormFieldText,
            'form-field-upload': FormFieldUpload,
            'form-field-buttons': FormFieldButtons,
        },
        data: function() {
            return {
                internalModel: this.model,
                internalSchema: this.schema,
                defaultItem: this.getDefaultValue(this.schema)
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
            // Gets the default value for an item in the list based on the schema
            // schema: The schema to get the default value for
            getDefaultValue(schema: any) {
                if (schema == null) {
                    return {};
                }
                let DefaultItem: any = {};
                for (let x = 0; x < schema.metadata.fields.length; ++x) {
                    let field = schema.metadata.fields[x];
                    if (field.propertyType.indexOf("complex") == -1) {
                        if (field.metadata.inputType === "date"
                            || field.metadata.inputType === "datetime-local"
                            || field.metadata.inputType === "datetime"
                            || field.metadata.inputType === "month") {

                            let tempDate = moment(new Date());
                            if (field.isUTC) {
                                tempDate = moment.utc(new Date()).local();
                            }

                            if (field.inputType === "date") {
                                DefaultItem[field.propertyName] = tempDate.format('YYYY-MM-DD');
                            }
                            else if (field.inputType === "datetime-local" || field.inputType === "datetime") {
                                DefaultItem[field.propertyName] = tempDate.format('YYYY-MM-DDTHH:mm');
                            }
                            else if (field.inputType === "month") {
                                DefaultItem[field.propertyName] = tempDate.format('YYYY-MM');
                            }
                        } else {
                            DefaultItem[field.propertyName] = "";
                        }
                    } else if (field.propertyType.indexOf("complex-list") > -1 || field.propertyType.indexOf("complex-repeater") > -1) {
                        DefaultItem[field.propertyName] = [];
                    } else {
                        DefaultItem[field.propertyName] = this.getDefaultValue(field);
                    }
                }
                return DefaultItem;
            },
            // Gets the field type for a given field
            // field: The field to get the type for
            getFieldType: function(field: any) {
                return "form-field-" + field.propertyType;
            },
            // Gets the model value for a given field and item
            // field: The field to get the value for
            // item: The item to get the value for
            getModelValue: function(field: any, item: any) {
                return item[field.propertyName];
            },
            // Sets the model value for a given field and item
            // Emits the changed event
            // field: The field to set the value for
            // item: The item to set the value for
            // newValue: The new value to set
            setModelValue: function(field: any, item: any, newValue: any) {
                item[field.propertyName] = newValue;
                this.$emit("changed", this.internalModel, this.schema);
            },
            // Emits the error event
            error: function(errorCode: any){
                this.$emit("error", errorCode);
            },
            // Emits the exception event
            exception: function(errorCode: any){
                this.$emit("exception", errorCode);
            },
            // Emits the click event when a button is clicked
            buttonClicked: function(event: any, field: any) {
                this.$emit("click", event, field);
            },
            // Removes an item from the list
            // item: The item to remove
            removeItem: function(item: any) {
                if (this.internalSchema.confirmRemoval && !confirm("Are you sure you want to remove this item?")) {
                    return;
                }
                if (!this.internalModel) {
                    this.internalModel = [];
                }
                let Index = this.internalModel.indexOf(item);
                this.internalModel.splice(Index, 1);
                this.$emit("changed", this.internalModel, this.internalSchema);
            },
            // Adds an item to the list using the default value
            // Emits the changed event
            addItem: function() {
                if (!this.internalModel) {
                    this.internalModel = [];
                }
                this.internalModel = this.internalModel.concat(Object.assign({}, this.defaultItem));
                this.$emit("changed", this.internalModel, this.schema);
            },
            // Generates a guid for an item
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
            // Watches for changes to the model and updates the internal model
            // newModel: The new model value
            // oldModel: The old model value
            model: function(newModel, oldModel) {
                if (oldModel === newModel) {
                    return;
                }
                this.internalModel = newModel;
            },
            // Watches for changes to the schema and updates the internal schema
            // newSchema: The new schema value
            // oldSchema: The old schema value
            schema: function(newSchema, oldSchema) {
                if (oldSchema === newSchema) {
                    return;
                }
                this.internalSchema = newSchema;
            }
        }
    });

</script>