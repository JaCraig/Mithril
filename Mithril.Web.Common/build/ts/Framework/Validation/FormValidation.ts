import "../Extensions/HTMLElement";
import "../Extensions/NodeList";

import { StringDictionary } from "../Types/StringDictionary";

// Class to help with form validation
export class FormValidation {
    constructor() {
        this.errors = [];
        this.messageAttributes = {};
        this.messageAttributes["patternMismatch"] = "data-error-message-pattern-mismatch";
        this.messageAttributes["rangeOverflow"] = "data-error-message-range-overflow";
        this.messageAttributes["rangeUnderflow"] = "data-error-message-range-underflow";
        this.messageAttributes["stepMismatch"] = "data-error-message-step-mismatch";
        this.messageAttributes["tooLong"] = "data-error-message-too-long";
        this.messageAttributes["tooShort"] = "data-error-message-too-short";
        this.messageAttributes["badInput"] = "data-error-message-bad-input";
        this.messageAttributes["typeMismatch"] = "data-error-message-type-mismatch";
        this.messageAttributes["valueMissing"] = "data-error-message-value-missing";
    }

    // The errors found when validating
    public errors: string[];

    // message attribute dictionary
    private messageAttributes: StringDictionary<string>;

    // initializes the validation system for adding custom error messages
    public initialize(): void {
        let inputElements = this.map(document.getElementsByTagName("input"), x => <HTMLInputElement>x).filter(x => x.willValidate);
        for (let x = 0; x < inputElements.length; ++x) {
            if (inputElements[x].type.toUpperCase() === "RADIO"
                || inputElements[x].type.toUpperCase() === "CHECKBOX") {
                inputElements[x].addEventListener("change", y => this.inputHandler(<HTMLInputElement>y.target));
            } else {
                inputElements[x].addEventListener("input", y => this.inputHandler(<HTMLInputElement>y.target));
            }
            inputElements[x].addEventListener("invalid", y => this.invalidInputHandler(<HTMLInputElement>y.target));
        }
        let textAreaElements = this.map(document.getElementsByTagName("textarea"), x => <HTMLTextAreaElement>x).filter(x => x.willValidate);
        for (let x = 0; x < textAreaElements.length; ++x) {
            textAreaElements[x].addEventListener("change", y => this.textAreaHandler(<HTMLTextAreaElement>y.target));
            textAreaElements[x].addEventListener("invalid", y => this.invalidTextAreaHandler(<HTMLTextAreaElement>y.target));
        }
        let selectElements = this.map(document.getElementsByTagName("select"), x => <HTMLSelectElement>x).filter(x => x.willValidate);
        for (let x = 0; x < selectElements.length; ++x) {
            selectElements[x].addEventListener("change", y => this.selectHandler(<HTMLSelectElement>y.target));
            selectElements[x].addEventListener("invalid", y => this.invalidSelectHandler(<HTMLSelectElement>y.target));
        }
    }

    private map<TResult>(elements: HTMLCollectionOf<Element>, callback: (x: Node) => TResult): TResult[] {
        let ReturnValues: TResult[] = [];
        for (let x = 0; x < elements.length; ++x) {
            ReturnValues = ReturnValues.concat(callback(elements[x]));
        }
        return ReturnValues;
    }

    private filter(elements: HTMLCollectionOf<Element>, callback: (x: Node) => boolean): Node[] {
        let ReturnValues: Node[] = [];
        for (let x = 0; x < elements.length; ++x) {
            if (callback(elements[x])) {
                ReturnValues = ReturnValues.concat(elements[x]);
            }
        }
        return ReturnValues;
    }

