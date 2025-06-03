import type { PageServerLoad } from '../../$types';
import { ApiMain } from '../../../../../ts/backend-services';
import type { PlainErrType } from '../../../../../ts/common/errs/t-plain-err';
import type { PostPreview } from '../../../../../ts/published-posts-previews';


export const load: PageServerLoad = async ({ params, fetch }):
    Promise<
        | { title: string, posts: PostPreview[] }
        | { title: string, errors: PlainErrType[] }
    > => {
    const { userId } = params;

    const response = await ApiMain.serverFetchJsonResponse<{
        posts: PostPreview[]
    }>(fetch, `/users/${userId}/list-published-posts`, {
        method: 'GET',
    });

    if (!response.isSuccess) {
        return { title: 'published posts', errors: response.errors };
    }

    return {
        title: `published posts (${response.data.posts.length})`,
        posts: response.data.posts
    };
};