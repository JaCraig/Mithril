declare global {
    interface String {
        slugify(): string;
        stripHTML(): string;
        toNumber(): number;
        replaceAll(): string;
    }
}

// "slugifies" the string.
String.prototype.slugify = function (): string {
    return this.trim().replace(/ /g, "-").replace(/-{2,}/g, "-").replace(/^-+|-+$/g, "").replace(/([^a-zA-Z0-9-_/./:]+)/g, "");
};

// Strips out HTML
String.prototype.stripHTML = function (): string {
    let tempDiv = document.createElement("div");
    tempDiv.innerHTML = this;
    return (tempDiv.textContent || tempDiv.innerText || "").replace(/^\s+|\s+$/g, "");
};

// Converts it to a number, stripping out non numeric values
String.prototype.toNumber = function (): number {
    if (this == null)
        return 0;
    return parseFloat(this.stripHTML().replace(/[^0-9.-]/g, ""));
};

export { }