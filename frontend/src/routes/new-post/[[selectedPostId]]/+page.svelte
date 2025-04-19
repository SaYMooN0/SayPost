<script lang="ts">
    import { goto, afterNavigate } from "$app/navigation";
    import { page } from "$app/state";
    import AuthView from "../../../components/AuthView.svelte";
    import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";
    import PageAuthNeeded from "../../../components/PageAuthNeeded.svelte";
    import { ApiMain } from "../../../ts/backend-services";
    import { Err } from "../../../ts/common/errs/err";
    import { StringUtils } from "../../../ts/string-utils";
    import DraftPostEditingView from "./components/DraftPostEditingView.svelte";
    import DraftPostsList from "./components/DraftPostsList.svelte";
    import NoDraftPosts from "./components/NoDraftPosts.svelte";
    import NoPostSelected from "./components/NoPostSelected.svelte";
    import {
        DraftPostsSortOption,
        type DraftPostFullInfo,
        type DraftPostMainInfo,
    } from "./draftPosts";

    let selectedPostId: string | null = $state(null);
    let draftPostCache: Map<string, DraftPostFullInfo> = new Map();

    function setDraftPostFullInfo(post: DraftPostFullInfo) {
        draftPostCache.set(post.id, post);
    }

    let draftPostsMainInfo: DraftPostMainInfo[] = $state([]);
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
            postsMainInfoFetchingErrs = [];
        } else {
            postsMainInfoFetchingErrs = response.errors;
            draftPostsMainInfo = [];
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
    afterNavigate(() => {
        selectedPostId = page.params.selectedPostId;
    });
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
        {:else if draftPostsMainInfo.length == 0}
            <NoDraftPosts refresh={fetchDraftPosts} {createNewPost} />
        {:else}
            <div class="page-content">
                <div class="left-side">
                    <button class="new-post-btn">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 24 24"
                            fill="none"
                        >
                            <path
                                d="M5.07579 17C4.08939 4.54502 12.9123 1.0121 19.9734 2.22417C20.2585 6.35185 18.2389 7.89748 14.3926 8.61125C15.1353 9.38731 16.4477 10.3639 16.3061 11.5847C16.2054 12.4534 15.6154 12.8797 14.4355 13.7322C11.8497 15.6004 8.85421 16.7785 5.07579 17Z"
                                stroke="currentColor"
                                stroke-width="1.5"
                                stroke-linecap="round"
                                stroke-linejoin="round"
                            />
                            <path
                                d="M4 22C4 15.5 7.84848 12.1818 10.5 10"
                                stroke="currentColor"
                                stroke-width="1.5"
                                stroke-linecap="round"
                                stroke-linejoin="round"
                            />
                        </svg>
                        Write new post
                    </button>
                    <label class="sorting-label">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 24 24"
                            fill="none"
                        >
                            <path
                                d="M7 4V20"
                                stroke="currentColor"
                                stroke-width="1.5"
                                stroke-linecap="round"
                                stroke-linejoin="round"
                            />
                            <path
                                d="M17 19L17 4"
                                stroke="currentColor"
                                stroke-width="1.5"
                                stroke-linecap="round"
                                stroke-linejoin="round"
                            />
                            <path
                                d="M10 6.99998C10 6.99998 7.79053 4.00001 6.99998 4C6.20942 3.99999 4 7 4 7"
                                stroke="currentColor"
                                stroke-width="1.5"
                                stroke-linecap="round"
                                stroke-linejoin="round"
                            />
                            <path
                                d="M20 17C20 17 17.7905 20 17 20C16.2094 20 14 17 14 17"
                                stroke="currentColor"
                                stroke-width="1.5"
                                stroke-linecap="round"
                                stroke-linejoin="round"
                            />
                        </svg>
                        Your draft posts:
                    </label>
                    <DraftPostsList posts={draftPostsMainInfo} />
                </div>
                {#if StringUtils.isNullOrWhiteSpace(selectedPostId)}
                    <NoPostSelected {createNewPost} />
                {:else}
                    <div class="post-to-edit">
                        <DraftPostEditingView
                            post={draftPostCache.get(selectedPostId || "")}
                            postId={selectedPostId || ""}
                            {setDraftPostFullInfo}
                        />
                    </div>
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
        display: grid;
        grid-template-columns: 18rem 1fr;
        box-sizing: border-box;
    }

    .left-side {
        display: flex;
        flex-direction: column;
        height: calc(98vh - var(--layout-header-height));
        padding: 0.5rem;
        box-sizing: border-box;
    }

    .new-post-btn {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 0.5rem;
        padding: 0.5rem;
        margin-top: 1rem;
        margin-bottom: auto;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.25rem;
        background-color: var(--back-second);
        color: var(--text);
    }

    .new-post-btn > svg {
        height: 2rem;
    }

    .sorting-label {
        display: flex;
        flex-direction: row;
        align-items: center;
        margin: 0.5rem 0;
        box-sizing: border-box;
    }

    .sorting-label > svg {
        height: 2rem;
        background-color: var(--back-second);
    }
</style>