    // Initialize an individual form
    public initializeForm(form: HTMLFormElement): void {
        let inputElements = this.map(form.getElementsByTagName("input"), x => <HTMLInputElement>x).filter(x => x.willValidate);
        for (let x = 0; x < inputElements.length; ++x) {
            if (inputElements[x].type.toUpperCase() === "RADIO"
                || inputElements[x].type.toUpperCase() === "CHECKBOX") {
                inputElements[x].addEventListener("change", y => this.inputHandler(<HTMLInputElement>y.target));
            } else {
                inputElements[x].addEventListener("input", y => this.inputHandler(<HTMLInputElement>y.target));
            }
            inputElements[x].addEventListener("invalid", y => this.invalidInputHandler(<HTMLInputElement>y.target));
        }
        let textAreaElements = this.map(form.getElementsByTagName("textarea"), x => <HTMLTextAreaElement>x).filter(x => x.willValidate);
        for (let x = 0; x < textAreaElements.length; ++x) {
            textAreaElements[x].addEventListener("change", y => this.textAreaHandler(<HTMLTextAreaElement>y.target));
            textAreaElements[x].addEventListener("invalid", y => this.invalidTextAreaHandler(<HTMLTextAreaElement>y.target));
        }
        let selectElements = this.map(form.getElementsByTagName("select"), x => <HTMLSelectElement>x).filter(x => x.willValidate);
        for (let x = 0; x < selectElements.length; ++x) {
            selectElements[x].addEventListener("change", y => this.selectHandler(<HTMLSelectElement>y.target));
            selectElements[x].addEventListener("invalid", y => this.invalidSelectHandler(<HTMLSelectElement>y.target));
        }
    }

    // invalid input handler
    private invalidInputHandler(input: HTMLInputElement): void {
        if (!input.validity.valid) {
            let errorMessages = this.getErrorMessages(input, input.validity);
            if (errorMessages.length > 0) {
                input.setCustomValidity(errorMessages.join("\n"));
            }
        }
    }

    // invalid text area handler
    private invalidTextAreaHandler(textarea: HTMLTextAreaElement): void {
        if (!textarea.validity.valid) {
            let errorMessages = this.getErrorMessages(textarea, textarea.validity);
            if (errorMessages.length > 0) {
                textarea.setCustomValidity(errorMessages.join("\n"));
            }
        }
    }

    // invalid select handler
    private invalidSelectHandler(select: HTMLSelectElement): void {
        if (!select.validity.valid) {
            let errorMessages = this.getErrorMessages(select, select.validity);
            if (errorMessages.length > 0) {
                select.setCustomValidity(errorMessages.join("\n"));
            }
        }
    }

    // Input handler
    private inputHandler(input: HTMLInputElement): void {
        if (input.type.toUpperCase() === "RADIO") {
            let elements = document.getElementsByName(input.name);
            for (let x = 0; x < elements.length; ++x) {
                let element = <HTMLInputElement>elements[x];
                element.setCustomValidity("");
            }
        } else {
            input.setCustomValidity("");
        }
        input.checkValidity();
    }

    // text area handler
    private textAreaHandler(textarea: HTMLTextAreaElement): void {
        textarea.setCustomValidity("");
        textarea.checkValidity();
    }

    // select handler
    private selectHandler(select: HTMLSelectElement): void {
        select.setCustomValidity("");
        select.checkValidity();
    }

    // Validates all elements, returning false if there are errors, true otherwise.
    public validate(): boolean {
        let result = true;
        this.errors = [];
        let inputElements = this.filter(document.getElementsByTagName("input"),
            x => !this.validateInput((<HTMLInputElement>x)));
        result = result && inputElements.length === 0;
        let selectElements = this.filter(document.getElementsByTagName("select"),
            x => !this.validateSelect((<HTMLSelectElement>x)));
        result = result && selectElements.length === 0;
        let textareaElements = this.filter(document.getElementsByTagName("textarea"),
            x => !this.validateTextArea((<HTMLTextAreaElement>x)));
        result = result && textareaElements.length === 0;
        return result;
    }

