<script lang="ts">
	import { applyAction } from "$app/forms";
	import DefaultErrBlock from "../../components/err_blocks/DefaultErrBlock.svelte";
	import { ApiMain } from "../../ts/backend-services";
	import type { Err } from "../../ts/common/errs/err";
	import { PostsFilterState } from "./posts-filter-state.svelte";
	import PostsFilter from "./posts_page_components/PostsFilter.svelte";
	import PostView from "./posts_page_components/PostView.svelte";
	import type { PublishedPost } from "./published-posts";

	const postsFilter = new PostsFilterState();
	let posts: PublishedPost[] = $state([]);
	let postsFetchingErrs: Err[] = $state([]);

	async function fetchPosts() {
		console.log("fetching");
		const url = `/posts?${postsFilter.getQuery()}`;
		const response = await ApiMain.fetchJsonResponse<{
			posts: PublishedPost[];
		}>(url, {
			method: "GET",
		});
		console.log(response);
		if (response.isSuccess) {
			posts = response.data.posts;
			if (postsFetchingErrs.length != 0) {
				postsFetchingErrs = [];
			}
		} else {
			posts = [];
			postsFetchingErrs = response.errors;
		}
	}
</script>

<div class="page">
	<PostsFilter {postsFilter} applyFilter={() => fetchPosts()} />
	{#if postsFetchingErrs.length != 0}
		<DefaultErrBlock errList={postsFetchingErrs} />
	{:else if posts.length == 0}
		<h1>There are no posts</h1>
	{:else}
		{#each posts as post}
			<PostView {post} />
		{/each}
	{/if}
</div>

<style>
	.page {
		width: 100%;
		box-sizing: border-box;
		margin: 0;
		display: grid;
		grid-template-rows: auto 1fr;
	}
</style>
