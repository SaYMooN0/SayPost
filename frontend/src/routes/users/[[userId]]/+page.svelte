<script lang="ts">
    import AuthView from "../../../components/AuthView.svelte";
    import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";
    import { AuthStoreData } from "../../../stores/auth-store.svelte";
    import ProfileBannerDisplay from "./user_page_components/ProfileBannerDisplay.svelte";
    import type { PageProps } from "./$types";
    import ProfileBannerEditingDialog from "./user_page_components/ProfileBannerEditingDialog.svelte";
    import { BannerDesign, DesignVariant } from "./user-profile";
    import UserStatisticsCard from "./user_page_components/UserStatisticsCard.svelte";

    let { data }: PageProps = $props();
    let pageProfileBanner = $state(
        data.pageUser?.profileBanner || {
            scale: 2,
            design: BannerDesign.Waves,
            variant: DesignVariant.Var1,
            colors: ["#000000", "#ffffff"],
        },
    );
    let isFollowing: null | boolean = $state(null);
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
                data.pageUser.statistics?.postsPublished || 0
            ).toString()}
        >
            <label>#</label>
        </UserStatisticsCard>
         <UserStatisticsCard
            label={"Comments left"}
            value={(
                data.pageUser.statistics?.commentsLeft || 0
            ).toString()}
        >
            <label>#</label>
        </UserStatisticsCard>
         <UserStatisticsCard
            label={"Posts Liked"}
            value={(
                data.pageUser.statistics?.postsLiked || 0
            ).toString()}
        >
            <label>#</label>
        </UserStatisticsCard>
    </div>
{/if}
{#snippet bannerAuthenticated(authData: AuthStoreData)}
    {#if data.pageUser && authData.UserId == data.pageUser.userId}
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
        <div class="is-following">Is following state</div>
    {/if}
{/snippet}

{#snippet bannerUnauthenticated()}
    <label>Log in to follow users</label>
{/snippet}

<style>
    .banner {
        margin-top: 1rem;
        position: relative;
        width: 100%;
        aspect-ratio: 5 / 1;
        border-radius: 0.75rem 0.75rem 0.25rem 0.25rem;
    }

    .banner > :global(svg) {
        display: block;
    }

    .banner-item {
        position: absolute;
        right: 2rem;
        bottom: 2rem;
    }
</style>
