<script lang="ts">
    import type {
        ListContentItem,
        PostContentItem,
    } from "../../../../../ts/common/post-content-item";
    import { StringUtils } from "../../../../../ts/common/utils/string-utils";
    let { list }: { list: ListContentItem } = $props<{
        list: PostContentItem;
    }>();
</script>

{#each list.items as listItem}
    <div class="list-display-item">
        <span class="bullet"></span>
        {listItem}
    </div>
{/each}

<style>
    .list-display-item {
        display: flex;
        align-items: center;
        gap: 0.25rem;
        font-size: var(--post-content-paragraph-font-size);
        font-weight: var(--post-content-paragraph-font-weight);
        overflow-wrap: break-word;
        word-break: break-word;
        margin: 0;

    }

    .bullet {
        display: block;
        width: 0.375rem;
        height: 0.375rem;
        margin-left: 0.25rem;
        border-radius: 1rem;
        background-color: var(--accent-main);
        user-select: none;
    }
</style>
