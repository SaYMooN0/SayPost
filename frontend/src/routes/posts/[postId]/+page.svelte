<script lang="ts">
	import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";
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
	// let comments = $state(data.post.comments); //fetch
</script>

{#if data.errors}
	<DefaultErrBlock errList={data.errors} />
{:else}
	<h1>{data.post.title}</h1>
	<p class="author">
		by <a
			href="/users/{data.post.authorId}"
			data-sveltekit-preload-data="tap"
		>
			{data.post.authorId}
		</a>
	</p>
	<div class="divider"></div>
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
	<div class="divider"></div>
	<!-- <CommentsSection postId={data.post.id} bind:comments /> -->
{/if}

<style>
	h1 {
		font-size: var(--post-title-font-size);
		font-weight: var(--post-title-font-weight);
		text-align: center;
		overflow-wrap: anywhere;
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
	}

	.divider {
		width: 100%;
		height: 0.125rem;
		margin: 1rem 0;
		border-radius: 0.25rem;
		background-color: var(--back-second);
	}
</style>
