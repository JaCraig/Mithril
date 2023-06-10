
<template>
    <div class="controls">
        Buttons
        <div class="input-group">
            <input v-for="(button) in schema.buttons"
                    v-bind:key="generateGuid(button)"
                    :type="button.type"
                    :value="button.value"
                    :class="button.classes"
                    @click="click($event,button)" />
        </div>
        
    </div>
</template>

<script lang="ts">
import Vue from 'vue'

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
        click: function(event: any, button: any) {
            this.$emit("click", event, button);
            return false;
        },
        generateGuid: function (item: any) {
            let Key = item.key;
            if(Key) {
                return Key;
            }
            let result = ''
            for (let j = 0; j < 32; j++) {
                let i = Math.floor(Math.random() * 16).toString(16).toUpperCase();
                result = result + i;
            }
            item.key = result;
            return item.key;
        },
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