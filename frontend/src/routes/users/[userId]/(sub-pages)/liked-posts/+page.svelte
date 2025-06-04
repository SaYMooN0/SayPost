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
            <PostBriefDataPreview
                id={post.id}
                title={post.title}
                authorUsername={post.authorUsername}
                authorId={post.authorId}
            />
        {/each}
    </div>
{/if}

<style>
    .posts-container {
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
    }
</style>
