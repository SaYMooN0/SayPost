<script lang="ts">
    import type { DraftPost } from "../draftPosts";

    let { post }: { post: DraftPost } = $props<{ post: DraftPost }>();
</script>

<div class="draft-post-editing-view">
    <h1 class="post-title">{post.title}</h1>
    <p>{post.content}</p>
    <button>Edit post</button>
</div>
