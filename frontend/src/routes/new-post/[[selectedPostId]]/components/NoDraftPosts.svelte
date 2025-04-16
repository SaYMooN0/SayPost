<script lang="ts">
    import GrayLabelWithOnclick from "../../../../components/GrayLabelWithOnclick.svelte";

    let { refresh, createNewPost } = $props<{
        refresh: () => Promise<void>;
        createNewPost: () => Promise<void>;
    }>();
</script>

<div class="no-posts">
    <h1>You don't have any unpublished posts</h1>
    <GrayLabelWithOnclick text="Refresh" onClick={() => refresh()} />
    <button class="new-post" onclick={createNewPost}>Create new post</button>
</div>

<style>
    .no-posts {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
</style>
