<script lang="ts">
    import BaseDialogWithCloseButton from "../../../../../../components/dialogs/BaseDialogWithCloseButton.svelte";
    import DefaultErrBlock from "../../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import { ApiMain } from "../../../../../../ts/backend-services";
    import type { Err } from "../../../../../../ts/common/errs/err";
    import TagOperatingDisplay from "./TagOperatingDisplay.svelte";
    import TagsDialogSearchBar from "./TagsDialogSearchBar.svelte";

    let { updateParent, postId } = $props<{
        updateParent: (newTags: string[], newLastModified: Date) => void;
        postId: string;
    }>();

    let dialogElement: BaseDialogWithCloseButton;
    let tagsSearchBar: TagsDialogSearchBar;

    let tagsToChooseFrom: string[] = $state([]);
    let errors: Err[] = $state([]);
    let chosenTags: string[] = $state([]);
    export function open(tags: string[]) {
        tagsToChooseFrom = [];
        errors = [];
        tagsToChooseFrom = tags;
        dialogElement.open();
    }

    async function saveData() {
        const response = await ApiMain.fetchJsonResponse<{
            newTags: string[];
            newLastModified: Date;
        }>(
            `/draft-posts/${postId}/update-tags`,
            ApiMain.requestJsonPostOptions({ newTags: chosenTags }, "PATCH"),
        );

        if (response.isSuccess) {
            updateParent(response.data.newTags, response.data.newLastModified);
            dialogElement.close();
        } else {
            errors = response.errors;
        }
    }
    function removeTag(tag: string) {
        chosenTags = chosenTags.filter((t) => t !== tag);
    }
    function addTag(tag: string) {
        if (!chosenTags.includes(tag)) {
            chosenTags.push(tag);
        }
    }
</script>

<BaseDialogWithCloseButton
    bind:this={dialogElement}
    dialogId={"post-tags-choosing"}
>
    <div class="tags-adding-container">
        <div class="left-part">
            <TagsDialogSearchBar
                bind:this={tagsSearchBar}
                setErrs={(errs) => (errors = errs)}
                setSearchedTags={(tags) => (tagsToChooseFrom = tags)}
            />
            <div class="tags-ops-list">
                {#each tagsToChooseFrom as tag}
                    <TagOperatingDisplay
                        {tag}
                        isTagAdded={chosenTags.includes(tag)}
                        isTagRemovingState={false}
                        btnOnClick={() => addTag(tag)}
                    />
                {/each}
            </div>
            <label class="continue-typing">
                If you don't find the tag you need continue entering the name of
                the tag
            </label>
        </div>
        <div class="right-part">
            <label class="chosen-tags-label">
                Tags chosen: ({chosenTags.length})
            </label>
            <div class="tags-ops-list">
                {#each chosenTags as tag}
                    <TagOperatingDisplay
                        {tag}
                        isTagAdded={true}
                        isTagRemovingState={true}
                        btnOnClick={() => removeTag(tag)}
                    />
                {/each}
            </div>
        </div>
    </div>
    <div class="divider"></div>
    {#if errors.length != 0}
        <DefaultErrBlock errList={errors} />
    {/if}
    <button class="save-btn" onclick={() => saveData()}>Save</button>
</BaseDialogWithCloseButton>

<style>
    :global(#post-tags-choosing-dialog-content) {
        --dialog-height: ;

        display: flex;
        flex-direction: column;
        align-items: center;
        min-height: 30rem;
        padding: 1rem 1.5rem;
        border-radius: 0.5rem;
        background-color: var(--back-main);
        box-sizing: border-box;
    }

    :global(#post-tags-choosing-dialog-content:has(.err-block)) {
        --dialog-height: 36rem;
    }

    :global(#post-tags-choosing-dialog-content > .close-button) {
        top: 0.5rem !important;
        right: 0.5rem !important;
    }

    .tags-adding-container {
        display: grid;
        gap: 0.5rem;
        width: 100%;
        grid-template-columns: 1fr 1fr;
    }

    .left-part,
    .right-part {
        display: flex;
        flex-direction: column;
        align-items: center;
        overflow-y: auto;
    }

    .tags-ops-list {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
        height: 22rem;
        overflow-y: auto;
    }

    .continue-typing {
        color: var(--gray);
        font-size: 1rem;
    }

    .chosen-tags-label {
        color: var(--text);
        font-size: 1.5rem;
    }

    .divider {
        margin-top: auto;
    }

    .save-btn {
        padding: 0.25rem 2rem;
        margin-top: 0.5rem;
        border: none;
        border-radius: 2rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1.5rem;
        cursor: pointer;
        outline: none;
    }

    .save-btn:hover {
        background-color: var(--accent-hov);
    }
</style>
