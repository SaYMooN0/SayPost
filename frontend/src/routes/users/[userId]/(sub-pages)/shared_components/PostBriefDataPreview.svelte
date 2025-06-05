<script lang="ts">
    const {
        id,
        title,
        authorUsername,
        authorId,
    }: {
        id: string;
        title: string;
        authorUsername: string | null;
        authorId: string;
    } = $props<{
        id: string;
        title: string;
        authorUsername: string | null;
        authorId: string;
    }>();
</script>

<div class="post">
    <h1 class="title">{title}</h1>
    <label class="author">
        by
        <a href="/users/{authorId}" data-sveltekit-preload-data="tap">
            {#if authorUsername}
                {authorUsername}
            {:else}
                unable to load username
            {/if}
        </a>
    </label>
    <a href="/posts/{id}" class="read-btn" data-sveltekit-preload-data="hover">
        Read
    </a>
</div>

<style>
    .post {
        display: flex;
        flex-direction: column;
        padding: 0.25rem 0.5rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.5rem;
        box-sizing: border-box;
    }

    .title {
        width: 100%;
        overflow-wrap: anywhere;
        text-indent: 0.25em;
        margin: 0;
        font-weight: 550;
    }

    .author {
        color: var(--gray);
        font-style: italic;
        font-weight: 450;
    }

    .author > a {
        color: var(--accent-main);
        font-weight: 500;
        text-decoration: none;
    }

    .author > a:hover {
        text-decoration: underline;
        text-decoration-thickness: 0.125rem;
    }

    .author > a:active {
        color: var(--accent-hov);
    }

    .read-btn {
        width: fit-content;
        padding: 0.25rem 1rem;
        margin: 0 0.25rem 0.25rem auto;
        border-radius: 0.25rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1.25rem;
        text-decoration: none;
        transition: all 0.12s ease;
    }

    .read-btn:hover {
        background-color: var(--accent-hov);
    }
</style>
