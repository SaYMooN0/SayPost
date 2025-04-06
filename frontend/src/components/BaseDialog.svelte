<script lang="ts">
    import type { Snippet } from "svelte";

    let { dialogId, children }: { dialogId: string; children: Snippet } =
        $props();

    let dialogElement: HTMLDialogElement;

    export function open() {
        dialogElement.showModal();
    }

    export function close() {
        dialogElement.close();
    }
</script>

<dialog bind:this={dialogElement} id={dialogId} class="base-dialog">
    {@render children()}
</dialog>

<style>
    .base-dialog {
        outline: none;
        border: none;
        border-radius: 10px;
        padding: 0;
        max-height: unset;
        max-width: unset;
    }
</style>
