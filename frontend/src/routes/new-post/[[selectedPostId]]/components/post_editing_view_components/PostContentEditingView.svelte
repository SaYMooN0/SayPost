<script lang="ts">
    import { tick } from "svelte";
    import { ApiMain } from "../../../../../ts/backend-services";
    import type { Err } from "../../../../../ts/common/errs/err";
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";

    let {
        postId,
        content,
        updateParentValue,
    }: {
        postId: string;
        content: string;
        updateParentValue: (newContent: string, newLastModified: Date) => void;
    } = $props<{
        postId: string;
        content: string;
        updateParentValue: (newContent: string, newLastModified: Date) => void;
    }>();
    export function isInEditingState() {
        return isEditing;
    }
    let editableValue = $state("");
    let isEditing = $state(false);
    let editingErrs: Err[] = $state([]);
    let editingEl: HTMLTextAreaElement;

    async function saveContentChanges() {
        const response = await ApiMain.fetchJsonResponse<{
            newPostContent: string;
            newLastModified: Date;
        }>(
            `/draft-posts/${postId}/update-content`,
            ApiMain.requestJsonPostOptions(
                { newPostContent: editableValue },
                "PATCH",
            ),
        );
        if (response.isSuccess) {
            content = response.data.newPostContent;
            isEditing = false;
            updateParentValue(content, response.data.newLastModified);
        } else {
            editingErrs = response.errors;
        }
    }
    async function startEditing() {
        isEditing = true;
        editableValue = content;
        editingErrs = [];
        await tick();
        adjustHeight();
        editingEl.focus();
    }

    function adjustHeight() {
        if (!editingEl) return;
        editingEl.style.height = "auto";
        editingEl.style.height = editingEl.scrollHeight + "px";
    }
</script>

<p class="section-p">
    Post content
    {#if !isEditing}
        <button onclick={startEditing}>Edit</button>
    {/if}
</p>
{#if isEditing}
    <div class="editing-state-container">
        <textarea
            bind:value={editableValue}
            bind:this={editingEl}
            rows="1"
            class="autosize-textarea"
            oninput={() => adjustHeight()}
        />
        <div class="btns-container">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                class="cancel-btn"
                onclick={() => (isEditing = false)}
                fill="none"
            >
                <path
                    d="M18 6L12 12M12 12L6 18M12 12L18 18M12 12L6 6"
                    stroke="currentColor"
                    stroke-width="2"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                ></path>
            </svg>
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                class="save-btn"
                onclick={() => saveContentChanges()}
                fill="none"
            >
                <path
                    d="M5 14.5C5 14.5 6.5 14.5 8.5 18C8.5 18 14.0588 8.83333 19 7"
                    stroke="currentColor"
                    stroke-width="2"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                ></path>
            </svg>
        </div>
        {#if editingErrs.length != 0}
            <DefaultErrBlock errList={editingErrs} />
        {/if}
    </div>
{:else}
    <p class="content">{content}</p>
{/if}

<style>
    .section-p {
        display: grid;
        align-items: center;
        gap: 1.5rem;
        margin: 0.75rem 0 0.25rem;
        color: var(--gray);
        font-size: 1.25rem;
        grid-template-columns: auto 1fr;
    }

    .section-p > button {
        width: fit-content;
        padding: 0.25rem 1.25rem;
        border: none;
        border-radius: 0.25rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1rem;
        transition: all 0.12s ease-in;
        cursor: pointer;
        outline: none;
    }

    .section-p > button:hover {
        background-color: var(--accent-hov);
    }

    .editing-state-container {
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
        margin: 0.5rem 0;
    }

    .editing-state-container > textarea {
        border: 0.125rem solid var(--back-second);
        border-radius: 0.25rem;
        background-color: var(--back-second);
        color: var(--text);
        font-size: 2rem;
        outline: none;
        resize: none;
        font-weight: 600;
    }

    .editing-state-container > textarea:focus {
        border-color: var(--accent-main);
        background-color: transparent;
    }

    .btns-container {
        display: flex;
        flex-direction: row;
        grid-template-columns: auto auto;
        justify-content: right;
        gap: 0.5rem;
        padding-left: auto;
    }

    .btns-container svg {
        width: 1.5rem;
        height: 1.5rem;
        padding: 1px;
        border: none;
        border-radius: 0.25rem;
        color: var(--back-main);
        cursor: pointer;
        box-sizing: border-box;
    }

    .cancel-btn {
        background-color: var(--gray);
    }

    .cancel-btn:hover {
        background-color: var(--err-red);
    }

    .save-btn {
        background-color: var(--accent-main);
    }

    .save-btn:hover {
        background-color: var(--accent-hov);
    }

    .content {
        margin: 0.25rem 0;
        font-size: 2rem;
    }
</style>
