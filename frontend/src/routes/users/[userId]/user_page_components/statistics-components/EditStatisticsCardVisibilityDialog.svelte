<script lang="ts">
    import BaseDialogWithCloseButton from "../../../../../components/dialogs/BaseDialogWithCloseButton.svelte";
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import { ApiMain } from "../../../../../ts/backend-services";
    import type { Err } from "../../../../../ts/common/errs/err";
    import VisibilityEditingDialogFieldInput from "./VisibilityEditingDialogFieldInput.svelte";

    let publishedPostsVisibleToAll = $state(true);
    let followersVisibleToAll = $state(true);
    let followingVisibleToAll = $state(true);
    let likedPostsVisibleToAll = $state(true);
    let commentsLeftVisibleToAll = $state(true);
    let fetchingErrs = $state<Err[]>([]);
    let savingErrs = $state<Err[]>([]);
    let baseDialog: BaseDialogWithCloseButton;
    export async function open() {
        savingErrs = [];
        const response = await ApiMain.fetchJsonResponse<{
            publishedPostsVisibleToAll: boolean;
            followersVisibleToAll: boolean;
            followingVisibleToAll: boolean;
            likedPostsVisibleToAll: boolean;
            commentsLeftVisibleToAll: boolean;
        }>("/profile/statistics-visibility", {
            method: "GET",
        });
        if (response.isSuccess) {
            fetchingErrs = [];
            publishedPostsVisibleToAll =
                response.data.publishedPostsVisibleToAll;
            followersVisibleToAll = response.data.followersVisibleToAll;
            followingVisibleToAll = response.data.followingVisibleToAll;
            likedPostsVisibleToAll = response.data.likedPostsVisibleToAll;
            commentsLeftVisibleToAll = response.data.commentsLeftVisibleToAll;
        } else {
            fetchingErrs = response.errors;
        }
        baseDialog.open();
    }
    async function save() {
        baseDialog.close();
    }
</script>

<BaseDialogWithCloseButton
    bind:this={baseDialog}
    dialogId={"statistics-visibility-editing"}
>
    {#if fetchingErrs.length > 0}
        <DefaultErrBlock errList={fetchingErrs} />
    {:else}
        <VisibilityEditingDialogFieldInput
            label="My published posts"
            bind:isVisibleToAll={publishedPostsVisibleToAll}
        />
        <VisibilityEditingDialogFieldInput
            label="My followers"
            bind:isVisibleToAll={followersVisibleToAll}
        />
        <VisibilityEditingDialogFieldInput
            label="My following"
            bind:isVisibleToAll={followingVisibleToAll}
        />
        <VisibilityEditingDialogFieldInput
            label="My liked posts"
            bind:isVisibleToAll={likedPostsVisibleToAll}
        />
        <VisibilityEditingDialogFieldInput
            label="My comments"
            bind:isVisibleToAll={commentsLeftVisibleToAll}
        />
        <DefaultErrBlock errList={savingErrs} />
        <button onclick={() => save()} class="save-btn"> Save </button>
    {/if}
</BaseDialogWithCloseButton>

<style>
    :global(#statistics-visibility-editing > .dialog-content) {
        display: flex;
        justify-content: center;
        width: 40rem;
        min-height: 20rem;
        padding: 0.25rem 1rem;
        border-radius: 0.5rem;
        background-color: var(--back-main);
        box-sizing: border-box;
    }

    :global(#statistics-visibility-editing > .dialog-content > .close-button) {
        top: 0.5rem;
        right: 0.5rem;
    }
</style>
