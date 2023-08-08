import { Globals } from './Globals'

// Individual key press
export class Keypress {
    // Constructor
    constructor(keys: string) {
        this.keys = this.getKeys(keys);
    }

    // actual key codes
    private keys: number[];

    // gets the key codes for the string passed in
    private getKeys(keyCode: string): number[] {
        return keyCode.toUpperCase().split(/-(?!$)/).map(x => Globals.keyMappings[x] || x.charCodeAt(0));
    }

    // determines if the key code is pressed
    public isPressed(keyCode: number[]): boolean {
        let keysPressed = keyCode;
        if (keysPressed.length !== this.keys.length) {
            return false;
        }
        for (let x = 0; x < keysPressed.length; ++x) {
            if (this.keys.indexOf(keysPressed[x]) === -1) {
                return false;
            }
        }
        return true;
    }
}