<script lang="ts">
    import { onMount } from "svelte";
    import { ApiMain } from "../../../../../ts/backend-services";
    import type { Err } from "../../../../../ts/common/errs/err";

    let {
        updatePostPinnedState,
        deletePost,
    }: {
        updatePostPinnedState: (
            postId: string,
            newIsPinnedState: boolean,
        ) => void;
        deletePost: (id: string) => Promise<void | Err[]>;
    } = $props<{
        updatePostPinnedState: (
            postId: string,
            newIsPinnedState: boolean,
        ) => void;
        deletePost: (id: string) => Promise<void | Err[]>;
    }>();
    async function onPinToggle() {
        if (!currentPostData) {
            return;
        }
        const url =
            `/draft-posts/${currentPostData.id}/` + currentPostData.isPinned
                ? "unpin"
                : "pin";
        const response = await ApiMain.fetchJsonResponse<{
            isPostPinned: boolean;
        }>(url, { method: "PATCH" });
        if (response.isSuccess) {
            updatePostPinnedState(
                currentPostData.id,
                response.data.isPostPinned,
            );
        } else {
        }
    }
    async function onDelete() {
        if (!currentPostData) {
            return;
        }
        await deletePost(currentPostData.id);
        close();
    }

    let currentPostData: { id: string; isPinned: boolean } | null =
        $state(null);
    let x = $state(0);
    let y = $state(0);

    export function open(
        event: MouseEvent,
        postData: { id: string; isPinned: boolean },
    ) {
        event.stopPropagation();

        currentPostData = postData;
        x = event.clientX + 10;
        y = event.clientY;
    }

    export function close() {
        currentPostData = null;
    }

    function handleClick(event: any) {
        if (contextMenu && !contextMenu.contains(event.target as Node)) {
            currentPostData = null;
        }
    }
    let contextMenu: HTMLElement;

    onMount(() => {
        document.addEventListener("click", handleClick);
        return () => {
            document.removeEventListener("click", handleClick);
        };
    });
</script>

{#if currentPostData}
    <div
        class="context-menu"
        style="top: {y}px; left: {x}px;"
        bind:this={contextMenu}
    >
        <button onclick={onPinToggle} class="action-btn">
            {#if currentPostData.isPinned}
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M7.5 8C6.95863 8.1281 6.49932 8.14239 5.99268 8.45891C5.07234 9.03388 4.85108 9.71674 5.08821 10.7612C5.94028 14.5139 9.48599 18.0596 13.2388 18.9117C14.2834 19.1489 14.9661 18.928 15.5416 18.0077C15.8411 17.5288 15.8716 17.0081 16 16.5"
                        stroke="currentColor"
                        stroke-width="1.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M12 7.79915C12.1776 7.77794 12.3182 7.74034 12.4295 7.68235C13.3997 7.17686 13.9291 5.53361 14.4498 4.60009C14.9311 3.73715 15.1718 3.30567 15.7379 3.10227C16.3041 2.89888 16.6448 3.02205 17.3262 3.26839C18.9197 3.8445 20.1555 5.08032 20.7316 6.6738C20.9779 7.35521 21.1011 7.69591 20.8977 8.26204C20.6943 8.82817 20.2628 9.06884 19.3999 9.55018C18.4608 10.074 16.7954 10.6108 16.2905 11.5898C16.2345 11.6983 16.1978 11.8327 16.1769 12"
                        stroke="currentColor"
                        stroke-width="1.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M3 21L8 16"
                        stroke="currentColor"
                        stroke-width="1.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M3 3L21 21"
                        stroke="currentColor"
                        stroke-width="1.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                </svg>
                Unpin
            {:else}
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M3 21L8 16"
                        stroke="currentColor"
                        stroke-width="1.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M13.2585 18.8714C9.51516 18.0215 5.97844 14.4848 5.12853 10.7415C4.99399 10.1489 4.92672 9.85266 5.12161 9.37197C5.3165 8.89129 5.55457 8.74255 6.03071 8.44509C7.10705 7.77265 8.27254 7.55888 9.48209 7.66586C11.1793 7.81598 12.0279 7.89104 12.4512 7.67048C12.8746 7.44991 13.1622 6.93417 13.7376 5.90269L14.4664 4.59604C14.9465 3.73528 15.1866 3.3049 15.7513 3.10202C16.316 2.89913 16.6558 3.02199 17.3355 3.26771C18.9249 3.84236 20.1576 5.07505 20.7323 6.66449C20.978 7.34417 21.1009 7.68401 20.898 8.2487C20.6951 8.8134 20.2647 9.05346 19.4039 9.53358L18.0672 10.2792C17.0376 10.8534 16.5229 11.1406 16.3024 11.568C16.0819 11.9955 16.162 12.8256 16.3221 14.4859C16.4399 15.7068 16.2369 16.88 15.5555 17.9697C15.2577 18.4458 15.1088 18.6839 14.6283 18.8786C14.1477 19.0733 13.8513 19.006 13.2585 18.8714Z"
                        stroke="currentColor"
                        stroke-width="1.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                </svg>
                Pin
            {/if}
        </button>
        <button onclick={onDelete} class="action-btn delete-btn">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M19.5 5.5L18.8803 15.5251C18.7219 18.0864 18.6428 19.3671 18.0008 20.2879C17.6833 20.7431 17.2747 21.1273 16.8007 21.416C15.8421 22 14.559 22 11.9927 22C9.42312 22 8.1383 22 7.17905 21.4149C6.7048 21.1257 6.296 20.7408 5.97868 20.2848C5.33688 19.3626 5.25945 18.0801 5.10461 15.5152L4.5 5.5"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                ></path>
                <path
                    d="M3 5.5H21M16.0557 5.5L15.3731 4.09173C14.9196 3.15626 14.6928 2.68852 14.3017 2.39681C14.215 2.3321 14.1231 2.27454 14.027 2.2247C13.5939 2 13.0741 2 12.0345 2C10.9688 2 10.436 2 9.99568 2.23412C9.8981 2.28601 9.80498 2.3459 9.71729 2.41317C9.32164 2.7167 9.10063 3.20155 8.65861 4.17126L8.05292 5.5"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                ></path>
                <path
                    d="M9.5 16.5L9.5 10.5"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                ></path>
                <path
                    d="M14.5 16.5L14.5 10.5"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                ></path>
            </svg>
            Delete
        </button>
    </div>
{/if}

<style>
    .context-menu {
        width: 8rem;
        position: fixed;
        z-index: 9999;
        display: flex;
        flex-direction: column;
        padding: 0.25rem;
        box-sizing: border-box;
        border: 1px solid var(--back-second);
        border-radius: 0.5rem;
        background: var(--back-main);
        box-shadow: 0 0.25rem 0.5rem rgb(0 0 0 / 10%);
    }

    .action-btn {
        padding: 0.125rem;
        border: none;
        background: none;
        font-size: 1rem;
        cursor: pointer;
        box-sizing: border-box;
        border-radius: 0.25rem;
        display: grid;
        grid-template-columns: 1.5rem 1fr;
        text-align: left;
        width: 100%;
        display: grid;
        align-items: center;
        color: var(--text);
        gap: 0.125rem;
        transition: all 0.1s ease-in;
    }
    .action-btn > svg {
        color: var(--accent-main);
        padding: 0.125rem;
    }
    .action-btn:hover {
        gap: 0.25rem;
        color: var(--accent-main);
        background-color: var(--back-second);
    }
    .delete-btn > svg {
        color: var(--err-red);
    }
    .delete-btn:hover {
        color: var(--err-red);
        background-color: var(--err-red-back);
    }
</style>
