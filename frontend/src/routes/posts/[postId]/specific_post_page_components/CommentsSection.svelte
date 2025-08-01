<script lang="ts">
    import AuthView from "../../../../components/AuthView.svelte";
    import DefaultErrBlock from "../../../../components/err_blocks/DefaultErrBlock.svelte";
    import CubeLoader from "../../../../components/loaders/CubeLoader.svelte";
    import { ApiMain } from "../../../../ts/backend-services";
    import type { Err } from "../../../../ts/common/errs/err";
    import { CommentsSortOption, type PostComment } from "../published-posts";
    import CommentView from "./comment_section_components/CommentView.svelte";
    import NewCommentInput from "./comment_section_components/NewCommentInput.svelte";
    import SelectCommentsSorting from "./comment_section_components/SelectCommentsSorting.svelte";

    let { postId }: { postId: string; comments: PostComment[] } = $props<{
        postId: string;
    }>();
    function addComment(comment: PostComment) {
        comments.unshift(comment);
    }
    let comments: PostComment[] = $state([]);

    let commentsFetchingErrs: Err[] = $state([]);
    let sortOption = $state(CommentsSortOption.Newest);
    async function fetchComments() {
        const url = `/posts/${postId}/comments?sortOption=${sortOption}`;
        const response = await ApiMain.fetchJsonResponse<{
            comments: PostComment[];
        }>(url, { method: "GET" });

        if (response.isSuccess) {
            comments = response.data.comments;
            commentsFetchingErrs = [];
        } else {
            comments = [];
            commentsFetchingErrs = response.errors;
        }
    }
</script>

{#snippet authenticated()}
    <NewCommentInput {postId} updateParentValue={addComment} />
{/snippet}
{#snippet unauthenticated()}
    <div class="auth-needed">To leave a comment you need to be logged in</div>
{/snippet}

<div class="comments-section">
    <AuthView {authenticated} {unauthenticated} />
    {#await fetchComments()}
        <div class="no-comments loader-wrapper">
            Loading comments
            <CubeLoader />
        </div>
    {:then}
        {#if comments.length == 0}
            <div class="no-comments">
                This comment has no comments yet. Be the first
            </div>
        {:else}
            <SelectCommentsSorting
                bind:sortOption
                commentsCount={comments.length}
            />
            {#if commentsFetchingErrs.length === 0}
                {#each comments as c}
                    <CommentView comment={c} />
                {/each}
            {:else}
                <DefaultErrBlock errList={commentsFetchingErrs} />
            {/if}
        {/if}
    {/await}
</div>

<style>
    .comments-section {
        margin: 1rem 0 4rem;
    }

    .auth-needed {
        width: fit-content;
        padding: 0.25rem 2rem;
        border-radius: 0.25rem;
        background-color: var(--back-second);
        color: var(--text-main);
        justify-self: center;
    }

    .no-comments {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 100%;
        color: var(--gray);
        font-size: 1.5rem;
        font-weight: 440;
    }

    .loader-wrapper {
        gap: 0.5rem;
    }

    .loader-wrapper > :global(.loader-container) {
        --uib-color: var(--gray);
        --uib-size: 2.1rem;
    }
</style>
