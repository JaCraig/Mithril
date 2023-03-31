import { FormValidation } from "./Validation/FormValidation";
import { ErrorLogging } from "./Logging/ErrorLogging";
import { LocalStorage } from "./WebStorage/LocalStorage";
import { SessionStorage } from "./WebStorage/SessionStorage";
import { Request, StorageMode } from "./AJAX/Request";
import { DatabaseConnection } from "./Database/Database";
import { BrowserUtils } from "./Browser/BrowserUtils";

import { Hotkeys } from "./Hotkey/Hotkeys";
import { Router } from "./Router/Router";
import { PageHistory } from "./History/PageHistory";
import { Downloader } from "./IO/Downloader";

// Starts up and generally manages the framework
class Framework {
    // constructor
    constructor() {
        this.validation = new FormValidation();
        this.errorLogger = new ErrorLogging();
        this.localStorage = new LocalStorage();
        this.sessionStorage = new SessionStorage();
        this.hotkeys = new Hotkeys();
        this.router = new Router();
        this.history = new PageHistory();

        window.addEventListener("keydown", x => this.hotkeys.press(x));
        window.addEventListener("load", x => this.validation.initialize(), false);
        window.onerror = (msg, url, ln, col, error) => {
            this.errorLogger.onError(msg.toString(), url, ln, col, error);
        };
        this.errorLogger.setLoggingFunction((message: string, stack: string) => { console.log(message); });
    }

    // the hotkeys object
    public hotkeys: Hotkeys;

    // The router object
    public router: Router;

    // The form validation object
    public validation: FormValidation;

    // The error logging object
    public errorLogger: ErrorLogging;

    // The page history object
    public history: PageHistory;

    // The local storage object
    public localStorage: LocalStorage;

    // The session storage object
    public sessionStorage: SessionStorage;
}

export { Request, DatabaseConnection, Framework, StorageMode, BrowserUtils, Downloader };