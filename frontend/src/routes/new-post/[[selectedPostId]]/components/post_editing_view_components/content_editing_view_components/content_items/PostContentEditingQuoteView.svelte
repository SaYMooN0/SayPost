<script lang="ts">
    import type {
        QuoteContentItem,
        PostContentItem,
    } from "../../../../../../../ts/common/post-content-item";

    let {
        isEditing,
        contentItem,
        editingValue = $bindable<QuoteContentItem>(),
    }: {
        isEditing: boolean;
        contentItem: QuoteContentItem;
        editingValue: QuoteContentItem;
    } = $props<{
        isEditing: boolean;
        contentItem: QuoteContentItem;
        editingValue: PostContentItem;
    }>();
</script>

<div class="quote">
    {#if isEditing}
        <label>Quote text</label>
        <textarea disabled bind:value={editingValue.text} />
    {:else}
        <p>
            {contentItem.text}
        </p>
        <label>

            {contentItem.author}
        </label>    
    {/if}
</div>

<style>
    .quote {
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
    }
</style>
