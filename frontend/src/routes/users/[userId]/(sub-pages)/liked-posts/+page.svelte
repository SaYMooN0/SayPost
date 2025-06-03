<script lang="ts">
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import ErrView from "../../../../../components/err_blocks/ErrView.svelte";
    import ListIsEmptyComponent from "../shared_components/ListIsEmptyComponent.svelte";
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
            <div class="post">
                <label class="post-title">{post.title}</label>
                <label class="post-author">
                    by
                    <a
                        href="/users/{post.authorId}"
                        data-sveltekit-preload-data="hover"
                    >
                        {#if post.authorUsername}
                            {post.authorUsername}
                        {:else}
                            unable to load author's username
                        {/if}
                    </a>
                </label>
                <a
                    href="/posts/{post.id}"
                    class="read-btn"
                    data-sveltekit-preload-data="hover"
                >
                    Read
                </a>
            </div>
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
