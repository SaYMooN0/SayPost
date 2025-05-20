import type { PostContent } from "../../../ts/common/post-content-item";

export type PublishedPost = {
    id: string;
    title: string;
    content: PostContent;
    publicationDate: Date;
    tags: string[];
    authorId: string;
    likesCount: number;
    isLikedByActor: boolean | null;
}
export type PostComment = {
    id: string;
    content: string;
    authorId: string;
    createdAt: Date;
}
export enum CommentsSortOption {
    Newest = "newest",
    Oldest = "oldest"
}