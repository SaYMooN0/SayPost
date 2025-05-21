<script lang="ts">
    let { authorId }: { authorId: string } = $props<{ authorId: string }>();
</script>

<p class="author">
    by <a href="/users/{authorId}" data-sveltekit-preload-data="tap">
        {authorId}
    </a>
</p>

<style>
    .author {
        margin: 0;
        color: var(--gray);
        font-size: 1.25rem;
        font-style: italic;
    }

    .author a {
        color: var(--accent-main);
        font-weight: 420;
        text-decoration: none;
    }

    .author a:hover {
        text-decoration: underline;
    }
</style>
