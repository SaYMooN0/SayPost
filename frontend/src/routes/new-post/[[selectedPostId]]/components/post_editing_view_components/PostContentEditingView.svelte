<script lang="ts">
    import { tick } from "svelte";
    import { ApiMain } from "../../../../../ts/backend-services";
    import type { Err } from "../../../../../ts/common/errs/err";
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import {
        copyPostContentItem,
        type PostContent,
    } from "../../../../../ts/common/post-content-item";
    import PostContentEditingHeadingView from "./content_editing_view_components/content_items/PostContentEditingHeadingView.svelte";
    import PostContentEditingListView from "./content_editing_view_components/content_items/PostContentEditingListView.svelte";
    import PostContentEditingParagraphView from "./content_editing_view_components/content_items/PostContentEditingParagraphView.svelte";
    import PostContentEditingQuoteView from "./content_editing_view_components/content_items/PostContentEditingQuoteView.svelte";
    import PostContentEditingSubheadingView from "./content_editing_view_components/content_items/PostContentEditingSubheadingView.svelte";
    import ContentEditingRightSideButtons from "./content_editing_view_components/ContentEditingRightSideButtons.svelte";

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
        return unsavedCount > 0;
    }
    let editedValues = $state(content.items.map(copyPostContentItem));

    let itemEditingStates = $state<boolean[]>(
        Array(content.items.length).fill(false),
    );
    let editingErrs: Err[] = $state([]);

    let unsavedCount = $derived(
        itemEditingStates.filter((state) => state).length,
    );
    function deleteContentItem(index: number) {}
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
        <div class="content-item">
            {#if item.$type === "HeadingContentItem"}
                <PostContentEditingHeadingView
                    isEditing={itemEditingStates[i]}
                    contentItem={item}
                    bind:editingValue={editedValues[i]}
                />
            {:else if item.$type === "SubheadingContentItem"}
                <PostContentEditingSubheadingView
                isEditing={itemEditingStates[i]}
                contentItem={item}
                bind:editingValue={editedValues[i]}
                />
            {:else if item.$type === "ParagraphContentItem"}
                <PostContentEditingParagraphView
                isEditing={itemEditingStates[i]}
                contentItem={item}
                bind:editingValue={editedValues[i]}
                />
            {:else if item.$type === "QuoteContentItem"}
                <PostContentEditingQuoteView
                isEditing={itemEditingStates[i]}
                contentItem={item}
                bind:editingValue={editedValues[i]}
                />
            {:else if item.$type === "ListContentItem"}
                <PostContentEditingListView
                isEditing={itemEditingStates[i]}
                contentItem={item}
                bind:editingValue={editedValues[i]}
                />
            {:else}
                <p>Unknown post content item type</p>
            {/if}
            <ContentEditingRightSideButtons
                isEditing={itemEditingStates[i]}
                startEditing={() => (itemEditingStates[i] = true)}
                cancelEditing={() => (itemEditingStates[i] = false)}
                deleteItem={() => deleteContentItem(i)}
            />
        </div>
    {/each}

    <button class="add-item-btn" onclick={() => {}}>Add item</button>
    {#if unsavedCount > 0}
        <div class="unsave-changes-count">{unsavedCount}</div>
    {/if}
    <div class="unsave-changes-count">{JSON.stringify(content.items)}</div>
    <div class="unsave-changes-count">{JSON.stringify(editedValues)}</div>

    <div class="save-btn">Save</div>
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
    .content-item {
        display: grid;
        grid-template-columns: 1fr auto;
        border-left: 0.125rem solid var(--back-second);
        padding-left: 0.25rem;
        margin-top: 0.5rem;
    }
    .content-item.has-unsave {
        border-color: var(--accent-main);
    }
    .items-divider {
        margin: 0.25rem 0;
        width: 100%;
        /* height: 2px; */
        background-color: var(--back-second);
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
    /*
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
