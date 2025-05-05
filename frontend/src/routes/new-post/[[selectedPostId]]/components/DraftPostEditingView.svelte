<script lang="ts">
    import type { DraftPostFullInfo } from "../draft-posts";
    import { Err } from "../../../../ts/common/errs/err";
    import DefaultErrBlock from "../../../../components/err_blocks/DefaultErrBlock.svelte";
    import PostTitleEditingView from "./post_editing_view_components/PostTitleEditingView.svelte";
    import { DateUtils } from "../../../../ts/common/utils/date-utils";
    import PostTagsEditingView from "./post_editing_view_components/PostTagsEditingView.svelte";
    import PostContentEditingView from "./post_editing_view_components/PostContentEditingView.svelte";
    import DraftPostPublishingView from "./post_editing_view_components/DraftPostPublishingView.svelte";
    import type { PostContent } from "../../../../ts/common/post-content-item";

    let {
        getPostData,
        updateCache,
        updateAfterPublishing,
    }: {
        getPostData: () => Promise<DraftPostFullInfo | Err[]>;
        updateCache: (newVal: DraftPostFullInfo) => void;
        updateAfterPublishing: () => void;
    } = $props<{
        getPostData: () => Promise<DraftPostFullInfo | Err[]>;
        updateCache: (newVal: DraftPostFullInfo) => void;
        updateAfterPublishing: () => void;
    }>();
    let postData = $state<DraftPostFullInfo>({
        id: "",
        title: "",
        isPinned: false,
        lastModifiedAt: new Date(),
        createdAt: new Date(),
        content: { items: [] },
        tags: [],
    });
    let fetchingErrs = $state<Err[]>([]);
    async function invokeGetPostData(): Promise<void> {
        const res = await getPostData();
        if (Array.isArray(res)) {
            fetchingErrs = res;
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
    function updateContent(newContent: PostContent, newLastModified: Date) {
        postData.lastModifiedAt = newLastModified;
        postData.content = newContent;
        updateCache(postData);
    }
    function updateTags(newTags: string[], newLastModified: Date) {
        postData.lastModifiedAt = newLastModified;
        postData.tags = newTags;
        updateCache(postData);
    }
    let titleEditingEl: PostTitleEditingView;
    let contentEditingEl: PostContentEditingView;
    function anyElementsInEditingState(): boolean {
        return (
            titleEditingEl.isInEditingState() ||
            contentEditingEl.isInEditingState()
        );
    }
</script>

<div class="editing-view">
    {#await invokeGetPostData() then _}
        {#if fetchingErrs.length != 0}
            <p class="error-p">An error has occurred</p>
            <DefaultErrBlock errList={fetchingErrs} />
        {:else}
            <label class="last-modified">
                Last modified at: {DateUtils.toLocale(postData.lastModifiedAt)}
            </label>
            <PostTitleEditingView
                bind:this={titleEditingEl}
                postId={postData.id}
                title={postData.title}
                updateParentValue={updateTitle}
            />
            <PostTagsEditingView
                postId={postData.id}
                tags={postData.tags}
                updateParentValue={updateTags}
            />
            <PostContentEditingView
                bind:this={contentEditingEl}
                postId={postData.id}
                content={postData.content}
                updateParentValue={updateContent}
            />
            <DraftPostPublishingView
                postId={postData.id}
                {updateAfterPublishing}
                anyElementsInEditingState={() => anyElementsInEditingState()}
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
        width: 100%;
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
