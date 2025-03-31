<script lang="ts">
    let { children, href, text } = $props();
</script>

<a data-sveltekit-preload-data="tap" {href}>
    {@render children()}
    {text}
</a>

<style>
    a{
        background-color: violet;
    }
    a > :global(svg) {
        height: 32px;
        width: 32px;
        color: var(--accent-main);
    }
</style>
