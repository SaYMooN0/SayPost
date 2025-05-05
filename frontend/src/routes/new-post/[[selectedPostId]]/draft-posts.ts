import type { PostContent, PostContentItem } from "../../../ts/common/post-content-item";

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
    content: PostContent;
    tags: string[];
}
export enum DraftPostsSortOption {
    lastModified = "lastModified",
    lastCreated = "lastCreated",
    oldestCreated = "oldestCreated",
    title = "title"
}