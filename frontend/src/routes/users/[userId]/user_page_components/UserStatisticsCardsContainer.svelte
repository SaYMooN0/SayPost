<script lang="ts">
    import type { UserProfileStatistics } from "../user-profile";

    let {
        statistics,
        userId,
    }: { statistics: UserProfileStatistics; userId: string } = $props<{
        statistics: UserProfileStatistics;
        userId: string;
    }>();
    let followersCountCopy = $state(statistics.followersCount);
    export function updateFollowersCount(newVal: number) {
        followersCountCopy = newVal;
    }
</script>

<div class="statistics-cards">
    <a
        href="/users/{userId}/published-posts"
        class="card"
        data-sveltekit-preload-data="hover"
    >
        <p class="value">{statistics.publishedPostsCount}</p>
        <label class="text">
            {statistics.publishedPostsCount != 1 ? "posts" : "post"} published
        </label>
    </a>
    <a
        href="/users/{userId}/followers"
        class="card"
        data-sveltekit-preload-data="hover"
    >
        <p class="value">{followersCountCopy}</p>
        <label class="text">
            {followersCountCopy != 1 ? "followers" : "follower"}
        </label>
    </a>
    <a
        href="/users/{userId}/followings"
        class="card"
        data-sveltekit-preload-data="hover"
    >
        <p class="value">{statistics.followingsCount}</p>
        <label class="text">
            {statistics.followingsCount != 1 ? "followings" : "following"}
        </label>
    </a>
    <a
        href="/users/{userId}/liked-posts"
        class="card"
        data-sveltekit-preload-data="hover"
    >
        <p class="value">{statistics.likedPostsCount}</p>
        <label class="text">
            {statistics.likedPostsCount != 1 ? "liked posts" : "liked post"}
        </label>
    </a>
    <a
        href="/users/{userId}/comments-left"
        class="card"
        data-sveltekit-preload-data="hover"
    >
        <p class="value">{statistics.commentsLeftCount}</p>
        <label class="text">
            {statistics.commentsLeftCount != 1 ? "comments" : "comment"}
            left
        </label>
    </a>
</div>

<style>
    .statistics-cards {
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: center;
        gap: 2rem;
        margin-top: 1rem;
    }

    .card {
        position: relative;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        width: 12rem;
        height: 5rem;
        border-radius: 0.25rem;
        text-decoration: none;
        box-shadow:
            rgb(60 64 67 / 30%) 0 1px 2px 0,
            rgb(60 64 67 / 15%) 0 1px 3px 1px;
        transition: all 0.2s cubic-bezier(0.23, 1, 0.32, 1);
        cursor: pointer;
        overflow: hidden;
    }

    .card:hover {
        box-shadow:
            rgb(60 64 67 / 30%) 0 1px 2px 0,
            rgb(60 64 67 / 15%) 0 1px 3px 1px,
            rgb(0 0 0 / 10%) 0 5px 10px;
        transform: scale(1.1);
    }

    .card > p {
        margin: 0;
        color: var(--accent-main);
        font-size: 2rem;
        font-weight: 550;
        cursor: inherit;

    }

    .card > label {
        color: var(--accent-main);
        font-size: 1.25rem;
        font-weight: 450;
             cursor: inherit;

    }
</style>
