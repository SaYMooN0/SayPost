<script lang="ts">
    let { name, isChosen, choose } = $props();
</script>

<div class="design-input" class:chosen={isChosen} onclick={() => choose()}>
    {name}
    <div class="selection-indicator" class:selected={isChosen}></div>
</div>

<style>
    .design-input {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
        gap: 0.25rem;
        width: 9rem;
        height: 3.25rem;
        font-size: 1rem;
        font-weight: 500;
        border-radius: 0.75rem;
        box-shadow: rgb(0 0 0 / 15%) 0 1px 4px;
        transition: all 0.04s ease-in;
        cursor: pointer;
        border: 0.125rem solid transparent;
    }

    .design-input:hover {
        box-shadow: rgb(0 0 0 / 25%) 0 1px 4px;
        transform: scale(1.02);
    }

    .design-input.chosen {
        box-shadow: rgb(43 15 189 / 35%) 0 1px 4px;
        transform: scale(1.06);
        border-color: var(--accent-main);
    }
</style>
