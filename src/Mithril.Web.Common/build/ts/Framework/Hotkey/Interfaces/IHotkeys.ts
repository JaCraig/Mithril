// Hotkey mapping interface
export interface IHotkeys {
    // maps a set of hotkeys
    bind(keyCodes: string, callback: (event: KeyboardEvent, handler: any) => void): IHotkeys;
}