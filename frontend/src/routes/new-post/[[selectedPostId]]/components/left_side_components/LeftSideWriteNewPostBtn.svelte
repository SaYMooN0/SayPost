<script lang="ts">
    import { onMount } from "svelte";
    import ErrView from "../../../../../components/err_blocks/ErrView.svelte";
    import type { Err } from "../../../../../ts/common/errs/err";

    const { createNewPost } = $props<{
        createNewPost: () => Promise<Err[] | void>;
    }>();
    let containerEl: HTMLElement;
    let creationErrs: Err[] = $state([]);
    async function onNewPostBtnClicked() {
        const res = await createNewPost();
        if (Array.isArray(res) && res.length != 0) {
            creationErrs = res;
        }
    }

    function handleClickOutside(event: MouseEvent) {
        if (!containerEl.contains(event.target as Node)) {
            creationErrs = [];
        }
    }

    onMount(() => {
        document.addEventListener("click", handleClickOutside);
        return () => {
            document.removeEventListener("click", handleClickOutside);
        };
    });
</script>

<div bind:this={containerEl} class="container">
    <button class="new-post-btn" onclick={() => onNewPostBtnClicked()}>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M5.07579 17C4.08939 4.54502 12.9123 1.0121 19.9734 2.22417C20.2585 6.35185 18.2389 7.89748 14.3926 8.61125C15.1353 9.38731 16.4477 10.3639 16.3061 11.5847C16.2054 12.4534 15.6154 12.8797 14.4355 13.7322C11.8497 15.6004 8.85421 16.7785 5.07579 17Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M4 22C4 15.5 7.84848 12.1818 10.5 10"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
        Write new post
    </button>
    {#if creationErrs.length != 0}
        <ErrView err={creationErrs[0]} />
    {/if}
</div>

<style>
    .container {
        display: flex;
        flex-direction: column;
    }

    .container>:global(.err-container){
        overflow: hidden auto;
        max-height: 4rem;
        margin-top: 0.5rem;
    }

    .new-post-btn {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 0.5rem;
        padding: 0.5rem;
        margin-top: 1rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.25rem;
        background-color: var(--back-second);
        color: var(--text);
    }

    .new-post-btn > svg {
        height: 2rem;
    }
</style>
