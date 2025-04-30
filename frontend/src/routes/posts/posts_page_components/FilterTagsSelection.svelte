<script lang="ts">
    import { ApiMain } from "../../../ts/backend-services";
    import type { Err } from "../../../ts/common/errs/err";
    import { StringUtils } from "../../../ts/common/utils/string-utils";

    let { selectedTags = $bindable() }: { selectedTags: string[] } = $props<{
        selectedTags: string[];
    }>();

    let tagSearchInput: string = $state("");
    let searchedTags: string[] = $state([]);
    let searchingErrs: Err[] = $state([]);
    let inputEl: HTMLInputElement;

    let debounceTimeout: ReturnType<typeof setTimeout> | null = null;

    $effect(() => {
        const value = tagSearchInput;

        if (value.trim() === "") {
            searchedTags = [];
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

<div>
    <div class="tags">
        {#each selectedTags as tag}
            <div class="tag">{tag}</div>
        {/each}
    </div>
    <input
        placeholder="Search for tag..."
        bind:value={tagSearchInput}
        bind:this={inputEl}
        name={"tag-search-" + StringUtils.rndStr(12)}
    />
    <svg
        class="reset-btn"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
        stroke-width="2.5"
        onclick={() => (tagSearchInput = "")}
    >
        <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M6 18L18 6M6 6l12 12"
        ></path>
    </svg>
</div>
<style>
    .reset-btn{
        height: 1.5rem;
        width:  1.5rem;
    }
</style>
