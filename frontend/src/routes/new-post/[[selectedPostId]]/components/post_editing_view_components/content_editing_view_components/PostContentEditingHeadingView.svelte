<script lang="ts">
    import type { HeadingContentItem } from "../../../../../../ts/common/post-content-item";

    let {
        value,
        deleteContentItem,
    }: { value: string; deleteContentItem: () => void } = $props<{
        value: string;
            deleteContentItem: () => void;
    }>();
    let isEditing = $state(false);
    let editingValue = $state(value);

    export function getValue(): HeadingContentItem {
        return {
            value: isEditing ? editingValue : value,
            $type: "HeadingContentItem"
        };
    }

    function startEditing() {
        editingValue = value;
        isEditing = true;
    }
</script>

<div class="content-editing-heading">
    {#if isEditing}
        <label>Heading</label>
        <textarea bind:value={editingValue} />
    {:else}
        <h2>{value}</h2>
    {/if}
    <div class="right-side-btns">
        {#if isEditing}
            <button class="cancel-btn" on:click={() => (isEditing = false)}>Cancel</button>
        {:else}
            <button class="cancel-btn" on:click={startEditing}>Edit</button>
            <button class="cancel-btn" on:click={deleteContentItem}>Delete</button>
        {/if}
    </div>
</div>
