<script lang="ts">
    import type { ParagraphContentItem } from "../../../../../../ts/common/post-content-item";

    let {
        value,
        deleteContentItem,
    }: { value: string; deleteContentItem: () => void } = $props<{
        value: string;
        deleteContentItem: () => void;
    }>();
    let isEditing = $state(false);
    let editingValue = $state(value);
    export function getValue(): ParagraphContentItem {
        return {
            value: isEditing ? editingValue : value,
            $type: "ParagraphContentItem",
        };
    }
    function startEditing() {
        editingValue = value;
        isEditing = true;
    }
</script>

<div class="content-editing-paragraph">
    {#if isEditing}
        <label>Paragraph Content Item</label>
        <textarea bind:value={editingValue} />
    {:else}
        <p>{value}</p>
    {/if}
    <div class="right-side-btns">
        {#if isEditing}
            <button class="cancel-btn" onclick={() => (isEditing = false)}>
                Cancel
            </button>
        {:else}
            <button class="cancel-btn" onclick={startEditing}>Edit</button>
            <button class="cancel-btn" onclick={deleteContentItem}>
                Delete
            </button>
        {/if}
    </div>
</div>
