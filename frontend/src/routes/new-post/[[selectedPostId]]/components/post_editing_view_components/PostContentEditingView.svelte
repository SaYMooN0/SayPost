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
    import { Err } from "../../../../../ts/common/errs/err";
    import { ApiMain } from "../../../../../ts/backend-services";
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import ErrView from "../../../../../components/err_blocks/ErrView.svelte";

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

    let initialDeleted = $state(0);
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
    function cancelEditing() {
        initialDeleted = 0;
        editingState = content.items.map((i) => ({
            editing: copyPostContentItem(i),
            initial: i,
            isNewlyAdded: false,
            isInEditingState: false,
        }));
    }
    async function saveContentChanges() {
        const dataToSave = editingState.map((i) => i.editing);
        const response = await ApiMain.fetchJsonResponse<{
            newPostContent: PostContent;
            newLastModified: Date;
        }>(
            `/draft-posts/${postId}/update-content`,
            ApiMain.requestJsonOptions(
                { newPostContent: dataToSave },
                "PATCH",
            ),
        );
        if (response.isSuccess) {
            content = response.data.newPostContent;
            updateParentValue(content, response.data.newLastModified);
            editingErrs = [];
            cancelEditing();
        } else {
            editingErrs = response.errors;
        }
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
                {:else if item.initial.$type === "SubheadingContentItem"}
                    <PostContentEditingSubheadingView
                        isEditing={item.isInEditingState}
                        initial={item.initial}
                        bind:editingValue={editingState[i].editing}
                    />
                {:else if item.initial.$type === "ParagraphContentItem"}
                    <PostContentEditingParagraphView
                        isEditing={item.isInEditingState}
                        initial={item.initial}
                        bind:editingValue={editingState[i].editing}
                    />
                {:else if item.initial.$type === "QuoteContentItem"}
                    <PostContentEditingQuoteView
                        isEditing={item.isInEditingState}
                        initial={item.initial}
                        bind:editingValue={editingState[i].editing}
                    />
                {:else if item.initial.$type === "ListContentItem"}
                    <PostContentEditingListView
                        isEditing={item.isInEditingState}
                        initial={item.initial}
                        bind:editingValue={editingState[i].editing}
                    />
                {:else}
                    <ErrView err={new Err("Unknown post content item type")} />
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
        <label class="unsaved-label">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M5.32171 9.6829C7.73539 5.41196 8.94222 3.27648 10.5983 2.72678C11.5093 2.42437 12.4907 2.42437 13.4017 2.72678C15.0578 3.27648 16.2646 5.41196 18.6783 9.6829C21.092 13.9538 22.2988 16.0893 21.9368 17.8293C21.7376 18.7866 21.2469 19.6548 20.535 20.3097C19.241 21.5 16.8274 21.5 12 21.5C7.17265 21.5 4.75897 21.5 3.46496 20.3097C2.75308 19.6548 2.26239 18.7866 2.06322 17.8293C1.70119 16.0893 2.90803 13.9538 5.32171 9.6829Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                />
                <path
                    d="M11.992 16H12.001"
                    stroke="currentColor"
                    stroke-width="2.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M12 13L12 8.99997"
                    stroke="currentColor"
                    stroke-width="2"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            </svg>
            Post content has some unsaved changes
        </label>
        <div class="save-cancel-btns unselectable">
            <button class="cancel-btn" onclick={cancelEditing}>Cancel</button>
            <button class="save-btn" onclick={() => saveContentChanges()}>
                Save content changes
            </button>
        </div>
    {/if}
    <DefaultErrBlock errList={editingErrs} />
</div>

<style>
    .content-editing-view {
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 1rem;
    }

    .empty-content {
        display: grid;
        color: var(--gray);
        font-size: 1.25rem;
        font-weight: 440;
        grid-template-rows: 4rem auto;
        justify-items: center;
    }

    .empty-content svg {
        height: 100%;
    }

    .content-item {
        display: grid;
        gap: 1rem;
        width: 100%;
        padding-left: 0.25rem;
        grid-template-columns: 1fr auto;
        border-left: 0.125rem solid var(--back-second);
    }

    .content-item:hover,
    .content-item:focus-within,
    .content-item:focus {
        border-color: var(--gray);
    }

    .content-item:hover :global(.btns-container),
    .content-item:focus-within :global(.btns-container),
    .content-item:focus :global(.btns-container) {
        opacity: 1 !important;
    }

    .content-item.unsaved {
        border-left-style: dashed;
        border-color: var(--accent-main);
    }

    .content-item.newly-added {
        border-left-style: solid;
        border-color: var(--accent-main);
    }

    .unsaved-label {
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 0.5rem;
        width: 100%;
        padding: 0.25rem 0;
        margin-top: 1.5rem;
        border-radius: 0.5rem;
        background-color: var(--warning-yellow-back);
        color: var(--warning-yellow);
        font-size: 1.125rem;
        font-weight: 420;
    }

    .unsaved-label > svg {
        height: 1.5rem;
        color: inherit;
    }

    .save-cancel-btns {
        display: flex;
        flex-direction: row;
        gap: 1rem;
        margin: 0.5rem 0;
    }

    .save-cancel-btns button {
        padding: 0.25rem 1rem;
        border: none;
        border-radius: 0.5rem;
        font-size: 1.375rem;
        font-weight: 500;
        transition: all 0.08s ease-in;
        cursor: pointer;
    }

    .cancel-btn {
        background-color: var(--back-second);
        color: var(--gray);
    }

    .cancel-btn:hover {
        background-color: var(--gray);
        color: var(--back-main);
    }

    .save-btn {
        background-color: var(--accent-main);
        color: var(--back-main);
    }

    .save-btn:hover {
        background-color: var(--accent-hov);
    }
</style>
