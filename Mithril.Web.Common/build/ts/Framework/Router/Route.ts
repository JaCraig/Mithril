import { StringDictionary } from '../Types/StringDictionary'
import { PathPart } from './PathPart'
import { QueryPart } from './QueryPart'
import { HashPart } from './HashPart'

// An individual route
export class Route {
    // Constructor
    constructor(url: string, callback: (parameters: StringDictionary<any>) => void, defaultValues?: StringDictionary<any>) {
        if (defaultValues === undefined) {
            defaultValues = new StringDictionary<any>();
        }
        this.url = this.fixUrl(url);
        this.pathParts = this.getPathParts(this.url).map(x => new PathPart(x, defaultValues));
        this.pathParts = this.pathParts ? this.pathParts : [];
        this.queryParts = this.getQueryParts(this.url).map(x => new QueryPart(x, defaultValues));
        this.queryParts = this.queryParts ? this.queryParts : [];
        this.hashParts = this.getHashParts(this.url).map(x => new HashPart(x, defaultValues));
        this.hashParts = this.hashParts ? this.hashParts : [];
        this.callbacks = [callback];
        this.defaultValues = defaultValues;
    }

    // URL path parts of the path
    private pathParts: PathPart[];

    // Query string parts of the path
    private queryParts: QueryPart[];

    private hashParts: HashPart[];

    // Default values for this path
    private defaultValues: StringDictionary<any>;

    // URL that matches with the individual route
    private url: string;

    // Callback function to call
    private callbacks: { (parameters: StringDictionary<any>): void }[];

    // Determines if the url passed in equals the current route
    public isRoute(url: string): boolean {
        return this.url === this.fixUrl(url);
    }

    // adds a callback to the route
    public addCallback(callback: (parameters: StringDictionary<any>) => void): void {
        this.callbacks.push(callback);
    }

    // removes a callback from the route
    public removeCallback(callback: (parameters: StringDictionary<any>) => void): void {
        this.callbacks = this.callbacks.filter(x => x !== callback);
    }

    // Fixes a url that is passed in
    private fixUrl(url: string): string {
        if (!url.length) {
            return url;
        }
        return url.replace(/^\//, "").replace(/\/+/g, "/").replace(/^\/|\/($|\?)/, "").replace(/\/\#/g, "#");
    }

    // Get the path parts for the route
    private getPathParts(url: string): string[] {
        return url.split("?", 2)[0].split("#", 2)[0].split("/");
    }

    // Gets the hash code portion of the url
    private getHashParts(url: string): string[] {
        let urlSplit = url.split("?", 2)[0].split("#", 2);
        return (urlSplit.length < 2) ? [] : [urlSplit[1].replace("!", "")];
    }

    // Gets the query string parts of the url
    private getQueryParts(url: string): string[] {
        let urlSplit = url.split("?", 2);
        if (urlSplit.length < 2) {
            return [];
        }
        url = urlSplit[1];
        return url ? url.split("&") : [];
    }

    // Gets the parameters from the url specified
    private getParametersFromUrl(pathParts: string[], queryParts: string[], hashParts: string[]): StringDictionary<any> {
        let parameters = new StringDictionary<any>();
        for (let x = 0; x < pathParts.length; ++x) {
            this.pathParts[x].setValue(pathParts[x], parameters);
        }
        for (let x = 0; x < queryParts.length; ++x) {
            this.queryParts[x].setValue(queryParts[x], parameters);
        }
        for (let x = 0; x < hashParts.length; ++x) {
            this.hashParts[x].setValue(hashParts[x], parameters);
        }
        if (this.pathParts.length > pathParts.length) {
            for (let x = pathParts.length; x < this.pathParts.length; ++x) {
                this.pathParts[x].setValue("", parameters);
            }
        }
        if (this.queryParts.length > queryParts.length) {
            for (let x = queryParts.length; x < this.queryParts.length; ++x) {
                this.queryParts[x].setValue("", parameters);
            }
        }
        if (this.hashParts.length > hashParts.length) {
            for (let x = hashParts.length; x < this.hashParts.length; ++x) {
                this.hashParts[x].setValue("", parameters);
            }
        }
        return parameters;
    }

    // Determines if the route is a match
    private isMatch(pathParts: string[], queryParts: string[], hashParts: string[]): boolean {
        if (this.pathParts.length < pathParts.length) {
            return false;
        }
        for (let x = 0; x < pathParts.length; ++x) {
            if (!this.pathParts[x].isMatch(pathParts[x])) {
                return false;
            }
        }
        if (this.pathParts.length > pathParts.length) {
            for (let x = pathParts.length; x < this.pathParts.length; ++x) {
                if (!this.pathParts[x].isMatch("")) {
                    return false;
                }
            }
        }

        if (this.queryParts.length < queryParts.length) {
            return false;
        }
        for (let x = 0; x < queryParts.length; ++x) {
            if (!this.queryParts[x].isMatch(queryParts[x])) {
                return false;
            }
        }
        if (this.queryParts.length > queryParts.length) {
            for (let x = queryParts.length; x < this.queryParts.length; ++x) {
                if (!this.queryParts[x].isMatch("")) {
                    return false;
                }
            }
        }

        if (this.hashParts.length < hashParts.length) {
            return false;
        }
        for (let x = 0; x < hashParts.length; ++x) {
            if (!this.hashParts[x].isMatch(hashParts[x])) {
                return false;
            }
        }
        if (this.hashParts.length > hashParts.length) {
            for (let x = hashParts.length; x < this.hashParts.length; ++x) {
                if (!this.hashParts[x].isMatch("")) {
                    return false;
                }
            }
        }

        return true;
    }

    // Calls the route's callback function
    public run(url: string): boolean {
        url = this.fixUrl(url);
        let pathParts = this.getPathParts(url);
        let queryParts = this.getQueryParts(url);
        let hashParts = this.getHashParts(url);
        if (!this.isMatch(pathParts, queryParts, hashParts)) {
            return false;
        }
        let parameters = this.getParametersFromUrl(pathParts, queryParts, hashParts);
        parameters["url"] = url;
        this.callbacks.forEach(x => x(parameters));
        return true;
    }
}