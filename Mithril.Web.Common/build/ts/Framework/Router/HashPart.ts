import { StringDictionary } from '../Types/StringDictionary'

// An individual hash part
export class HashPart {
    // Constructor
    constructor(part: string, defaultValues: StringDictionary<any>) {
        part = part.replace(/[#!]/g, "");
        this.variable = part.charAt(0) === "{" && part.charAt(part.length - 1) === "}";
        part = part.replace(/[{}]/g, "");
        this.optional = part.charAt(0) === "^";
        this.part = part.replace(/[\^]/g, "");
        if (defaultValues[this.part] !== undefined) {
            this.defaultValue = defaultValues[this.part];
        } else {
            this.defaultValue = "";
        }
    }

    // The actual part of the path to match on
    private part: string;

    // Is this a variable?
    private variable: boolean;

    // Is this optional?
    private optional: boolean;

    // The default value for this part
    private defaultValue: any;

    // Determines if this is a match.
    public isMatch(part: string): boolean {
        if (part === undefined || part === null) {
            return this.optional;
        }
        part = part.replace(/[#!]/g, "");
        if (this.variable) {
            return this.optional || part !== "";
        }
        return this.optional || part.toUpperCase() === this.part.toUpperCase();
    }

    // Gets the value, if it is a variable. If it is not a variable, it returns undefined and if it is a variable but no
    // value is present it returns null.
    private getValue(part: string): string {
        if (!this.variable) {
            return part || this.defaultValue;
        }
        if (part !== undefined) {
            return part || this.defaultValue;
        } else {
            return this.defaultValue;
        }
    }

    // Sets the value used for the path part
    public setValue(part: string, parameters: StringDictionary<any>): void {
        part = part ? part : "";
        part = part.replace(/[#!]/g, "");
        parameters[this.part] = this.getValue(part);
    }
}