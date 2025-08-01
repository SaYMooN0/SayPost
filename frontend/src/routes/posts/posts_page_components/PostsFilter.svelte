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
        isVisible = !isVisible;
        if (iconElement) {
            iconElement.classList.remove("rotate-down", "rotate-up");
            iconElement.classList.add(isVisible ? "rotate-down" : "rotate-up");
        }
    }
</script>

<div class="container">
    <p class="always-shown unselectable" class:filter-not-hidden={isVisible}>
        Posts filter
        <svg
            bind:this={iconElement}
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
            onclick={(e) => toggleVisibility(e)}
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
        <p class="filter-p">
            Include tags:
            {#if postsFilter.includeTags.length === 0}
                <span class="no-tags-selected">(No tags selected)</span>
            {/if}
        </p>
        <FilterTagsSelection bind:selectedTags={postsFilter.includeTags} />
        <p class="filter-p">
            Exclude tags:
            {#if postsFilter.excludeTags.length === 0}
                <span class="no-tags-selected">(No tags selected)</span>
            {/if}
        </p>
        <FilterTagsSelection bind:selectedTags={postsFilter.excludeTags} />
        <div class="filter-btns">
            <button class="reset" onclick={() => postsFilter.reset()}
                >Reset</button
            >
            <button class="apply" onclick={applyFilter}>Apply</button>
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
        font-weight: 400;
        transition: padding-left 0.2s ease-in;
    }

    .always-shown.filter-not-hidden {
        padding-left: 2rem;
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
        height: auto;
        padding: 0 1rem;
        margin: 0.5rem 0 0.75rem;
        border-radius: 1rem;
        background-color: var(--back-main);
        font-size: 1.25rem;
        opacity: 1;
        transition:
            all 0.2s ease-in,
            opacity 0.5s ease-out;
        interpolate-size: allow-keywords;
        box-sizing: border-box;
    }

    .hidden {
        height: 0;
        padding: 0;
        margin: 0;
        font-size: 0.25rem;
        opacity: 0;
        transition:
            all 0.2s ease-in,
            opacity 0.08s ease-out;
    }

    .filter-p {
        display: flex;
        align-items: center;
        gap: 0.5rem;
        margin: 0.75rem 0 0;
        color: var(--text-main);
        font-size: 1.25rem;
    }

    .filter :global(*){
        transition: inherit;
    }

    .hidden :global(*){
        height: 0;
        padding: 0;
        margin: 0;
        font-size: 0.25rem;

    }

    .filter-p input[type="date"] {
        padding: 0.25rem 0.75rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.5rem;
        background-color: var(--back-second);
        color: var(--text-main);
        font-size: 1rem;
        font-weight: 440;
        transition: border-color 0.15s ease;
        outline: none;
    }

    .filter-p input[type="date"]:hover {
        border-color: var(--gray);
    }

    .filter-p input[type="date"]:focus {
        border-color: var(--accent-main);
        background-color: transparent;
    }

    .no-tags-selected {
        color: var(--gray);
        font-style: italic;
        font-size: 1rem;
    }

    .filter-btns {
        display: grid;
        justify-content: right;
        gap: 0.5rem;
        padding-right: 0.25rem;
        padding-bottom: 0.5rem;
        grid-template-columns: 8rem 8rem;
    }

    .filter-btns button {
        padding: 0.25rem 0.5rem;
        margin-top: 1rem;
        border: none;
        border-radius: 1rem;
        font-size: 1.25rem;
        transition: all 0.12s ease-in;
        cursor: pointer;
        outline: none;
    }

    .reset {
        background-color: var(--gray);
        color: var(--back-main);
    }

    .apply {
        background-color: var(--accent-main);
        color: var(--back-main);
    }

    .apply:hover {
        background-color: var(--accent-hov);
    }

    .filter-btns button:hover {
        border-radius: 0.75rem;
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
