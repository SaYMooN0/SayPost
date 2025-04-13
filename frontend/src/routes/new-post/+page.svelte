<script lang="ts">
    import AuthView from "../../components/AuthView.svelte";
    import PageAuthNeeded from "../../components/PageAuthNeeded.svelte";
    import { AuthStoreData } from "../../stores/auth-store.svelte";

    let draftPosts = $state([]);
    async function fetchDraftPosts(userId: string | null) {}
</script>

<h1>Your draft posts</h1>

{#snippet authenticated(authData: AuthStoreData)}
    {#await fetchDraftPosts(authData.UserId)}{/await}
{/snippet}
{#snippet unauthenticated()}
    <PageAuthNeeded />
{/snippet}
<AuthView {authenticated} {unauthenticated} />
