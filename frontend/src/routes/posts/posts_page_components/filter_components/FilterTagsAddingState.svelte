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
                            <div class="added">added</div>
                        {:else}
                            <div
                                onclick={() => selectedTags.push(tag)}
                                class="add-tag-btn"
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
        padding: 0.5rem;
        margin: 0.5rem 0;
        border: 0.125rem solid var(--gray);
        border-radius: 0.5rem;
        background-color: var(--back-main);
        box-sizing: border-box;
        display: flex;
        flex-direction: column;
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
    }

    .adding-state:has(.tags-grid) .searched-tags {
        display: grid;
        grid-template-columns: 1fr 1fr 1fr;
        gap: 0.5rem;
        padding: 0 0.5rem;
        box-sizing: border-box;
    }
    .top-elements {
        display: grid;
        grid-template-columns: 1fr auto;
        gap: 0.5rem;
    }
    input {
        width: 100%;
        box-sizing: border-box;
    }
    .close-btn {
        top: 0.25rem;
        right: 0.25rem;
        height: 1.25rem;
        padding: 0.125rem;
        border-radius: 1rem;
        background-color: var(--gray);
        color: var(--back-main);
        cursor: pointer;
        box-sizing: border-box;
        transition: all 0.06s ease-in;
    }
    .close-btn:hover {
        transform: scale(1.08);
    }
    .tag {
        height: 1.75rem;
        display: grid;
        align-items: center;
        grid-template-columns: 1fr 5rem;
        border-left: 0.125rem solid transparent;
        padding-left: 0.5rem;
        transition: all 0.08s ease-in;
    }
    .tag:hover {
        border-left-color: var(--accent-main);
    }
    .tag > div {
        height: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
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
</style>
