<script lang="ts">
    import type { Snippet } from "svelte";
    import { StringUtils } from "../../../ts/string-utils";

    let {
        type = "text",
        fieldName,
        value = $bindable(),
        children,
    } = $props<{
        type?: string;
        fieldName: string;
        value: string;
        children: Snippet;
    }>();
    let inputName = StringUtils.rndStr(8);
</script>

<div class="input-field unselectable">
    <input
        required
        autocomplete="off"
        {type}
        name={inputName}
        placeholder=" "
        bind:value
    />
    <label for={inputName}>
        {@render children?.()}
        {fieldName}
    </label>
</div>

<style>
    .input-field {
        position: relative;
        width: 22rem;
        margin: 0.5rem 0;
    }

    .input-field label {
        position: absolute;
        top: 50%;
        left: 1rem;
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 0.25rem;
        padding: 0;
        padding: 0 0.125rem;
        background-color: transparent;
        color: var(--gray);
        transition: all 0.3s ease;
        transform: translateY(-50%);
        pointer-events: none;
    }

    :global(.input-field label > svg) {
        height: 1.25rem;
    }

    .input-field input {
        width: 100%;
        box-sizing: border-box;
        padding: 0.75rem 1rem;
        border: solid 0.125rem var(--gray);
        border-radius: 8px;
        background-color: var(--back-main);
        font-size: 1rem;
        letter-spacing: 1px;
        outline: none;
    }

    .input-field input:focus,
    .input-field input:not(:placeholder-shown) {
        border-color: var(--accent-main);
    }

    .input-field input:focus ~ label,
    .input-field input:not(:placeholder-shown) ~ label {
        top: 0;
        left: 1rem;
        border: none;
        border-radius: 100px;
        background-color: var(--back-main);
        color: var(--accent-main);
        transform: translateY(-50%);
        letter-spacing: 1px;
    }
</style>
