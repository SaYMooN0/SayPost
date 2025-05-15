<script lang="ts">
    let {
        color = $bindable(),
        labelText,
    }: { color: string; labelText: string } = $props<{
        color: string;
        labelText: string;
    }>();

    const colorPattern: string = "#[0-9a-fA-F]{6}";
</script>

<div class="container">
    <label>{labelText}</label>
    <input type="color" bind:value={color} class="color-picker" />
    <input
        type="text"
        bind:value={color}
        class="text-input"
        maxlength="7"
        pattern={colorPattern}
        placeholder="#000000"
    />
</div>

<style>
    .container {
        display: flex;
        flex-direction: column;
        width: fit-content;
        box-shadow: rgba(0, 0, 0, 0.2) 0px 1px 4px;
        align-items: center;
        border-radius: 0.5rem;
        box-sizing: border-box;
        width: 8rem;
        padding: 0.5rem;
    }
    .container:hover,
    .container:focus,
    .container:focus-within {
        transform: scale(1.02);
        box-shadow: rgba(0, 0, 0, 0.25) 0px 1px 4px;
    }
    label {
        font-size: 1rem;
    }
    .color-picker {
        width: 7rem;
        height: 7rem;
        padding: 0;
        border: none;
        border-radius: 0.5rem;
        background-color: transparent;
        cursor: pointer;
    }

    .text-input {
        margin-top: 0.25rem;
        width: 100%;
        font-size: 1rem;
        border: none;
        border-bottom: 0.125rem solid var(--back-second);
        background-color: var(--back-main);
        color: var(--text);
        outline: none;
        box-sizing: border-box;
    }
    .container:hover .text-input {
        border-color: var(--text-main);
    }
    .text-input:hover,
    .text-input:focus {
        border-color: var(--accent-main);
    }

    .text-input {
        color: var(--text-main);
    }
</style>
