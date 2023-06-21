<style lang="less" scoped>
    .main-application {
        display: flex;
        flex-direction: row;
        flex-wrap: nowrap;
        & .left-nav

    {
        width: 200px;
        height: calc(100vh - 1px);
        border-bottom: 0;
        border-left: 0;
        border-top: 0;
        margin: 0;
        overflow-y: auto;
        & ul

    {
        padding: 0 5px;
        list-style: none;
    }

    }

    & .editor-content {
        flex-grow: 1;
        overflow-y: auto;
        & pre

    {
        width: 100%;
    }

    }
    }
</style>
<template>
    <div class="main-application">
        <div class="left-nav panel">
            <section v-for="category in groupedEditors" :key="category.category">
                <header>{{$filters.capitalize(category.category)}}</header>
                <ul>
                    <li v-for="(editor, editorIndex) in category.editors" :key="editor.name">
                        <a href="#!" @click.stop.prevent="editorSelected(editor)" :title="editor.description"><span :class="editor.icon"></span>{{editor.name}}</a>
                    </li>
                </ul>
            </section>
        </div>
        <div class="editor-content">
            <component :is="currentEditor.componentDefinition.schema.type"
                       :schema="currentEditor.componentDefinition.schema"
                       :name="currentEditor.name"
                       :debug="debug"
                       @error="error" v-if="currentEditor">
            </component>
            <div v-if="debug && currentEditor" class="panel debug">
                <header>Selected Editor Info</header>
                <ul>
                    <li>
                        Component Name: {{currentEditor.componentDefinition.name}}
                    </li>
                    <li>
                        Component Type: {{currentEditor.componentDefinition.schema.type}}
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import { Request, StorageMode } from "../../../../Mithril.Web.Common/build/ts/Framework/AJAX/Request";
    import { Logger } from "../../../../Mithril.Web.Common/build/ts/Framework/Logging/Logging";

    export default Vue.defineComponent({
        name: "admin-application",
        computed: {
            groupedEditors: function () {
                const groups = {};
                this.sortedEditors.forEach(editor => {
                    if (!groups[editor.category]) {
                        groups[editor.category] = [];
                    }
                    groups[editor.category].push(editor);
                });
                return Object.keys(groups).map(category => ({
                    category,
                    editors: groups[category]
                }));
            },
            sortedEditors: function () {
                return this.editors.sort((a, b) => {
                    if (a.category < b.category) return -1;
                    if (a.category > b.category) return 1;
                    if (a.name < b.name) return -1;
                    if (a.name > b.name) return 1;
                    return 0;
                });
            }
        },
        data: function () {
            return {
                editors: [],
                currentEditor: null
            };
        },
        methods: {
            editorSelected: function (editor: any) {
                if (this.debug) {
                    Logger.debug("Switching to editor:", editor.name);
                }
                this.currentEditor = editor;
            },
            loadEditors: function () {
                if (this.debug) {
                    Logger.debug("Loading Editors");
                }
                let that = this;
                Request.post('/api/query', {
                    query: `query{
  editors {
    category
    componentDefinition {
      name
      schema
      scriptFile
    }
    description
    icon
    name
  }
}`
                })
                    .onSuccess((data) => {
                        that.editors = data.data.editors;
                        if (that.debug) {
                            Logger.debug("Finished loading editors:", data.data.editors);
                        }
                    })
                    .withStorageMode(StorageMode.StorageAndUpdate)
                    .send();
            }
        },
        props: {
            debug: {
                type: Boolean,
                default: true
            }
        },
        created: function () {
            this.loadEditors();
        }
    });
</script>