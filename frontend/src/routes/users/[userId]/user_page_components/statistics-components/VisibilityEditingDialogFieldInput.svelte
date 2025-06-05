<script lang="ts">
    let {
        isVisibleToAll = $bindable(true),
        label,
    }: { isVisibleToAll: boolean; label: string } = $props<{
        isVisibleToAll: boolean;
        label: string;
    }>();
</script>

<div class="input-container">
    <label>{label}</label>
    <div class="btns">
        <button
            class="everyone-btn"
            class:selected={isVisibleToAll}
            onclick={() => (isVisibleToAll = true)}
        >
            Everyone
        </button>
        <button
            class="only-me-btn"
            class:selected={!isVisibleToAll}
            onclick={() => (isVisibleToAll = false)}
        >
            Only me
        </button>
    </div>
</div>

<style>
    .input-container {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        gap: 0.5rem;
    }

    .btns {
        display: inline-grid;
        grid-template-columns: 1fr 1fr;
        gap: 0.25rem;
    }

    .btns button {
        padding: 0.25rem 0.5rem;
        border: 0.125rem solid var(--gray);
        border-radius: 0.25rem;
        background-color: var(--back-main);
        color: var(--gray);
        transition: all 0.12s ease-in;
    }

    .btns button.selected {
        border-color: var(--accent-main);
        background-color: var(--accent-main);
        color: var(--back-main);
    }
</style>
