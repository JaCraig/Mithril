
<template>
    <div>
        <div v-if="show(internalModel.display || schema.display)">
            <div v-for="(item) in schema.fields" v-bind:key="generateGuid(item)">
                <component :is="getFieldType(item)"
                        :schema="getSchema(item)"
                        :model="getModelValue(item)"
                        :id-suffix="getIDSuffix(item)"
                        @changed="setModelValue"
                        @click="buttonClicked"
                        @error="error"
                        @exception="exception">
                </component>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import Vue from 'vue'

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
        show: function(method: any) {
            if(typeof method === 'function') {
                return method();
            }
            return method;
        },
        getFieldType: function(field: any) {
            if(field.type.indexOf("form-field-")==0) {
                return field.type;
            }
            return "form-field-" + field.type;
        },
        getModelValue: function(field: any) {
            return this.internalModel[field.model];
        },
        error: function(errorCode: any){
            this.$emit("error", errorCode);
        },
        exception: function(errorCode: any){
            this.$emit("exception", errorCode);
        },
        setModelValue: function(newValue: any, field: any) {
            this.internalModel[field.model] = newValue;
            this.$emit("changed", this.internalModel, this.schema);
        },
        buttonClicked: function(event: any, field: any) {
            this.$emit("click", event, field);
        },
        getSchema: function(field: any) {
            if (field.type.startsWith("complex")) {
                if (field.schema.model === undefined) {
                    field.schema.model = field.model;
                }
                return field.schema;
            }
            return field;
        },
        getIDSuffix: function(field: any) {
            if(this.idSuffix === undefined) {
                return "";
            }
            return this.idSuffix;
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