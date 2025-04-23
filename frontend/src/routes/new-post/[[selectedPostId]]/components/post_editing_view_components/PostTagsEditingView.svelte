<script lang="ts">
    import type { Err } from "../../../../../ts/common/errs/err";
    import PostTagsChoosingDialog from "./tags_editing_view_components/PostTagsChoosingDialog.svelte";

    let {
        postId,
        tags,
        updateParentValue,
    }: {
        postId: string;
        tags: string[];
        updateParentValue: (newTags: string[], newLastModified: Date) => void;
    } = $props<{
        postId: string;
        tags: string[];
        updateParentValue: (newTags: string[], newLastModified: Date) => void;
    }>();

    let tagsEditingDialog: PostTagsChoosingDialog;
</script>

<PostTagsChoosingDialog
    bind:this={tagsEditingDialog}
    updateParent={(newTags, newLastModified) =>
        updateParentValue(newTags, newLastModified)}
    {postId}
/>
<div class="tags">
    {#each tags as tag}
        <div>#{tag}</div>
    {/each}
    <div class="unselectable change-tags-btn" onclick={() => tagsEditingDialog.open(tags)}>
        + Change post tags
    </div>
</div>

<style>
    .tags {
        display: flex;
        flex-wrap: wrap;
        gap: 8px;
        padding: 8px;
        margin: 10px;
        border-radius: 4px;
        background-color: var(--back);
    }

    .tags > div {
        padding: 4px 8px;
        border-radius: 4px;
        background-color: var(--accent-main);
        color: var(--back-main);
        transition: all 0.12s ease-in;
        cursor: pointer;
    }
</style>
