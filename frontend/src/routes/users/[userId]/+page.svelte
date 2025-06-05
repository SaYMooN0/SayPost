<script lang="ts">
    import AuthView from "../../../components/AuthView.svelte";
    import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";
    import { AuthStoreData } from "../../../stores/auth-store.svelte";
    import type { PageProps } from "./$types";
    import ErrView from "../../../components/err_blocks/ErrView.svelte";
    import MyProfileBanner from "./user_page_components/profile-banner/MyProfileBanner.svelte";
    import OtherUserProfileBannerWithFollow from "./user_page_components/profile-banner/OtherUserProfileBannerWithFollow.svelte";
    import OtherUserProfileBannerUnauthorized from "./user_page_components/profile-banner/OtherUserProfileBannerUnauthorized.svelte";
    import UserStatisticsCardsContainer from "./user_page_components/UserStatisticsCardsContainer.svelte";

    let { data }: PageProps = $props();
    let statisticsContainerEl: UserStatisticsCardsContainer;
    function isViewersPage(viewerData: AuthStoreData): boolean {
        if (data.pageUser === undefined) {
            return false;
        }
        return data.pageUser.userId == viewerData.UserId;
    }
</script>

{#if data.errors && data.errors.length > 0}
    <DefaultErrBlock errList={data.errors} />
{:else if data.pageUser === undefined}
    <ErrView err={{ message: "Unable to load " }} />
{:else}
    {#snippet bannerAuthenticated(authData: AuthStoreData)}
        {#if isViewersPage(authData)}
            <MyProfileBanner bannerData={data.pageUser.profileBanner} />
        {:else}
            <OtherUserProfileBannerWithFollow
                userId={data.pageUser.userId}
                bannerData={data.pageUser.profileBanner}
                isFollowedByViewer={data.pageUser.isFollowedByViewer}
                updateFollowersCountOnThePage={(newVal: number) => {
                    statisticsContainerEl.updateFollowersCount(newVal);
                }}
            />
        {/if}
    {/snippet}

    {#snippet bannerUnauthenticated()}
        <OtherUserProfileBannerUnauthorized
            bannerData={data.pageUser.profileBanner}
        />
    {/snippet}

    <AuthView
        authenticated={bannerAuthenticated}
        unauthenticated={bannerUnauthenticated}
    />
    <UserStatisticsCardsContainer
        bind:this={statisticsContainerEl}
        statistics={data.pageUser.statistics}
        userId={data.pageUser.userId}
        {isViewersPage}
    />
{/if}

<style>
    :global(.banner) {
        position: relative;
        width: 100%;
        margin-top: 1rem;
        border-radius: 0.75rem 0.75rem 0.25rem 0.25rem;
        aspect-ratio: 5 / 1;
    }

    :global(.banner > svg) {
        display: block;
    }

    :global(.banner-item) {
        position: absolute;
        right: 2rem;
        bottom: 2rem;
    }
</style>
