// Validation rule interface
export interface IValidationRule {
    // The name of the validation rule (required)
    name: string;

    // The validation method. This method should return a promise that resolves to true if the value is valid, or false if it is not.
    // value: The value to validate
    // params: Any additional parameters to pass to the validation method
    // Returns a promise that resolves to true if the value is valid, or false if it is not.
    validate: (value: any, ...params: any[]) => Promise<IValidationResult>;
}

// Validation result interface
export interface IValidationResult {
    // True if the validation was successful, otherwise false
    isValid: boolean;
    // The error message to display if the validation fails
    errorMessage: string;
}

// Input element validation rule
export class InputElementValidationRule implements IValidationRule {
    // The name of the validation rule (required)
    name: string = "inputElement";

    // The validation method. This method should return a promise that resolves to true if the value is valid, or false if it is not.
    // value: The value to validate
    // params: Any additional parameters to pass to the validation method
    // Returns a promise that resolves to true if the value is valid, or false if it is not.
    validate(value: any, ...params: any[]): Promise<IValidationResult> {
        let inputElement = value as HTMLInputElement;
        if (inputElement == null) {
            return new Promise<IValidationResult>((resolve, reject) => { resolve({ isValid: true, errorMessage: "" }) });
        }
        return new Promise<IValidationResult>((resolve, reject) => {
            if (inputElement.checkValidity()) {
                resolve({ isValid: true, errorMessage: inputElement.validationMessage });
            }
            else {
                resolve({ isValid: false, errorMessage: inputElement.validationMessage });
            }
        });
    }
}

// Textarea element validation rule
export class SelectElementValidationRule implements IValidationRule {
    // The name of the validation rule (required)
    name: string = "selectElement";

    // The error message to display if the validation fails
    errorMessage: string;

    // The validation method. This method should return a promise that resolves to true if the value is valid, or false if it is not.
    // value: The value to validate
    // params: Any additional parameters to pass to the validation method
    // Returns a promise that resolves to true if the value is valid, or false if it is not.
    validate(value: any, ...params: any[]): Promise<IValidationResult> {
        let inputElement = value as HTMLSelectElement;
        if (inputElement == null) {
            return new Promise<IValidationResult>((resolve, reject) => { resolve({ isValid: true, errorMessage: "" }) });
        }
        return new Promise<IValidationResult>((resolve, reject) => {
            if (inputElement.checkValidity()) {
                resolve({ isValid: true, errorMessage: inputElement.validationMessage });
            }
            else {
                resolve({ isValid: false, errorMessage: inputElement.validationMessage });
            }
        });
    }
}

// Textarea element validation rule
export class TextAreaElementValidationRule implements IValidationRule {
    // The name of the validation rule (required)
    name: string = "textAreaElement";

    // The error message to display if the validation fails
    errorMessage: string;

    // The validation method. This method should return a promise that resolves to true if the value is valid, or false if it is not.
    // value: The value to validate
    // params: Any additional parameters to pass to the validation method
    // Returns a promise that resolves to true if the value is valid, or false if it is not.
    validate(value: any, ...params: any[]): Promise<IValidationResult> {
        let inputElement = value as HTMLTextAreaElement;
        if (inputElement == null) {
            return new Promise<IValidationResult>((resolve, reject) => { resolve({ isValid: true, errorMessage: "" }) });
        }
        return new Promise<IValidationResult>((resolve, reject) => {
            if (inputElement.checkValidity()) {
                resolve({ isValid: true, errorMessage: inputElement.validationMessage });
            }
            else {
                resolve({ isValid: false, errorMessage: inputElement.validationMessage });
            }
        });
    }
}

// Required validation rule
export class RequiredValidationRule implements IValidationRule {
    // The name of the validation rule (required)
    name: string = "required";

    // The validation method. This method should return a promise that resolves to true if the value is valid, or false if it is not.
    // value: The value to validate
    // params: Any additional parameters to pass to the validation method
    // Returns a promise that resolves to true if the value is valid, or false if it is not.
    validate(value: any, ...params: any[]): Promise<IValidationResult> {
        if (value == null) {
            return new Promise<IValidationResult>((resolve, reject) => { resolve({ isValid: true, errorMessage: "" }) });
        }
        return new Promise<IValidationResult>((resolve, reject) => {
            resolve({ isValid: value != null && value != undefined && value != "", errorMessage: "This field is required" });
        });
    }
}

// Validation rule group interface
export interface IValidationRuleGroup {
    // The rules in the group
    // The key is the name of the property to validate
    // The value is an array of validation rules
    [property: string]: IValidationRule[];
}

// Validation class
export class Validation {
    // Constructor - private to prevent instantiation
    private constructor() { }

    // Initializes the validation rules
    private static initialize(): void {
        this.rules ??= window.ValidationConfiguration || {};
        window.ValidationConfiguration = this.rules;
    }

