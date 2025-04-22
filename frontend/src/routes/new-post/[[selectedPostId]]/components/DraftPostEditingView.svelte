<script lang="ts">
    import type { DraftPostFullInfo } from "../draftPosts";
    import { Err } from "../../../../ts/common/errs/err";
    import DefaultErrBlock from "../../../../components/err_blocks/DefaultErrBlock.svelte";
    import PostTitleEditingView from "./post_editing_view_components/PostTitleEditingView.svelte";
    import { DateUtils } from "../../../../ts/common/utils/date-utils";
    import PostTagsEditingView from "./post_editing_view_components/PostTagsEditingView.svelte";
    import TagOperatingDisplay from "./post_editing_view_components/tags_editing_view_components/TagOperatingDisplay.svelte";

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
        lastModifiedAt: new Date(),
        createdAt: new Date(),
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
        postData.lastModifiedAt = newLastModified;
        postData.title = newTitle;
        updateCache(postData);
    }
    function updateTags(newTags: string[], newLastModified: Date) {
        postData.lastModifiedAt = newLastModified;
        postData.tags = newTags;
        updateCache(postData);
    }
</script>

<div class="editing-view">
    {#await invokeGetPostData() then _}
        {#if fetchingErrs && fetchingErrs.length != 0}
            <p class="error-p">An error has occurred</p>
            <DefaultErrBlock errList={fetchingErrs} />
        {:else}
            {#key postData}
                <label class="last-modified">
                    Last modified at: {DateUtils.toLocale(
                        postData.lastModifiedAt,
                    )}
                </label>
            {/key}
            <PostTitleEditingView
                postId={postData.id}
                title={postData.title}
                updateParentValue={updateTitle}
            />
            <PostTagsEditingView
                postId={postData.id}
                tags={postData.tags}
                updateParentValue={updateTags}
            />
        {/if}
    {/await}
</div>

<style>
    .editing-view {
        display: flex;
        flex-direction: column;
        width: 100%;
        padding: 1rem 1rem 0;
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

    .last-modified {
        margin-left: 0.5rem;
        color: var(--gray);
        text-decoration: underline;
    }
</style>
