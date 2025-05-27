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
        width: 12rem;
        height: 6rem;
        border-radius: 0.25rem;
        box-shadow:
            rgba(60, 64, 67, 0.3) 0px 1px 2px 0px,
            rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
        display: flex;
        align-content: center;
    }
</style>
