import { BrowserUtils, DatabaseConnection, Mithril, Request, StorageMode, Downloader } from "../../../Mithril.Web.Common/build/ts/Mithril"

class ThemeStartup {
    constructor() {
        this.Core = new Mithril();
    }

    private Core: Mithril;
}

export default (() => {
    let DefaultTheme = new ThemeStartup();
})();