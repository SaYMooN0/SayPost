<script lang="ts">
    import type { Snippet } from "svelte";
    import { AuthStoreData, getAuthData } from "../stores/auth-store.svelte";

    const {
        loading = null,
        authenticated = null,
        unauthenticated = null,
    } = $props<{
        loading?: Snippet;
        authenticated?: Snippet<[AuthStoreData]>;
        unauthenticated?: Snippet;
    }>();
</script>

{#await getAuthData()}
    {@render loading?.()}
{:then authData}
    {#if authData !== null && authData.isAuthenticated()}
        {@render authenticated?.(authData)}
    {:else}
        {@render unauthenticated?.()}
    {/if}
{/await}
