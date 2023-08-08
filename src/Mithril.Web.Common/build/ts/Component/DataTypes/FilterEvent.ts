// Event data when filtering occurs on lists
export default class FilterEvent {
    // Constructor
    constructor(filter: string, pageSize: number, page: number, sortField: string, sortAscending: boolean) {
        this.filter = filter;
        this.pageSize = pageSize;
        this.page = page;
        this.sortField = sortField;
        this.sortAscending = sortAscending;
    }

    // Filter string
    public filter: string;

    // Number of items to display
    public pageSize: number;

    // Page number (starts at 0)
    public page: number;

    // Sort field
    public sortField: string;

    // Should this sort ascending?
    public sortAscending: boolean;
}