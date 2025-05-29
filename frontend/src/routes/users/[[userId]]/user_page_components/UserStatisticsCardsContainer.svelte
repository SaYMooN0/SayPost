<script lang="ts">
    import type { UserProfileStatistics } from "../user-profile";

    let { statistics }: { statistics: UserProfileStatistics } = $props<{
        statistics: UserProfileStatistics;
    }>();
    let followersCountCopy = $state(statistics.followersCount);
    export function updateFollowersCount(newVal: number) {
        followersCountCopy = newVal;
    }
</script>

<div class="statistics-cards">
    <div class="card">
        <p class="value">{statistics.publishedPostsCount}</p>
        <label class="text">
            {statistics.publishedPostsCount != 1 ? "posts" : "post"} published
        </label>
    </div>
    <div class="card">
        <p class="value">{followersCountCopy}</p>
        <label class="text">
            {followersCountCopy != 1 ? "followers" : "follower"}
        </label>
    </div>
    <div class="card">
        <p class="value">{statistics.followingsCount}</p>
        <label class="text">
            {statistics.followingsCount != 1 ? "followings" : "following"}
        </label>
    </div>
    <div class="card">
        <p class="value">{statistics.likedPostsCount}</p>
        <label class="text">
            {statistics.likedPostsCount != 1 ? "liked posts" : "liked post"}
        </label>
    </div>
    <div class="card">
        <p class="value">{statistics.commentsLeftCount}</p>
        <label class="text">
            {statistics.commentsLeftCount != 1
                ? "comments left"
                : "comment left"}
        </label>
    </div>
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
        box-shadow:
            rgb(60 64 67 / 30%) 0 1px 2px 0,
            rgb(60 64 67 / 15%) 0 1px 3px 1px;
        transition: all 0.2s cubic-bezier(0.23, 1, 0.32, 1);
        overflow: hidden;
    }

    .card:hover {
        box-shadow:
            rgb(60 64 67 / 30%) 0 1px 2px 0,
            rgb(60 64 67 / 15%) 0 1px 3px 1px,
            rgb(0 0 0 / 10%) 0 5px 10px;
        transform:rotateY(-10deg) rotateX(18deg) scale(1.1);
    }

    .card > p {
        margin: 0;
        color: var(--accent-main);
        font-size: 2rem;
        font-weight: 550;
        cursor: default;
    }

    .card > label {
        color: var(--accent-main);
        font-size: 1.25rem;
        font-weight: 450;
        cursor: default;
    }
</style>
