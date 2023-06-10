
<template>
    <div>
        Complex List
        <h2 v-if="schema.label" :class="schema.labelClasses">
            {{ $filters.capitalize(schema.label) }}
        </h2>
        <table class="form-table" :class="schema.tableClasses">
            <thead>
                <tr>
                    <th v-for="(item) in schema.fields" v-bind:key="generateGuid(item)">
                        <span v-if="item.label">
                            {{ $filters.capitalize(item.label) }}
                        </span>
                        <span v-else>
                            {{ $filters.capitalize(item.model) }}
                        </span>
                        <span v-if="getSchema(item).hint"
                            :data-tooltip="getSchema(item).hint"
                            data-tooltip-size="extra-large">
                            <span class="fas fa-info-circle no-border small"></span>
                        </span>
                    </th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(item, index) in internalModel"  v-bind:key="generateGuid(item)">
                    <td v-for="(field) in schema.fields"  v-bind:key="generateGuid(field)">
                        <component :is="getFieldType(field)"
                                    :schema="getSchema(field)"
                                    :model="getModelValue(field,item)"
                                    :label="false"
                                    :id-suffix="getIDSuffix(field,index)"
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
                    <td :colspan="schema.fields.length + 1">
                        <a class="fas fa-plus-circle" @click.stop.prevent="addItem">Add More</a>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import moment from 'moment';

export default Vue.defineComponent({
    data: function() {
        return {
            internalModel: this.model,
            defaultItem: this.getDefaultValue(this.schema),
        };
    },
    props: {
        model: Object,
        schema: Object,
        idSuffix: String,
    },
    methods: {
        getDefaultValue(schema: any) {
            if (schema == null) {
                return {};
            }
            let DefaultItem: any = {};
            for(let x = 0; x < schema.fields.length; ++x) {
                let field = schema.fields[x];
                if(field.type.indexOf("complex") == -1) {
                    if (field.inputType === "date"
                        || field.inputType === "datetime-local" 
                        || field.inputType === "datetime"
                        || field.inputType === "month") {

                            let tempDate = moment(new Date());
                            if (field.isUTC) {
                                tempDate = moment.utc(new Date()).local();
                            }

                            if (field.inputType === "date") {
                                DefaultItem[field.model]= tempDate.format('YYYY-MM-DD');
                            }
                            else if (field.inputType === "datetime-local" || field.inputType === "datetime") {
                                DefaultItem[field.model]= tempDate.format('YYYY-MM-DDTHH:mm');
                            }
                            else if (field.inputType === "month") {
                                DefaultItem[field.model]= tempDate.format('YYYY-MM');
                            }
                    } else {
                        DefaultItem[field.model] = "";
                    }
                } else if(field.type.indexOf("complex-list") > -1||field.type.indexOf("complex-repeater") > -1) {
                    DefaultItem[field.model] = [];
                } else {
                    DefaultItem[field.model] = this.getDefaultValue(field);
                }
            }
            return DefaultItem;
        },
        getFieldType: function(field: any) {
            if(field.type.indexOf("form-field-")==0) {
                return field.type;
            }
            return "form-field-" + field.type;
        },
        getModelValue: function(field: any, item: any) {
            return item[field.model];
        },
        setModelValue: function(field: any, item: any, newValue: any) {
            item[field.model] = newValue;
            this.$emit("changed", this.internalModel, this.schema);
        },
        error: function(errorCode: any){
            this.$emit("error", errorCode);
        },
        exception: function(errorCode: any){
            this.$emit("exception", errorCode);
        },
        buttonClicked: function(event: any, field: any) {
            this.$emit("click", event, field);
        },
        getSchema: function(field: any) {
            return field;
        },
        removeItem: function(item: any) {
            if (this.schema.confirmRemoval && !confirm("Are you sure you want to remove this item?")) {
                return;
            }
            if (!this.internalModel) {
                this.internalModel = [];
            }
            let Index = this.internalModel.indexOf(item);
            this.internalModel.splice(Index, 1);
            this.$emit("changed", this.internalModel, this.schema);
        },
        addItem: function(item: any) {
            if (!this.internalModel) {
                this.internalModel = [];
            }
            this.internalModel = this.internalModel.concat(Object.assign({}, this.defaultItem));
            this.$emit("changed", this.internalModel, this.schema);
        },
        getIDSuffix: function(field: any, index: any) {
            if(this.idSuffix === undefined) {
                return index;
            }
            return this.idSuffix + index;
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
        },
    }
});

</script>