<script lang="ts">
    import BaseDialogWithCloseButton from "../../../../../components/dialogs/BaseDialogWithCloseButton.svelte";
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import { ApiMain } from "../../../../../ts/backend-services";
    import type { Err } from "../../../../../ts/common/errs/err";
    import type { StatisticsVisibility } from "../../user-profile";
    import VisibilityEditingDialogFieldInput from "./VisibilityEditingDialogFieldInput.svelte";

    let settings: StatisticsVisibility = $state<StatisticsVisibility>({
        publishedPostsOnlyForFollowers: false,
        followersOnlyForFollowers: false,
        followingOnlyForFollowers: false,
        likedPostsOnlyForFollowers: false,
        leftCommentsOnlyForFollowers: false,
    });
    let fetchingErrs = $state<Err[]>([]);
    let savingErrs = $state<Err[]>([]);
    let baseDialog: BaseDialogWithCloseButton;
    export function open() {
        fetch();
        baseDialog.open();
    }
    async function fetch() {
        savingErrs = [];
        const response = await ApiMain.fetchJsonResponse<StatisticsVisibility>(
            "/profile/statistics-visibility",
            { method: "GET" },
        );
        if (response.isSuccess) {
            fetchingErrs = [];
            console.log(response.data);
            settings = response.data;
        } else {
            fetchingErrs = response.errors;
        }
    }
    async function save() {
        const response = await ApiMain.fetchJsonResponse<StatisticsVisibility>(
            `/profile/update-statistics-visibility`,
            ApiMain.requestJsonOptions(settings, "PATCH"),
        );
        if (response.isSuccess) {
            savingErrs = [];
            settings = response.data;
        } else {
            savingErrs = response.errors;
        }
    }
</script>

<BaseDialogWithCloseButton
    bind:this={baseDialog}
    dialogId="statistics-visibility-editing"
>
    {#if fetchingErrs.length > 0}
        <DefaultErrBlock errList={fetchingErrs} />
    {:else}
        <h1 class="title">Statistics cards visibility settings</h1>
        <VisibilityEditingDialogFieldInput
            label="My published posts"
            bind:onlyForFollowers={settings.publishedPostsOnlyForFollowers}
        />
        <VisibilityEditingDialogFieldInput
            label="My followers"
            bind:onlyForFollowers={settings.followersOnlyForFollowers}
        />
        <VisibilityEditingDialogFieldInput
            label="My following"
            bind:onlyForFollowers={settings.followingOnlyForFollowers}
        />
        <VisibilityEditingDialogFieldInput
            label="My liked posts"
            bind:onlyForFollowers={settings.likedPostsOnlyForFollowers}
        />
        <VisibilityEditingDialogFieldInput
            label="My comments"
            bind:onlyForFollowers={settings.leftCommentsOnlyForFollowers}
        />
        <DefaultErrBlock errList={savingErrs} />
        <button onclick={() => save()} class="save-btn">Save</button>
    {/if}
</BaseDialogWithCloseButton>

<style>
    :global(#statistics-visibility-editing > .dialog-content) {
        display: flex;
        flex-direction: column;
        gap: 1rem;
        width: 40rem;
        min-height: 20rem;
        padding: 0.5rem 1rem;
        border-radius: 0.5rem;
        background-color: var(--back-main);
        box-sizing: border-box;
    }

    :global(#statistics-visibility-editing > .dialog-content > .close-button) {
        top: 0.5rem;
        right: 0.5rem;
    }

    .title {
        margin: 0.5rem;
        font-size: 1.5rem;
        font-weight: 520;
        text-align: center;
    }

    .save-btn {
        width: fit-content;
        padding: 0.25rem 1rem;
        margin-top: auto;
        border: none;
        border-radius: 0.25rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1.25rem;
        transition: all 0.04s ease-in;
        cursor: pointer;
        align-self: center;
    }

    .save-btn:hover {
        background-color: var(--accent-hov);
    }
</style>
