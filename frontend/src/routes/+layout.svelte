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
    :root {
        --accent-main: #6648f1;
        --accent-hov: #4e35f2;

        --text-main: #121222;

        --back-main: #f5f5f5;

        --err-red: #ef421a;
        --err-red-back: #f5dcdc;

        --warning-yellow: #f2bc46;
        --warning-yellow-back: #f8e9d3;

        --grey: #8b8ba7;
    }

    body {
        margin: 0;
        padding: 0;
        background-color: var(--back-main);
    }
    .page {
        display: flex;
        flex-direction: column;
        width: 100vw;
    }
</style>
