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

<div class="items-container">
    {#each list.items as listItem}
        <div class="list-display-item">
            <span class="bullet"></span>
            {listItem}
        </div>
    {/each}
</div>

<style>
    .items-container {
        margin: 0;
        display: flex;
        flex-direction: column;
        gap: 0.375rem;
    }
    .list-display-item {
        display: flex;
        align-items: center;
        gap: 0.5rem;
        font-size: var(--post-content-paragraph-font-size);
        font-weight: var(--post-content-paragraph-font-weight);
        overflow-wrap: anywhere;
    }

    .bullet {
        display: block;
        width: 0.5rem;
        height: 0.5rem;
        margin-left: 0.25rem;
        border-radius: 1rem;
        background-color: var(--accent-main);
        user-select: none;
    }
</style>