    // Validates all elements in the form, returning the list of error messages.
    public validateForm(form: HTMLFormElement): String[] {
        let result: String[] = [];
        this.errors = [];
        let inputElements = this.filter(form.getElementsByTagName("input"),
            x => !this.validateInput((<HTMLInputElement>x)))
            .map(x => this.getErrorMessages(<HTMLInputElement>x, (<HTMLInputElement>x).validity))
            .filter(x => x.length !== 0);
        for (let x = 0; x < inputElements.length; ++x) {
            result = result.concat(inputElements[x]);
        }
        let selectElements = this.filter(form.getElementsByTagName("select"),
            x => !this.validateSelect((<HTMLSelectElement>x)))
            .map(x => this.getErrorMessages(<HTMLSelectElement>x, (<HTMLSelectElement>x).validity))
            .filter(x => x.length !== 0);
        for (let x = 0; x < selectElements.length; ++x) {
            result = result.concat(selectElements[x]);
        }
        let textareaElements = this.filter(form.getElementsByTagName("textarea"),
            x => !this.validateTextArea((<HTMLTextAreaElement>x)))
            .map(x => this.getErrorMessages(<HTMLTextAreaElement>x, (<HTMLTextAreaElement>x).validity))
            .filter(x => x.length !== 0);
        for (let x = 0; x < textareaElements.length; ++x) {
            result = result.concat(textareaElements[x]);
        }
        return result;
    }

    // Validates an individual element, returning the list of error messages.
    public validateElement(element: HTMLElement): String[] {
        let result: String[] = [];
        if (element.tagName === "INPUT" && !this.validateInput((<HTMLInputElement>element))) {
            let tempResults = this.getErrorMessages((<HTMLInputElement>element), (<HTMLInputElement>element).validity)
                .filter(x => x.length !== 0);
            for (let x = 0; x < tempResults.length; ++x) {
                result = result.concat(tempResults[x]);
            }
        } else if (element.tagName === "SELECT" && !this.validateSelect((<HTMLSelectElement>element))) {
            let tempResults = this.getErrorMessages((<HTMLSelectElement>element), (<HTMLSelectElement>element).validity)
                .filter(x => x.length !== 0);
            for (let x = 0; x < tempResults.length; ++x) {
                result = result.concat(tempResults[x]);
            }
        } else if (element.tagName === "TEXTAREA" && !this.validateTextArea((<HTMLTextAreaElement>element))) {
            let tempResults = this.getErrorMessages((<HTMLTextAreaElement>element), (<HTMLTextAreaElement>element).validity)
                .filter(x => x.length !== 0);
            for (let x = 0; x < tempResults.length; ++x) {
                result = result.concat(tempResults[x]);
            }
        }
        return result;
    }

    // validates a select object
    private validateSelect(select: HTMLSelectElement): boolean {
        if (!select.checkValidity()) {
            let tempValue = this.getErrorMessages(select, select.validity);
            if (tempValue.length !== 0) {
                this.errors = this.errors.concat(tempValue);
            }
            return false;
        }
        return true;
    }

    // validates a text area object
    private validateTextArea(textarea: HTMLTextAreaElement): boolean {
        if (!textarea.checkValidity()) {
            let tempValue = this.getErrorMessages(textarea, textarea.validity);
            if (tempValue.length !== 0) {
                this.errors = this.errors.concat(tempValue);
            }
            return false;
        }
        return true;
    }

    // gets the error messages based on the validation state and element's defined error messages
    private getErrorMessages(element: HTMLElement, validity: ValidityState): string[] {
        let tempValue: string[] = [];
        for (let key in this.messageAttributes) {
            if ((<any>validity)[key]) {
                let message = element.attribute(this.messageAttributes[key]);
                if (message !== null) {
                    tempValue = tempValue.concat(message);
                }
            }
        }
        if (tempValue.length === 0) {
            let generalMessage = element.attribute("data-error-message");
            if (generalMessage !== null) {
                tempValue = tempValue.concat(generalMessage);
            } else {
                tempValue = tempValue.concat((<HTMLInputElement>element).validationMessage);
            }
        }
        return tempValue;
    }

    // validates an input object
    private validateInput(input: HTMLInputElement): boolean {
        if (!input.checkValidity()) {
            let tempValue = this.getErrorMessages(input, input.validity);
            if (tempValue.length !== 0) {
                this.errors = this.errors.concat(tempValue);
            }
            return false;
        }
        return true;
    }
}