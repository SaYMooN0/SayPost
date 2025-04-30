<script lang="ts">
    import DefaultErrBlock from "../../../../components/err_blocks/DefaultErrBlock.svelte";
    import GrayLabelWithOnclick from "../../../../components/GrayLabelWithOnclick.svelte";
    import type { Err } from "../../../../ts/common/errs/err";
    import BigCreateNewPostBtn from "./CreateNewPostBtn.svelte";

    let { refresh, createNewPost } = $props<{
        refresh: () => Promise<void>;
        createNewPost: () => Promise<Err[] | void>;
    }>();
    let postCreationErrs: Err[] = $state([]);

    async function createNewPostBtnClicked() {
        const res = await createNewPost();
        if (Array.isArray(res)) {
            postCreationErrs = res;
        }
    }
</script>

<div class="no-posts">
    <h1>You don't have any unpublished posts</h1>
    <GrayLabelWithOnclick text="Refresh" onClick={() => refresh()} />
    <BigCreateNewPostBtn {createNewPostBtnClicked} />
    {#if postCreationErrs.length != 0}
        <label class="errs-label">
            Something went wrong during new post creation
        </label>
        <DefaultErrBlock errList={postCreationErrs} />
    {/if}
</div>

<style>
    .no-posts {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .no-posts h1 {
        margin-top: 10rem;
        margin-bottom: 0;
    }

    .errs-label {
        margin-top: 8rem;
        margin-bottom: 2rem;
    }
</style>
