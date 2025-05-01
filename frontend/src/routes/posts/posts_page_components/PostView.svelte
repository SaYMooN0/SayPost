<script lang="ts">
    import type { PostPreview } from "../published-posts";

    let { post }: { post: PostPreview } = $props<{ post: PostPreview }>();
</script>

<div class="post-view">
    <h1>{post.title}</h1>
    <label>{post.publicationDate}</label>
    {#if post.tags.length === 0}
        <div class="no-tags">No tags</div>
    {:else}
        <div class="tags">
            {#each post.tags as tag}
                <div class="tag">#{tag}</div>
            {/each}
        </div>
    {/if}
    <p class="comments-count">
        {post.commentsCount === 0
            ? "No comments"
            : `${post.commentsCount} comments`}
    </p>
</div>

<style>
    .post-view {
        margin-top: 1rem;
        border: 0.125rem solid var(--accent-main);
        border-radius: 0.5rem;
        padding: 0.5rem;
        box-sizing: border-box;
    }
</style>
