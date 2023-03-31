import { IHotkeys } from '../Hotkey/Interfaces/IHotkeys'

// Hotkey configuration interface
export interface IHotkeyConfiguration {
    // called when configuring the hotekeys
    configureHotkeys(hotkeys: IHotkeys): void;
}