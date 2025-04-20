<script lang="ts">
    import type {
        DraftPostMainInfo,
        DraftPostsSortOption,
    } from "../draftPosts";

    let { posts }: { posts: DraftPostMainInfo[] } = $props<{
        posts: DraftPostMainInfo[];
    }>();
</script>

<div class="posts-list">
    {#each posts as post}
        <a
            class="post-link"
            href="/new-post/{post.id}"
            data-sveltekit-preload-data="hover"
            data-sveltekit-noscroll
        >
            <label class="post-title">
                {post.title}
            </label>
            <label class="last-modified">{post.lastModified}</label>
        </a>
    {/each}
</div>

<style>
    .posts-list {
        width: 100%;
        max-height: calc(98vh - var(--layout-header-height) - 8.5rem);
        overflow-y: auto;
        box-sizing: border-box;
    }
   
    .post-link {
        display: grid;
        padding: 0.5rem;
        margin: 0.125rem;
        border: 0.125rem solid var(--back-second);
        box-sizing: border-box;
    }

    .post-link:hover {
        background-color: var(--back-second);
    }

    .post-title {
        margin-bottom: 0.25rem;
        font-size: 1rem;
        font-weight: 400;
    }

    .last-modified {
        font-size: 0.75rem;
        font-weight: 400;
    }
</style>
