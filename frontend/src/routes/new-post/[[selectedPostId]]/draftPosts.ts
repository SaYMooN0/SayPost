export type DraftPostMainInfo = {
    id: string;
    title: string;
    lastModifiedAt: Date;
}
export type DraftPostFullInfo = {
    id: string;
    title: string;
    lastModifiedAt: Date;
    createdAt: Date;
    content: string;
    tags: string[];
}
export enum DraftPostsSortOption {
    lastModified = "lastModified",
    lastCreated = "lastCreated",
    oldestCreated = "oldestCreated",
    title = "title"
}