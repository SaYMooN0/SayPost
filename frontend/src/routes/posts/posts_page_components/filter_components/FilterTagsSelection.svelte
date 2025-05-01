<script lang="ts">
    import FilterTagsAddingState from "./FilterTagsAddingState.svelte";

    let { selectedTags = $bindable() }: { selectedTags: string[] } = $props<{
        selectedTags: string[];
    }>();

    let addingState: boolean = $state(false);
</script>

<div class="tags-selection">
    <div class="tags">
        {#each selectedTags as tag}
            <div class="tag">{tag}</div>
        {/each}

        <button class="add-tags-btn" onclick={() => (addingState = true)}>
            Add tags
        </button>
    </div>
    {#if addingState}
        <FilterTagsAddingState
            {selectedTags}
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
        padding: 0.25rem 0.75rem;
        border-radius: 6rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1rem;
    }
</style>
