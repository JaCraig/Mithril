import { DatabaseConnection } from "../Database/Database";

// Cancellation token
export class CancellationToken {
    // Indicates whether the request is cancelled (default: false)
    canceled: boolean;
}

// Authentication provider interface (used to authenticate requests)
export interface AuthenticationProvider {
    // Authenticates the request (e.g. adds authentication headers)
    // request: The request to authenticate
    // Note: This method should reject the promise if the request cannot be authenticated.
    authenticate(request: Request): Promise<void>;

    // Called when the request fails with a 401 Unauthorized response
    // request: The request that failed
    // reason: The reason for the failure
    authenticationFailed(request: Request, reason: Response): Promise<void>;
}

// Request options
export interface RequestOptions {
    // Authentication provider (default: null)
    authenticationProvider?: AuthenticationProvider;
    // Cache key (default: url + JSON.stringify(data))
    cacheKey?: string;
    // Request cancellation token (default: null)
    cancellationToken?: CancellationToken;
    // Request credentials (default: same-origin)
    credentials?: RequestCredentials;
    // Request data
    data?: any;
    // Database name (default: MithrilStorage)
    databaseName?: string;
    // Request error callback (default: console.error)
    error?: (reason: any) => void;
    // Request headers (default: {})
    headers?: Record<string, string>;
    // Request method (default: GET)
    method: string;
    // Request parser (default: response.json())
    parser?: (response: Response) => Promise<any>;
    // Request retry callback (default: console.log)
    retry?: (attempt: number) => void;
    // Retry attempts (default: 3)
    retryAttempts?: number;
    // Retry delay in milliseconds (default: 1000)
    retryDelay?: number;
    // Request serializer (default: JSON.stringify)
    serializer?: (data: any) => string;
    // Storage mode (default: StorageMode.NetworkFirst)
    storageMode?: StorageMode;
    // Request success callback (default: console.log)
    success?: (response: any) => void;
    // Timeout in milliseconds (default: 60000)
    timeout?: number;
    // Request url (default: "")
    url: string;
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
        retry: (attempt) => { console.log("Request retry:", attempt) },
        storageMode: StorageMode.NetworkFirst,
        cacheKey: "",
        databaseName: "MithrilStorage",
        timeout: 60000,
        retryAttempts: 3,
        retryDelay: 1000
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

    // Creates a request of the specified type (e.g. GET, POST, PUT, DELETE, etc.)
    // method: The request method
    // url: The request url
    // data: The request data
    public static ofType(method: string, url: string, data?: any): Request {
        return new Request({ method, url, data, cacheKey: url + JSON.stringify(data) })
            .withHeaders({
                "Accept": "application/json"
            });
    }

    // Adds an authentication provider to the request (used to authenticate the request)
    // authenticationProvider: The authentication provider
    public withAuthenticationProvider(authenticationProvider: AuthenticationProvider): this {
        this.options.authenticationProvider = authenticationProvider;
        return this;
    }

    // Adds header values to the request
    // headers: The header values
    public withHeaders(headers: Record<string, string>): this {
        this.options.headers = { ...this.options.headers, ...headers };
        return this;
    }

    // Adds a cancellation token to the request (used to cancel the request)
    // cancellationToken: The cancellation token
    // Note: The request will finish executing, but the success/error callbacks will not be called if the request is cancelled.
    public withCancellationToken(cancellationToken: CancellationToken): this {
        this.options.cancellationToken = cancellationToken;
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

    // Sets the retry callback for the request
    // callback: The retry callback
    public onRetry(callback: (attempt: number) => void): this {
        this.options.retry = callback ?? ((attempt) => { console.log("Request retry:", attempt) });
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

    // Sets the number of retry attempts for the request
    // retryAttempts: The number of retry attempts (default: 3)
    // Note: The retry attempts are only used for network requests
    public withRetryAttempts(retryAttempts: number): this {
        this.options.retryAttempts = retryAttempts;
        return this;
    }

    // Sets the retry delay for the request in milliseconds
    // retryDelay: The retry delay in milliseconds (default: 1000)
    // Note: The retry delay is only used for network requests
    public withRetryDelay(retryDelay: number): this {
        this.options.retryDelay = retryDelay;
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
        const { authenticationProvider, method, url, data, headers, credentials, serializer, parser, success, error, storageMode, cacheKey, databaseName, timeout, retryAttempts, retryDelay, retry, cancellationToken } = this.options;
        const abortController = new AbortController();
        this.abortController = abortController;

        // Retry-related variables
        let attempts = 0;
        let lastError: any = null;

        const sendRequest = async (): Promise<any> => {
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
                }
                const errorMessage = new Error("System is offline");
                error(errorMessage);
                return Promise.reject(errorMessage);
            }

            try {
                const serializedData = serializer(data);
                await authenticationProvider?.authenticate(this);

                const response = await Promise.race([
                    fetch(url, {
                        method,
                        credentials,
                        headers,
                        body: serializedData,
                        signal: abortController.signal,
                    }),
                    this.handleTimeout(timeout)
                ]);
                if (cancellationToken?.canceled) {
                    return Promise.reject(new Error("The request was canceled."));
                }
                if (response.status >= 200 && response.status < 300) {
                    const parsedResponse = await parser(response);
                    success(parsedResponse);

                    if (storageMode !== StorageMode.NetworkOnly) {
                        Request.saveValueToDB(parsedResponse, cacheKey, databaseName);
                    }

                    return parsedResponse;
                }
                lastError = new Error(response.statusText);
                if (response.status === 401) {
                    await authenticationProvider?.authenticationFailed(this, response);
                }
            } catch (err) {
                lastError = err;
            }
            if (attempts < retryAttempts) {
                ++attempts;
                await new Promise(resolve => setTimeout(resolve, retryDelay)); // Delay before retrying
                retry(attempts);
                return sendRequest(); // Retry the request
            }
            error(lastError);
            return Promise.reject(lastError);
        }
        return sendRequest();
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