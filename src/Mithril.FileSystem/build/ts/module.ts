import Vue from "vue";
//import FileBrowserApplication from "./VueComponents/FileBrowserApplication.vue";
//import MithrilPlugin from "../../../Mithril.Web.Common/build/ts/Component/VueExtensions/MithrilPlugin";
import { Logger } from "../../../Mithril.Web.Common/build/ts/Framework/Logging";
import { BrowserUtils } from "../../../Mithril.Web.Common/build/ts/Framework/Browser/BrowserUtils";

// Starts up the FileBrowser application
class FileBrowserInitializer {
    // constructor
    constructor() {
        Logger.debug("File Browser loading");
        var links = document.getElementsByTagName('a');
        var directoryPath = BrowserUtils.GetQueryString('host');
        if (directoryPath == null)
            directoryPath = "/";
        if (!BrowserUtils.isLocal) {
            directoryPath = BrowserUtils.domain + directoryPath;
        }
        for (var x = 0; x < links.length; ++x) {
            links[x].addEventListener('click', function (event) {
                event.preventDefault();
                var ReturnURL = this.href.replace(BrowserUtils.domain, "");
                window.parent.postMessage(JSON.stringify({ href: ReturnURL }), directoryPath);
                return false;
            });
        }
        //this.FileBrowserApp = this.SetupComponents(Vue.createApp({
        //    data: function () {
        //        return {};
        //    }
        //}));
        //this.FileBrowserApp.mount("#FileBrowserApplication");
        Logger.debug("File Browser loaded");
    }

    // FileBrowser application
    //private FileBrowserApp: Vue.App<Element>;

    // Sets up Vue components
    public SetupComponents(app: Vue.App<Element>): Vue.App<Element> {
        //app.use(MithrilPlugin);
        //app.component("FileBrowser-application", FileBrowserApplication);
        //app.component("settings-editor-component", SettingsEditorComponent);
        //app.component("data-editor-component", EntityEditorComponent);
        //return app;
    }
}

export default (() => {
    let fileBrowserApp = new FileBrowserInitializer();
})();