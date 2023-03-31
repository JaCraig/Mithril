import { DatabaseConnection } from "../Database/Database";

//Ajax request that the system uses.
export class Request {
    // Constructor
    constructor(method: string, url: string, data?: any) {
        this.url = url;
        this.method = method.toUpperCase();
        this.data = data;
        this.headers = new Headers();
        if (this.method !== "GET" &&
            this.method !== "HEAD" &&
            this.method !== "DELETE" &&
            this.method !== "TRACE") {
            this.type("application/json");
        }
        this.accept("application/json");
        this.parser = x => x.json();
        this.serializer = x => JSON.stringify(x);
        this.storageMode = StorageMode.NetworkOnly;
        this.databaseName = "MithrilStorage"
        this.cacheKey = this.url + this.serializer(this.data);
        this.credentials = "same-origin";
    }

    // The serializer that the application uses
    private serializer: (data: any) => string;

    // URL to call
    private url: string;

    // Credentials type sent with the request ("same-origin", "include", or "omit")
    private credentials: RequestCredentials;

    // Method to use when calling
    private method: string;

    // Success callback
    private success: (response: any) => any;

    // Parser callback
    private parser: (response: Response) => Promise<any>;

    // Data to send in the request
    private data: any;

    // Error callback
    private error: (reason: any) => any;

    // Any headers to add to the call
    private headers: Headers;

    // The cache key
    private cacheKey: string;

    // Database to cache the results
    private databaseName: string;

    // Storage mode
    private storageMode: StorageMode;

    // GET method.
    public static get(url: string, data?: any): Request {
        return Request.makeRequest("GET", url, data);
    }

    // A request using a HTTP verb that is not GET, POST, PUT, or DELETE
    public static makeRequest(method: string, url: string, data?: any): Request {
        return new Request(method, url, data);
    }

    // POST method.
    public static post(url: string, data?: any): Request {
        return Request.makeRequest("POST", url, data);
    }

    // PUT method.
    public static put(url: string, data?: any): Request {
        return Request.makeRequest("PUT", url, data);
    }

    // DELETE method.
    public static delete(url: string, data?: any): Request {
        return Request.makeRequest("DELETE", url, data);
    }

    // Adds a callback to call if the AJAX request succeeds.
    public onSuccess(callback: (ev: any) => any): Request {
        this.success = callback;
        return this;
    }

    // Adds a callback to call if the AJAX request fails.
    public onError(callback: (ev: any) => any): Request {
        this.error = callback;
        return this;
    }

    // Adds a header value to the AJAX request.
    public setHeader(key: string, value: string): Request {
        this.headers.set(key, value);
        return this;
    }

    // Sets the cache key
    public setCacheKey(key: string): Request {
        this.cacheKey = key;
        return this;
    }

    // Sets the credentials type used for the call
    public setCredentials(type: RequestCredentials): Request {
        this.credentials = type;
        return this;
    }

    // Short hand for setting the content type header value
    public type(value: string): Request {
        return this.setHeader("Content-Type", value);
    }

    // Short hand for setting the accepts header value
    public accept(value: string): Request {
        return this.setHeader("Accept", value);
    }

    // Sets the parser that the request uses
    public setParser(parser: (response: Response) => Promise<any>): Request {
        this.parser = parser;
        return this;
    }

    // Ensures that the result of the request will be cached and used in future requests.
    public setMode(storageMode: StorageMode, databaseName: string = "MithrilStorage"): Request {
        this.databaseName = databaseName;
        this.storageMode = storageMode;
        return this;
    }

    // Sets the serializer that the request uses
    public setSerializer(serializer: (data: any) => string): Request {
        this.serializer = serializer;
        return this;
    }

    // Actually sends the request, parses it, and calls either the
    // success or error functions if they exist.
    public send(): void {
        if (this.error === undefined || this.error === null) {
            this.error = x => { };
        }
        if (this.success === undefined || this.success === null) {
            this.success = x => { };
        }
        let serializedData = this.serializer(this.data);
        if (this.storageMode === StorageMode.StorageFirst) {
            Request.returnValueFromDB(this.cacheKey, this.databaseName, this.success);
            this.queryNetwork(serializedData, this.cacheKey, this.databaseName, response => { }, response => {
                Request.saveValueToDB(response, this.cacheKey, this.databaseName);
            });
            return;
        }
        if (this.storageMode === StorageMode.StorageAndUpdate) {
            Request.returnValueFromDB(this.cacheKey, this.databaseName, this.success);
            this.queryNetwork(serializedData, this.cacheKey, this.databaseName, response => { }, response => {
                Request.saveValueToDB(response, this.cacheKey, this.databaseName);
                this.success(response);
            });
            return;
        }
        if (this.storageMode === StorageMode.NetworkFirst) {
            this.queryNetwork(serializedData, this.cacheKey, this.databaseName, this.success, response => {
                Request.saveValueToDB(response, this.cacheKey, this.databaseName);
                this.success(response);
            });
            return;
        }
        this.queryNetwork(serializedData, this.cacheKey, this.databaseName, x => { }, this.success);
    }

    // Saves a value to the database/cache
    private static saveValueToDB(data: string, dataKey: string, databaseName: string) {
        new DatabaseConnection(databaseName, ["cache", "cacheExpirations"], 1)
            .openDatabase(database => {
                if (data === undefined) {
                    return;
                }
                database.add("cache", data, dataKey);
                database.add("cacheExpirations", Date.now(), dataKey);
            });
    }

    // Queries the network and saves the data to the appropriate cache table.
    private queryNetwork(
        serializedData: string,
        dataKey: string,
        databaseName: string,
        offlineCallback: (response: any) => any,
        onlineCallback: (response: any) => any) {
        if (!navigator.onLine) {
            if (this.storageMode === StorageMode.NetworkFirst) {
                Request.returnValueFromDB(dataKey, databaseName, offlineCallback);
            }
            return;
        }
        fetch(this.url, {
            credentials: this.credentials,
            method: this.method,
            body: serializedData,
            headers: this.headers
        })
            .then(this.parser)
            .then(onlineCallback)
            .catch(this.error);
    }

    // Gets the value in the database and returns that for success.
    private static returnValueFromDB(dataKey: string, databaseName: string, callback: (response: any) => any) {
        new DatabaseConnection(databaseName, ["cache", "cacheExpirations"], 1)
            .openDatabase(database => {
                database.getByKey("cache", dataKey, event => {
                    let result = (<any>event.target).result;
                    if (result === undefined) {
                        return;
                    }
                    callback(result);
                });
            });
    }
}

// Storage mode
export enum StorageMode {
    // network first
    NetworkFirst = 0,
    // storage first
    StorageFirst = 1,
    // network only
    NetworkOnly = 2,
    // Storage and then update
    StorageAndUpdate = 3
}