<script lang="ts">
    import DefaultErrBlock from "../../../../components/err_blocks/DefaultErrBlock.svelte";
    import GrayLabelWithOnclick from "../../../../components/GrayLabelWithOnclick.svelte";
    import type { Err } from "../../../../ts/common/errs/err";

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
    <button class="new-post-btn" onclick={() => createNewPostBtnClicked()}>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M12 8V16M16 12L8 12"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22C17.5228 22 22 17.5228 22 12Z"
                stroke="currentColor"
                stroke-width="1.5"
            />
        </svg>
        Create new post
    </button>
    {#if postCreationErrs.length != 0}
    <label class="errs-label">Something went wrong during new post creation</label>
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
        margin-bottom: 0;
        margin-top: 10rem;
    }
    .new-post-btn {
        margin-top: 1rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        border: none;
        border-radius: 4rem;
        padding: 0.25rem 1.25rem;
        display: flex;
        align-items: center;
        gap: 0.5rem;
        width: max-content;
        font-size: 1.75rem;
        transition: all 0.12s ease-in;
        cursor: pointer;
    }
    .new-post-btn svg {
        height: 2rem;
    }
    .new-post-btn:hover {
        background-color: var(--accent-hov);
        padding: 0.25rem 1.5rem;
        gap: 1rem;
    }
    .errs-label{
        margin-top: 8rem;
        margin-bottom: 2rem;
    }
</style>
