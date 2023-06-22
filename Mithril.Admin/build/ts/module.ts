import Vue from "vue";
import moment from "moment";
import SettingsEditorComponent from "./VueComponents/SettingsEditorComponent.vue";
import EntityEditorComponent from "./VueComponents/EntityEditorComponent.vue";
import AdminApplication from "./VueComponents/AdminApplication.vue";
import MithrilPlugin from "../../../Mithril.Web.Common/build/ts/Component/VueExtensions/MithrilPlugin";
import { Logger } from "../../../Mithril.Web.Common/build/ts/Framework/Logging";

class AdminInitializer {
    constructor() {
        Logger.debug("Admin loading");
        this.AdminApp = this.SetupComponents(Vue.createApp({
            data: function () {
                return {};
            }
        }));
        this.AdminApp.mount("#AdminApplication");
        Logger.debug("Admin loaded");
    }

    private AdminApp: Vue.App<Element>;

    // Sets up Vue components
    public SetupComponents(app: Vue.App<Element>): Vue.App<Element> {
        app.use(MithrilPlugin);
        app.component("admin-application", AdminApplication);
        app.component("settings-editor-component", SettingsEditorComponent);
        app.component("data-editor-component", EntityEditorComponent);
        return app;
    }
}

export default (() => {
    let adminApp = new AdminInitializer();
})();