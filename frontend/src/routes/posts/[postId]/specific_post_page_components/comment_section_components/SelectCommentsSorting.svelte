<script lang="ts">
    import { onMount } from "svelte";
    import { CommentsSortOption } from "../../published-posts";

    let {
        sortOption = $bindable(),
        commentsCount,
    }: { sortOption: CommentsSortOption; commentsCount: number } = $props<{
        sortOption: CommentsSortOption;
        commentsCount: number;
    }>();
    let isSelectMenuOpen = $state(false);
    let buttonElement: HTMLElement;
    function sortOptionBtnClick(e: MouseEvent, newOption: CommentsSortOption) {
        e.stopPropagation();
        sortOption = newOption;
    }
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
</script>

<div
    class="sorting-label unselectable"
    onclick={() => (isSelectMenuOpen = !isSelectMenuOpen)}
    bind:this={buttonElement}
>
    {commentsCount} comments
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
        <path
            d="M11.0001 8L19.0001 8.00006"
            stroke="currentColor"
            stroke-width="1.8"
            stroke-linecap="round"
            stroke-linejoin="round"
        ></path>
        <path
            d="M11.0001 12H16.0001"
            stroke="currentColor"
            stroke-width="1.8"
            stroke-linecap="round"
            stroke-linejoin="round"
        ></path>
        <path
            d="M11.0001 16H14.0001"
            stroke="currentColor"
            stroke-width="1.8"
            stroke-linecap="round"
            stroke-linejoin="round"
        ></path>
        <path
            d="M11.0001 4H21.0001"
            stroke="currentColor"
            stroke-width="1.8"
            stroke-linecap="round"
            stroke-linejoin="round"
        ></path>
        <path
            d="M5.5 21V3M5.5 21C4.79977 21 3.49153 19.0057 3 18.5M5.5 21C6.20023 21 7.50847 19.0057 8 18.5"
            stroke="currentColor"
            stroke-width="1.8"
            stroke-linecap="round"
            stroke-linejoin="round"
        ></path>
    </svg>
    <div class="context-menu" class:open={isSelectMenuOpen}>
        <div
            onclick={(e) => sortOptionBtnClick(e, CommentsSortOption.Newest)}
            class="sort-option"
            class:chosen={sortOption == CommentsSortOption.Newest}
        >
            Newest first
            <span></span>
        </div>
        <div
            onclick={(e) => sortOptionBtnClick(e, CommentsSortOption.Oldest)}
            class="sort-option"
            class:chosen={sortOption == CommentsSortOption.Oldest}
        >
            Oldest first
            <span></span>
        </div>
    </div>
</div>

<style>
    .sorting-label {
        position: relative;
        display: inline-flex;
        flex-direction: row;
        align-items: center;
        gap: 0.5rem;
        margin-top: 0.5rem;
        font-size: 1.25rem;
        font-weight: 500;
    }

    .sorting-label > svg {
        width: 2rem;
        height: 2rem;
        padding: 0.25rem;
        border-radius: 0.5rem;
        background-color: var(--back-second);
        box-sizing: border-box;
    }

    .sorting-label > svg:hover {
        color: var(--accent-hov);
    }

    .sorting-label > svg:active {
        color: var(--accent-hov);
    }

    .context-menu {
        position: absolute;
        top: -50%;
        left: calc(100% + 0.5rem);
        z-index: 1000;
        display: none;
        width: 14rem;
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
