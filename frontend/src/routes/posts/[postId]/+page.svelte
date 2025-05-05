<script lang="ts">
	import type { PageProps } from './$types';
    import CommentsSection from './specific_post_page_components/CommentsSection.svelte';

	let { data }: PageProps = $props();
</script>

<h1>{data.post.title}</h1>
<div>{data.post.content}</div>

<CommentsSection bind:comments={data.post.comments} />