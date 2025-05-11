<script lang="ts">
    import type {
        ParagraphContentItem,
        PostContentItem,
    } from "../../../../../../../ts/common/post-content-item";

    let {
        isEditing,
        initial,
        editingValue = $bindable<ParagraphContentItem>(),
    }: {
        isEditing: boolean;
        initial: ParagraphContentItem;
        editingValue: ParagraphContentItem;
    } = $props<{
        isEditing: boolean;
        initial: ParagraphContentItem;
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

<div class="paragraph">
    {#if isEditing}
        <textarea
            bind:value={editingValue.value}
            bind:this={editingEl}
            rows="1"
            oninput={() => adjustHeight()}
        />
    {:else}
        <p>{initial.value}</p>
    {/if}
</div>

<style>
    .paragraph {
        align-content: center;
        font-size: var(--post-content-paragraph-font-size);
        font-weight: var(--post-content-paragraph-font-weight);
    }

    textarea {
        width: 100%;
        padding: 0 0.5rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.25rem;
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

    .paragraph > p {
        margin: 0;
        font-size: inherit;
        font-weight: inherit;
        overflow-wrap: anywhere;
        text-indent: 1.5em;
    }
</style>
