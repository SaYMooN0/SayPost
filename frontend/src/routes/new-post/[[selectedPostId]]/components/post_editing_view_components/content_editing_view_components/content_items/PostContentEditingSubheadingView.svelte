<script lang="ts">
    import type {
        SubheadingContentItem,
        PostContentItem
    } from "../../../../../../../ts/common/post-content-item";

    let {
        isEditing,
        contentItem,
        editingValue = $bindable<SubheadingContentItem>(),
    }: {
        isEditing: boolean;
        contentItem: SubheadingContentItem;
        editingValue: SubheadingContentItem;
    } = $props<{
        isEditing: boolean;
        contentItem: SubheadingContentItem;
        editingValue: PostContentItem;
    }>();
</script>

<div class="subheading">
    {#if isEditing}
        <div>
            <label>Subheading</label>
            <textarea bind:value={editingValue.value} />
        </div>
    {:else}
        <h3>{contentItem.value}</h3>
    {/if}
</div>

<style>
    .subheading {
        position: relative;
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
    }
</style>
