<style type="text/less" scoped>
    a {
        cursor: pointer;
    }

    table {
        margin-bottom: 10px;
    }
</style>
<template>
    <div>
        <div class="flex row">
            <input type="text" placeholder="Find an item" @keyup="filterData" v-model="filter" />
            <div>
                <label class="right">
                    Show
                    <select v-model="pageSize" @change="filterData">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                        <option value="-1">All</option>
                    </select>
                    Records
                </label>
            </div>
        </div>
        <br class="clear" />
        <table>
            <thead>
                <tr>
                    <th v-for="column in internalSchema" :key="column.propertyName">{{ column.displayName }}</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="entity in internalModel" :key="entity.id">
                    <td v-for="column in internalSchema" :key="column.propertyName">
                        <a @click="entitySelected(entity)">
                            {{entity[column.propertyName]}}
                        </a>
                    </td>
                </tr>
            </tbody>
        </table>
        <div v-if="debug" class="flex row">
            <div class="panel">
                <header>Listing Schema</header>
                <div class="body">
                    <pre>
                        {{internalSchema}}
                    </pre>
                </div>
            </div>
            <div class="panel">
                <header>Listing Model</header>
                <div class="body">
                    <pre>
                        {{internalModel}}
                    </pre>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import "../Framework/Extensions/String";
    import debounce from "../Framework/Utils/Debounce";
    import Vue from 'vue';
    import PropertySchema from "./DataTypes/PropertySchema";
    import FilterEvent from "./DataTypes/FilterEvent";

    export default Vue.defineComponent({
        name: "mithril-listing",
        data: function () {
            return {
                internalModel: this.model,
                internalSchema: this.schema.filter(x => x.metadata?.canList ?? true),
                filter: "",
                pageSize: 10,
                page: 0,
                sortField: "",
                sortAscending: false
            };
        },
        props: {
            model: {
                type: Object,
                default: {}
            },
            schema: {
                type: Array<PropertySchema>,
                default: []
            },
            debug: {
                type: Boolean,
                default: false
            }
        },
        methods: {
            filterData: debounce(function (event: Event) {
                let that = this;
                that.$emit("filter", event, new FilterEvent(that.filter, that.pageSize, that.page, that.sortField, that.sortAscending));
            }, 300),
            entitySelected: function (entity: any) {
                this.$emit("entity-selected", entity);
            }
        },
        created: function () {

        },
        watch: {
            model: function (newModel: Object, oldModel: Object) {
                if (oldModel === newModel) {
                    return;
                }
                this.internalModel = newModel;
            },
            schema: function (newSchema: Array<PropertySchema>, oldSchema: Array<PropertySchema>) {
                if (oldSchema === newSchema) {
                    return;
                }
                this.internalSchema = newSchema.filter(x => x.metadata?.canList ?? true);
            }
        }
    });
</script>