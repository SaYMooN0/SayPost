<script lang="ts">
    import PostAuthorView from "./author_and_likes_section_components/PostAuthorView.svelte";
    import PostLikesComponent from "./author_and_likes_section_components/PostLikesComponent.svelte";

    let {
        postId,
        authorId,
        likesCount,
        isLikedByViewer,
    }: {
        postId: string;
        authorId: string;
        likesCount: number;
        isLikedByViewer: boolean;
    } = $props<{
        postId: string;
        authorId: string;
        likesCount: number;
        isLikedByViewer: boolean;
    }>();
</script>

<div class="author-and-likes-section">
    <PostAuthorView {authorId} />
    <PostLikesComponent {postId} {likesCount} {isLikedByViewer} />
</div>

<style>
    .author-and-likes-section {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
    }
</style>
