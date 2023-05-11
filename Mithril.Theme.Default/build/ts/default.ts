import { BrowserUtils, DatabaseConnection, Mithril, Request, StorageMode, Downloader } from "../../../Mithril.Web.Common/build/ts/Mithril";

export class ThemeStartup {
    constructor() {
        this.Core = new Mithril();
    }

    public Core: Mithril;
}

export { Request, DatabaseConnection, StorageMode, BrowserUtils, Mithril, Downloader };