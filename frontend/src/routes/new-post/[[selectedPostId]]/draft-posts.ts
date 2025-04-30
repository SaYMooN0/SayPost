export type DraftPostMainInfo = {
    id: string;
    title: string;
    isPinned: boolean;
    lastModifiedAt: Date;
}
export type DraftPostFullInfo = {
    id: string;
    title: string;
    isPinned: boolean;
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