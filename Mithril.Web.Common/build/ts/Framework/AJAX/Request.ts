//TODO: Add retry logic, cancellation tokens, progress tracking, authentication/authorization, etc.
import { DatabaseConnection } from "../Database/Database";

// Request options
export interface RequestOptions {
    // Request method (default: GET)
    method: string;
    // Request url (default: "")
    url: string;
    // Request data
    data?: any;
    // Request headers (default: {})
    headers?: Record<string, string>;
    // Request credentials (default: same-origin)
    credentials?: RequestCredentials;
    // Request serializer (default: JSON.stringify)
    serializer?: (data: any) => string;
    // Request parser (default: response.json())
    parser?: (response: Response) => Promise<any>;
    // Request success callback (default: console.log)
    success?: (response: any) => void;
    // Request error callback (default: console.error)
    error?: (reason: any) => void;
    // Storage mode (default: StorageMode.NetworkFirst)
    storageMode?: StorageMode;
    // Cache key (default: url + JSON.stringify(data))
    cacheKey?: string;
    // Database name (default: MithrilStorage)
    databaseName?: string;
    // Timeout in milliseconds (default: 60000)
    timeout?: number;
}

// Request class
// Used to make AJAX requests and also cache the response in IndexedDB.
// It can also return the cached response if the request fails or times out (see StorageMode).
export class Request {
    // Request options
    private options: RequestOptions = {
        method: "GET",
        url: "",
        headers: {},
        credentials: "same-origin",
        serializer: JSON.stringify,
        parser: (response: Response) => response.json(),
        success: (response) => { console.log("Request response:", response) },
        error: (reason) => { console.error("Request error:", reason) },
        storageMode: StorageMode.NetworkFirst,
        cacheKey: "",
        databaseName: "MithrilStorage",
        timeout: 60000
    };

    // Abort controller (used to abort the request) (default: null)
    private abortController: AbortController | null = null;

    // Constructor
    // options: The request options
    constructor(options: RequestOptions) {
        this.options = { ...this.options, ...options };
    }

    // Creates a GET request
    // Note: GET requests are cached by default
    // url: The request url
    // data: The request data
    public static get(url: string, data?: any): Request {
        return new Request({ method: "GET", url, data, cacheKey: url + JSON.stringify(data) })
            .withHeaders({
                "Accept": "application/json"
            });
    }

