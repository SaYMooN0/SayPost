<script lang="ts">
    import AuthView from "../../../components/AuthView.svelte";
    import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";
    import { AuthStoreData } from "../../../stores/auth-store.svelte";
    import ProfileBannerDisplay from "./ProfileBannerDisplay.svelte";
    import type { PageProps } from "./$types";

    let { data }: PageProps = $props();
    let isFollowing: null | boolean = $state(null);
</script>

{#if data.errors}
    <DefaultErrBlock errList={data.errors} />
{:else}
    <div class="banner">
        <p>{JSON.stringify(data.pageUser)}</p>
        <ProfileBannerDisplay
            colors={data.pageUser.profileBanner.colors}
            design={data.pageUser.profileBanner.design}
            designVariant={data.pageUser.profileBanner.designVariant}
        />
        <div class="banner-item">
            <AuthView
                authenticated={bannerAuthenticated}
                unauthenticated={bannerUnauthenticated}
            />
        </div>
    </div>
{/if}

{#snippet bannerAuthenticated(authData: AuthStoreData)}
    {#if authData.UserId == data.pageUser?.userId}
        <button class="edit-banner-btn">Edit</button>
    {:else}
        <div class="is-following">Is following state</div>
    {/if}
{/snippet}

{#snippet bannerUnauthenticated()}
    <label>Log in to follow users</label>
{/snippet}

<style>
    .banner {
        position: relative;
        width: 100%;
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
</style>
