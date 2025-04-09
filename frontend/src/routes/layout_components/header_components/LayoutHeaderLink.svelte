<script lang="ts">
    import type { Snippet } from "svelte";

    const { children, href, text } = $props<{
        children: Snippet;
        href: string;
        text: string;
    }>();
</script>

<a {href}>
    {@render children()}
    {text}
</a>

<style>
    a {
        display: flex;
        align-items: center;
        padding: 0.125rem 0.5rem 0;
        color: var(--accent-main);
        font-size: var(--header-font-size);
        text-decoration: none;
        box-sizing: border-box;
        border-bottom: 0.125rem solid transparent;
    }

    a > :global(svg) {
        width: 2rem;
        height: 2rem;
        color: inherit;
    }

    a:hover {
        gap: 0.3rem;
        padding: 0.125rem 0.35rem 0;
        color: var(--accent-hov);
        border-bottom: 0.125rem solid var(--accent-hov);
    }
</style>
