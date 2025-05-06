<script lang="ts">
    import type {
        ParagraphContentItem,
        PostContentItem
    } from "../../../../../../../ts/common/post-content-item";

    let {
        isEditing,
        contentItem,
        editingValue = $bindable<ParagraphContentItem>(),
    }: {
        isEditing: boolean;
        contentItem: ParagraphContentItem;
        editingValue: ParagraphContentItem;
    } = $props<{
        isEditing: boolean;
        contentItem: ParagraphContentItem;
        editingValue: PostContentItem;
    }>();
</script>

<div class="paragraph">
    {#if isEditing}
        <div>
            <label>Paragraph</label>
            <textarea bind:value={editingValue.value} />
        </div>
    {:else}
        <p>{contentItem.value}</p>
    {/if}
</div>

<style>
    .paragraph {
        display: flex;
        flex-direction: column;
        position: relative;
        gap: 0.5rem;
    }
</style>
