<script lang="ts">
    import { onMount } from "svelte";
    import { CommentsSortOption } from "../../published-posts";

    let { sortOption = $bindable() }: { sortOption: CommentsSortOption } =
        $props<{
            sortOption: CommentsSortOption;
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
</style>
