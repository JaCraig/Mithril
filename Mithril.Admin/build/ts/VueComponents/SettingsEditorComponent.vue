<style type="text/less" scoped>
</style>
<template>
    <div class="panel" :class="schema.class">
        <header>{{name}}</header>
        <div class="body">
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

    export default Vue.defineComponent({
        name: "settings-editor-component",
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
        }
    });
</script>