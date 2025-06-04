<script lang="ts">
    import type { Snippet } from "svelte";
    import { page } from "$app/state";
    const { children } = $props<{ children: Snippet }>();
</script>

<div class="layout-header">
    <a
        href="/users/{page.params.userId}"
        class="back-btn"
        data-sveltekit-preload-data="hover"
    >
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M8.99996 16.9998L4 11.9997L9 6.99976"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M4 12H20"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    </a>
    User's {page.data.title}
</div>

{@render children()}

<style>
    .layout-header {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 0.5rem;
        padding: 0;
        margin: 0.25rem 0 1rem;
        font-size: 2rem;
        font-weight: 450;
        box-sizing: border-box;
    }

    .back-btn {
        width: 2rem;
        height: 2rem;
        background-color: var(--back-second);
    }

    .back-btn > svg {
        width: 100%;
        height: 100%;
        border-radius: 0.25rem;
        color: var(--gray);
        transition: all 0.08s ease-in;
    }

    .back-btn:hover > svg {
        color: var(--accent-main);
        transform: scale(1.06);
        cursor: pointer;
    }
</style>
