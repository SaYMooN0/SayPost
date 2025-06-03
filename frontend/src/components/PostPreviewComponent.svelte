<script lang="ts">
    import { DateUtils } from "../ts/common/utils/date-utils";
    import type { PostPreview } from "../ts/published-posts-previews";

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
    <div class="date-comments-likes-div">
        <div class="comments-likes-div">
            <label>
                {post.likesCount}
            </label>
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
                ><path
                    d="M2 12.5C2 11.3954 2.89543 10.5 4 10.5C5.65685 10.5 7 11.8431 7 13.5V17.5C7 19.1569 5.65685 20.5 4 20.5C2.89543 20.5 2 19.6046 2 18.5V12.5Z"
                    stroke="currentColor"
                    stroke-width="1.8"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                ></path>
                <path
                    d="M15.4787 7.80626L15.2124 8.66634C14.9942 9.37111 14.8851 9.72349 14.969 10.0018C15.0369 10.2269 15.1859 10.421 15.389 10.5487C15.64 10.7065 16.0197 10.7065 16.7791 10.7065H17.1831C19.7532 10.7065 21.0382 10.7065 21.6452 11.4673C21.7145 11.5542 21.7762 11.6467 21.8296 11.7437C22.2965 12.5921 21.7657 13.7351 20.704 16.0211C19.7297 18.1189 19.2425 19.1678 18.338 19.7852C18.2505 19.8449 18.1605 19.9013 18.0683 19.9541C17.116 20.5 15.9362 20.5 13.5764 20.5H13.0646C10.2057 20.5 8.77628 20.5 7.88814 19.6395C7 18.7789 7 17.3939 7 14.6239V13.6503C7 12.1946 7 11.4668 7.25834 10.8006C7.51668 10.1344 8.01135 9.58664 9.00069 8.49112L13.0921 3.96056C13.1947 3.84694 13.246 3.79012 13.2913 3.75075C13.7135 3.38328 14.3652 3.42464 14.7344 3.84235C14.774 3.8871 14.8172 3.94991 14.9036 4.07554C15.0388 4.27205 15.1064 4.37031 15.1654 4.46765C15.6928 5.33913 15.8524 6.37436 15.6108 7.35715C15.5838 7.46692 15.5488 7.5801 15.4787 7.80626Z"
                    stroke="currentColor"
                    stroke-width="1.8"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                ></path>
            </svg>
            <div />
            <label>
                {post.commentsCount}
            </label>
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M8 13.5H16M8 8.5H12"
                    stroke="currentColor"
                    stroke-width="1.8"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M6.09881 19C4.7987 18.8721 3.82475 18.4816 3.17157 17.8284C2 16.6569 2 14.7712 2 11V10.5C2 6.72876 2 4.84315 3.17157 3.67157C4.34315 2.5 6.22876 2.5 10 2.5H14C17.7712 2.5 19.6569 2.5 20.8284 3.67157C22 4.84315 22 6.72876 22 10.5V11C22 14.7712 22 16.6569 20.8284 17.8284C19.6569 19 17.7712 19 14 19C13.4395 19.0125 12.9931 19.0551 12.5546 19.155C11.3562 19.4309 10.2465 20.0441 9.14987 20.5789C7.58729 21.3408 6.806 21.7218 6.31569 21.3651C5.37769 20.6665 6.29454 18.5019 6.5 17.5"
                    stroke="currentColor"
                    stroke-width="1.8"
                    stroke-linecap="round"
                />
            </svg>
        </div>
        <label>Published on {DateUtils.toLocale(post.publicationDate)}</label>
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
        border: 0.125rem solid var(--accent-main);
        border-radius: 0.5rem;
        box-sizing: border-box;
        gap: 0.5rem;
    }

    .title {
        width: 100%;
        overflow-wrap: anywhere;
        text-indent: 0.25em;
        margin: 0;
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

    .date-comments-likes-div {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
    }
    .comments-likes-div {
        display: grid;
        align-items: center;
        width: 6rem;
        grid-template-columns: min-content 1.25rem 1fr min-content 1.25rem;
        gap: 0.25rem;
        box-sizing: border-box;
    }
    .comments-likes-div label {
        color: var(--text-main);
        width: fit-content;
        box-sizing: border-box;
    }
    .comments-likes-div svg {
        width: 100%;
        color: var(--text-main);
    }
    .read-link {
        width: 90%;
        padding: 0.375rem 0;
        margin: 0 auto;
        border-radius: 0.25rem;
        background-color: var(--back-second);
        color: var(--accent-main);
        font-size: 1.25rem;
        font-weight: 500;
        text-align: center;
        text-decoration: none;
        transition:
            all 0.08s ease-in,
            border-radius 0.2s ease-in;
        cursor: pointer;
    }

    .read-link:hover {
        background-color: var(--accent-main);
        color: var(--back-main);
        width: 92%;
    }

    .read-link:active {
        border-radius: 1.5rem;
        transform: scale(0.98);
        background-color: var(--accent-hov);
    }
</style>
