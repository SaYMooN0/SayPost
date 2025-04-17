<script lang="ts">
    import type { DraftPostFullInfo } from "../draftPosts";

    let { post }: { post: DraftPostFullInfo } = $props<{ post: DraftPostFullInfo }>();
</script>

<div class="draft-post-editing-view">
    <h1 class="post-title">{post.title}</h1>
    <p>{post.content}</p>
    <button>Edit post</button>
</div>
