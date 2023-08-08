import { BrowserUtils, DatabaseConnection, Mithril, Request, StorageMode, Downloader } from "../../../Mithril.Web.Common/build/ts/Mithril";
import "../less/default.less";

export class ThemeStartup {
    constructor() {
        this.Core = new Mithril();
    }

    public Core: Mithril;
}

export { Request, DatabaseConnection, StorageMode, BrowserUtils, Mithril, Downloader };