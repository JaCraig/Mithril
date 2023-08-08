import { StringDictionary } from '../../Types/StringDictionary'

//An individual route
export class RouteData {
    //Constructor
    constructor(public url: string,
        public action: (parameters: StringDictionary<any>) => void,
        public defaultValues?: StringDictionary<any>) {
    }
}