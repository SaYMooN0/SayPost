<script lang="ts">
    import BaseDialogWithCloseButton from "../../../../../components/dialogs/BaseDialogWithCloseButton.svelte";
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import { ApiMain } from "../../../../../ts/backend-services";
    import type { Err } from "../../../../../ts/common/errs/err";
    import type { StatisticsVisibility } from "../../user-profile";
    import VisibilityEditingDialogFieldInput from "./VisibilityEditingDialogFieldInput.svelte";

    let publishedPostsOnlyForFollowers = $state(false);
    let followersOnlyForFollowers = $state(false);
    let followingOnlyForFollowers = $state(false);
    let likedPostsOnlyForFollowers = $state(false);
    let commentsLeftOnlyForFollowers = $state(false);
    let fetchingErrs = $state<Err[]>([]);
    let savingErrs = $state<Err[]>([]);
    let baseDialog: BaseDialogWithCloseButton;
    export async function open() {
        savingErrs = [];
        const response = await ApiMain.fetchJsonResponse<StatisticsVisibility>(
            "/profile/statistics-visibility",
            { method: "GET" }
        );
        if (response.isSuccess) {
            fetchingErrs = [];
            publishedPostsOnlyForFollowers =
                response.data.publishedPostsOnlyForFollowers;
            followersOnlyForFollowers = response.data.followersOnlyForFollowers;
            followingOnlyForFollowers = response.data.followingOnlyForFollowers;
            likedPostsOnlyForFollowers =
                response.data.likedPostsOnlyForFollowers;
            commentsLeftOnlyForFollowers =
                response.data.commentsLeftOnlyForFollowers;
        } else {
            fetchingErrs = response.errors;
        }
        baseDialog.open();
    }
    async function save() {
        const response = await ApiMain.fetchJsonResponse<StatisticsVisibility>(
            `/profile/update-statistics-visibility`,
            ApiMain.requestJsonOptions({}, "PATCH"),
        );
        if (response.isSuccess) {
        } else {
            savingErrs = response.errors;
        }
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
            bind:isVisibleToAll={publishedPostsOnlyForFollowers}
        />
        <VisibilityEditingDialogFieldInput
            label="My followers"
            bind:isVisibleToAll={followersOnlyForFollowers}
        />
        <VisibilityEditingDialogFieldInput
            label="My following"
            bind:isVisibleToAll={followingOnlyForFollowers}
        />
        <VisibilityEditingDialogFieldInput
            label="My liked posts"
            bind:isVisibleToAll={likedPostsOnlyForFollowers}
        />
        <VisibilityEditingDialogFieldInput
            label="My comments"
            bind:isVisibleToAll={commentsLeftOnlyForFollowers}
        />
        <DefaultErrBlock errList={savingErrs} />
        <button onclick={() => save()} class="save-btn">Save</button>
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
