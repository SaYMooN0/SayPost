<script lang="ts">
    import { onMount } from "svelte";
    import type { DraftPostFullInfo } from "../draftPosts";
    import { ApiMain } from "../../../../ts/backend-services";
    import type { Err } from "../../../../ts/common/errs/err";

    export let post: DraftPostFullInfo | undefined;
    export let postId: string;
    export let setDraftPostFullInfo: (post: DraftPostFullInfo) => void;

    let loading = false;
    let error: Err[] = [];

    onMount(async () => {
        if (!post && postId) {
            loading = true;
            const response = await ApiMain.fetchJsonResponse<DraftPostFullInfo>(
                `/draft-posts/${postId}`, {
                    method: "GET"
                }
            );
            if (response.isSuccess) {
                post = response.data;
                setDraftPostFullInfo(response.data);
            } else {
                error = response.errors;
            }
            loading = false;
        }
    });
</script>

{#if loading}
    <p>Loading post...</p>
{:else if error.length}
    <p>Failed to load post</p>
{:else if post}
    <div class="draft-post-editing-view">
        <h1 class="post-title">{post.title}</h1>
        <p>{post.content}</p>
        <button>Edit post</button>
    </div>
{:else}
    <p>Post not found</p>
{/if}
