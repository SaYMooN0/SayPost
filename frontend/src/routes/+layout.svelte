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
    {@render children()}
</div>

<style>
    :root {
        --accent-main: #6648f1;
        --accent-hov: #4e35f2;
        --text-main: #121222;
        --back-main: #f5f5f5;
        --back-second: #ece7f3;
        --err-red: #dc2003;
        --err-red-back: #f5dcdc;
        --warning-yellow: #f2bc46;
        --warning-yellow-back: #f8e9d3;
        --gray: #6f6b88;

    }

    :global(body) {
        padding: 0;
        margin: 0;
        background-color: var(--back-main);
    }
    
    :global(*) {
        font-family: Rubik, sans-serif;
    }

    .page {
        width: 100vw;
    }

    :global(.unselectable) {
        user-select: none;
    }
</style>
