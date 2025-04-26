<script lang="ts">
    import { goto, afterNavigate } from "$app/navigation";
    import { page } from "$app/state";
    import AuthView from "../../../components/AuthView.svelte";
    import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";
    import PageAuthNeeded from "../../../components/PageAuthNeeded.svelte";
    import { StringUtils } from "../../../ts/common/utils/string-utils";
    import DraftPostEditingView from "./components/DraftPostEditingView.svelte";
    import DraftPostItemMoreActionsMenu from "./components/left_side_components/DraftPostItemMoreActionsMenu.svelte";
    import DraftPostsList from "./components/left_side_components/DraftPostsList.svelte";
    import DraftPostsListSortingLabel from "./components/left_side_components/DraftPostsListSortingLabel.svelte";
    import LeftSideWriteNewPostBtn from "./components/left_side_components/LeftSideWriteNewPostBtn.svelte";
    import NoDraftPosts from "./components/NoDraftPosts.svelte";
    import NoPostSelected from "./components/NoPostSelected.svelte";
    import type { DraftPostMainInfo } from "./draftPosts";
    import { NewPostPageState } from "./new-post-page-state.svelte";

    let pageState: NewPostPageState = new NewPostPageState();

    afterNavigate(async () => {
        pageState.selectedPostId = page.params.selectedPostId;
    });

    let moreActionsBtnMenu: DraftPostItemMoreActionsMenu;
    function openMoreActionsMenu(e: MouseEvent, post: DraftPostMainInfo) {
        e.preventDefault();
        moreActionsBtnMenu.open(e, { id: post.id, isPinned: post.isPinned });
    }
</script>

<AuthView {authenticated} {unauthenticated} />

{#snippet unauthenticated()}
    <PageAuthNeeded />
{/snippet}
{#snippet authenticated()}
    {#if pageState.postsMainInfoFetchingErrs.length != 0}
        <div class="fetching-err">
            <label>Something went wrong...</label>
            <DefaultErrBlock errList={pageState.postsMainInfoFetchingErrs} />
        </div>
    {:else if pageState.draftPostsCount == 0}
        <NoDraftPosts
            refresh={async () => await pageState.fetchDraftPosts()}
            createNewPost={async () => await pageState.createNewPost()}
        />
    {:else}
        <div class="page-content">
            <div class="left-side">
                <DraftPostItemMoreActionsMenu
                    bind:this={moreActionsBtnMenu}
                    updatePostPinnedState={pageState.updatePostPinnedState}
                    deletePost={async (id) => await pageState.deletePost(id)}
                />
                <LeftSideWriteNewPostBtn
                    createNewPost={async () => await pageState.createNewPost()}
                />
                <DraftPostsListSortingLabel
                    bind:sortOption={pageState.draftPostsSortOption}
                    bind:pinnedPostsOnTop={pageState.draftPostsPinnedPostsOnTop}
                />
                <DraftPostsList
                    posts={pageState.draftPostsMainInfo}
                    {openMoreActionsMenu}
                />
            </div>
            {#if StringUtils.isNullOrWhiteSpace(pageState.selectedPostId)}
                <NoPostSelected
                    createNewPost={async () => await pageState.createNewPost()}
                />
            {:else}
                <DraftPostEditingView
                    getPostData={async () => await pageState.getPostFullInfo()}
                    updateCache={(newVal) => pageState.updateCache(newVal)}
                />
            {/if}
        </div>
    {/if}
{/snippet}

<style>
    .fetching-err {
        display: flex;
        flex-direction: column;
        align-items: center;
        max-width: 40rem;
        margin: 10rem auto 0;
        box-sizing: border-box;
    }

    .fetching-err label {
        margin-bottom: 0.25rem;
        font-size: 3rem;
        font-weight: 400;
    }

    .page-content {
        --left-side-width: 18rem;

        display: grid;
        grid-template-columns: var(--left-side-width) 1fr;
        box-sizing: border-box;
    }

    .left-side {
        display: flex;
        flex-direction: column;
        height: calc(98vh - var(--layout-header-height));
        box-sizing: border-box;
    }
</style>
