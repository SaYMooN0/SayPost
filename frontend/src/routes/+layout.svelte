<script lang="ts">
    import type { Snippet } from "svelte";
    import LayoutAuthDialog from "./LayoutAuthDialog.svelte";
    import LayoutHeader from "./LayoutHeader.svelte";

    let { children }: { children: Snippet } = $props();
    let authDialog = $state<ReturnType<typeof LayoutAuthDialog>>();
</script>

<LayoutAuthDialog bind:this={authDialog} />
<div class="page">
    <LayoutHeader
        openLogin={() => authDialog?.openLogin()}
        openSignUp={() => authDialog?.openSignUp()}
    />
    {@render children()}
</div>

<style>
    .page {
        display: flex;
        flex-direction: column;
        width: 100vw;
    }
</style>
