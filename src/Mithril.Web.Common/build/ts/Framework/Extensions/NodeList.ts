declare interface NodeList {
    map<TResult>(callback: (x: Node) => TResult): TResult[];
    filter(callback: (x: Node) => boolean): Node[];
}

/**
 * Basically runs through a node list, runs a function on it and returns the result
 *
 * @template TResult
 * @param {(x: Node) => TResult} callback
 * @returns {TResult[]}
 */
NodeList.prototype.map = function <TResult>(callback: (x: Node) => TResult): TResult[] {
    let ReturnValues: TResult[] = [];
    for (let x = 0; x < this.length; ++x) {
        ReturnValues = ReturnValues.concat(callback(this[x]));
    }
    return ReturnValues;
};

/**
 * Filters a node list based on the function specified
 *
 * @param {(x: Node) => boolean} callback
 * @returns {Node[]}
 */
NodeList.prototype.filter = function (callback: (x: Node) => boolean): Node[] {
    let ReturnValues: Node[] = [];
    for (let x = 0; x < this.length; ++x) {
        if (callback(this[x])) {
            ReturnValues = ReturnValues.concat(this[x]);
        }
    }
    return ReturnValues;
};