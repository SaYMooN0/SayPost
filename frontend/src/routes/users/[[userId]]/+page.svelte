<script lang="ts">
    import AuthView from "../../../components/AuthView.svelte";
    import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";
    import { AuthStoreData } from "../../../stores/auth-store.svelte";
    import type { PageProps } from "./$types";

    let { data }: PageProps = $props();
    let isFollowing: null | boolean = $state(null);
    
</script>

{#if data.errors}
    <DefaultErrBlock errList={data.errors.ToErrInstances()} />
{:else}
    User profile
    <div class="banner">
        <AuthView
            authenticated={bannerAuthenticated}
            unauthenticated={bannerUnauthenticated}
        />
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
