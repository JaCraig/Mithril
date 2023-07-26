<style type="text/less" scoped>
    a {
        cursor: pointer;
    }
</style>
<template>
    <div class="panel" :class="schema.class">
        <header>
            {{name}}
            <a @click="editEntity({ iD: 0 })" :title="'Add New ' + name" v-if="mode=='listing'"><span class="fas fa-plus right"></span></a>
            <a @click="openListing" title="Show Listing" v-if="mode=='editor'"><span class="fas fa-list right"></span></a>
        </header>
        <div class="body">
            <div v-if="mode=='listing'">
                <listing :schema="schema.modelSchema" :model="entities" :debug="debug" @entity-selected="editEntity" @filter="filter"></listing>
            </div>
            <div v-if="mode=='editor'">
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
    import Listing from "../../../../Mithril.Web.Common/build/ts/Component/Listing.vue";
    import Form from "../../../../Mithril.Web.Common/build/ts/Component/Form.vue";
    import { Request, StorageMode, CancellationToken } from "../../../../Mithril.Web.Common/build/ts/Framework/Request";
    import FilterEvent from "../../../../Mithril.Web.Common/build/ts/Component/DataTypes/FilterEvent";
    import debounce from "../../../../Mithril.Web.Common/build/ts/Framework/Utils/Debounce";
    import { Logger } from "../../../../Mithril.Web.Common/build/ts/Framework/Logging";

    export default Vue.defineComponent({
        name: "data-editor-component",
        components: {
            "listing": Listing,
            "mithril-form": Form
        },
        data: function () {
            return {
                entities: [],
                mode: "listing",
                currentEntity: null,
                entitiesQuery: `query($entityType: String, $pageSize: Int, $page: Int, $sortField: String, $sortAscending: Boolean, $filter: String) {
                    entities(entityType: $entityType, pageSize: $pageSize, page: $page, sortField: $sortField, sortAscending: $sortAscending, filter: $filter)
                }`,
                currentFilter: new FilterEvent("", 10, 0, "", false),
                currentRequest: null,
                entityQuery: `query($entityType: String, $id: Long) {
                    entity(entityType: $entityType, id: $id)
                }`,
            };
        },
        methods: {
            // edits an entity
            // entity: the entity to edit
            editEntity: async function (entity: any) {
                let that = this;
                Logger.debug("Editing entity", entity);
                let currentRequest = Request.post("/api/query", {
                        query: that.entityQuery,
                        variables: {
                            entityType: that.schema.dataType,
                            id: entity.iD
                        }
                    })
                    .onSuccess(results => {
                        Logger.debug("Entity loaded successfully", results.data.entity);
                        that.currentEntity = results.data.entity;
                        this.mode = "editor";
                    });
                await currentRequest.send();
            },
            // filter event handler
            // event: the event that triggered the filter
            // filter: the filter event
            filter: function (event: Event, filter: FilterEvent) {
                //Load data based on the filter string
                this.currentFilter = filter;
                this.loadData();
            },
            // loads the data
            loadData: debounce(async function () {
                let that = this;
                if (that.currentRequest != null) {
                    that.cancellationToken.canceled = true;
                }
                that.cancellationToken = new CancellationToken();
                that.currentRequest = Request.post("/api/query", {
                    query: that.entitiesQuery,
                    variables: {
                        entityType: that.schema.dataType,
                        pageSize: Number.parseInt(that.currentFilter.pageSize),
                        page: Number.parseInt(that.currentFilter.page),
                        sortField: that.currentFilter.sortField,
                        sortAscending: that.currentFilter.sortAscending,
                        filter: that.currentFilter.filter
                    }
                })
                    .withCancellationToken(that.cancellationToken)
                    .onSuccess(results => {
                        Logger.debug("Entities loaded successfully", results.data.entities);
                        that.entities = results.data.entities;
                    });
                await that.currentRequest.send();
            }, 100),
            // opens the listing
            openListing: function () {
                Logger.debug("Opening listing");
                this.mode = "listing";
                this.loadData();
            },
            // saves an entity
            // event: the event that triggered the save
            // entity: the entity to save
            saveEntity: function (event: any, entity: any) {
                let that = this;
                event.preventDefault();
                if (entity == null) {
                    return;
                }
                this.currentEntity = null;
                Logger.debug("Saving entity:", entity);

                Request.post("/api/command/v1/SaveModelCommand", {
                    "data": entity,
                    "entityType": that.schema.dataType,
                    id: entity.iD
                })
                    .onSuccess(results => {
                        Logger.debug("Entity saved successfully", results.data);
                        that.mode = "listing";
                        that.loadData();
                    })
                    .onError(error => Logger.error("Error saving entity", error))
                    .send();

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
        // create event (load data)
        created: function () {
            this.openListing();
            this.loadData();
        },
        watch: {
            // watch for changes to the schema
            schema: function () {
                this.openListing();
                this.loadData();
            }
        }
    });
</script>