<script lang="ts">
    import AuthView from "../../../../components/AuthView.svelte";
    import type { PostComment } from "../published-posts";
    import CommentView from "./comment_section_components/CommentView.svelte";
    import NewCommentInput from "./comment_section_components/NewCommentInput.svelte";

    let {
        postId,
        comments = $bindable(),
    }: { postId: string; comments: PostComment[] } = $props<{
        postId: string;
        comments: PostComment[];
    }>();
    function addComment(comment: PostComment) {
        comments.unshift(comment);
    }
</script>

{#snippet authenticated()}
    <NewCommentInput {postId} updateParentValue={addComment} />
{/snippet}
{#snippet unauthenticated()}
    <div class="auth-needed">To leave a comment, you need to be logged in</div>
{/snippet}

<AuthView {authenticated} {unauthenticated} />
{#if comments.length == 0}
    <div class="no-comments">
        This comment has no comments yet. Be the first
    </div>
{:else}
    {#each comments as c}
        <CommentView comment={c} />
    {/each}
{/if}

<style>
    .auth-needed {
        background-color: var(--back-second);
        color: var(--text-main);
    }

    .no-comments {
        margin: 1rem auto 4rem;
        color: var(--gray);
        font-size: 1.5rem;
        text-align: center;
    }
</style>
