
<template>
    <div>
        Text
        <div :class="schema.classes" v-html="internalModel || schema.model"></div>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import "../../Framework/Extensions/String";

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