<script lang="ts">
    import type {
        QuoteContentItem,
        PostContentItem,
    } from "../../../../../../../ts/common/post-content-item";

    let {
        isEditing,
        initial,
        editingValue = $bindable<QuoteContentItem>(),
    }: {
        isEditing: boolean;
        initial: QuoteContentItem;
        editingValue: QuoteContentItem;
    } = $props<{
        isEditing: boolean;
        initial: QuoteContentItem;
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

<div class="quote">
    {#if isEditing}
        <textarea
            bind:value={editingValue.text}
            bind:this={editingEl}
            rows="1"
            oninput={() => adjustHeight()}
            placeholder="Quote text"
        />
        <label>Author: <input bind:value={editingValue.author} /> </label>
    {:else}
        <p class="quote-text">
            {initial.text}
        </p>
        {#if initial.author}
            <label class="quote-author"> {initial.author}</label>
        {/if}
    {/if}
</div>

<style>
    .quote {
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
        font-size: var(--post-content-paragraph-font-size);
        font-weight: var(--post-content-paragraph-font-weight);
    }

    .quote > label {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 0.5rem;
        color: var(--gray);
    }

    textarea,
    input {
        width: 100%;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.5rem;
        background-color: var(--back-main);
        color: var(--text-main);
        font-size: inherit;
        font-weight: inherit;
        box-sizing: border-box;
        outline: none;
    }

    textarea {
        resize: none;
    }

    textarea:hover,
    input:hover {
        border-color: var(--gray);
    }

    textarea:focus,
    input:focus {
        border-color: var(--accent-main);
        background-color: transparent;
    }

    .quote-text {
        max-width: 90%;
        padding: 0.5rem;
        margin: 0;
        border-radius: 0.5rem;
        background-color: var(--back-second);
        color: var(--text-main);
        font-size: inherit;
        font-weight: inherit;
        font-style: italic;
        align-self: center;
        box-sizing: border-box;
        overflow-wrap: break-word; 
        word-break: break-word; 
        text-indent: 1.5em;
    }

    .quote-author {
        color: var(--gray);
        align-self: flex-end;
        overflow-wrap: break-word; 
        word-break: break-word; 
    }
</style>