    // Creates a POST request
    // Note: POST requests are not cached by default
    // url: The request url
    // data: The request data
    public static post(url: string, data?: any): Request {
        return new Request({ method: "POST", url, data, cacheKey: url + JSON.stringify(data), storageMode: StorageMode.NetworkOnly })
            .withHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            });
    }

    // Creates a PUT request
    // Note: PUT requests are not cached by default
    // url: The request url
    // data: The request data
    public static put(url: string, data?: any): Request {
        return new Request({ method: "PUT", url, data, cacheKey: url + JSON.stringify(data), storageMode: StorageMode.NetworkOnly })
            .withHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            });
    }

    // Creates a DELETE request
    // Note: DELETE requests are not cached by default
    // url: The request url
    // data: The request data
    public static delete(url: string, data?: any): Request {
        return new Request({ method: "DELETE", url, data, cacheKey: url + JSON.stringify(data), storageMode: StorageMode.NetworkOnly })
            .withHeaders({
                "Accept": "application/json"
            });
    }

    // Adds header values to the request
    // headers: The header values
    public withHeaders(headers: Record<string, string>): this {
        this.options.headers = { ...this.options.headers, ...headers };
        return this;
    }

    // Adds credentials to the request
    // credentials: The credentials
    public withCredentials(credentials: RequestCredentials): this {
        this.options.credentials = credentials;
        return this;
    }

    // Sets the serializer for the request
    // serializer: The serializer
    public withSerializer(serializer: (data: any) => string): this {
        this.options.serializer = serializer;
        return this;
    }

    // Sets the parser for the request
    // parser: The parser
    public withParser(parser: (response: Response) => Promise<any>): this {
        this.options.parser = parser;
        return this;
    }

    // Sets the success callback for the request
    // callback: The success callback
    public onSuccess(callback: (response: any) => void): this {
        this.options.success = callback ?? ((response) => { console.log("Request response:", response) });
        return this;
    }

    // Sets the error callback for the request
    // callback: The error callback
    public onError(callback: (reason: any) => void): this {
        this.options.error = callback ?? ((reason) => { console.error("Request error:", reason) });
        return this;
    }

    // Sets the storage mode for the request
    // storageMode: The storage mode
    // databaseName: The database name (default: MithrilStorage)
    public withStorageMode(storageMode: StorageMode, databaseName = "MithrilStorage"): this {
        this.options.storageMode = storageMode;
        this.options.databaseName = databaseName;
        return this;
    }

    // Sets the cache key for the request
    // cacheKey: The cache key
    public withCacheKey(cacheKey: string): this {
        this.options.cacheKey = cacheKey;
        return this;
    }

    // Sets the timeout for the request
    // timeout: The timeout in milliseconds (default: 60000)
    // Note: The timeout is only used for network requests
    public withTimeout(timeout?: number): this {
        this.options.timeout = timeout ?? 60000;
        return this;
    }

    // Aborts the request, if it is still running, and calls the error callback.
    // Note: This is only supported for network requests
    public abort(): this {
        if (this.abortController === null) {
            return this;
        }
        this.abortController.abort();
        this.options.error(new Error("The request was aborted."));
        return this;
    }

    // Actually sends the request, parses it, and calls either the
    // success or error functions if they exist.
    // Returns the parsed response.
    public async send(): Promise<any> {
        const { method, url, data, headers, credentials, serializer, parser, success, error, storageMode, cacheKey, databaseName, timeout } = this.options;
        const serializedData = serializer(data);
        const abortController = new AbortController();
        this.abortController = abortController;

        if (storageMode === StorageMode.StorageFirst || storageMode === StorageMode.StorageAndUpdate) {
            const cachedValue = await Request.getValueFromDB(cacheKey, databaseName);
            if (cachedValue !== undefined) {
                success(cachedValue);
                if (storageMode === StorageMode.StorageFirst) {
                    return cachedValue;
                }
            }
        }

        if (!navigator.onLine) {
            if (storageMode === StorageMode.NetworkFirst) {
                const cachedValue = await Request.getValueFromDB(cacheKey, databaseName);
                if (cachedValue !== undefined) {
                    success(cachedValue);
                    return cachedValue;
                }
                let errorMessage = new Error("No cached value found and system is offline");
                error(errorMessage);
                return Promise.reject(errorMessage);
            }
            const errorMessage = new Error("System is offline");
            error(errorMessage);
            return Promise.reject(errorMessage);
        }

        try {
            const response = await Promise.race([
                fetch(url, {
                    method,
                    credentials,
                    headers,
                    body: serializedData,
                    signal: abortController.signal
                }),
                this.handleTimeout(timeout)
            ]);

            const parsedResponse = await parser(response);
            success(parsedResponse);

            if (storageMode !== StorageMode.NetworkOnly) {
                Request.saveValueToDB(parsedResponse, cacheKey, databaseName);
            }

            return parsedResponse;
        } catch (err) {
            error(err);
            return Promise.reject(err);
        }
    }

    // Handles the timeout for the request
    // timeout: The timeout in milliseconds (default: 60000)
    // returns: A promise that rejects when the timeout is reached
    private async handleTimeout(timeout?: number): Promise<never> {
        timeout ??= 60000;
        await new Promise<void>((_, reject) => {
            setTimeout(() => {
                reject(new Error('Request timeout'));
            }, timeout);
        });
        throw new Error('Request timeout');
    }

    // Saves a value to the database/cache
    // data: The data to save
    // dataKey: The key to save the data under
    // databaseName: The database name
    // returns: A promise that resolves when the data is saved
    private static async saveValueToDB(data: string, dataKey: string, databaseName: string): Promise<void> {
        if (data === undefined) {
            return;
        }
        const connection = new DatabaseConnection(databaseName, ["cache", "cacheExpirations"], 1);
        const database = await connection.openDatabase();
        await database.add("cache", data, dataKey);
        await database.add("cacheExpirations", Date.now(), dataKey);
    }

    // Returns a value from the database/cache
    // dataKey: The key to get the data from
    // databaseName: The database name
    // returns: The value from the database
    private static async getValueFromDB(dataKey: string, databaseName: string): Promise<any> {
        const connection = new DatabaseConnection(databaseName, ["cache", "cacheExpirations"], 1);
        const database = await connection.openDatabase();
        return await database.getByKey("cache", dataKey);
    }
}

// Storage mode for the request
export enum StorageMode {
    // network first (default)
    NetworkFirst = 0,
    // storage first (cache first)
    StorageFirst = 1,
    // network only (no storage)
    NetworkOnly = 2,
    // Storage and then update from network
    StorageAndUpdate = 3
}