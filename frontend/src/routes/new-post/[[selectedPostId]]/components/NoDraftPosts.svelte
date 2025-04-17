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
    <button class="new-post-btn" onclick={createNewPost}>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M12 8V16M16 12L8 12"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22C17.5228 22 22 17.5228 22 12Z"
                stroke="currentColor"
                stroke-width="1.5"
            />
        </svg>
        Create new post
    </button>
</div>

<style>
    .no-posts {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .new-post-btn {
        background-color: var(--accent);
        color: var(--back-main);
    }
    .new-post-btn:hover {
        background-color: var(--accent-hov);
    }
</style>
