import type { PageServerLoad } from '../../$types';
import { cachedUsersStore } from '../../../../../stores/cached-users.svelte';
import { ApiAuth, ApiMain } from '../../../../../ts/backend-services';
import type { PlainErrType } from '../../../../../ts/common/errs/t-plain-err';

export const load: PageServerLoad = async ({ params, fetch }):
    Promise<
        | { title: string, posts: { id: string, title: string; authorUsername: string | null; authorId: string }[] }
        | { title: string, errors: PlainErrType[] }
    > => {
    const { userId } = params;

    const response = await ApiMain.serverFetchJsonResponse<{
        posts: { id: string, title: string; authorId: string }[]
    }>(fetch, `/users/${userId}/list-liked-posts`, {
        method: 'GET',
    });

    if (!response.isSuccess) {
        return { title: 'liked posts', errors: response.errors };
    }


    const usernameResult = await cachedUsersStore.getUsernameForIds(
        response.data.posts.map((p) => p.authorId),
        async (missingIds) => await ApiAuth.serverFetchJsonResponse<{ users: { id: string, username: string }[] }>(
            fetch, '/users/usernames-by-ids', ApiAuth.requestJsonOptions({ ids: missingIds })
        )
    );
    return {
        title: `liked posts (${response.data.posts.length})`,
        posts: response.data.posts.map((p) => ({
            ...p,
            authorUsername: usernameResult[p.authorId] ?? null
        })),
    };
};