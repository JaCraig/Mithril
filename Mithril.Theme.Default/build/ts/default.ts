class ThemeStartup {
    constructor() {
        console.log("A");
    }
}

export default (() => {
    let DefaultTheme = new ThemeStartup();
})();