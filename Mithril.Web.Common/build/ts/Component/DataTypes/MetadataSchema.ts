import KeyValuePair from "./KeyValuePair";

// Basic schema describing metadata
export default class Metadata {
    // Input type
    public inputType: string | undefined;

    // Is this required?
    public required: boolean | undefined;

    // Max length
    public maxlength: number | undefined;

    // Error message when past max length
    public errorMessageTooLong: string | undefined;

    // Min length
    public minlength: number | undefined;

    // Error message when too short
    public errorMessageTooShort: string | undefined;

    // Error message when value is missing
    public errorMessageValueMissing: string | undefined;

    // Does this accept multiple entries
    public multiple: boolean | undefined;

    // What is accepted?
    public accept: string | undefined;

    // Number of rows
    public rows: number | undefined;

    // Options for the property
    public options: Array<KeyValuePair> | undefined;

    // Is this using UTC?
    public isUTC: boolean | undefined;

    // Subtitle
    public subtitle: string | undefined;

    // Step
    public step: number | undefined;

    // Is this read only?
    public readonly: boolean | undefined;

    // Query type
    public queryType: string | undefined;

    // Query filter
    public queryFilter: string | undefined;

    // Placeholder
    public placeholder: string | undefined;

    // Does this have a data pattern?
    public pattern: string | undefined;

    // Min value allowed
    public min: number | undefined;

    // Max value allowed
    public max: number | undefined;

    // Hint value
    public hint: string | undefined;

    // Can this be listed?
    public canList: boolean | undefined;
}