<script lang="ts">
    import type {
        HeadingContentItem,
        PostContentItem,
    } from "../../../../../../../ts/common/post-content-item";

    let {
        isEditing,
        initial,
        editingValue = $bindable<HeadingContentItem>(),
    }: {
        isEditing: boolean;
        initial: HeadingContentItem;
        editingValue: HeadingContentItem;
    } = $props<{
        isEditing: boolean;
        initial: HeadingContentItem;
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
        <h2>{initial.value}</h2>
    {/if}
</div>

<style>
    .heading {
        position: relative;
        display: flex;
        flex-direction: column;

        gap: 0.5rem;
    }
    .heading:has(h2) {
        justify-content: center;
    }
    .heading > h2 {
        font-size: 2rem;
        margin: 0;
    }
</style>
