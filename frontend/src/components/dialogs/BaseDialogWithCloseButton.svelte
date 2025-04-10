<script lang="ts">
    import type { Snippet } from "svelte";
    import BaseDialog from "./BaseDialog.svelte";

    const { dialogId = null, children } = $props<{
        dialogId?: string;
        children: Snippet;
    }>();

    export async function open() {
        await dialogElement.open();
    }

    export function close() {
        dialogElement.close();
    }
    let dialogElement: BaseDialog;
</script>

<BaseDialog {dialogId} bind:this={dialogElement}>
    <svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        class="close-button"
        onclick={close}
        aria-label="Close dialog"
        fill="none"
        role="button"
        tabindex="0"
    >
        <path
            d="M19.0005 4.99988L5.00049 18.9999M5.00049 4.99988L19.0005 18.9999"
            stroke="currentColor"
            stroke-width="3"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
    </svg>
    {@render children()}
</BaseDialog>

<style>
    :global(.dialog-content) {
        position: relative;
        padding: 0.25rem;
    }

    .close-button {
        position: absolute;
        top: 0.25rem;
        right: 0.25rem;
        width: 1.5rem;
        height: 1.5rem;
        padding: 0.25rem;
        border: none;
        border-radius: 0.25rem;
        background: transparent;
        background-color: var(--gray);
        color: var(--back-main);
        cursor: pointer;
        box-sizing: border-box;
        outline: none;
    }

    .close-button:hover {
        background-color: var(--err-red);
    }

    .close-button:active {
        transform: scale(0.96);
    }
</style>
