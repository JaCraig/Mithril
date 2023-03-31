import moment from "moment";

// Various sort functions
export default class SortFunc {
    // Date sort
    public static sortDate(val1: Date, val2: Date): number {
        let value1 = moment(val1);
        let value2 = moment(val2);
        if (!value1.isValid()) {
            value1 = moment(new Date(0));
        }
        if (!value2.isValid()) {
            value2 = moment(new Date(0));
        }
        if (value1.isBefore(value2)) return -1;
        if (value1.isAfter(value2)) return 1;
        return 0;
    }

    // Number sort
    public static sortNumber(val1: number, val2: number): number {
        if (isNaN(val1)) { val1 = 0; }
        if (isNaN(val2)) { val2 = 0; }
        return val1 - val2;
    }

    // String sort
    public static sortString(val1: string, val2: string): number {
        if (val1 === val2) { return 0; }
        if (val1 < val2) { return -1; }
        return 1;
    }
}