let clickOutside: any = {
    mounted: function (el: any, binding: any) {
        clickOutside.onEventBound = clickOutside.onEvent.bind({ el });
        document.addEventListener("click", clickOutside.onEventBound);
        if (typeof binding.value !== "function") {
            throw new Error("Argument must be a function");
        }
        clickOutside.cb = binding.value;
    },
    cb: function (event: any) {
        return;
    },
    onEvent: function (event: { target: any; }) {
        if (event.target === this.el || this.el.contains(event.target) || !clickOutside.cb) {
            return;
        }
        clickOutside.cb(event);
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

export default clickOutside;