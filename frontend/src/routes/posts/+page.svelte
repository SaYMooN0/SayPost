<script lang="ts">
	import { afterNavigate, goto } from "$app/navigation";
	import { page } from "$app/state";
	import DefaultErrBlock from "../../components/err_blocks/DefaultErrBlock.svelte";
	import { ApiMain } from "../../ts/backend-services";
	import type { Err } from "../../ts/common/errs/err";
	import { StringUtils } from "../../ts/common/utils/string-utils";
	import { PostsFilterState } from "./posts-filter-state.svelte";
	import PostsFilter from "./posts_page_components/PostsFilter.svelte";
	import PostView from "./posts_page_components/PostView.svelte";
	import type { PostPreview } from "./published-posts-previews";

	let postsFilter = $state(new PostsFilterState());
	let posts: PostPreview[] = $state([]);
	let postsFetchingErrs: Err[] = $state([]);

	async function fetchPosts() {
		const queryParams = postsFilter.getQuery();
		const url = `/posts${StringUtils.isNullOrWhiteSpace(queryParams) ? "" : "?" + queryParams}`;
		const response = await ApiMain.fetchJsonResponse<{
			posts: PostPreview[];
		}>(url, {
			method: "GET",
		});
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
	function applyFilter() {
		goto(`/posts?${postsFilter.getQuery()}`);
	}
	afterNavigate(() => {
		postsFilter.applyQueryParams(page.url.searchParams);
		fetchPosts();
	});
</script>

<div class="page">
	<PostsFilter bind:postsFilter {applyFilter} />
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
		display: grid;
		width: 100%;
		margin: 0;
		box-sizing: border-box;
		grid-template-rows: auto 1fr;
	}
	.page :global(.err-block) {
		margin-top: 1rem;
		margin-bottom: 4rem;
	}
</style>
