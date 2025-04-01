<script lang="ts">
    import LayoutHeader from "./LayoutHeader.svelte";
    let { children } = $props();
</script>

<div class="page">
    <LayoutHeader />

    {@render children()}
</div>

<style>
    .page {
        display: flex;
        flex-direction: column;
        width: 100vw;
    }
    
</style>
