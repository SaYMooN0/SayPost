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
    let editingEl: HTMLTextAreaElement;

    function adjustHeight() {
        if (!editingEl) return;
        editingEl.style.height = "auto";
        editingEl.style.height = editingEl.scrollHeight + 4 + "px";
    }
    $effect(() => {
        isEditing;
        adjustHeight();
    });
</script>

<div class="heading">
    {#if isEditing}
        <textarea
            bind:value={editingValue.value}
            bind:this={editingEl}
            rows="1"
            oninput={() => adjustHeight()}
        />
    {:else}
        <h2>{initial.value}</h2>
    {/if}
</div>

<style>
    .heading {
        align-content: center;
        font-size: var(--post-content-heading-font-size);
        font-weight: var(--post-content-heading-font-weight);
    }

    textarea {
        width: 100%;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.5rem;
        background-color: var(--back-main);
        color: var(--text-main);
        font-size: inherit;
        font-weight: inherit;
        box-sizing: border-box;
        outline: none;
        resize: none;
        text-indent: 1.5em;
    }

    textarea:hover {
        border-color: var(--gray);
    }

    textarea:focus {
        border-color: var(--accent-main);
        background-color: transparent;
    }

    .heading > h2 {
        margin: 0;
        font-size: inherit;
        font-weight: inherit;
        overflow-wrap: anywhere;
        text-indent: 1.5em;
    }
</style>
