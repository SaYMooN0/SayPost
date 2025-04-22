<script lang="ts">
    import { goto, afterNavigate } from "$app/navigation";
    import { page } from "$app/state";
    import AuthView from "../../../components/AuthView.svelte";
    import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";
    import PageAuthNeeded from "../../../components/PageAuthNeeded.svelte";
    import { ApiMain } from "../../../ts/backend-services";
    import { Err } from "../../../ts/common/errs/err";
    import { StringUtils } from "../../../ts/common/utils/string-utils";
    import DraftPostEditingView from "./components/DraftPostEditingView.svelte";
    import DraftPostsList from "./components/left_side_components/DraftPostsList.svelte";
    import DraftPostsListSortingLabel from "./components/left_side_components/DraftPostsListSortingLabel.svelte";
    import LeftSideWriteNewPostBtn from "./components/left_side_components/LeftSideWriteNewPostBtn.svelte";
    import NoDraftPosts from "./components/NoDraftPosts.svelte";
    import NoPostSelected from "./components/NoPostSelected.svelte";
    import {
        DraftPostsSortOption,
        type DraftPostFullInfo,
        type DraftPostMainInfo,
    } from "./draftPosts";

    let selectedPostId: string | null = $state(null);
    let draftPostCache: Map<string, DraftPostFullInfo> = new Map();

    let draftPostsMainInfo: DraftPostMainInfo[] = $state([]);
    let draftPostsCount = $state(0);

    let postsMainInfoFetchingErrs: Err[] = $state([]);
    let draftPostsSortOption: DraftPostsSortOption = $state(
        DraftPostsSortOption.lastModified,
    );

    async function fetchDraftPosts() {
        const url = `/draft-posts?sortOption=${draftPostsSortOption}`;
        const response = await ApiMain.fetchJsonResponse<{
            posts: DraftPostMainInfo[];
        }>(url, {
            method: "GET",
        });
        if (response.isSuccess) {
            draftPostsMainInfo = response.data.posts;
            draftPostsCount = draftPostsMainInfo.length;
            if (postsMainInfoFetchingErrs.length != 0) {
                postsMainInfoFetchingErrs = [];
            }
        } else {
            postsMainInfoFetchingErrs = response.errors;
            draftPostsMainInfo = [];
            draftPostsCount = 0;
        }
    }
    async function createNewPost(): Promise<Err[] | void> {
        const response = await ApiMain.fetchJsonResponse<DraftPostFullInfo>(
            "/draft-posts/create",
            {
                method: "POST",
            },
        );
        if (response.isSuccess) {
            draftPostsSortOption = DraftPostsSortOption.lastModified;
            goto(`/new-post/${response.data.id}`, {
                noScroll: true,
            });
            await fetchDraftPosts();
        } else {
            return response.errors;
        }
    }
    afterNavigate(async () => {
        selectedPostId = page.params.selectedPostId;
    });
    async function getPostFullInfoFromCache(): Promise<
        DraftPostFullInfo | undefined | Err[]
    > {
        const postToGetId = selectedPostId ?? "";
        if (draftPostCache.has(postToGetId)) {
            return draftPostCache.get(postToGetId);
        }
        const response = await ApiMain.fetchJsonResponse<DraftPostFullInfo>(
            `/draft-posts/${selectedPostId}`,
            {
                method: "GET",
            },
        );
        if (response.isSuccess) {
            draftPostCache.set(postToGetId, response.data);
            return draftPostCache.get(postToGetId);
        } else {
            return response.errors;
        }
    }
    function updateCache(newVal: DraftPostFullInfo) {
        draftPostCache.set(newVal.id, newVal);
        const idx = draftPostsMainInfo.findIndex(
            (post) => post.id === newVal.id,
        );
        if (idx !== -1) {
            draftPostsMainInfo[idx] = newVal;
        }
    }
</script>

{#snippet unauthenticated()}
    <PageAuthNeeded />
{/snippet}
<AuthView {authenticated} {unauthenticated} />

{#snippet authenticated()}
    {#await fetchDraftPosts() then _}
        {#if postsMainInfoFetchingErrs.length != 0}
            <div class="fetching-err">
                <label>Something went wrong...</label>
                <DefaultErrBlock errList={postsMainInfoFetchingErrs} />
            </div>
        {:else if draftPostsCount == 0}
            <NoDraftPosts refresh={fetchDraftPosts} {createNewPost} />
        {:else}
            <div class="page-content">
                <div class="left-side">
                    <LeftSideWriteNewPostBtn {createNewPost} />
                    <DraftPostsListSortingLabel
                        bind:sortOption={draftPostsSortOption}
                    />
                    <DraftPostsList posts={draftPostsMainInfo} />
                </div>
                {#if StringUtils.isNullOrWhiteSpace(selectedPostId)}
                    <NoPostSelected {createNewPost} />
                {:else}
                    <DraftPostEditingView
                        getPostData={getPostFullInfoFromCache}
                        {updateCache}
                    />
                {/if}
            </div>
        {/if}
    {/await}
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
