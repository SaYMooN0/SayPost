<script lang="ts">
    import { DateUtils } from "../../../../../ts/common/utils/date-utils";
    import type { DraftPostMainInfo } from "../../draft-posts";

    let {
        posts,
        currentPostId,
        openMoreActionsMenu,
    }: {
        posts: DraftPostMainInfo[];
        currentPostId: string;
        openMoreActionsMenu: (
            event: MouseEvent,
            postData: DraftPostMainInfo,
        ) => void;
    } = $props<{
        posts: DraftPostMainInfo[];
        currentPostId: string;
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
            class:is-selected={post.id === currentPostId}
            href="/new-post/{post.id}"
            data-sveltekit-preload-data="hover"
            data-sveltekit-noscroll
        >
            <div class="hover-indicator"></div>
            {#if post.isPinned}
                <svg
                    class="pin-indicator"
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        fill-rule="evenodd"
                        clip-rule="evenodd"
                        d="M8.95711 15.0429C9.34763 15.4334 9.34763 16.0666 8.95711 16.4571L3.95711 21.4571C3.56658 21.8476 2.93342 21.8476 2.54289 21.4571C2.15237 21.0666 2.15237 20.4334 2.54289 20.0429L7.54289 15.0429C7.93342 14.6524 8.56658 14.6524 8.95711 15.0429Z"
                        fill="currentColor"
                    />
                    <path
                        d="M17.7093 2.55346C19.4569 3.18528 20.8147 4.54305 21.4465 6.29064C21.5536 6.58633 21.6798 6.93507 21.7258 7.24165C21.7801 7.60384 21.744 7.94519 21.608 8.32365C21.3168 9.13408 20.6643 9.49626 19.9452 9.89542L18.5273 10.6861C18.0145 10.9721 17.676 11.1618 17.4348 11.3286C17.2 11.4909 17.1333 11.582 17.1056 11.6358C17.0909 11.6641 17.0453 11.8022 17.0573 12.2506C17.0686 12.6742 17.1236 13.2487 17.2024 14.0663C17.3296 15.3845 17.1109 16.6904 16.3502 17.9068C16.2218 18.1123 16.0604 18.3708 15.88 18.563C15.6669 18.7901 15.4215 18.9506 15.1053 19.0787C14.7835 19.2091 14.4907 19.2619 14.1748 19.2478C13.906 19.2358 13.5974 19.1656 13.3398 19.107C11.3641 18.6584 9.47833 17.5121 7.98305 16.0169C6.48778 14.5216 5.34151 12.6359 4.89294 10.6602C4.83436 10.4027 4.76418 10.0941 4.75221 9.82535C4.73813 9.50941 4.79103 9.21658 4.92151 8.89474C5.04969 8.5786 5.21026 8.3331 5.43747 8.12006C5.62978 7.93974 5.88832 7.7784 6.09379 7.65019C7.29324 6.90084 8.58854 6.66967 9.89679 6.78538C10.7321 6.85926 11.321 6.91095 11.754 6.91998C11.968 6.92445 12.2391 6.94301 12.3803 6.86947C12.4326 6.84219 12.5235 6.7753 12.6865 6.53889C12.8534 6.29676 13.0438 5.95698 13.3302 5.44342L14.1046 4.05478C14.5037 3.33567 14.8659 2.68318 15.6763 2.39201C16.0548 2.25603 16.3961 2.2199 16.7583 2.27422C17.0649 2.32019 17.4136 2.44643 17.7093 2.55346Z"
                        fill="currentColor"
                    />
                </svg>
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
        transition: all 0.08s ease-in;
        grid-template-columns: 1fr auto;
        box-sizing: border-box;
    }

    .post-link:hover {
        padding-right: 0.25rem;
        margin-left: 0.5rem;
        background-color: var(--back-second);
    }

    .post-link:active {
        transform: scale(0.98);
    }

    .post-link.is-selected {
        border-color: var(--accent-main);
    }

    .hover-indicator {
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

    .pin-indicator {
        position: absolute;
        top: 0.25rem;
        right: 0.25rem;
        width: 1rem;
        color: var(--accent-main);
        opacity: 1;
        transition:
            all 0.12s ease-in,
            width 0.2s ease-in;
    }

    .post-link:hover > .pin-indicator {
        top: 0.125rem;
        right: 0.125rem;
        width: 0.675rem;
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
        color: var(--gray) !important;
        font-size: 0.75rem;
        font-weight: 400;
        cursor: inherit;
    }
</style>
