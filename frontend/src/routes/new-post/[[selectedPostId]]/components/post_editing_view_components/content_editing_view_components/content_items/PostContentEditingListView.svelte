<script lang="ts">
    import type {
        ListContentItem,
        PostContentItem,
    } from "../../../../../../../ts/common/post-content-item";

    let {
        isEditing,
        contentItem,
        editingValue = $bindable<ListContentItem>(),
    }: {
        isEditing: boolean;
        contentItem: ListContentItem;
        editingValue: ListContentItem;
    } = $props<{
        isEditing: boolean;
        contentItem: ListContentItem;
        editingValue: PostContentItem;
    }>();
</script>

<div class="list">
    {#if isEditing}
        <label>List</label>
        {#each contentItem.items as item, i}
            <input type="text" bind:value={editingValue.items[i]} />
        {/each}
    {:else}
        {#each contentItem.items as item}
            <p>- {item}</p>
        {/each}
    {/if}
</div>

<style>
    .list {
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
    }
</style>
