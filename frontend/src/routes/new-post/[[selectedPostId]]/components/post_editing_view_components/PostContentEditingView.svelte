<script lang="ts">
    import { tick } from "svelte";
    import { ApiMain } from "../../../../../ts/backend-services";
    import type { Err } from "../../../../../ts/common/errs/err";
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import type { PostContent } from "../../../../../ts/common/post-content-item";
    import PostContentEditingSubheadingView from "./content_editing_view_components/PostContentEditingSubheadingView.svelte";
    import PostContentEditingParagraphView from "./content_editing_view_components/PostContentEditingParagraphView.svelte";
    import PostContentEditingQuoteView from "./content_editing_view_components/PostContentEditingQuoteView.svelte";
    import PostContentEditingListView from "./content_editing_view_components/PostContentEditingListView.svelte";
    import PostContentEditingHeadingView from "./content_editing_view_components/PostContentEditingHeadingView.svelte";

    let {
        postId,
        content,
        updateParentValue,
    }: {
        postId: string;
        content: PostContent;
        updateParentValue: (
            newContent: PostContent,
            newLastModified: Date,
        ) => void;
    } = $props<{
        postId: string;
        content: PostContent;
        updateParentValue: (
            newContent: PostContent,
            newLastModified: Date,
        ) => void;
    }>();
    export function isInEditingState() {
        return isEditing;
    }
    let editableValue = $state("");
    let isEditing = $state(false);
    let editingErrs: Err[] = $state([]);
    let editingEl: HTMLTextAreaElement;
    function deleteContentItem(index: number) {
      
    }
    async function saveContentChanges() {
        // const response = await ApiMain.fetchJsonResponse<{
        //     newPostContent: PostContent;
        //     newLastModified: Date;
        // }>(
        //     `/draft-posts/${postId}/update-content`,
        //     ApiMain.requestJsonPostOptions(
        //         { newPostContent: editableValue },
        //         "PATCH",
        //     ),
        // );
        // if (response.isSuccess) {
        //     content = response.data.newPostContent;
        //     isEditing = false;
        //     updateParentValue(content, response.data.newLastModified);
        // } else {
        //     editingErrs = response.errors;
        // }
    }
</script>

<div class="content-editing-view">
    <p class="section-p">Post Content</p>
    {#each content.items as item, i}
        {#if item.$type === "HeadingContentItem"}
            <PostContentEditingHeadingView value={item.value} deleteContentItem={() => deleteContentItem(i)} />
        {:else if item.$type === "SubheadingContentItem"}
            <PostContentEditingSubheadingView value={item.value} deleteContentItem={() => deleteContentItem(i)} />
        {:else if item.$type === "ParagraphContentItem"}
            <PostContentEditingParagraphView value={item.value} deleteContentItem={() => deleteContentItem(i)} />
        {:else if item.$type === "QuoteContentItem"}
            <PostContentEditingQuoteView />
        {:else if item.$type === "ListContentItem"}
            <PostContentEditingListView />
        {:else}
            <p>Unknown post content item type</p>
        {/if}
    {/each}
</div>

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

    /* 
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
        margin-left: auto;
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
    } */
</style>
