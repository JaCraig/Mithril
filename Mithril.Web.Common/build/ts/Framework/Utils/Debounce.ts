// Simple debounce method
export default function debounce(func: Function, delay: number = 300): Function {
    let timeoutId: ReturnType<typeof setTimeout>;

    return function (...args: any[]) {
        clearTimeout(timeoutId);
        timeoutId = setTimeout(() => {
            func.apply(this, args);
        }, delay);
    };
}