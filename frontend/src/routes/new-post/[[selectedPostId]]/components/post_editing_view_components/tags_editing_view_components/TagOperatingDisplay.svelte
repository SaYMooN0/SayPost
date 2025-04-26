<script lang="ts">
    let {
        tag,
        isTagAdded,
        isTagRemovingState,
        btnOnClick,
    }: {
        tag: string;
        isTagAdded: boolean;
        isTagRemovingState: boolean;
        btnOnClick: () => void;
    } = $props<{
        tag: string;
        isTagAdded: boolean;
        isTagRemovingState: boolean;
        btnOnClick: () => void;
    }>();
</script>

<div class="tag-display">
    <span>#{tag}</span>
    {#if isTagRemovingState}
        <svg
            class="remove-btn"
            onclick={() => btnOnClick()}
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M20 12L4 12"
                stroke="currentColor"
                stroke-width="2.6"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    {:else if isTagAdded}
        <svg viewBox="0 0 24 24" fill="none">
            <path
                d="M5 14.5C5 14.5 6.5 14.5 8.5 18C8.5 18 14.0588 8.83333 19 7"
                stroke="currentColor"
                stroke-width="2.6"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    {:else}
        <svg
            class="add-btn"
            onclick={() => btnOnClick()}
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M12 4V20M20 12H4"
                stroke="currentColor"
                stroke-width="2.6"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    {/if}
</div>

<style>
    .tag-display {
        display: grid;
        align-items: center;
        width: 100%;
        padding: 0.25rem 0.5rem;
        border-radius: 0.25rem;
        background-color: var(--back-second);
        box-sizing: border-box;
        grid-template-columns: 1fr auto;
    }

    .tag-display span {
        font-size: 1.25rem;
        word-break: break-all;
    }

    .tag-display svg {
        height: 1.5rem;
        padding: 0.25rem;
        border-radius: 16%;
        background-color: var(--gray);
        color: var(--back-main);
        transition: all 0.08s ease-in;
        cursor: pointer;
        aspect-ratio: 1/1;
        box-sizing: border-box;
    }

    .add-btn:hover {
        background-color: var(--accent-main);
    }

    .remove-btn:hover {
        background-color: var(--err-red);
    }
</style>
