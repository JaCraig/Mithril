<template>
    <div>
        ASDF
        <component v-for="component in schema.children"
                   :key="component.type"
                   :is="component.type"
                   :data="internalData"
                   :schema="component"
                   @save="saveData"
                   @delete="deleteData"
                   @load="fetchData">
        </component>
        <pre>{{internalData}}</pre>
    </div>
</template>

<script lang="ts">
    import { Request, StorageMode } from '../Framework/Request';
    import ComponentSchema from "./DataTypes/ComponentSchema";
    import "../Framework/Extensions/String";
    import Vue from 'vue';

    export default Vue.defineComponent({
        name: "DataHandler",
        data: function () {
            return {
                count: 0,
                timer: 0,
                internalData: this.model
            };
        },
        props: {
            model: {
                type: Object,
                default: {}
            },
            schema: {
                type: ComponentSchema,
                default: new ComponentSchema()
            }
        },
        methods: {
            fetchData: function () {
                if (!this.schema.loadUrl) {
                    return;
                }
                let that = this;
                Request.post('/api/query', {
                    query: that.schema.datalistQuery
                })
                    .onSuccess((data: any) => {
                        that.schema.datalist = data.data.dropDown;
                    })
                    .withStorageMode(StorageMode.StorageAndUpdate)
                    .send();
            },
            saveData: function () {
                let that = this;
            },
            deleteData: function () {
                let that = this;
            }
        },
        created: function () {
            if (!this.schema.datalistQuery) {
                return;
            }
            let that = this;
            that.fetchData();
        },
        watch: {
            model: function (newModel, oldModel) {
                if (oldModel === newModel) {
                    return;
                }
                this.internalModel = newModel;
            },
        }
    });
</script>