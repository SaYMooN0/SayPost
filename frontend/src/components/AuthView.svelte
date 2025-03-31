<script lang="ts">
    import { getAuthData } from "../stores/auth-store.svelte";

    let { loading, authenticated, unauthenticated } = $props();
</script>

{#await getAuthData()}
    {@render loading()}
{:then authData}
    {#if authData !== null && authData.isAuthenticated()}
        {@render authenticated(authData)}
    {:else}
        {@render unauthenticated()}
    {/if}
{/await}
