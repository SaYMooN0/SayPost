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
        margin: 0;
        font-size: var(--post-content-subheading-font-size);
        font-weight: var(--post-content-subheading-font-weight);
        overflow-wrap: anywhere;
        text-indent: 1.5em;
    }
</style>
