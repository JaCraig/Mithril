import Vue from 'vue';

let clickOutside: any = {
    bind: function (el: any) {
        clickOutside.onEventBound = clickOutside.onEvent.bind({ el });
        document.addEventListener("click", clickOutside.onEventBound);
    },
    cb: function (event: any) {
        return;
    },
    onEvent: function (event: { target: any; }) {
        if (event.target === this.el || this.el.contains(event.target) || clickOutside.cb) {
            clickOutside.cb(event);
        }
    },
    onEventBound: function () {
        return;
    },
    unbind: function () {
        document.removeEventListener("click", clickOutside.onEventBound);
    },
    update: function (el: any, binding: { value: (event: any) => void; }) {
        if (typeof binding.value !== "function") {
            throw new Error("Argument must be a function");
        }
        clickOutside.cb = binding.value;
    },
};

export function RegisterDirectives(app: Vue.App<Element>): Vue.App<Element> {
    app.directive("click-outside", clickOutside);
    return app;
}