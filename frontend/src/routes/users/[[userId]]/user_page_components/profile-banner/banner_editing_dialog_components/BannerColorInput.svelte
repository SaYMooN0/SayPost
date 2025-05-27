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
        align-items: center;
        width: fit-content;
        width: 8rem;
        padding: 0.5rem;
        border-radius: 0.5rem;
        box-shadow: rgb(0 0 0 / 20%) 0 1px 4px;
        box-sizing: border-box;
    }

    .container:hover,
    .container:focus,
    .container:focus-within {
        box-shadow: rgb(0 0 0 / 25%) 0 1px 4px;
        transform: scale(1.02);
    }

    label {
        font-size: 1rem;
    }

    .color-picker {
        width: 5rem;
        height: 5rem;
        padding: 0;
        border: none;
        border-radius: 0.5rem;
        background-color: transparent;
        cursor: pointer;
    }

    .text-input {
        width: 100%;
        margin-top: 0.25rem;
        border: none;
        background-color: var(--back-main);
        color: var(--text-main);
        font-size: 1rem;
        border-bottom: 0.125rem solid var(--back-second);
        outline: none;
        box-sizing: border-box;
    }

    .container:hover .text-input {
        border-color: var(--text-main);
    }

    .text-input:focus,
    .text-input:hover {
        border-color: var(--accent-main);
    }
</style>
