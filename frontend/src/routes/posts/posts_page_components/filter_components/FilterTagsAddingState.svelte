<script lang="ts">
    import DefaultErrBlock from "../../../../components/err_blocks/DefaultErrBlock.svelte";
    import { ApiMain } from "../../../../ts/backend-services";
    import type { Err } from "../../../../ts/common/errs/err";
    import { StringUtils } from "../../../../ts/common/utils/string-utils";

    let {
        selectedTags = $bindable(),
        closeAddingState,
    }: {
        selectedTags: string[];
        closeAddingState: () => void;
    } = $props<{
        selectedTags: string[];
        closeAddingState: () => void;
    }>();

    let searchedTags: string[] = $state([]);
    let tagSearchInput: string = $state("");
    let searchingErrs: Err[] = $state([]);
    let inputEl: HTMLInputElement;

    let debounceTimeout: ReturnType<typeof setTimeout> | null = null;

    $effect(() => {
        const value = tagSearchInput;

        if (value.trim() === "") {
            searchedTags = [];
            searchingErrs = [];
            return;
        }

        if (debounceTimeout) clearTimeout(debounceTimeout);

        debounceTimeout = setTimeout(async () => {
            const response = await ApiMain.fetchJsonResponse<{
                tags: string[];
            }>(`/post-tags/search/${encodeURIComponent(value)}`, {
                method: "GET",
            });

            if (response.isSuccess) {
                searchedTags = response.data.tags;
                searchingErrs = [];
            } else if (response) {
                searchingErrs = response.errors;
            }
        }, 260);
    });
</script>

<div class="adding-state">
    <div class="top-elements">
        <input
            placeholder="Search for tag..."
            bind:value={tagSearchInput}
            bind:this={inputEl}
            name={StringUtils.rndStrWithPref("tag-search-", 12)}
        />
        <svg
            class="close-btn"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            stroke-width="2.5"
            onclick={closeAddingState}
        >
            <path
                stroke-linecap="round"
                stroke-linejoin="round"
                d="M6 18L18 6M6 6l12 12"
            ></path>
        </svg>
        <div
            class="searched-tags {searchedTags.length === 0
                ? 'no-tags'
                : 'tags-grid'}"
        >
            {#if searchedTags.length === 0}
                <p>Use input to add tags</p>
            {:else}
                {#each searchedTags as tag}
                    <div class="tag">
                        {tag}
                        {#if selectedTags.includes(tag)}
                            <div class="added unselectable">added</div>
                        {:else}
                            <div
                                onclick={() => selectedTags.push(tag)}
                                class="add-tag-btn unselectable"
                            >
                                add
                            </div>
                        {/if}
                    </div>
                {/each}
            {/if}
        </div>
    </div>
    <DefaultErrBlock errList={searchingErrs} />
</div>

<style>
    .adding-state {
        display: flex;
        flex-direction: column;
        padding: 0.5rem;
        margin: 0.5rem 0;
        border: 0.125rem solid var(--gray);
        border-radius: 0.5rem;
        background-color: var(--back-main);
        box-sizing: border-box;
    }

    .adding-state:focus-within,
    .adding-state:hover {
        border-color: var(--accent-main);
    }

    .searched-tags {
        display: none;
        height: 10rem;
    }

    .adding-state:focus-within .no-tags {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 2rem;
    }

    .adding-state:has(.tags-grid) .searched-tags {
        display: grid;
        gap: 0.125rem;
        height: fit-content;
        padding: 0 0.5rem;
        grid-template-columns: 1fr 1fr;
        box-sizing: border-box;
    }

    .top-elements {
        display: grid;
        grid-template-columns: 1fr auto;
        align-items: center;
        gap: 0.5rem;
    }

    input {
        width: 100%;
        box-sizing: border-box;
        padding: 0.125rem 0.5rem;
        outline: none;
        border: 0.125rem solid var(--gray);
        border-radius: 1.25rem;
        font-size: 1.25rem;
        transition: all 0.08s ease-in;
    }

    input:hover {
        border-color: var(--accent-main);
        border-radius: 0.875rem;
    }

    input:focus,
    input:active {
        border-color: var(--accent-hov);
        border-radius: 0.875rem;
    }

    .close-btn {
        top: 0.25rem;
        right: 0.25rem;
        height: 1.375rem;
        padding: 0.125rem;
        border-radius: 1rem;
        background-color: var(--gray);
        color: var(--back-main);
        transition: all 0.06s ease-in;
        cursor: pointer;
        box-sizing: border-box;
    }

    .close-btn:hover {
        transform: scale(1.08);
    }

    .tag {
        display: grid;
        align-items: center;
        padding: 0.125rem 0.5rem;
        font-size: 1.25rem;
        transition: all 0.08s ease-in;
        transition: border-left-color 0.02s ease-in;
        animation: show-tags 0.2s ease-in;
        grid-template-columns: 1fr 5rem;
        border-left: 0.125rem solid transparent;
        box-sizing: border-box;
    }

    .tag:hover {
        border-left-color: var(--accent-main);
    }

    .tag > div {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100%;
        border-radius: 0.25rem;
        cursor: default;
    }

    .added {
        background-color: var(--back-second);
        color: var(--gray);
    }

    .add-tag-btn {
        background-color: var(--accent-main);
        color: var(--back-main);
    }

    .add-tag-btn:hover {
        background-color: var(--accent-hov);
    }

    @keyframes show-tags {
        0% {
            font-size: 0;
            opacity: 0.1;
        }

        60% {
            font-size: 0.75rem;
            opacity: 0.2;
        }

        75% {
            font-size: 1rem;
        }
    }
</style>
