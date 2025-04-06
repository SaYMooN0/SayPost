<script lang="ts">
    import type { Snippet } from "svelte";

    let {
        children,
        href,
        text,
    }: { children: Snippet; href: string; text: string } = $props();
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
