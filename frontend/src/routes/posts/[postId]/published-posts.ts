export type PublishedPost = {
    id: string;
    title: string;
    authorId: string;
    content: string;
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