class CachedUsersStore {
  _userMap = /* @__PURE__ */ new Map();
  async getUsernameForIds(userIds, fetchIfNotFound) {
    const result = {};
    const missing = [];
    for (const id of userIds) {
      if (this._userMap.has(id)) {
        result[id] = this._userMap.get(id);
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
const cachedUsersStore = new CachedUsersStore();

export { cachedUsersStore as c };
//# sourceMappingURL=cached-users.svelte-CCMv31fB.js.map
