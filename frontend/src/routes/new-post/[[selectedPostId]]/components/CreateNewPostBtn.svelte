<script lang="ts">
    let { createNewPostBtnClicked } = $props<{
        createNewPostBtnClicked: () => void;
    }>();
</script>

<button class="new-post-btn" onclick={() => createNewPostBtnClicked()}>
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

<style>
    .new-post-btn {
        display: flex;
        align-items: center;
        gap: 0.5rem;
        width: max-content;
        padding: 0.375rem 1.25rem;
        border: none;
        border-radius: 4rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1.75rem;
        transition: all 0.12s ease-in;
        cursor: pointer;
    }

    .new-post-btn svg {
        height: 2rem;
    }

    .new-post-btn:hover {
        gap: 1rem;
        padding: 0.375rem 1.5rem;
        background-color: var(--accent-hov);
    }
</style>
