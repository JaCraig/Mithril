import { LocalStorage } from "./WebStorage/LocalStorage";
import { SessionStorage } from "./WebStorage/SessionStorage";
import { Request, StorageMode } from "./Request";
import { DatabaseConnection } from "./Database";
import { BrowserUtils } from "./Browser/BrowserUtils";

import { Hotkeys } from "./Hotkey/Hotkeys";
import { Router } from "./Router/Router";
import { PageHistory } from "./History/PageHistory";
import { Downloader } from "./IO/Downloader";
import { CallerEnricher, ConsoleSink, DefaultFormatter, Logger, UrlEnricher } from "./Logging";

// Starts up and generally manages the framework
class Framework {
    // constructor
    constructor() {
        Logger.configure()
            .minimumLevel("Debug")
            .enrichWith(new CallerEnricher())
            .enrichWith(new UrlEnricher())
            .formatUsing(new DefaultFormatter("[{Timestamp}]\t[{Level}]\t[{caller}]\t\t{Message}{Exception}"))
            .writeTo(new ConsoleSink());
        this.localStorage = new LocalStorage();
        this.sessionStorage = new SessionStorage();
        this.hotkeys = new Hotkeys();
        this.router = new Router();
        this.history = new PageHistory();

        window.addEventListener("keydown", x => this.hotkeys.press(x));
        window.onerror = (msg, url, ln, col, error) => {
            Logger.error(msg.toString(), { "url": url, "line": ln, "column": col, "stack": error?.stack || "" }, error);
        };
    }

    // the hotkeys object
    public hotkeys: Hotkeys;

    // The router object
    public router: Router;

    // The page history object
    public history: PageHistory;

    // The local storage object
    public localStorage: LocalStorage;

    // The session storage object
    public sessionStorage: SessionStorage;
}

export { Request, DatabaseConnection, Framework, StorageMode, BrowserUtils, Downloader };