<script lang="ts">
    import AuthView from "../../../components/AuthView.svelte";
    import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";
    import { AuthStoreData } from "../../../stores/auth-store.svelte";
    import ProfileBannerDisplay from "./user_page_components/ProfileBannerDisplay.svelte";
    import type { PageProps } from "./$types";
    import ProfileBannerEditingDialog from "./user_page_components/ProfileBannerEditingDialog.svelte";
    import { BannerDesign, DesignVariant } from "./user-profile";
    import UserStatisticsCard from "./user_page_components/UserStatisticsCard.svelte";
    import UserPageFollowButton from "./user_page_components/UserPageFollowButton.svelte";

    let { data }: PageProps = $props();
    let pageProfileBanner = $state(
        data.pageUser?.profileBanner || {
            scale: 2,
            design: BannerDesign.Waves,
            variant: DesignVariant.Var1,
            colors: ["#000000", "#ffffff"],
        },
    );
    let bannerEditingDialog: ProfileBannerEditingDialog;
</script>

{#if data.errors}
    <DefaultErrBlock errList={data.errors} />
{:else}
    <div class="banner">
        <ProfileBannerDisplay
            scale={pageProfileBanner.scale}
            colors={pageProfileBanner.colors}
            design={pageProfileBanner.design}
            designVariant={pageProfileBanner.variant}
        />
        <div class="banner-item">
            <AuthView
                authenticated={bannerAuthenticated}
                unauthenticated={bannerUnauthenticated}
            />
        </div>
    </div>
    <div class="statistics-cards">
        <UserStatisticsCard
            label={"Posts published"}
            value={(
                data.pageUser.statistics?.publishedPostsCount || 0
            ).toString()}
        >
            <label>#</label>
        </UserStatisticsCard>
        <p>
            {JSON.stringify(data.pageUser.statistics, null, 2)}
        </p>
    </div>
{/if}
{#snippet bannerAuthenticated(authData: AuthStoreData)}
    {#if data.pageUser}
        {#if authData.UserId == data.pageUser.userId}
            <button
                class="edit-banner-btn"
                onclick={() => bannerEditingDialog.open()}>Edit</button
            >
            <ProfileBannerEditingDialog
                bind:this={bannerEditingDialog}
                scale={pageProfileBanner.scale}
                colors={pageProfileBanner.colors}
                design={pageProfileBanner.design}
                designVariant={pageProfileBanner.variant}
                updateValuesOnPage={(newVal) => {
                    pageProfileBanner = newVal;
                }}
            />
        {:else}
            <UserPageFollowButton
                userId={data.pageUser.userId}
                isFollowedByViewer={data.pageUser.isFollowedByViewer}
            />
        {/if}
    {/if}
{/snippet}

{#snippet bannerUnauthenticated()}
    <label class="logging-to-follow-msg">Log in to follow users</label>
{/snippet}

<style>
    .banner {
        position: relative;
        width: 100%;
        margin-top: 1rem;
        border-radius: 0.75rem 0.75rem 0.25rem 0.25rem;
        aspect-ratio: 5 / 1;
    }

    .banner > :global(svg) {
        display: block;
    }

    .banner-item {
        position: absolute;
        right: 2rem;
        bottom: 2rem;
    }
    .logging-to-follow-msg {
        background-color: var(--back-second);
        padding: 0.375rem 0.75rem;
        border-radius: 0.25rem;
        font-weight: 450;
    }
</style>
