import { BrowserUtils } from "../Browser/BrowserUtils";

// Acts as a way to "download" or export data to a file in the browser via JS.
export class Downloader {
    // Exports the array of entries to the file specified
    public static exportData(data: Array<any>, columns: Array<ColumnHeader>, fileName: string = BrowserUtils.Id, type: FileTypes = FileTypes.CSV): void {
        if (data.length == 0) {
            return;
        }
        fileName = fileName || BrowserUtils.Id;
        let returnValue = "";
        if (type === FileTypes.CSV) {
            returnValue = this.exportCSV(data, columns, fileName);
        } else {
            returnValue = data.join(",");
        }
        this.download(returnValue, fileName);
    }

    // Capitalizes a string's first character
    private static capitalize(str: string | String): string {
        if (!str) {
            return "";
        }
        return str.charAt(0).toUpperCase() + str.slice(1);
    }

    // Exports a CSV of the data
    private static exportCSV(data: Array<any>, columns: Array<ColumnHeader>, fileName: string): string {
        let returnValue = "";
        let columnNames = Object.keys(data[0]);
        let splitter = "";
        for (let z = 0; z < columnNames.length; ++z) {
            let columnName = columnNames[z];
            let actualName = columns.filter(function (column) {
                return column.property === columnName;
            });
            if (actualName.length === 0) {
                actualName = [{ display: columnName, property: columnName }];
            }
            let header = actualName[0]?.display || actualName[0]?.property || columnName;
            returnValue += splitter + "\"" + this.capitalize(header) + "\"";
            splitter = ",";
        }
        returnValue += "\n";
        for (let x = 0; x < data.length; ++x) {
            let row = data[x];
            let columns = Object.keys(row);
            splitter = "";
            for (let y = 0; y < columns.length; ++y) {
                returnValue += splitter + "\"" + row[columns[y]].replaceAll("\"", "'") + "\"";
                splitter = ",";
            }
            returnValue += "\n";
        }
        return returnValue;
    }

    // Downloads the data as the file specified
    public static download(data: string, fileName: string): void {
        if (data == null) {
            return;
        }
        if ((<any>navigator).msSaveBlob) { // IE10+ : (has Blob, but not a[download] or URL)
            (<any>navigator).msSaveBlob(new Blob([data]), fileName);
            return;
        }
        var element = document.createElement('a');
        element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(data));
        element.setAttribute('download', fileName);

        element.style.display = 'none';
        document.body.appendChild(element);

        element.click();

        document.body.removeChild(element);
    }
}

export class ColumnHeader {
    public display: String;
    public property: String;
}

export enum FileTypes {
    CSV
}