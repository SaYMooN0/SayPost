<script lang="ts">
    let {
        onlyForFollowers = $bindable(false),
        label,
    }: { onlyForFollowers: boolean; label: string } = $props<{
        onlyForFollowers: boolean;
        label: string;
    }>();
</script>

<div class="input-container">
    <label>{label}</label>
    <div class="btns unselectable">
        <button
            class="everyone-btn"
            class:selected={!onlyForFollowers}
            onclick={() => (onlyForFollowers = false)}
        >
            Everyone
        </button>
        <button
            class="only-followers-btn"
            class:selected={onlyForFollowers}
            onclick={() => (onlyForFollowers = true)}
        >
            Only followers
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
        gap: 0.5rem;
    }

    .btns button {
        padding: 0.25rem 0.5rem;
        border: 0.125rem solid var(--gray);
        border-radius: 0.25rem;
        background-color: var(--back-main);
        color: var(--gray);
        font-weight: 450;
        cursor: pointer;
    }

    .btns button:not(.selected):hover {
        border-color: var(--accent-main);
        color: var(--accent-main);
    }

    .btns button.selected {
        border-color: var(--accent-main);
        background-color: var(--accent-main);
        color: var(--back-main);
    }
</style>
