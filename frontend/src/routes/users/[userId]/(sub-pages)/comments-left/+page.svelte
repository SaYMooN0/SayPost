<script lang="ts">
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import ErrView from "../../../../../components/err_blocks/ErrView.svelte";
    import ListIsEmptyComponent from "../shared_components/ListIsEmptyComponent.svelte";
    import PostBriefDataPreview from "../shared_components/PostBriefDataPreview.svelte";
    import type { PageProps } from "./$types";

    let { data }: PageProps = $props();
</script>

{#if data.errors && data.errors.length > 0}
    <DefaultErrBlock errList={data.errors} />
{:else if data.posts === undefined}
    <ErrView err={{ message: "Unable to load user's liked posts" }} />
{:else if data.posts.length === 0}
    <ListIsEmptyComponent />
{:else}
    <div class="posts-container">
        {#each data.posts as post}
            <div class="post-wrapper">
                <PostBriefDataPreview
                    id={post.postId}
                    title={post.postTitle}
                    authorUsername={post.authorUsername}
                    authorId={post.postAuthorId}
                />
                {#each post.comments as comment}
                    <div class="comment">
                        {comment}
                    </div>
                {/each}
            </div>
        {/each}
    </div>
{/if}

<style>
    .posts-container {
        display: flex;
        flex-direction: column;
        gap: 1.25rem;
    }

    .post-wrapper {
        display: flex;
        flex-direction: column;
        gap: 0.125rem;
    }

    .comment {
        width: 100%;
        margin-left: 0.5rem;
        box-sizing: border-box;
        border-left: 0.125rem solid var(--gray);
        text-indent: 0.25em;
        padding-left: 0.125rem;
        font-size: 1.25rem;
        margin-bottom: 0.5rem;
    }
</style>
