import Vue from 'vue';
import moment from 'moment';

export function RegisterFilters(app: Vue.App<Element>): Vue.App<Element> {
    if (app == null) {
        return app;
    }
    app.config.globalProperties.$filters = {
        moment: function (date: Date, format: string, parsingFormat?: string) {
            if (date == null) {
                return "N/A";
            }
            parsingFormat ??= "YYYY-MM-DDThh:mm:ss";
            format ??= "M-D-YYYY h:mm A";
            return moment(date, parsingFormat).format(format);
        },
        capitalize: function (str: string) {
            if (str == null) {
                return "";
            }
            return str.charAt(0).toUpperCase() + str.slice(1);
        },
        maxsize: function (value: string, size: number, substitutionString?: string) {
            if (value == null) {
                return "";
            }
            if (value.length <= size) {
                return value;
            }
            substitutionString ??= "...";
            return value.substring(0, size) + substitutionString;
        },
        currency: function (value: number, locales?: string, format?: Intl.NumberFormatOptions) {
            if (value == null) {
                return "";
            }
            locales ??= "en-US";
            format ??= { style: 'currency', currency: 'USD' };
            return Intl.NumberFormat(locales, format).format(value);
        }
    };
    return app;
}