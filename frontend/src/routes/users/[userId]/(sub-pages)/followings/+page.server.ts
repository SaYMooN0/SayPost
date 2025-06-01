import type { PageServerLoad } from "../../$types";
import { cachedUsersStore } from "../../../../../stores/cached-users.svelte";
import { ApiAuth, ApiMain } from "../../../../../ts/backend-services";
import type { PlainErrType } from "../../../../../ts/common/errs/t-plain-err";

export const load: PageServerLoad = async ({ params, fetch }):
    Promise<
        | { title: string, users: { id: string, username: string | null }[] }
        | { title: string, errors: PlainErrType[] }
    > => {
    const { userId } = params;

    const response = await ApiMain.serverFetchJsonResponse<{
        users: { id: string }[];
    }>(fetch, `/users/${userId}/list-following-ids`, {
        method: 'GET',
    });

    if (!response.isSuccess) {
        return { title: 'followings', errors: response.errors };
    }

    const ids = response.data.users.map((u) => u.id);

    const usernameResult = await cachedUsersStore.getUsernameForIds(
        ids,
        async (missingIds) => await ApiAuth.serverFetchJsonResponse<Record<string, string>>(
            fetch, '/users/usernames-by-ids', ApiAuth.requestJsonOptions({ userIds: missingIds })
        )
    );

    return {
        title: 'followings',
        users: response.data.users.map((u) => ({
            id: u.id,
            username: usernameResult[u.id] ?? null,
        })),
    };
};
