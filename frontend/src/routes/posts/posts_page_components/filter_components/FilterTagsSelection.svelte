<script lang="ts">
    import FilterTagsAddingState from "./FilterTagsAddingState.svelte";

    let { selectedTags = $bindable() }: { selectedTags: string[] } = $props<{
        selectedTags: string[];
    }>();

    let addingState: boolean = $state(false);
    function removeTag(tag: string) {
        selectedTags = selectedTags.filter((t) => t !== tag);
    }
</script>

<div class="tags-selection">
    <div class="tags">
        {#each selectedTags as tag}
            <div class="tag">
                {tag}
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                    onclick={() => removeTag(tag)}
                >
                    <path
                        d="M19.0005 4.99988L5.00049 18.9999M5.00049 4.99988L19.0005 18.9999"
                        stroke="currentColor"
                        stroke-width="2.4"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    />
                </svg>
            </div>
        {/each}

        {#if !addingState}
            <button class="add-tags-btn" onclick={() => (addingState = true)}>
                Add tags
            </button>
        {/if}
    </div>
    {#if addingState}
        <FilterTagsAddingState
            bind:selectedTags
            closeAddingState={() => (addingState = false)}
        />
    {/if}
</div>

<style>
    .tags {
        display: flex;
        flex-wrap: wrap;
        gap: 0.5rem;
    }

    .tag {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 0.125rem;
        padding: 0.25rem 0.75rem;
        border-radius: 6rem;
        background-color: var(--back-second);
        color: var(--gray);
        font-size: 1rem;
        font-weight: 420;
    }

    .tag svg {
        width: 1rem;
        height: 1rem;
        color: inherit;
        transition: 0.08s all ease-in;
        cursor: pointer;
    }

    .tag svg:hover {
        color: var(--text-main);
        transform: scale(1.04);
    }
</style>
