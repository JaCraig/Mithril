
<template>
    <div>
        <div v-for="(item, index) in internalModel"  v-bind:key="generateGuid(item)" class="border-bottom">
            <a class="fas fa-minus-circle right" @click.stop.prevent="removeItem(item)"></a>
            <div v-for="(field) in schema.fields"  v-bind:key="generateGuid(field)">

                <component :is="getFieldType(field)"
                            :schema="getSchema(field)"
                            :model="getModelValue(field,item)"
                            :label="true"
                            :id-suffix="getIDSuffix(field,index)"
                            @changed="newValue => setModelValue(field,item,newValue)"
                            @click="buttonClicked"
                            @error="error"
                            @exception="exception">
                </component>
            </div>
        </div>
        <a class="fas fa-plus-circle" @click.stop.prevent="addItem">Add More</a>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.defineComponent({
    data: function() {
        let DefaultItem: any = {};
        for (let property in this.model[0]) {
            if (this.model[0].hasOwnProperty(property)) {
                DefaultItem[property] = "";
            }
        }
        return {
            internalModel: this.model,
            defaultItem: DefaultItem,
        };
    },
    props: {
        model: Object,
        schema: Object,
        idSuffix: String,
    },
    methods: {
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