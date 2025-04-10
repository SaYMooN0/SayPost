<script lang="ts">
    import { tick, type Snippet } from "svelte";
    import { StringUtils } from "../../ts/string-utils";

    const { dialogId = StringUtils.rndStrWithPref("dialog-"), children } =
        $props<{
            dialogId?: string;
            children: Snippet;
        }>();

    let show = $state<boolean>(false);
    let dialogElement: HTMLDialogElement;

    export async function open() {
        show = true;
        await tick();
        dialogElement.showModal();
    }

    export function close() {
        dialogElement.close();
    }
</script>

{#if show}
    <dialog
        bind:this={dialogElement}
        id={dialogId}
        class="base-dialog"
        onclose={() => {
            show = false;
        }}
    >
        <div class="dialog-content">
            {@render children()}
        </div>
    </dialog>
{/if}

<style>
    .base-dialog {
        max-width: unset;
        max-height: unset;
        padding: 0;
        outline: none;
        border: none;
        background: none;
    }
</style>
