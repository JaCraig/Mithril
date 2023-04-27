<template>
    <ul class="unstyled">
        <li>
            <a class="fa-bars" href="#" v-on:click.stop.prevent="showDropDownFunc"></a>
            <div v-bind:class="{ 'show': showDropDown, 'z-depth-2': showDropDown }" v-click-outside="hideDropDownFunc">
                <ul class="grouped">
                    <li v-for="item in items" v-bind:key="item.displayName">
                        <a :href="item.url" v-bind:class="[item.icon]">{{ item.displayName }}</a>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.defineComponent({
    data: function () {
        return {
            items: this.items,
            showDropDown: false,
        };
    },
    props: {
        items: Array,
    },
    methods: {
        showDropDownFunc: function () {
            this.showDropDown = true;
        },
        hideDropDownFunc: function () {
            this.showDropDown = false;
        },
    },
});


</script>