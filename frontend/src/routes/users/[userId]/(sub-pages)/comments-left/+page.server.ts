import { cachedUsersStore } from "../../../../../stores/cached-users.svelte";
import { ApiMain, ApiAuth } from "../../../../../ts/backend-services";
import type { PlainErrType } from "../../../../../ts/common/errs/t-plain-err";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ params, fetch }):
    Promise<
        | {
            title: string;
            posts: {
                postId: string;
                postTitle: string;
                postAuthorId: string;
                comments: string[];
                authorUsername: string | null;
            }[]
        }
        | { title: string, errors: PlainErrType[] }
    > => {
    const { userId } = params;

    const response = await ApiMain.serverFetchJsonResponse<{
        posts: {
            postId: string,
            postTitle: string;
            postAuthorId: string;
            comments: string[]
        }[]
    }>(fetch, `/users/${userId}/list-left-comments`, { method: 'GET', });

    console.log("----", response);
    console.log(response.isSuccess);


    if (!response.isSuccess) {
        return { title: 'left comments', errors: response.errors };
    }


    const usernameResult = await cachedUsersStore.getUsernameForIds(
        response.data.posts.map((p) => p.postAuthorId),
        async (missingIds) => await ApiAuth.serverFetchJsonResponse<{ users: { id: string, username: string }[] }>(
            fetch, '/users/usernames-by-ids', ApiAuth.requestJsonOptions({ ids: missingIds })
        )
    );
    const totalComments = response.data.posts.reduce((total, p) => total + p.comments.length, 0);
    return {
        title: `left comments (${totalComments})`,
        posts: response.data.posts.map((p) => ({
            ...p,
            authorUsername: usernameResult[p.postAuthorId] ?? null
        })),
    };
};