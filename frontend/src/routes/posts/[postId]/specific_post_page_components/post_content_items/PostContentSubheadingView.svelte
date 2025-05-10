<script lang="ts">
    import type {
        PostContentItem,
        SubheadingContentItem,
    } from "../../../../../ts/common/post-content-item";
    let { subheading }: { subheading: SubheadingContentItem } = $props<{
        subheading: PostContentItem;
    }>();
</script>

<h3>{subheading.value}</h3>

<style>
    h3 {
        font-size: var(--post-content-subheading-font-size);
        font-weight: var(--post-content-subheading-font-weight);
        overflow-wrap: break-word;
        word-break: break-word;
        text-indent: 1.5em;
        margin: 0;

    }
</style>
