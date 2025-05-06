<script lang="ts">
    import type {
        HeadingContentItem,
        PostContentItem,
    } from "../../../../../../../ts/common/post-content-item";

    let {
        isEditing,
        contentItem,
        editingValue = $bindable<HeadingContentItem>(),
    }: {
        isEditing: boolean;
        contentItem: HeadingContentItem;
        editingValue: HeadingContentItem;
    } = $props<{
        isEditing: boolean;
        contentItem: HeadingContentItem;
        editingValue: PostContentItem;
    }>();
</script>

<div class="heading">
    {#if isEditing}
        <div>
            <label>Heading</label>
            <textarea bind:value={editingValue.value} />
        </div>
    {:else}
        <h2>{contentItem.value}</h2>
    {/if}
</div>

<style>
    .heading {
        display: flex;
        flex-direction: column;
        position: relative;
        gap: 0.5rem;
    }
</style>
