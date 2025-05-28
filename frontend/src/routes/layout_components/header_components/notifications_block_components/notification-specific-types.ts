export type NotificationData =
    | CommentLeftNotification
    | PostLikedNotification
    | PostPublishedNotification
    | UserGotFollowerNotification;

type CommentLeftNotification = {
    type: "CommentLeft";
    postTitle: string;
    commentAuthorId: string;
};
type PostLikedNotification = {
    type: "PostLiked";
    postId: string;
    userThatLikedId: string;
};
type PostPublishedNotification = {
    type: "PostPublished";
    postId: string;
    postAuthorId: string;
};
type UserGotFollowerNotification = {
    type: "UserGotFollower";
    followerId: string;
};