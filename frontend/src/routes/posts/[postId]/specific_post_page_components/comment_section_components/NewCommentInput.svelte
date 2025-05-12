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
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
        width: 100%;
    }

    textarea {
        width: 100%;
        padding: 0.125rem 0.25rem 0;
        border: none;
        border-radius: 0.25rem 0.25rem 0.125rem 0.125rem;
        background-color: var(--back-second);
        color: var(--text-main);
        font-size: 1.25rem;
        border-bottom: 0.125rem solid var(--gray);
        outline: none;
        resize: none;
    }

    textarea:focus {
        border-color: var(--accent-main);
        background-color: transparent;
    }

    .btns-container {
        display: none;
        flex-direction: row;
        justify-content: right;
        gap: 0.5rem;
        height: 0;
        opacity: 0;
        transition: all 0.2s ease;
        grid-template-columns: 1fr 1fr;
        interpolate-size: allow-keywords;
    }

    .container:has(textarea:focus) .btns-container,
    .container:has(textarea:not(:placeholder-shown)) .btns-container {
        display: flex;
        height: auto;
        opacity: 1;
    }

    .container:has(textarea:not(:empty)) .btns-container {
        display: flex;
        height: auto;
        opacity: 1;
    }
</style>
