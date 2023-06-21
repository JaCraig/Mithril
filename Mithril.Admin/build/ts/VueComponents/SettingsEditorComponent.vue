<style type="text/less" scoped>
</style>
<template>
    <div class="panel" :class="schema.class">
        <header>{{name}}</header>
        <div class="body">
            <div v-if="mode == 'listing'">
                <mithril-listing :model="model" :schema="schema.modelSchema"></mithril-listing>
            </div>
            <div v-if="mode=='form'">
                <mithril-form :model="model" :schema="schema.modelSchema"></mithril-form>
            </div>
            <div v-if="debug && schema" class="panel debug">
                <header>Editor Schema</header>
                <pre>
{{schema}}
            </pre>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import { Request, StorageMode } from "../../../../Mithril.Web.Common/build/ts/Framework/AJAX/Request";
    import Form from "../../../../Mithril.Web.Common/build/ts/Component/Form.vue";
    import { Logger } from "../../../../Mithril.Web.Common/build/ts/Framework/Logging/Logging";

    export default Vue.defineComponent({
        name: "settings-editor-component",
        components: {
            "mithril-form": Form
        },
        data: function () {
            return {
                model: {},
                mode: "listing"
            };
        },
        props: {
            name: {
                type: String,
                default: ""
            },
            schema: {
                type: Object,
                default: {}
            },
            debug: {
                type: Boolean,
                default: true
            }
        },
        methods: {
            loadData: function () {
                if (this.debug) {
                    Logger.debug("Loading" + this.name + "data.");
                }
                let that = this;
                Request.post('/api/query', {
                    query: `query($entityType: String){
  entity(entityType: $entityType)
}`,
                    variables: { "entityType": that.schema.dataType }
                })
                    .onSuccess((data) => {
                        if (that.debug) {
                            Logger.debug("Successfully loaded data:", data);
                        }
                        that.model = data.data.entity;
                    })
                    .withStorageMode(StorageMode.NetworkOnly)
                    .send();
            }
        },
        created: function () {
            this.loadData();
        }
    });
</script>