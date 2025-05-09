import type { PostContent } from "../../../ts/common/post-content-item";

export type PublishedPost = {
    id: string;
    title: string;
    authorId: string;
    content: PostContent;
    publicationDate: Date;
    tags: string[];
    comments: PostComment[];
}
export type PostComment = {
    id: string;
    content: string;
    authorId: string;
    createdAt: Date;
}