<script lang="ts">
    import type {
        SubheadingContentItem,
        PostContentItem,
    } from "../../../../../../../ts/common/post-content-item";

    let {
        isEditing,
        initial,
        editingValue = $bindable<SubheadingContentItem>(),
    }: {
        isEditing: boolean;
        initial: SubheadingContentItem;
        editingValue: SubheadingContentItem;
    } = $props<{
        isEditing: boolean;
        initial: SubheadingContentItem;
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

<div class="subheading">
    {#if isEditing}
        <textarea
            bind:value={editingValue.value}
            bind:this={editingEl}
            rows="1"
            oninput={() => adjustHeight()}
        />
    {:else}
        <h3>{initial.value}</h3>
    {/if}
</div>

<style>
    .subheading {
        position: relative;
        display: flex;
        flex-direction: column;
        justify-content: center;
        gap: 0.5rem;
        font-size: var(--post-content-subheading-font-size);
        font-weight: var(--post-content-subheading-font-weight);
    }

    textarea {
        width: 100%;
        padding: 0 0.5rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.25rem;
        background-color: var(--back-main);
        color: var(--text);
        font-size: inherit;
        font-weight: inherit;
        box-sizing: border-box;
        outline: none;
        resize: none;
    }

    textarea:hover {
        border-color: var(--gray);
    }

    textarea:focus {
        border-color: var(--accent-main);
        background-color: transparent;
    }

    .subheading > h3 {
        margin: 0;
        font-size: inherit;
        font-weight: inherit;
    }
</style>
