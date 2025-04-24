<script lang="ts">
    import { DateUtils } from "../../../../../ts/common/utils/date-utils";
    import type { DraftPostMainInfo } from "../../draftPosts";

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
            <div class="hover-indicator"></div>
            <div class="text">
                <label class="post-title"> {post.title} </label>
                <label class="last-modified">
                    last modified at {DateUtils.toLocale(post.lastModifiedAt)}
                </label>
            </div>
            <svg
                class="more-btn"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M11.992 12H12.001"
                    stroke="currentColor"
                    stroke-width="3"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M11.9842 18H11.9932"
                    stroke="currentColor"
                    stroke-width="3"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M11.9998 6H12.0088"
                    stroke="currentColor"
                    stroke-width="3"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            </svg>
        </a>
    {/each}
</div>

<style>
    .posts-list {
        width: 100%;
        max-height: calc(98vh - var(--layout-header-height) - 8.5rem);
        overflow: hidden auto;
        box-sizing: border-box;
    }

    .post-link {
        position: relative;
        display: grid;
        grid-template-columns: 1fr auto;
        align-items: center;
        padding: 0.375rem 0.125rem 0.375rem 0.5rem;
        margin: 0.125rem;
        border: 0.125rem solid var(--back-second);
        box-sizing: border-box;
        text-decoration: none;
        border-radius: 0.5rem;
        transition: all 0.1s ease-in;
    }

    .post-link:hover {
        background-color: var(--back-second);
        margin-left: 0.5rem;
        padding-right: 0.25rem;
    }
    .post-link > .hover-indicator {
        position: absolute;
        left: 0;
        top: 0;
        width: 0.125rem;
        height: 100%;
        border-radius: 5rem;
        background-color: var(--accent-main);
        opacity: 0;
        pointer-events: none;
        transition: inherit;
    }
    .post-link:hover > .hover-indicator {
        left: -0.5rem;
        opacity: 1;
    }
    .more-btn {
        opacity: 0;
        height: 1.75rem;
        width: 1.75rem;
        color: var(--gray);
        transition: inherit;
    }
    .more-btn:hover {
        color: var(--accent-main);
    }
    .more-btn:hover path {
        stroke-width: 3.6;
    }
    .post-link:hover > .more-btn {
        opacity: 1;
    }
    .post-link > .text {
        width: 100%;
        display: grid;
        grid-template-rows: 1.6fr 1fr;
    }
    .post-title {
        display: inline-block;
        margin-bottom: 0.25rem;
        font-size: 1rem;
        font-weight: 400;
        box-sizing: border-box;
        text-overflow: ellipsis;
        overflow: hidden;
        white-space: nowrap;
        cursor: inherit;
        color: var(--accent-main);
    }

    .last-modified {
        font-size: 0.75rem;
        font-weight: 400;
        cursor: inherit;
    }
</style>
