<style lang="less" scoped>
    .main-application {
        display: flex;

        & .left-nav {
            width: 200px;
        }
    }
</style>
<template>
    <div class="main-application">
        Admin Application
        <div class="left-nav">
            Left
            <pre>
                {{editors}}
            </pre>
        </div>
        <div>
            <pre>

            </pre>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import { Request, StorageMode } from "../../../../Mithril.Web.Common/build/ts/Framework/AJAX/Request";

    export default Vue.defineComponent({
        name: "admin-application",
        data: function() {
            return {
                editors: []
            };
        },
        methods: {
            loadEditors: function () {
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
                    })
                    .setMode(StorageMode.StorageAndUpdate)
                    .send();
            }
        },
        created: function () {
            console.log("Loading Editors");
            this.loadEditors();
        }
    });
</script>