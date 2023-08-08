// A set of browser/window related util functions.
export class BrowserUtils {
    // Returns the current domain.
    static get domain(): string {
        return window.location.protocol + "//" + window.location.host + "/";
    }

    // Determines if this is being run locally or in production.
    static get isLocal(): boolean {
        return (/^http:\/\/localhost:\d{5}\/$/).test(BrowserUtils.domain);
    }

    // Gets the hash without the hash bang.
    static get HashBang(): string {
        return window.location.hash.replace("#!", "");
    }

    // Gets the text after the last slash. Presumably the ID needed.
    static get Id(): string {
        return window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);
    }

    // Gets a value from the query string.
    public static GetQueryString(field: string): string {
        let href = window.location.href;
        var reg = new RegExp('[?&]' + field + '=([^&#]*)', 'i');
        var string = reg.exec(href);
        return string ? string[1] : null;
    }

    // Sets the title for the page.
    public static setPageTitle(title: string): void {
        document.title = title;
    }
}