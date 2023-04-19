import Vue from "vue";
import { RegisterDirectives } from "./VueDirectives";
import { RegisterFilters } from "./VueFilters";
import VueComponents from "./VueComponents";

export default {
    install: (app: Vue.App<Element>, options: any) => {
        RegisterFilters(app);
        RegisterDirectives(app);
        VueComponents(app);
    }
}