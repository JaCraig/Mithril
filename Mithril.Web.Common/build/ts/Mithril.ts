import { Framework, DatabaseConnection, Request, StorageMode, BrowserUtils, Downloader } from "./Framework/Framework";
import { Logger, LogEvent, LogLevel, LogSink } from "./Framework/Logging/Logging";
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
        Logger.configure().minimumLevel("Error").writeTo(new APILoggerSink());
    }
}

// Logs errors to the API
class APILoggerSink implements LogSink {
    // Writes a log event to the sink. This is called by the logger.
    // event: The event to write.
    write(event: LogEvent): void {
        if (event == null || !window.navigator.onLine) {
            return;
        }
        var stack = event.exception?.stack || "";
        var out = event.message;
        if (stack !== null) {
            out += "\n at " + stack;
        }
        Request.post("API/Command/v1/Log", { logLevel: "Error", message: out }).send();
    }
}

export { Request, DatabaseConnection, StorageMode, BrowserUtils, Mithril, Downloader };