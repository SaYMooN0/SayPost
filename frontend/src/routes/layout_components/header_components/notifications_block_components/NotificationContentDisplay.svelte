<script lang="ts">
    import type { NotificationData } from "./notification-specific-types";

    let { notification }: { notification: NotificationData } = $props<{
        notification: any;
    }>();
</script>

<p>
    {#if notification.type === "CommentLeft"}
        <a class="userlink" href="/users/{notification.commentAuthorId}">
            User
        </a>
        commented your post
        <span class="post-title">{notification.postTitle}</span>
    {:else if notification.type === "PostLiked"}
        <a class="userlink" href="/users/{notification.userThatLikedId}">
            User
        </a>
        liked your post {notification.postId}
    {:else if notification.type === "PostPublished"}
        <a class="userlink" href="/users/{notification.postAuthorId}">User</a>
        published new post {notification.postId}
    {:else if notification.type === "UserGotFollower"}
        <a class="userlink" href="/users/{notification.followerId}">User</a>
        followed you
    {:else}
        Notification of unsupported type
    {/if}
</p>

<style>
    p {
        margin: 0;
    }
    .userlink {
        color: var(--accent-main);
        font-weight: 600;
        text-decoration: underline;
        text-decoration-thickness: 2px;

        word-break: normal;
        overflow-wrap: anywhere;
    }
</style>
