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
        userIds: string[]
    }>(fetch, `/users/${userId}/list-follower-ids`, {
        method: 'GET',
    });

    if (!response.isSuccess) {
        return { title: 'followers', errors: response.errors };
    }


    const usernameResult = await cachedUsersStore.getUsernameForIds(
        response.data.userIds,
        async (missingIds) => await ApiAuth.serverFetchJsonResponse<{ users: { id: string, username: string }[] }>(
            fetch, '/users/usernames-by-ids', ApiAuth.requestJsonOptions({ ids: missingIds })
        )
    );
    return {
        title: `followers (${response.data.userIds.length})`,
        users: response.data.userIds.map((id) => ({
            id: id,
            username: usernameResult[id] ?? null,
        })),
    };
};
