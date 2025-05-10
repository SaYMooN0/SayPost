<script lang="ts">
    import type {
        ParagraphContentItem,
        PostContentItem,
    } from "../../../../../ts/common/post-content-item";
    let { paragraph }: { paragraph: ParagraphContentItem } = $props<{
        paragraph: PostContentItem;
    }>();
</script>

<p>{paragraph.value}</p>

<style>
    p {
        font-size: var(--post-content-paragraph-font-size);
        font-weight: var(--post-content-paragraph-font-weight);
        margin: 0;

    }
</style>
