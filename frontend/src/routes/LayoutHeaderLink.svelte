<script lang="ts">
    let { children, href, text } = $props();
</script>

<a data-sveltekit-preload-data="tap" {href}>
    {@render children()}
    {text}
</a>

<style>
    a {
        display: flex;
        align-items: center;
    }
    a > :global(svg) {
        height: 32px;
        width: 32px;
        color: var(--accent-main);
    }
</style>
