<script lang="ts">
    import type {
        HeadingContentItem,
        PostContentItem,
    } from "../../../../../ts/common/post-content-item";
    let { heading }: { heading: HeadingContentItem } = $props<{
        heading: PostContentItem;
    }>();
</script>

<h2>{heading.value}</h2>

<style>
    h2 {
        font-size: var(--post-content-heading-font-size);
        font-weight: var(--post-content-heading-font-weight);
        overflow-wrap: break-word;
        word-break: break-word;
        text-indent: 1.5em;
        margin: 0;
    }
</style>
