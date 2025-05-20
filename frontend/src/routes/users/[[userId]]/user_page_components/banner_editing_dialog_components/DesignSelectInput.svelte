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
        flex-direction: column;
        justify-content: center;
        align-items: center;
        gap: 0.25rem;
        width: 9rem;
        height: 3.25rem;
        border: 0.125rem solid transparent;
        border-radius: 0.75rem;
        font-size: 1rem;
        font-weight: 500;
        box-shadow: rgb(0 0 0 / 15%) 0 1px 4px;
        transition: all 0.04s ease-in;
        cursor: pointer;
    }

    .design-input:hover {
        box-shadow: rgb(0 0 0 / 25%) 0 1px 4px;
        transform: scale(1.02);
    }

    .design-input.chosen {
        border-color: var(--accent-main);
        box-shadow: rgb(43 15 189 / 35%) 0 1px 4px;
        transform: scale(1.06);
    }
</style>
