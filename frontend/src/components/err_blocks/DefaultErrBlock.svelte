<script lang="ts">
    import type { PlainErrType } from "../../ts/common/errs/t-plain-err";
    import ErrView from "./ErrView.svelte";

    let { errList }: { errList: PlainErrType[] } = $props();
</script>

{#if errList.length > 0}
    <div class="err-block">
        {#each errList as err}
            <ErrView {err} />
        {/each}
    </div>
{/if}

<style>
    .err-block {
        display: flex;
        flex-direction: column;
        gap: 0.25rem;
        width: 100%;
        border-radius: 0.5rem;
        box-sizing: border-box;
    }
</style>
