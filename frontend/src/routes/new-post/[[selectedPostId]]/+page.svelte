<script lang="ts">
    import { page } from "$app/state";
    import AuthView from "../../../components/AuthView.svelte";
    import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";
    import PageAuthNeeded from "../../../components/PageAuthNeeded.svelte";
    import { ApiMain } from "../../../ts/backend-services";
    import { Err } from "../../../ts/common/errs/err";
    import DraftPostsList from "./components/DraftPostsList.svelte";
    import NoDraftPosts from "./components/NoDraftPosts.svelte";
    import {
        DraftPostsSortOption,
        type DraftPostFullInfo,
        type DraftPostMainInfo,
    } from "./draftPosts";

    let selectedPostId: string | null = page.params.selectedPostId;
    let userId = $state<string>();

    let draftPostsMainInfo: DraftPostMainInfo[] = $state([]);
    let postsMainInfoFetchingErrs: Err[] = $state([]);

    async function fetchDraftPosts(
        sortOption: DraftPostsSortOption = DraftPostsSortOption.lastModified,
    ) {
        const url = `/draft-posts?sortOption=${sortOption}`;
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
        const response = await ApiMain.fetchJsonResponse<{
            draftPost: DraftPostFullInfo;
        }>("/draft-posts/create", {
            method: "POST",
        });
        if (response.isSuccess) {
            console.log(response);
            console.log(response.data);
            console.log(response.data.draftPost);
        } else {
            return response.errors;
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
                    <DraftPostsList posts={draftPostsMainInfo} />
                </div>
                <div class="post-to-edit">Selected post</div>
                <div>{selectedPostId}</div>
            </div>
        {/if}
    {/await}
{/snippet}

<style>
    .fetching-err {
        max-width: 40rem;
        margin: 10rem auto 0 auto;
        box-sizing: border-box;
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .fetching-err label {
        font-weight: 400;
        font-size: 3rem;
        margin-bottom: 0.25rem;
    }
    .page-content {
        display: grid;
        grid-template-columns: 15rem 1fr;
    }
</style>
