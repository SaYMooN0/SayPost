<script lang="ts">
    import type { DraftPostFullInfo } from "../draftPosts";
    import { Err } from "../../../../ts/common/errs/err";
    import DefaultErrBlock from "../../../../components/err_blocks/DefaultErrBlock.svelte";
    import PostTitleEditingView from "./post_editing_view_components/PostTitleEditingView.svelte";

    let {
        getPostData,
        updateCache,
    }: {
        getPostData: () => Promise<DraftPostFullInfo | undefined | Err[]>;
        updateCache: (newVal: DraftPostFullInfo) => void;
    } = $props<{
        getPostData: () => Promise<DraftPostFullInfo | undefined | Err[]>;
        updateCache: (newVal: DraftPostFullInfo) => void;
    }>();
    let postData: DraftPostFullInfo = $state({
        id: "",
        title: "",
        lastModified: new Date(),
        content: "",
        tags: [],
    });
    let fetchingErrs = $state<Err[]>([]);
    async function invokeGetPostData(): Promise<void> {
        const res = await getPostData();
        if (Array.isArray(res)) {
            fetchingErrs = res;
        } else if (res === undefined) {
            fetchingErrs = [
                new Err(
                    "Something went during fetching the post data. Please try again later",
                ),
            ];
        } else {
            fetchingErrs = [];
            postData = res;
        }
    }
    function updateTitle(newTitle: string, newLastModified: Date) {
        updateCache({
            ...postData,
            title: newTitle,
            lastModified: newLastModified,
        });
    }
</script>

<div class="editing-view">
    {#await invokeGetPostData() then _}
        {#if fetchingErrs && fetchingErrs.length != 0}
            <p class="error-p">An error has occurred</p>
            <DefaultErrBlock errList={fetchingErrs} />
        {:else}
            <label class="last-modified">{postData.lastModified}</label>
            <PostTitleEditingView
                postId={postData.id}
                title={postData.title}
                updateParentValue={updateTitle}
            />
        {/if}
    {/await}
</div>

<style>
    .editing-view {
        display: flex;
        flex-direction: column;
        width: 100%;
        padding: 0 1rem;
        box-sizing: border-box;
    }

    .error-p {
        margin: 4rem 0 1rem 4rem;
        font-size: 2rem;
        font-weight: 500;
    }

    .editing-view > :global(.error-p + .err-block) {
        width: fit-content;
        min-width: 20rem;
        max-width: 40rem !important;
        margin-left: 4rem;
    }
</style>
