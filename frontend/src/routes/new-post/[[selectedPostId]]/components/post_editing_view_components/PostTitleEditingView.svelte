<script lang="ts">
    import { tick } from "svelte";
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import { ApiMain } from "../../../../../ts/backend-services";
    import type { Err } from "../../../../../ts/common/errs/err";

    let {
        postId,
        title,
        updateParentValue,
    }: {
        postId: string;
        title: string;
        updateParentValue: (newTitle: string, newLastModified: Date) => void;
    } = $props<{
        postId: string;
        title: string;
        updateParentValue: (newTitle: string, newLastModified: Date) => void;
    }>();
    export function isInEditingState() {
        return isEditing;
    }

    let editableValue = $state("");
    let isEditing = $state(false);
    let editingErrs: Err[] = $state([]);
    let editingEl: HTMLTextAreaElement;

    async function saveTitleChanges() {
        const response = await ApiMain.fetchJsonResponse<{
            newTitle: string;
            newLastModified: Date;
        }>(
            `/draft-posts/${postId}/update-title`,
            ApiMain.requestJsonPostOptions(
                { newPostTitle: editableValue },
                "PATCH",
            ),
        );
        if (response.isSuccess) {
            isEditing = false;
            updateParentValue(
                response.data.newTitle,
                response.data.newLastModified,
            );
        } else {
            editingErrs = response.errors;
        }
    }
    async function startEditing() {
        isEditing = true;
        editableValue = title;
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

{#if isEditing}
    <div class="editing-state-container">
        <textarea
            bind:value={editableValue}
            bind:this={editingEl}
            rows="1"
            oninput={() => adjustHeight()}
        />
        <div class="btns-container">
            <button class="cancel-btn" onclick={() => (isEditing = false)}>
                Cancel
            </button>
            <button class="save-btn" onclick={() => saveTitleChanges()}>
                Save
            </button>
        </div>
        {#if editingErrs.length != 0}
            <DefaultErrBlock errList={editingErrs} />
        {/if}
    </div>
{:else}
    <h1 class="post-title">
        {title}
        <svg
            onclick={() => startEditing()}
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M14.0737 3.88545C14.8189 3.07808 15.1915 2.6744 15.5874 2.43893C16.5427 1.87076 17.7191 1.85309 18.6904 2.39232C19.0929 2.6158 19.4769 3.00812 20.245 3.79276C21.0131 4.5774 21.3972 4.96972 21.6159 5.38093C22.1438 6.37312 22.1265 7.57479 21.5703 8.5507C21.3398 8.95516 20.9446 9.33578 20.1543 10.097L10.7506 19.1543C9.25288 20.5969 8.504 21.3182 7.56806 21.6837C6.63212 22.0493 5.6032 22.0224 3.54536 21.9686L3.26538 21.9613C2.63891 21.9449 2.32567 21.9367 2.14359 21.73C1.9615 21.5234 1.98636 21.2043 2.03608 20.5662L2.06308 20.2197C2.20301 18.4235 2.27297 17.5255 2.62371 16.7182C2.97444 15.9109 3.57944 15.2555 4.78943 13.9445L14.0737 3.88545Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linejoin="round"
            />
            <path
                d="M13 4L20 11"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linejoin="round"
            />
            <path
                d="M14 22L22 22"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    </h1>
{/if}

<style>
    .editing-state-container {
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
        margin-top: 0.5rem;
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
        grid-template-columns: 1fr 1fr;
        justify-content: right;
        gap: 0.5rem;
    }

    .btns-container button {
        width: 8rem;
        height: 2rem;
        border: none;
        border-radius: 1rem;
        color: var(--back-main);
        font-size: 1.25rem;
        transition: all 0.08s ease-in;
        cursor: pointer;
        box-sizing: border-box;
    }

    .btns-container button:hover {
        letter-spacing: 2px;
    }

    .btns-container button:active {
        transform: scale(0.96);
    }

    .cancel-btn {
        background-color: var(--gray);
    }

    .save-btn {
        background-color: var(--accent-main);
    }

    .post-title {
        margin: 0.5rem 0;
        font-size: 2rem;
        word-break: break-all;
    }

    .post-title > svg {
        width: 2rem;
        height: 2rem;
        padding: 0.25rem;
        border: 0.125rem solid transparent;
        border-radius: 0.375rem;
        box-sizing: border-box;
        color: var(--accent-main);
        vertical-align: middle;
    }

    .post-title > svg:hover {
        border-color: var(--back-second);
        background-color: var(--back-second);
    }

    .post-title > svg:active {
        border-color: var(--accent-main);
    }
</style>