    // The list of validation rules
    private static rules: Record<string, IValidationRuleGroup>;

    // Adds a validation rule group to the list of validation rules
    // name: The name of the validation rule group
    // group: The validation rule group to add
    public static addRuleGroup(name: string, group: IValidationRuleGroup): void {
        this.initialize();
        this.rules[name] = group;
    }

    // Validates an object against a validation rule group
    // obj: The object to validate
    // ruleGroup: The name of the validation rule group to validate against
    // errors: An optional object to populate with errors if the object is invalid (the key is the property name, the value is an array of error messages) (optional)
    // Returns a promise that resolves to true if the object is valid, or false if it is not
    public static async validate(obj: any, ruleGroup: string, errors: Record<string, string[]> = {}): Promise<boolean> {
        this.initialize();
        let isValid = true;
        let rules = this.rules[ruleGroup];
        if (!rules) {
            return isValid;
        }
        for (let prop in rules) {
            if (!rules.hasOwnProperty(prop)) {
                continue;
            }
            let value = obj[prop];
            let propRules = rules[prop];
            for (let i = 0; i < propRules.length; i++) {
                let rule = propRules[i];
                let result = await rule.validate(value);
                if (!result.isValid) {
                    isValid = false;
                    errors.hasOwnProperty(prop) ? errors[prop].push(result.errorMessage) : errors[prop] = [result.errorMessage];
                }
            }
        }
        return isValid;
    }

    // Validates a form
    // form: The form to validate
    // errors: An optional object to populate with errors if the form is invalid (the key is the property name, the value is an array of error messages) (optional)
    // ruleGroup: The name of the validation rule group to validate against (optional)
    // Returns a promise that resolves to true if the form is valid, or false if it is not
    public static async validateForm(form: HTMLFormElement, ruleGroup: string = null, errors: Record<string, string[]> = {}): Promise<boolean> {
        this.initialize();
        ruleGroup ??= form.getAttribute("data-validation-rule-group");
        if (!ruleGroup) {
            ruleGroup = this.deriveObjectType(form);
        }
        let rules = this.rules[ruleGroup];
        if (!rules) {
            rules = this.extractRulesFromForm(form);
            this.addRuleGroup(ruleGroup, rules);
        }
        const formData = this.extractFormData(form);
        return this.validate(formData, ruleGroup, errors);
    }

    // Extracts the form data from a form and returns it as an array
    // form: The form to extract the data from
    // Returns an array of form data
    public static extractFormData(form: HTMLFormElement): Record<string, HTMLElement> {
        let result: Record<string, HTMLElement> = {};
        let inputElements = form.getElementsByTagName("input");
        let textAreaElements = form.getElementsByTagName("textarea");
        let selectElements = form.getElementsByTagName("select");
        for (let x = 0; x < inputElements.length; x++) {
            result[inputElements[x].name] = inputElements[x];
        }
        for (let x = 0; x < textAreaElements.length; x++) {
            result[textAreaElements[x].name] = textAreaElements[x];
        }
        for (let x = 0; x < selectElements.length; x++) {
            result[selectElements[x].name] = selectElements[x];
        }
        return result;
    }

    // Derives the object type from a form element (if no object type is specified)
    // form: The form to derive the object type from
    // Returns the object type
    public static deriveObjectType(form: HTMLFormElement): string {
        return form.action || form.id || form.name;
    }

    // Extracts the validation rules from a form and adds them to the list of validation rules
    // form: The form to extract the validation rules from
    // Returns the validation rule group that was extracted
    public static extractRulesFromForm(form: HTMLFormElement): IValidationRuleGroup {
        let rules: IValidationRuleGroup = {};
        let inputElements = form.getElementsByTagName("input");
        let textAreaElements = form.getElementsByTagName("textarea");
        let selectElements = form.getElementsByTagName("select");

        for (let x = 0; x < inputElements.length; ++x) {
            let inputElement = inputElements[x];
            if (!inputElement.willValidate) {
                continue;
            }
            rules[inputElement.name] ??= [];
            rules[inputElement.name].push(new InputElementValidationRule());
        }
        for (let x = 0; x < textAreaElements.length; ++x) {
            let textAreaElement = textAreaElements[x];
            if (!textAreaElement.willValidate) {
                continue;
            }
            rules[textAreaElement.name] ??= [];
            rules[textAreaElement.name].push(new TextAreaElementValidationRule());
        }
        for (let x = 0; x < selectElements.length; ++x) {
            let selectElement = selectElements[x];
            if (!selectElement.willValidate) {
                continue;
            }
            rules[selectElement.name] ??= [];
            rules[selectElement.name].push(new SelectElementValidationRule());
        }
        return rules;
    }
}