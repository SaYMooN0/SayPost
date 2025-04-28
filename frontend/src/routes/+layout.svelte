<script lang="ts">
    import type { Snippet } from "svelte";
    import LayoutAuthDialog from "./layout_components/LayoutAuthDialog.svelte";
    import LayoutHeader from "./layout_components/LayoutHeader.svelte";

    const { children } = $props<{ children: Snippet }>();
    let authDialog = $state<ReturnType<typeof LayoutAuthDialog>>();
</script>

<LayoutAuthDialog bind:this={authDialog} />
<div class="page">
    <LayoutHeader
        openLogin={() => authDialog?.openLogin()}
        openSignUp={() => authDialog?.openSignUp()}
    />
    <div class="page-content">
        {@render children()}
    </div>
</div>

<style>
    .page {
        width: 100%;
        box-sizing: border-box;
        margin: 0;
    }

    .page-content {
        width: 80rem;
        margin: 0 auto;
        box-sizing: border-box;
    }
</style>
