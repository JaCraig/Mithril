// import Form from "../Form.vue";
// import Tabs from "../Tabs.vue";
import DataHandler from "../DataHandler.vue";
import Vue from "vue";

export default (app: Vue.App<Element>) => {
    app.component('mithril-data-handler', DataHandler);
    // app.component('mithril-tabs', Tabs);
    // app.component('mithril-form', Form);
};