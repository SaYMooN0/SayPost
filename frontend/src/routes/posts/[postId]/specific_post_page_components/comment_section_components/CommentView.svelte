<script lang="ts">
    import type { PostComment } from "../../published-posts";

    let {comment} : {comment: PostComment} = $props<{comment: PostComment}>();
</script>

<div class="comment-view">
    <div class="comment-content">{comment.content}</div>
</div>