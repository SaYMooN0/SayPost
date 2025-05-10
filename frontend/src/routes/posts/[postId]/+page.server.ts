import { ApiMain } from "../../../ts/backend-services";
import type { PageServerLoad } from "./$types";
import type { PublishedPost } from "./published-posts";

export const load: PageServerLoad = async ({ params, fetch }) => {
	const { postId } = params;

	const response = await ApiMain.serverFetchJsonResponse<PublishedPost>(fetch, `/posts/${postId}`, {
		method: 'GET',
	});

	if (!response.isSuccess) {
		throw new Error('Failed to fetch post');
	}
	return {
		post: response.data
	};
};