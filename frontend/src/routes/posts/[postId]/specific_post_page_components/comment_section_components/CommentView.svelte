<script lang="ts">
    import type { PostComment } from "../../published-posts";

    let { comment }: { comment: PostComment } = $props<{
        comment: PostComment;
    }>();
</script>

<div class="comment-view">
    <div class="comment-content">{comment.content}</div>
</div>

<style>
    .comment-view {
        padding: 0.5rem;
        margin-top: 1rem;
        border: 0.125rem solid var(--back-second);
    }
</style>
