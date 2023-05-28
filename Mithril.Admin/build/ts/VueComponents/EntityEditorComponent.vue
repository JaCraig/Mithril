<style type="text/less" scoped>
</style>
<template>
    <div>
        Entity Editor
        <div v-if="debug && schema" class="panel debug">
            <header>Editor Schema</header>
            <pre>
    {{schema}}
                </pre>
        </div>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";

    export default Vue.defineComponent({
        name: "entity-editor-component",
        props: {
            schema: {
                type: Object,
                default: {}
            },
            debug: {
                type: Boolean,
                default: true
            }
        }
    });
</script>