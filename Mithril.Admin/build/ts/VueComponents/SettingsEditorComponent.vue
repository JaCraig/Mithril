<style type="text/less" scoped>
</style>
<template>
    <div class="panel" :class="schema.class">
        <header>{{name}}</header>
        <div class="body">
            <div>
                <mithril-form :schema="schema.modelSchema" :model="currentEntity" :debug="debug" @submit="saveEntity"></mithril-form>
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
    import Form from "../../../../Mithril.Web.Common/build/ts/Component/Form.vue";
    import { Request, StorageMode, CancellationToken } from "../../../../Mithril.Web.Common/build/ts/Framework/Request";
    import debounce from "../../../../Mithril.Web.Common/build/ts/Framework/Utils/Debounce";
    import { Logger } from "../../../../Mithril.Web.Common/build/ts/Framework/Logging";

    export default Vue.defineComponent({
        name: "settings-editor-component",
        components: {
            "mithril-form": Form
        },
        data: function () {
            return {
                currentEntity: null,
                entityQuery: `query($entityType: String) {
                    entity(entityType: $entityType)
                }`,
            };
        },
        methods: {
            // close the settings editor
            close: function () {
                this.$emit("close");
            },
            // load the entity
            loadData: debounce(async function () {
                let that = this;
                let currentRequest = Request.post("/api/query", {
                    query: that.entityQuery,
                    variables: {
                        entityType: that.schema.dataType
                    }
                })
                    .onSuccess(results => {
                        Logger.debug("Entity loaded successfully", results.data.entity);
                        that.currentEntity = results.data.entity;
                    });
                await currentRequest.send();
            }, 100),
            // save the entity
            // event: the event that triggered the save
            // entity: the entity to save
            saveEntity: function (event: any, entity: any) {
                let that = this;
                event.preventDefault();
                if (entity == null) {
                    return;
                }
                this.currentEntity = null;
                Logger.debug(entity);

                Request.post("/api/command/v1/SaveModelCommand", {
                    "data": entity,
                    "entityType": that.schema.dataType,
                    id: entity.iD
                })
                    .onSuccess(results => {
                        Logger.debug("Entity saved successfully", results.data);
                        that.close();
                    })
                    .onError(results => {
                        Logger.error("Error saving entity", results.data);
                    }).send();

                return false;
            }
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
        // created event handler (load the entity)
        created: function () {
            this.loadData();
        },
        watch: {
            schema: function () {
                this.loadData();
            }
        }
    });
</script>