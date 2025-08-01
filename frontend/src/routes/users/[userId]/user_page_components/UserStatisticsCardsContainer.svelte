<script lang="ts">
    import AuthView from "../../../../components/AuthView.svelte";
    import { AuthStoreData } from "../../../../stores/auth-store.svelte";
    import { ApiMain } from "../../../../ts/backend-services";
    import type { UserProfileStatistics } from "../user-profile";
    import StatisticsCommentsLeftCard from "./statistics-components/cards/StatisticsCommentsLeftCard.svelte";
    import StatisticsFollowersCard from "./statistics-components/cards/StatisticsFollowersCard.svelte";
    import StatisticsFollowingsCard from "./statistics-components/cards/StatisticsFollowingsCard.svelte";
    import StatisticsLikedPostsCard from "./statistics-components/cards/StatisticsLikedPostsCard.svelte";
    import StatisticsPublishedPostsCard from "./statistics-components/cards/StatisticsPublishedPostsCard.svelte";
    import EditStatisticsCardVisibilityDialog from "./statistics-components/EditStatisticsCardVisibilityDialog.svelte";

    let {
        userId,
        isViewersPage,
    }: {
        statistics: UserProfileStatistics;
        userId: string;
        isViewersPage: (viewerData: AuthStoreData) => boolean;
    } = $props<{
        userId: string;
        isViewersPage: (viewerData: AuthStoreData) => boolean;
    }>();
    let statistics: UserProfileStatistics = $state<UserProfileStatistics>({
        publishedPosts: { isHidden: true },
        followers: { isHidden: true },
        followings: { isHidden: true },
        likedPosts: { isHidden: true },
        commentsLeft: { isHidden: true },
    });
    export async function updateStatisticsState() {
        const response =
            await ApiMain.serverFetchJsonResponse<UserProfileStatistics>(
                fetch,
                `/users/${userId}/statistics`,
                {
                    method: "GET",
                },
            );
        if (response.isSuccess) {
            statistics = response.data;
        }
    }
    let visibilityEditingDialog: EditStatisticsCardVisibilityDialog;
</script>

<div class="statistics-cards">
    {#await updateStatisticsState() then _}
        <EditStatisticsCardVisibilityDialog
            bind:this={visibilityEditingDialog}
        />
        <StatisticsPublishedPostsCard
            cardValue={statistics.publishedPosts}
            {userId}
        />
        <StatisticsFollowersCard cardValue={statistics.followers} {userId} />
        <StatisticsFollowingsCard cardValue={statistics.followings} {userId} />
        <StatisticsLikedPostsCard cardValue={statistics.likedPosts} {userId} />
        <StatisticsCommentsLeftCard
            cardValue={statistics.commentsLeft}
            {userId}
        />
    {/await}
    <AuthView authenticated={editVisibilityButton} />
    {#snippet editVisibilityButton(authData: AuthStoreData)}
        {#if isViewersPage(authData)}
            <svg
                class="edit-visibility-btn"
                onclick={() => visibilityEditingDialog.open()}
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M15.5 12C15.5 13.933 13.933 15.5 12 15.5C10.067 15.5 8.5 13.933 8.5 12C8.5 10.067 10.067 8.5 12 8.5C13.933 8.5 15.5 10.067 15.5 12Z"
                    stroke="currentColor"
                    stroke-width="1.75"
                ></path>
                <path
                    d="M21.011 14.0965C21.5329 13.9558 21.7939 13.8854 21.8969 13.7508C22 13.6163 22 13.3998 22 12.9669V11.0332C22 10.6003 22 10.3838 21.8969 10.2493C21.7938 10.1147 21.5329 10.0443 21.011 9.90358C19.0606 9.37759 17.8399 7.33851 18.3433 5.40087C18.4817 4.86799 18.5509 4.60156 18.4848 4.44529C18.4187 4.28902 18.2291 4.18134 17.8497 3.96596L16.125 2.98673C15.7528 2.77539 15.5667 2.66972 15.3997 2.69222C15.2326 2.71472 15.0442 2.90273 14.6672 3.27873C13.208 4.73448 10.7936 4.73442 9.33434 3.27864C8.95743 2.90263 8.76898 2.71463 8.60193 2.69212C8.43489 2.66962 8.24877 2.77529 7.87653 2.98663L6.15184 3.96587C5.77253 4.18123 5.58287 4.28891 5.51678 4.44515C5.45068 4.6014 5.51987 4.86787 5.65825 5.4008C6.16137 7.3385 4.93972 9.37763 2.98902 9.9036C2.46712 10.0443 2.20617 10.1147 2.10308 10.2492C2 10.3838 2 10.6003 2 11.0332V12.9669C2 13.3998 2 13.6163 2.10308 13.7508C2.20615 13.8854 2.46711 13.9558 2.98902 14.0965C4.9394 14.6225 6.16008 16.6616 5.65672 18.5992C5.51829 19.1321 5.44907 19.3985 5.51516 19.5548C5.58126 19.7111 5.77092 19.8188 6.15025 20.0341L7.87495 21.0134C8.24721 21.2247 8.43334 21.3304 8.6004 21.3079C8.76746 21.2854 8.95588 21.0973 9.33271 20.7213C10.7927 19.2644 13.2088 19.2643 14.6689 20.7212C15.0457 21.0973 15.2341 21.2853 15.4012 21.3078C15.5682 21.3303 15.7544 21.2246 16.1266 21.0133L17.8513 20.034C18.2307 19.8187 18.4204 19.711 18.4864 19.5547C18.5525 19.3984 18.4833 19.132 18.3448 18.5991C17.8412 16.6616 19.0609 14.6226 21.011 14.0965Z"
                    stroke="currentColor"
                    stroke-width="1.75"
                    stroke-linecap="round"
                ></path>
            </svg>
        {/if}
    {/snippet}
</div>

<style>
    .statistics-cards {
        position: relative;
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: center;
        gap: 2rem;
        margin-top: 1rem;
    }

    .edit-visibility-btn {
        position: absolute;
        top: 0;
        right: 1rem;
        height: 2rem;
        padding: 0.0625rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.25rem;
        background-color: var(--back-second);
        color: var(--gray);
        transition: all 0.04s ease-in;
        box-sizing: border-box;
    }

    .edit-visibility-btn:hover {
        border-color: var(--accent-main);
        color: var(--accent-main);
    }

    .edit-visibility-btn:active {
        border-color: var(--accent-hov);
        color: var(--accent-hov);
        transform: scale(0.96);
    }
</style>
