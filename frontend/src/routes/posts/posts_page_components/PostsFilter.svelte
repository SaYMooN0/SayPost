<script lang="ts">
    import type { PostsFilterState } from "../posts-filter-state.svelte";
    import FilterTagsSelection from "./filter_components/FilterTagsSelection.svelte";

    let {
        postsFilter = $bindable(),
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
        <p class="filter-p">
            Published from: <input
                type="date"
                bind:value={postsFilter.dateFrom}
            />
            to: <input type="date" bind:value={postsFilter.dateTo} />
        </p>
        <p class="filter-p">Include tags:</p>
        <FilterTagsSelection bind:selectedTags={postsFilter.includeTags} />
        <p class="filter-p">Exclude tags:</p>
        <FilterTagsSelection bind:selectedTags={postsFilter.excludeTags} />
        <div class="filter-btns">
            <button onclick={postsFilter.reset}>Reset</button>
            <button onclick={applyFilter}>Apply</button>
        </div>
    </div>
</div>

<style>
    .container {
        padding: 0.25rem 1rem;
        border-radius: 1rem;
        background-color: var(--back-second);
    }

    .always-shown {
        display: grid;
        align-items: center;
        margin: 0;
        font-size: 1.5rem;
        grid-template-columns: 1fr auto;
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
        display: flex;
        flex-direction: column;
        gap: 0.25rem;
        height: auto;
        margin-top: 0.5rem;
        font-size: 1.25rem;
        opacity: 1;
        transition:
            all 0.2s ease-in,
            opacity 0.5s ease-out;
        interpolate-size: allow-keywords;
    }

    .hidden {
        height: 0;
        margin-top: 0;
        font-size: 0.25rem;
        opacity: 0;
        transition:
            all 0.2s ease-in,
            opacity 0.04s ease-out;
    }

    .filter-p {
        margin: 0;
    }
    .filter-btns {
        display: grid;
        grid-template-columns: 8rem 8rem;
        gap: 0.5rem;
        justify-content: right;
        padding-right: 0.25rem;
        padding-bottom: 0.5rem;
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
