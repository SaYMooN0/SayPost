<script lang="ts">
    import { DateUtils } from "../../../ts/common/utils/date-utils";
    import type { PostPreview } from "../published-posts-previews";

    let { post }: { post: PostPreview } = $props<{ post: PostPreview }>();
</script>

<div class="post-view">
    <h1 class="title">{post.title}</h1>
    {#if post.tags.length === 0}
        <div class="no-tags">(No tags)</div>
    {:else}
        <div class="tags">
            {#each post.tags as tag}
                <a
                    class="tag"
                    href={`/posts?includeTags=${encodeURIComponent(tag)}`}
                    data-sveltekit-preload-data="tap"
                >
                    #{tag}
                </a>
            {/each}
        </div>
    {/if}
    <div class="date-comments-div">
        <label>Published on {DateUtils.toLocale(post.publicationDate)}</label>
        <p class="comments-count">
            {post.commentsCount === 0
                ? "No comments"
                : `${post.commentsCount} comments`}
        </p>
    </div>

    <a
        data-sveltekit-preload-data="hover"
        class="read-link"
        href="/posts/{post.id}"
    >
        Read
    </a>
</div>

<style>
    .post-view {
        display: flex;
        flex-direction: column;
        padding: 0.5rem;
        margin-top: 1rem;
        border: 0.125rem solid var(--accent-main);
        border-radius: 0.5rem;
        box-sizing: border-box;
    }

    .title {
        width: 100%;
        overflow-wrap: anywhere;
        text-indent: 0.5em;
        margin: 0.25rem 0;
    }

    .no-tags {
        color: var(--gray);
        font-style: italic;
        font-size: 1rem;
    }

    .tags {
        display: flex;
        flex-wrap: wrap;
        gap: 0.5rem;
        padding: 0.25rem 0;
        border-radius: 0.25rem;
    }

    .tag {
        padding: 0.25rem 0.75rem;
        border-radius: 6rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1rem;
        text-decoration: none;
        transition: all 0.04s ease-in;
        cursor: default;
    }

    .tag:hover {
        text-decoration: underline;
        transform: scale(1.02);
    }
    .date-comments-div {
        display: flex;
        justify-content: space-between;
        align-items: center;
        flex-direction: row;
    }
    .read-link {
        background-color: var(--back-second);
        width: 90%;
        margin: 0 auto;
        padding: 0.375rem 0;
        border-radius: 0.25rem;
        color: var(--accent-main);
        font-size: 1.25rem;
        text-decoration: none;
        text-align: center;
        transition:
            background-color 0.08s ease-in,
            border-radius 0.16s ease-in;
        cursor: pointer;
        font-weight: 500;
    }
    .read-link:hover {
        background-color: var(--accent-main);
        color: var(--back-main);
    }
    .read-link:active {
        border-radius: 1.5rem;
    }
</style>
