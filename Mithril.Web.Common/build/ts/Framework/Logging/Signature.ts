export class Signature {
    //Constructor
    constructor() {
        this.params = [];
    }

    //Function name
    public name: string;

    //Function parameters
    public params: any[];

    //Converts the class to a string
    public toString(): string {
        let params = this.params.length > 0
            ? "'" + this.params.join("', '") + "'"
            : "";
        return this.name + "(" + params + ")";
    }
}