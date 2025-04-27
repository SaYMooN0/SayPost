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

    let editableValue = $state("");
    let isEditing = $state(false);
    let editingErrs: Err[] = $state([]);
    let editingEl: HTMLTextAreaElement;

    async function saveContentChanges() {
        const response = await ApiMain.fetchJsonResponse<{
            newContent: string;
            newLastModified: Date;
        }>(
            `/draft-posts/${postId}/update-content`,
            ApiMain.requestJsonPostOptions(
                { newPostTitle: editableValue },
                "PATCH",
            ),
        );
        if (response.isSuccess) {
            content = response.data.newContent;
            isEditing = false;
            updateParentValue(content, response.data.newLastModified);
        } else {
            editingErrs = response.errors;
        }
    }
    async function startEditing() {
        isEditing = true;
        editableValue = content;
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
    Post content <button onclick={startEditing}>Edit</button>
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
            <button class="cancel-btn" onclick={() => (isEditing = false)}>
                Cancel
            </button>
            <button class="save-btn" onclick={() => saveContentChanges()}>
                Save
            </button>
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
        font-size: 1.25rem;
        color: var(--gray);
        display: grid;
        grid-template-columns: auto 1fr;
        margin: 0.75rem 0 0.25rem 0;
        gap: 1.5rem;
        align-items: center;
    }
    .section-p > button {
        width: fit-content;
        font-size: 1rem;
        padding: 0.25rem 1.25rem;
        border-radius: 0.25rem;
        border: none;
        background-color: var(--accent-main);
        color: var(--back-main);
        transition: all 0.12s ease-in;
        cursor: pointer;
        outline: none;
    }
    .section-p > button:hover {
        background-color: var(--accent-hov);
    }
    .content {
        font-size: 2rem;
    }
</style>
