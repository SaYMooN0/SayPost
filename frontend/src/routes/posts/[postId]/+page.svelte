<script lang="ts">
	import ErrView from "../../../components/err_blocks/ErrView.svelte";
	import { Err } from "../../../ts/common/errs/err";
	import type { PageProps } from "./$types";
	import CommentsSection from "./specific_post_page_components/CommentsSection.svelte";
	import PostContentHeadingView from "./specific_post_page_components/post_content_items/PostContentHeadingView.svelte";
	import PostContentListView from "./specific_post_page_components/post_content_items/PostContentListView.svelte";
	import PostContentParagraphView from "./specific_post_page_components/post_content_items/PostContentParagraphView.svelte";
	import PostContentQuoteView from "./specific_post_page_components/post_content_items/PostContentQuoteView.svelte";
	import PostContentSubheadingView from "./specific_post_page_components/post_content_items/PostContentSubheadingView.svelte";

	let { data }: PageProps = $props();
</script>

<h1>{data.post.title}</h1>
<p class="author">
	by <a href="/users/{data.post.authorId}" data-sveltekit-preload-data="tap">
		{data.post.authorId}
	</a>
</p>
<div class="post-content">
	{#each data.post.content.items as item}
		{#if item.$type === "HeadingContentItem"}
			<PostContentHeadingView heading={item} />
		{:else if item.$type === "SubheadingContentItem"}
			<PostContentSubheadingView subheading={item} />
		{:else if item.$type === "ParagraphContentItem"}
			<PostContentParagraphView paragraph={item} />
		{:else if item.$type === "QuoteContentItem"}
			<PostContentQuoteView quote={item} />
		{:else if item.$type === "ListContentItem"}
			<PostContentListView list={item} />
		{:else}
			<ErrView err={new Err("Unknown post content item type")} />
		{/if}
	{/each}
</div>

<CommentsSection bind:comments={data.post.comments} />

<style>
	h1 {
		text-align: center;
		font-size: var(--post-title-font-size);
		font-weight: var(--post-title-font-weight);
		overflow-wrap: break-word;
		word-break: break-word;
		text-wrap: balance;
	}
	.author {
		width: 100%;
		color: var(--gray);
		font-size: 1.25rem;
		text-align: end;
	}
	.post-content {
		display: flex;
		flex-direction: column;
		gap: 1.25rem;
		max-width: 100ch;
		margin: 0 auto;
		border-top: 0.25rem solid var(--back-second);
	}
</style>
