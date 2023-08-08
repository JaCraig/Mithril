// import Form from "../Form.vue";
// import Tabs from "../Tabs.vue";
import DataHandler from "../DataHandler.vue";
import Vue from "vue";
import SideMenuVue from "../SideMenu.vue";

export default (app: Vue.App<Element>) => {
    app.component('mithril-data-handler', DataHandler);
    app.component('mithril-side-menu', SideMenuVue);
};