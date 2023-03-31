import { Signature } from "./Signature";

declare global {
    interface Function {
        trace(): any[];
        signature(): Signature;
        getName(): any;
    }
}

// declare var Function: FunctionConstructor;

//Does a stack trace of the function.
Function.prototype.trace = function () {
    var trace = [];
    var curr = this;
    while (curr) {
        trace.push(curr.signature());
        curr = curr.caller;
    }
    return trace;
};

//Gets the function's signature.
Function.prototype.signature = function () {
    var signature = new Signature();
    signature.name = this.getName();
    if (this.arguments) {
        for (var i = 0; i < this.arguments.length; i++) {
            signature.params.push(this.arguments[i]);
        }
    }
    return signature;
};

//Gets the function's name if it has one.
Function.prototype.getName = function () {
    if (this.name) {
        return this.name;
    }
    var definition = this.toString().split("\n")[0];
    var exp = /^function ([^\s(]+).+/;
    if (exp.test(definition)) {
        return definition.split("\n")[0].replace(exp, "$1") || "anonymous";
    }
    return "anonymous";
};

//Handles error logging
export class ErrorLogging {
    //constructor
    constructor() {
        this.logError = (ex, stack) => { };
    }

    //Logs the error message. Includes the message and stack trace information.
    public logError: (message: string, stack: string) => void;

    //Sets the logging function that the system uses
    public setLoggingFunction(logger: (message: string, stack: string) => void): void {
        this.logError = logger;
    }

    //called when an error is thrown.
    public onError(message: string, filename?: string, lineno?: number, colno?: number, error?: Error): void {
        this.logError(message, error?.stack || "");
    }
}