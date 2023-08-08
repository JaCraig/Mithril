<template>
    <ul class="unstyled"  v-click-outside="hideDropDownFunc">
        <li>
            <a class="fas fa-circle-user" href="#" v-on:click.stop.prevent="showDropDownFunc"></a>
            <div v-bind:class="{ 'show': showDropDown, 'z-depth-2': showDropDown, 'hidden': !showDropDown }">
                <ul class="grouped">
                    <li v-for="item in items" v-bind:key="item.display">
                        <a :href="item.url" v-bind:class="[item.icon]">{{ item.display }}</a>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
</template>

<script lang="ts">
import Vue from 'vue'
import clickOutside from './VueExtensions/Directives/clickOutside';


export default Vue.defineComponent({
    data: function () {
        return {
            items: this.items,
            showDropDown: false,
        };
    },
    props: {
        items: Array
    },
    methods: {
        showDropDownFunc: function () {
            this.showDropDown = true;
        },
        hideDropDownFunc: function () {
            this.showDropDown = false;
        },
    },
    directives: {
        clickOutside
    }
});


</script>