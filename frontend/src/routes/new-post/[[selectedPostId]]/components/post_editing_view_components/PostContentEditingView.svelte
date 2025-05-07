<script lang="ts">
    import PostContentEditingHeadingView from "./content_editing_view_components/content_items/PostContentEditingHeadingView.svelte";
    import PostContentEditingListView from "./content_editing_view_components/content_items/PostContentEditingListView.svelte";
    import PostContentEditingParagraphView from "./content_editing_view_components/content_items/PostContentEditingParagraphView.svelte";
    import PostContentEditingQuoteView from "./content_editing_view_components/content_items/PostContentEditingQuoteView.svelte";
    import PostContentEditingSubheadingView from "./content_editing_view_components/content_items/PostContentEditingSubheadingView.svelte";
    import ContentEditingRightSideButtons from "./content_editing_view_components/ContentEditingRightSideButtons.svelte";
    import AddPostItemButton from "./content_editing_view_components/AddPostItemButton.svelte";
    import {
        copyPostContentItem,
        type PostContent,
        type PostContentItem,
    } from "../../../../../ts/common/post-content-item";
    import type { Err } from "../../../../../ts/common/errs/err";

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
        return anyUnsaved;
    }
    type ContentItemToEdit = {
        editing: PostContentItem;
        initial: PostContentItem;
        isNewlyAdded: boolean;
        isInEditingState: boolean;
    };
    let editingState = $state<ContentItemToEdit[]>(
        content.items.map((i) => ({
            editing: copyPostContentItem(i),
            initial: i,
            isNewlyAdded: false,
            isInEditingState: false,
        })),
    );

    let editingErrs: Err[] = $state([]);

    let initialDeleted = 0;
    let anyUnsaved = $derived(
        initialDeleted > 0
            ? true
            : editingState.filter((i) =>
                  i.isNewlyAdded ? true : i.isInEditingState,
              ).length > 0,
    );
    function addNewContentItem(item: PostContentItem) {
        editingState.push({
            editing: item,
            initial: item,
            isNewlyAdded: true,
            isInEditingState: true,
        });
    }
    function deleteContentItem(index: number) {
        if (!editingState[index].isNewlyAdded) {
            initialDeleted++;
        }
        editingState.splice(index, 1);
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
    {#if editingState.length > 0}
        {#each editingState as item, i}
            <div
                class="content-item"
                class:unsaved={item.isInEditingState}
                class:newly-added={item.isNewlyAdded}
            >
                {#if item.initial.$type === "HeadingContentItem"}
                    <PostContentEditingHeadingView
                        isEditing={item.isInEditingState}
                        initial={item.initial}
                        bind:editingValue={editingState[i].editing}
                    />
                    <!-- {:else if item.item.$type === "SubheadingContentItem"}
                <PostContentEditingSubheadingView
                    isEditing={itemEditingStates[i]}
                    bind:editingValue={editedValues[i]}
                />
            {:else if item.item.$type === "ParagraphContentItem"}
                <PostContentEditingParagraphView
                    isEditing={itemEditingStates[i]}
                    bind:editingValue={editedValues[i]}
                />
            {:else if item.item.$type === "QuoteContentItem"}
                <PostContentEditingQuoteView
                    isEditing={itemEditingStates[i]}
                    bind:editingValue={editedValues[i]}
                />
            {:else if item.item.$type === "ListContentItem"}
                <PostContentEditingListView
                    isEditing={itemEditingStates[i]}
                    bind:editingValue={editedValues[i]}
                /> -->
                {:else}
                    <p>Unknown post content item type</p>
                {/if}
                <ContentEditingRightSideButtons
                    isEditing={item.isInEditingState}
                    isNewlyAdded={item.isNewlyAdded}
                    startEditing={() =>
                        (editingState[i].isInEditingState = true)}
                    cancelEditing={() =>
                        (editingState[i].isInEditingState = false)}
                    deleteItem={() => deleteContentItem(i)}
                />
            </div>
        {/each}
    {:else}
        <div class="empty-content">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M13 21.5V21C13 18.1716 13 16.7574 13.8787 15.8787C14.7574 15 16.1716 15 19 15H19.5M20 13.3431V10C20 6.22876 20 4.34315 18.8284 3.17157C17.6569 2 15.7712 2 12 2C8.22877 2 6.34315 2 5.17157 3.17157C4 4.34314 4 6.22876 4 10L4 14.5442C4 17.7892 4 19.4117 4.88607 20.5107C5.06508 20.7327 5.26731 20.9349 5.48933 21.1139C6.58831 22 8.21082 22 11.4558 22C12.1614 22 12.5141 22 12.8372 21.886C12.9044 21.8623 12.9702 21.835 13.0345 21.8043C13.3436 21.6564 13.593 21.407 14.0919 20.9081L18.8284 16.1716C19.4065 15.5935 19.6955 15.3045 19.8478 14.9369C20 14.5694 20 14.1606 20 13.3431Z"
                    stroke="currentColor"
                    stroke-width="1.25"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            </svg>
            Post is empty. Please add content items
        </div>
    {/if}
    <AddPostItemButton {addNewContentItem} />
    {#if anyUnsaved}
        <label class="unsaved-label"
            >Post content has some unsaved changes. Please save them
        </label>
        <button class="save-btn unselectable">Save</button>
    {/if}
    <!-- 
    <div class="unsave-changes-count">{JSON.stringify(content.items)}</div>
    <div class="unsave-changes-count">{JSON.stringify(editingState)}</div> -->
</div>

<style>
    .content-editing-view {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .empty-content {
        display: grid;
        grid-template-rows: 4rem auto;
        font-size: 1.25rem;
        font-weight: 440;
        justify-items: center;
        color: var(--gray);
    }
    .empty-content svg {
        height: 100%;
    }
    .content-item {
        width: 100%;
        display: grid;
        grid-template-columns: 1fr auto;
        border-left: 0.125rem solid var(--back-second);
        padding-left: 0.25rem;
        margin-top: 0.5rem;
    }
    .content-item:hover,
    .content-item:focus-within,
    .content-item:focus {
        border-color: var(--gray);
    }
    .content-item.unsaved {
        border-left-style: dashed;
        border-color: var(--accent-main);
    }
    .content-item.newly-added {
        border-left-style: solid;
        border-color: var(--accent-main);
    }
</style>
