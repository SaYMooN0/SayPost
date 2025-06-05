<script lang="ts">
    const {
        link,
        value,
        labelText,
    }: { link: string; value: string; labelText: string } = $props<{
        link: string;
        value: string;
        labelText: string;
    }>();
</script>

<a href={link} class="card" data-sveltekit-preload-data="hover">
    <p class="value">{value}</p>
    <label class="text">{labelText} </label>
</a>

<style>
    .card {
        position: relative;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        width: 12rem;
        height: 5rem;
        border-radius: 0.25rem;
        text-decoration: none;
        box-shadow:
            rgb(60 64 67 / 30%) 0 1px 2px 0,
            rgb(60 64 67 / 15%) 0 1px 3px 1px;
        transition: all 0.2s cubic-bezier(0.23, 1, 0.32, 1);
        cursor: pointer;
        overflow: hidden;
    }

    .card:hover {
        box-shadow:
            rgb(60 64 67 / 30%) 0 1px 2px 0,
            rgb(60 64 67 / 15%) 0 1px 3px 1px,
            rgb(0 0 0 / 10%) 0 5px 10px;
        transform: scale(1.1);
    }

    .card > p {
        margin: 0;
        color: var(--accent-main);
        font-size: 2rem;
        font-weight: 550;
        cursor: inherit;
    }

    .card > label {
        color: var(--accent-main);
        font-size: 1.25rem;
        font-weight: 450;
        cursor: inherit;
    }
</style>
