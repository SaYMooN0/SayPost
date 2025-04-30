<script lang="ts">
    import type { PostsFilterState } from "../posts-filter-state.svelte";
    import FilterTagsSelection from "./FilterTagsSelection.svelte";

    let {
        postsFilter,
        applyFilter,
    }: {
        postsFilter: PostsFilterState;
        applyFilter: () => void;
    } = $props<{
        postsFilter: PostsFilterState;
        applyFilter: () => void;
    }>();
    let isVisible = $state(false);
    let iconElement: SVGElement;

    function toggleVisibility(event: MouseEvent) {
        event.preventDefault();
        isVisible = !isVisible;
        if (iconElement) {
            iconElement.classList.remove("rotate-down", "rotate-up");
            iconElement.classList.add(isVisible ? "rotate-down" : "rotate-up");
        }
    }
</script>

<div class="container">
    <p class="always-shown">
        Posts filter
        <svg
            bind:this={iconElement}
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
            onclick={toggleVisibility}
        >
            <path
                d="M17.9998 15C17.9998 15 13.5809 9.00001 11.9998 9C10.4187 8.99999 5.99985 15 5.99985 15"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    </p>
    <div class="filter" class:hidden={!isVisible}>
        <p class="filter-p">Published from: <input /> to: <input /></p>
        <p class="filter-p">Include tags:</p>
        <FilterTagsSelection bind:selectedTags={postsFilter.includeTags} />
        <p class="filter-p">Exclude tags:</p>
        <FilterTagsSelection bind:selectedTags={postsFilter.excludeTags} />
        <div class="filter-btns">
            <button>Reset</button>
            <button>Apply</button>
        </div>
    </div>
</div>

<style>
    .container {
        background-color: var(--back-second);
        padding: 0.125rem 1rem;
        border-radius: 1rem;
    }
    .always-shown {
        margin: 0;
        display: grid;
        grid-template-columns: 1fr auto;
        align-items: center;
        font-size: 1.5rem;
    }
    .always-shown > svg {
        height: 1.75rem;
        transition: transform 0.17s ease-in;
        transform: scale(1.2);
        cursor: pointer;
    }

    :global(.always-shown .rotate-down) {
        animation: rotate-down 0.4s ease-in-out forwards;
    }

    :global(.always-shown .rotate-up) {
        animation: rotate-up 0.4s ease-in-out forwards;
    }
    .filter {
        margin-top: 0.5rem;
        interpolate-size: allow-keywords;
        font-size: 1.25rem;
        height: auto;
        opacity: 1;
        transition:
            all 0.2s ease-in,
            opacity 0.5s ease-out;
        display: flex;
        flex-direction: column;
        gap: 0.25rem;
    }
    .hidden {
        margin-top: 0;
        height: 0;
        opacity: 0;
        font-size: 0.25rem;
        transition:
            all 0.2s ease-in,
            opacity 0.04s ease-out;
    }
    .filter-p {
        margin: 0;
    }
    @keyframes rotate-down {
        from {
            transform: rotate(0deg) scale(1.2);
        }

        to {
            transform: rotate(-180deg) scale(1.2);
        }
    }

    @keyframes rotate-up {
        from {
            transform: rotate(-180deg) scale(1.2);
        }

        to {
            transform: rotate(0deg) scale(1.2);
        }
    }
</style>
