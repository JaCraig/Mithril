// Wrapper for history object
export class PageHistory {
    // Goes back by the value specified
    public back(delta?: number): void {
        if (delta === undefined) {
            delta = 1;
        }
        window.history.go(-1 * delta);
    }

    // Goes forward by the value specified
    public forward(delta?: number): void {
        if (delta === undefined) {
            delta = 1;
        }
        window.history.go(delta);
    }

    // pushes a new url/state onto the history
    public push(state: any, url?: string, title?: string): void {
        window.history.pushState(state, title, url);
    }

    // replaces the current url with a new url/state
    public replace(state: any, url?: string, title?: string): void {
        window.history.replaceState(state, title, url);
    }

    // gets the current state
    get state(): any {
        return window.history.state;
    }

    // Returns the number of items in storage
    get length(): number {
        return window.history.length;
    }
}