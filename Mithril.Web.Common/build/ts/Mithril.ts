import { Framework, DatabaseConnection, Request, StorageMode, BrowserUtils, Downloader } from "./Framework/Framework";
import MithrilPlugin from "./Component/VueExtensions/MithrilPlugin";

import Vue from 'vue';

// Starts up and generally manages the framework
class Mithril {
    // constructor
    constructor() {
        this.framework = new Framework();
        this.setupLogging();
    }

    // Sets up Vue components
    public SetupComponents(app: Vue.App<Element>): Vue.App<Element> {
        app.use(MithrilPlugin);
        return app;
    }

    //Framework items
    private framework: Framework;

    // Set up error logging.
    private setupLogging(): void {
        var errorLogged = false;
        this.framework.errorLogger.setLoggingFunction(function (ex: string, error: string) {
            if (ex === null || errorLogged) {
                return;
            }
            errorLogged = true;
            var url = document.location;
            var stack = error;
            var out = ex;
            out += ": at document path '" + url + "'.";
            if (stack !== null) {
                out += "\n at " + stack;
            }
            Request.post("API/Command/v1/Log", { logLevel: "Error", message: out }).send();
        });
    }
}

export { Request, DatabaseConnection, StorageMode, BrowserUtils, Mithril, Downloader };