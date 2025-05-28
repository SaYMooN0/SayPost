<script lang="ts">
    import { onMount, tick } from "svelte";
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import { ApiMain } from "../../../../../ts/backend-services";
    import type { Err } from "../../../../../ts/common/errs/err";
    import type { PostComment } from "../../published-posts";
    import { StringUtils } from "../../../../../ts/common/utils/string-utils";

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
            `/posts/${postId}/add-comment`,
            ApiMain.requestJsonOptions({ content: commentValue }, "POST"),
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
        inputEl.style.height = inputEl.scrollHeight + 4 + "px";
    }
    function cancelEditing() {
        commentValue = "";
        saveErrs = [];
    }
    onMount(() => {
        onCommentInput();
    });
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
        <button
            class="save-btn"
            disabled={StringUtils.isNullOrWhiteSpace(commentValue)}
            onclick={() => saveComment()}
        >
            Comment
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
        border: 0.125rem solid var(--back-main);
        border-radius: 0;
        background-color: var(--back-main);
        color: var(--text-main);
        font-size: 1.25rem;
        transition: all 0.12s ease-in;
        box-sizing: border-box;
        border-bottom-color: var(--gray);
        outline: none;
        resize: none;
    }

    textarea:focus {
        border: 0.125rem solid var(--accent-main);
        border-radius: 0.25rem;
        background-color: transparent;
    }

    .btns-container {
        display: flex;
        flex-direction: row;
        justify-content: right;
        gap: 0.5rem;
        height: 0;
        margin-top: -0.5rem;
        opacity: 0;
        transition: all 0.12s ease;
        grid-template-columns: 1fr 1fr;
        interpolate-size: allow-keywords;
    }

    .btns-container button {
        padding: 0;
        border: none;
        border-radius: 0.25rem;
        font-size: 0;
        transition: inherit;
    }

    .container:focus-within .btns-container,
    .container:has(textarea:not(:placeholder-shown)) .btns-container {
        height: auto;
        margin-top: 0;
        opacity: 1;
    }

    .container:focus-within .btns-container button,
    .container:has(textarea:not(:placeholder-shown)) .btns-container button {
        padding: 0.25rem 0.5rem;
        font-size: 1rem;
    }

    .btns-container button:active {
        transform: scale(0.95);
    }

    .cancel-btn {
        background-color: var(--gray);
        color: var(--back-main);
    }

    .save-btn {
        background-color: var(--accent-main);
        color: var(--back-main);
    }

    .save-btn:hover {
        background-color: var(--accent-main);
        color: var(--back-main);
    }

    .save-btn:disabled {
        background-color: var(--back-second);
        color: var(--gray);
        cursor: not-allowed;
    }
</style>
