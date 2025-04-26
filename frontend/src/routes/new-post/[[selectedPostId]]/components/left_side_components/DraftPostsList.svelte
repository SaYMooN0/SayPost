<script lang="ts">
    import { DateUtils } from "../../../../../ts/common/utils/date-utils";
    import type { DraftPostMainInfo } from "../../draftPosts";

    let {
        posts,
        openMoreActionsMenu,
    }: {
        posts: DraftPostMainInfo[];
        openMoreActionsMenu: (
            event: MouseEvent,
            postData: DraftPostMainInfo,
        ) => void;
    } = $props<{
        posts: DraftPostMainInfo[];
        openMoreActionsMenu: (
            event: MouseEvent,
            postData: DraftPostMainInfo,
        ) => void;
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
            {#if post.isPinned}
                <div class="pinned-indicator"></div>
            {/if}
            <div class="text">
                <label class="post-title">{post.title}</label>
                <label class="last-modified">
                    last modified at {DateUtils.toLocale(post.lastModifiedAt)}
                </label>
            </div>
            <svg
                class="more-btn"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
                onclick={(e) => openMoreActionsMenu(e, post)}
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
        align-items: center;
        padding: 0.375rem 0.125rem 0.375rem 0.5rem;
        margin: 0.125rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.5rem;
        text-decoration: none;
        transition: all 0.1s ease-in;
        grid-template-columns: 1fr auto;
        box-sizing: border-box;
    }

    .post-link:hover {
        padding-right: 0.25rem;
        margin-left: 0.5rem;
        background-color: var(--back-second);
    }

    .post-link > .hover-indicator {
        position: absolute;
        top: 0;
        left: 0;
        width: 0.125rem;
        height: 100%;
        border-radius: 5rem;
        background-color: var(--accent-main);
        opacity: 0;
        transition: inherit;
        pointer-events: none;
    }

    .post-link:hover > .hover-indicator {
        left: -0.5rem;
        opacity: 1;
    }

    .more-btn {
        width: 1.75rem;
        height: 1.75rem;
        color: var(--gray);
        opacity: 0;
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
        display: grid;
        width: 100%;
        grid-template-rows: 1.6fr 1fr;
    }

    .post-title {
        display: inline-block;
        margin-bottom: 0.25rem;
        color: var(--accent-main);
        font-size: 1rem;
        font-weight: 400;
        cursor: inherit;
        box-sizing: border-box;
        text-overflow: ellipsis;
        overflow: hidden;
        white-space: nowrap;
    }

    .last-modified {
        font-size: 0.75rem;
        font-weight: 400;
        cursor: inherit;
    }
</style>
