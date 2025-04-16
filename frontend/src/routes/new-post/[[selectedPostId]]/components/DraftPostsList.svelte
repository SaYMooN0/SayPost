<script lang="ts">
    import type { DraftPostMainInfo } from "../draftPosts";

    let { posts }: { posts: DraftPostMainInfo[] } = $props<{ posts: DraftPostMainInfo[] }>();
</script>

<div class="posts-list">
    <label class="your-draft-posts">
        Your draft posts:
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M7 4V20"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M17 19L17 4"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M10 6.99998C10 6.99998 7.79053 4.00001 6.99998 4C6.20942 3.99999 4 7 4 7"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M20 17C20 17 17.7905 20 17 20C16.2094 20 14 17 14 17"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>

        {#each posts as post}
            <a
                href="/new-post/{post.id}"
                data-sveltekit-preload-data="hover"
            >
                <label class="post-title">
                    {post.title}
                </label>
                <label class="last-moified">{post.lastModified}</label>
            </a>
        {/each}
    </label>
</div>
