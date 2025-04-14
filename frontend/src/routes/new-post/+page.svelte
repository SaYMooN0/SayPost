<script lang="ts">
    import AuthView from "../../components/AuthView.svelte";
    import DefaultErrBlock from "../../components/err_blocks/DefaultErrBlock.svelte";
    import PageAuthNeeded from "../../components/PageAuthNeeded.svelte";
    import { AuthStoreData } from "../../stores/auth-store.svelte";
    import { ApiMain } from "../../ts/backend-services";
    import type { Err } from "../../ts/common/errs/err";

    type DraftPost = {
        id: string;
        title: string;
        lastModified: Date;
    };
    let draftPosts = $state<DraftPost[]>([]);
    let draftPostsLoadingErrs = $state<Err[]>([]);
    async function fetchDraftPosts(userId: string | null) {
        const response = await ApiMain.fetchJsonResponse<{
            posts: DraftPost[];
        }>("/draft-posts", {
            method: "GET",
        });
        console.log(response);
        if (response.isSuccess) {
            draftPosts = response.data.posts;
        } else {
            draftPostsLoadingErrs = response.errors;
        }
    }
</script>

{#snippet authenticated(authData: AuthStoreData)}
    <div class="page-content">
        <div class="posts-list">
            {#await fetchDraftPosts(authData.UserId)}
                {#if draftPostsLoadingErrs.length > 0}
                    <DefaultErrBlock errList={draftPostsLoadingErrs} />
                {:else if draftPosts.length > 0}
                    <label class="your-draft-posts">
                        Your draft posts
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
                    </label>
                    <div class="your-draft-posts-list">
                        {@render writeNewPostButton()}
                        {#each draftPosts as draftPost}
                            <a
                                href="/new-post/{draftPost.id}"
                                data-sveltekit-preload-data="hover"
                            >
                                <label class="post-title">
                                    {draftPost.title}
                                </label>
                                <label class="last-moified"
                                    >{draftPost.lastModified}</label
                                >
                            </a>
                        {/each}
                    </div>
                {:else}
                    <div class="no-posts">
                        You don't have any draft posts
                        {@render writeNewPostButton()}
                    </div>
                {/if}
            {/await}
        </div>
        <div class="post-to-edit">Selected post</div>
    </div>
{/snippet}
{#snippet unauthenticated()}
    <PageAuthNeeded />
{/snippet}
{#snippet writeNewPostButton()}
    <button class="new-post-btn">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
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
{/snippet}
<AuthView {authenticated} {unauthenticated} />

<style>
    .page-content{
        display: grid;
        grid-template-columns: 15rem 1fr;
    }
</style>
