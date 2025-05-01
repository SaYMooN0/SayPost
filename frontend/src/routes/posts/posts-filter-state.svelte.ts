export class PostsFilterState {
    dateFrom: string = $state(""); //"YYYY-MM-DD"
    dateTo: string = $state("");   //"YYYY-MM-DD"
    includeTags: string[] = $state([]);
    excludeTags: string[] = $state([]);

    constructor() {}

    public reset() {
        this.dateFrom = "";
        this.dateTo = "";
        this.includeTags = [];
        this.excludeTags = [];
    }

    public applyQueryParams(params: URLSearchParams) {
        const tagsParam = params.get("includeTags");
        this.includeTags = tagsParam ? tagsParam.split(",") : [];

        const excludeTagsParam = params.get("excludeTags");
        this.excludeTags = excludeTagsParam ? excludeTagsParam.split(",") : [];

        const from = params.get("dateFrom");
        if (from) {
            const date = new Date(parseInt(from));
            this.dateFrom = date.toISOString().slice(0, 10); // "YYYY-MM-DD"
        }

        const to = params.get("dateTo");
        if (to) {
            const date = new Date(parseInt(to));
            this.dateTo = date.toISOString().slice(0, 10);
        }
    }

    public getQuery(): string {
        const params = new URLSearchParams();

        if (this.includeTags.length > 0) {
            params.set("includeTags", this.includeTags.join(","));
        }

        if (this.excludeTags.length > 0) {
            params.set("excludeTags", this.excludeTags.join(","));
        }

        if (this.dateFrom) {
            params.set("dateFrom", new Date(this.dateFrom).getTime().toString());
        }

        if (this.dateTo) {
            params.set("dateTo",  new Date(this.dateTo).getTime().toString());
        }

        return params.toString();
    }
}
