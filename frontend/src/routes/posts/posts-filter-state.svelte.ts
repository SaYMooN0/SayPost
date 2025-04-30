export class PostsFilterState {
    dateFrom: Date | null = $state(null);
    dateTo: Date | null = $state(null);
    includeTags: string[] = $state([]);
    excludeTags: string[] = $state([]);
    constructor() { }
    public applyQueryParams(params: URLSearchParams) {
        const tagsParam = params.get('tags');
        this.includeTags = tagsParam ? tagsParam.split(',') : [];

        const from = params.get('dateFrom');
        this.dateFrom = from ? new Date(from) : null;

        const to = params.get('dateTo');
        this.dateTo = to ? new Date(to) : null;
    }
    getQuery() {
        const params = new URLSearchParams();
        if (this.includeTags.length > 0)
            params.set('tags', this.includeTags.join(','));

        if (this.dateFrom)
            params.set('dateFrom', this.dateFrom.toISOString());

        if (this.dateTo)
            params.set('dateTo', this.dateTo.toISOString());

        return params.toString();
    }
}