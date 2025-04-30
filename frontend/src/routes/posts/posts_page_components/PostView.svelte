<script lang="ts">
    import type { PublishedPost } from "../published-posts";

    let {post}: {post:PublishedPost} = $props<{post:PublishedPost}>();
</script>

<div>
    <h1>{post.title}</h1>
    <p>{post.content}</p>
</div>