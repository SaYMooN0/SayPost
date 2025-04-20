<script lang="ts">
    import DefaultErrBlock from "../../../../components/err_blocks/DefaultErrBlock.svelte";
    import { Err } from "../../../../ts/common/errs/err";
    import BigCreateNewPostBtn from "./CreateNewPostBtn.svelte";

    let { createNewPost } = $props<{
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

<div class="no-post-selected">
    <h1>Select a post to edit or create a new one</h1>
    <BigCreateNewPostBtn {createNewPostBtnClicked} />
    {#if postCreationErrs.length != 0}
        <div class="errs-div">
            <label> Something went wrong during new post creation </label>
            <DefaultErrBlock errList={postCreationErrs} />
        </div>
    {/if}
</div>

<style>
    .no-post-selected {
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 2rem;
        height: fit-content;
        padding: 4rem 0;
        margin: 4rem;
        border: 0.25rem dashed var(--accent-main);
        border-radius: 1.5rem;
        background-color: var(--back-second);
        box-sizing: border-box;
    }

    .no-post-selected h1 {
        margin-bottom: 0;
    }

    .errs-div {
        display: flex;
        flex-direction: column;
        padding: 0.25rem 1rem;
        border-radius: 0.25rem;
        background-color: var(--err-red-back);
        box-sizing: border-box;
    }

    .errs-div > label {
        margin-bottom: 0.25rem;
        color: var(--err-red);
        font-size: 2rem;
    }
</style>
