<script lang="ts">
    let { userId, username }: { userId: string; username: string | null } =
        $props<{
            userId: string;
            username: string;
        }>();
</script>

<div class="item">
    {#if username}
        <label class="username">
            {username}
        </label>
    {:else}
        <label class="unable-to-load">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M14.9994 15L9 9M9.00064 15L15 9"
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
            Unable to load username
        </label>
    {/if}
    <a data-sveltekit-preload-data="hover" href="/users/{userId}">
        Visit profile
    </a>
</div>

<style>
    .item {
        display: flex;
        box-shadow:
            rgb(60 64 67 / 30%) 0 1px 2px 0,
            rgb(60 64 67 / 15%) 0 1px 3px 1px;
    }

    a {
        height: 1.5rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        transition: all 0.08s ease-in;
    }

    a:hover {
        background-color: var(--accent-hov);
    }
</style>
