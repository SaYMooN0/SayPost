<script lang="ts">
    import DefaultErrBlock from "../../../../components/err_blocks/DefaultErrBlock.svelte";
    import { ApiMain } from "../../../../ts/backend-services";
    import type { Err } from "../../../../ts/common/errs/err";
    import { StringUtils } from "../../../../ts/common/utils/string-utils";

    let {
        selectedTags,
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
    <input
        placeholder="Search for tag..."
        bind:value={tagSearchInput}
        bind:this={inputEl}
        name={"tag-search-" + StringUtils.rndStr(12)}
    />
    <div class="searched-tags {searchedTags.length === 0 ? "no-tags" : "tags-grid"}">
        {#if searchedTags.length === 0}
            <p>Use input to add tags</p>
        {:else}
            {#each searchedTags as tag}
                <div class="tag">
                    {tag}
                    {#if selectedTags.includes(tag)}
                        <div>added</div>
                    {:else}
                        <div onclick={() => selectedTags.push(tag)}>add</div>
                    {/if}
                </div>
            {/each}
        {/if}
    </div>
    <DefaultErrBlock errList={searchingErrs} />
</div>

<style>
    .adding-state {
        position: relative;
        border: 0.125rem solid var(--gray);
        padding: 0.5rem;
        box-sizing: border-box;
    }
    .searched-tags{
        height: 10rem;
        display: none;
    }
    .adding-state:focus-within .no-tags{
        display: flex;
        align-items: center;
        justify-content: center;
    }
    .adding-state:has(.tags-grid) .searched-tags{
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 0.5rem;
    }
    
    .close-btn {
        position: absolute;
        top: 0.25rem;
        right: 0.25rem;
        height: 1.25rem;
        padding: 0.125rem;
        box-sizing: border-box;
        background-color: var(--gray);
        cursor: pointer;
        color: var(--back-main);
        border-radius: 1rem;
    }
    .tag {
        display: grid;
        grid-template-columns: 1fr 4rem;
    }
</style>
