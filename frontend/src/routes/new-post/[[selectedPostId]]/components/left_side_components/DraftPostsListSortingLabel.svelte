<script lang="ts">
    import { onMount } from "svelte";
    import { DraftPostsSortOption } from "../../draft-posts";
    import DefaultSwitch from "../../../../../components/DefaultSwitch.svelte";

    let { sortOption = $bindable(), pinnedPostsOnTop = $bindable() } = $props<{
        sortOption: DraftPostsSortOption;
        pinnedPostsOnTop: boolean;
    }>();

    let isSelectMenuOpen = $state(false);
    let buttonElement: HTMLElement;
    function handleClickOutside(event: any) {
        if (!buttonElement.contains(event.target)) {
            isSelectMenuOpen = false;
        }
    }
    onMount(() => {
        document.addEventListener("click", handleClickOutside);
        return () => {
            document.removeEventListener("click", handleClickOutside);
        };
    });

    function sortOptionBtnClick(
        e: MouseEvent,
        oldestCreated: DraftPostsSortOption,
    ) {
        e.stopPropagation();
        sortOption = oldestCreated;
    }
</script>

<div
    class="sorting-label unselectable"
    onclick={() => (isSelectMenuOpen = !isSelectMenuOpen)}
    bind:this={buttonElement}
>
    Your draft posts:
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
        <path
            d="M11 8L21 8.00006"
            stroke="currentColor"
            stroke-width="1.7"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M11 12H21"
            stroke="currentColor"
            stroke-width="1.7"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M11 16H21"
            stroke="currentColor"
            stroke-width="1.7"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M11 4H21"
            stroke="currentColor"
            stroke-width="1.7"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M5.5 21V3M5.5 21C4.79977 21 3.49153 19.0057 3 18.5M5.5 21C6.20023 21 7.50847 19.0057 8 18.5"
            stroke="currentColor"
            stroke-width="1.7"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
    </svg>
    <div class="context-menu" class:open={isSelectMenuOpen}>
        <label class="sort-by-label">Sort by</label>
        <div
            onclick={(e) =>
                sortOptionBtnClick(e, DraftPostsSortOption.lastModified)}
            class="sort-option"
            class:chosen={sortOption == DraftPostsSortOption.lastModified}
        >
            Last modified
            <span></span>
        </div>
        <div
            onclick={(e) => sortOptionBtnClick(e, DraftPostsSortOption.title)}
            class="sort-option"
            class:chosen={sortOption == DraftPostsSortOption.title}
        >
            Title
            <span></span>
        </div>
        <div
            onclick={(e) =>
                sortOptionBtnClick(e, DraftPostsSortOption.lastCreated)}
            class="sort-option"
            class:chosen={sortOption == DraftPostsSortOption.lastCreated}
        >
            Last created
            <span></span>
        </div>
        <div
            onclick={(e) =>
                sortOptionBtnClick(e, DraftPostsSortOption.oldestCreated)}
            class="sort-option"
            class:chosen={sortOption == DraftPostsSortOption.oldestCreated}
        >
            Oldest created
            <span></span>
        </div>
        <div class="pinned-option-div">
            Put pinned posts on top:
            <DefaultSwitch bind:isChecked={pinnedPostsOnTop} />
        </div>
    </div>
</div>

<style>
    .sorting-label {
        position: relative;
        display: inline-flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        padding: 0.25rem 1.25rem;
        margin: 0.25em 0.75rem;
        color: var(--text-main);
        font-size: 1.25rem;
        font-weight: 500;
        transition: all 0.12s ease-in;
        box-sizing: border-box;
        border-bottom: 0.125rem solid transparent;
        cursor: pointer;
    }

    .sorting-label > svg {
        width: 2rem;
        height: 2rem;
        padding: 0.25rem;
        border-radius: 0.5rem;
        background-color: var(--back-second);
        box-sizing: border-box;
        transition: inherit;
    }

    .sorting-label:hover {
        padding: 0.25rem 1.5rem;
        border-color: var(--accent-main);
    }

    .sorting-label:hover > svg {
        color: var(--accent-hov);
    }

    .sorting-label:active {
        padding: 0.25rem 1.75rem;
        border-color: var(--accent-hov);
    }

    .sorting-label:active > svg {
        color: var(--accent-hov);
    }

    .context-menu {
        position: absolute;
        top: 110%;
        left: 0;
        z-index: 1000;
        display: none;
        width: 100%;
        padding: 0.25rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.5rem;
        background: var(--back-main);
        box-sizing: border-box;
    }

    .context-menu.open {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .context-menu.open > * {
        width: 100%;
        box-sizing: border-box;
    }

    .pinned-option-div {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        margin: 0.5rem;
        font-size: 1rem;
    }

    .sort-by-label {
        margin-left: 2rem;
        color: var(--gray);
        font-size: 1rem;
    }

    .sort-option {
        display: grid;
        align-items: center;
        padding: 0.25rem 0.5rem;
        border-radius: 0.25rem;
        transition: all 0.08s ease-in;
        grid-template-columns: 1fr auto;
    }

    .sort-option > span {
        display: inline;
        width: 0.875rem;
        height: 0.875rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.75rem;
        background-color: var(--back-second);
        transition: inherit;
        box-sizing: border-box;
    }

    .sort-option.chosen > span {
        border-color: var(--accent-main);
        background-color: var(--accent-main);
    }

    .sort-option:hover {
        padding-left: 0.75rem;
        background-color: var(--back-second);
    }

    .sort-option:hover > span {
        border-color: var(--accent-main);
    }
</style>
