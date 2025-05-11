<script lang="ts">
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import { ApiMain } from "../../../../../ts/backend-services";
    import type { Err } from "../../../../../ts/common/errs/err";
    import type { PostComment } from "../../published-posts";

    let commentValue = $state("");
    let saveErrs: Err[] = $state([]);
    let inputEl: HTMLTextAreaElement;
    let {
        postId,
        updateParentValue,
    }: {
        postId: string;
        updateParentValue: (newComment: PostComment) => void;
    } = $props<{
        postId: string;
        updateParentValue: (newComment: PostComment) => void;
    }>();
    async function saveComment() {
        const response = await ApiMain.fetchJsonResponse<PostComment>(
            `/posts/${postId}/comment`,
            ApiMain.requestJsonPostOptions({ content: commentValue }, "POST"),
        );
        if (response.isSuccess) {
            commentValue = "";
            updateParentValue(response.data);
            saveErrs = [];
        } else {
            saveErrs = response.errors;
        }
    }

    function onCommentInput() {
        if (saveErrs.length != 0) {
            saveErrs = [];
        }
        if (!inputEl) return;
        inputEl.style.height = "auto";
        inputEl.style.height = inputEl.scrollHeight + "px";
    }
    function cancelEditing() {
        commentValue = "";
        saveErrs = [];
    }
</script>

<div class="container">
    <textarea
        placeholder="Add a comment..."
        bind:value={commentValue}
        bind:this={inputEl}
        rows="1"
        oninput={() => onCommentInput()}
    />
    <div class="btns-container">
        <button class="cancel-btn" onclick={cancelEditing}> Cancel </button>
        <button class="save-btn" onclick={() => saveComment()}>
            Add a comment
        </button>
    </div>
    <DefaultErrBlock errList={saveErrs} />
</div>

<style>
    .container {
        width: 100%;
        gap: 0.5rem;
        display: flex;
        flex-direction: column;
    }

    textarea {
        width: 100%;
        border: none;
        border-bottom: 0.125rem solid var(--gray);
        background-color: var(--back-second);
        color: var(--text-main);
        font-size: 1.25rem;
        outline: none;
        padding: 0.125rem 0.25rem 0 0.25rem;
        border-radius: 0.25rem 0.25rem 0.125rem 0.125rem;
        resize: none;
    }
    textarea:focus {
        border-color: var(--accent-main);
        background-color: transparent;
    }

    .btns-container {
        flex-direction: row;
        grid-template-columns: 1fr 1fr;
        justify-content: right;
        gap: 0.5rem;
        height: 0;
        display: none;
        opacity: 0;
        interpolate-size: allow-keywords;
        transition: all 0.2s ease;
    }
    .container:has(textarea:focus) .btns-container,
    .container:has(textarea:not(:placeholder-shown)) .btns-container {
        height: auto;
        display: flex;
        opacity: 1;
    }
    .container:has(textarea:not(:empty)) .btns-container {
        height: auto;
        display: flex;
        opacity: 1;
    }
</style>
