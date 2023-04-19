
<template>
    <div class="tabs" v-cloak>
        <header>
            <ul class="row flex align-items-stretch">
                <li v-for="(section, index) in sections" v-bind:key="index">
                    <a href="#!" v-on:click.stop.prevent="switchSelected(section.name)" class="tab"
                        v-bind:class="{ selected: section.selected }">
                        <span class="fas" v-bind:class="[section.icon]"></span>
                        {{ section.name }}
                    </a>
                </li>
            </ul>
        </header>
        <section>
            <slot></slot>
        </section>
    </div>
</template>

<script lang="ts">
import Vue from 'vue'
import Tab from "./DataTypes/Tab";

export default Vue.defineComponent({
    methods: {
        findTab: function (tabName: string): Tab {
            if (!this.sections || this.sections.length === 0) {
                return null;
            }
            if (!tabName) {
                tabName = this.sections[0].name;
            }
            return this.sections.filter((x: any) => x.name === tabName)[0];
        },
        switchSelected: function (item: string): void {
            this.sectionPicked = this.findTab(item);
            this.switchTabs();
            this.$emit("section-changed", this.sectionPicked);
        },
        switchTabs: function (): void {
            if (!this.sections || this.sections.length === 0) {
                return;
            }
            if (!this.sections.some((x: any) => x === this.sectionPicked)) {
                this.sectionPicked = this.sections[0];
            }
            if (!this.sectionPicked) {
                return;
            }
            for (let x = 0; x < this.sections.length; ++x) {
                this.sections[x].selected = false;
            }
            this.sectionPicked.selected = true;
        },
    },
    props: {
        initialSectionPicked: {
            type: String,
            default: ""
        },
        sections: {
            type: Array,
            default: []
        }
    },
    watch: {
        sections: function (value): void {
            this.switchSelected(this.sectionPicked?.name);
        },
        initialSectionPicked: function (value): void {
            this.switchSelected(this.initialSectionPicked);
        }
    },
    data() {
        return {
            sectionPicked: this.findTab(this.initialSectionPicked),
        };
    },
    beforeMount: function (): void {
        this.switchSelected(this.initialSectionPicked);
    }
});
</script>