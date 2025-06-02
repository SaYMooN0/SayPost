import type { ServerResponseResult } from "../ts/backend-services";

class CachedUsersStore {
    private _userMap: Map<string, string> = new Map();

    async getUsernameForIds(
        userIds: string[],
        fetchIfNotFound: (missingIds: string[]) => Promise<ServerResponseResult<{ users: { id: string, username: string }[] }>>
    ): Promise<Record<string, string | null>> {

        const result: Record<string, string | null> = {};
        const missing: string[] = [];

        for (const id of userIds) {
            if (this._userMap.has(id)) {
                result[id] = this._userMap.get(id)!;
            } else {
                missing.push(id);
            }
        }

        if (missing.length > 0) {
            const fetchResult = await fetchIfNotFound(missing);
            if (fetchResult.isSuccess) {
                for (const user of fetchResult.data.users) {
                    this._userMap.set(user.id, user.username);
                    result[user.id] = user.username;
                }
                const fetchedUsersIds = new Set(fetchResult.data.users.map((u) => u.id));
                for (const id of missing) {
                    if (!fetchedUsersIds.has(id)) {
                        result[id] = null;
                    }
                }
            } else {
                for (const id of missing) {
                    result[id] = null;
                }
            }
        }
        return result;
    }
}

export const cachedUsersStore = new CachedUsersStore();
