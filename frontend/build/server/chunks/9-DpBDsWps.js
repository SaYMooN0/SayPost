import { c as cachedUsersStore } from './cached-users.svelte-CCMv31fB.js';
import { A as ApiMain, a as ApiAuth } from './backend-services-gK9Cr6ja.js';
import './date-utils-BMgSYFUH.js';

const load = async ({ params, fetch }) => {
  const { userId } = params;
  const response = await ApiMain.serverFetchJsonResponse(fetch, `/users/${userId}/list-left-comments`, { method: "GET" });
  console.log("----", response);
  console.log(response.isSuccess);
  if (!response.isSuccess) {
    return { title: "left comments", errors: response.errors };
  }
  const usernameResult = await cachedUsersStore.getUsernameForIds(
    response.data.posts.map((p) => p.postAuthorId),
    async (missingIds) => await ApiAuth.serverFetchJsonResponse(
      fetch,
      "/users/usernames-by-ids",
      ApiAuth.requestJsonOptions({ ids: missingIds })
    )
  );
  const totalComments = response.data.posts.reduce((total, p) => total + p.comments.length, 0);
  return {
    title: `left comments (${totalComments})`,
    posts: response.data.posts.map((p) => ({
      ...p,
      authorUsername: usernameResult[p.postAuthorId] ?? null
    }))
  };
};

var _page_server_ts = /*#__PURE__*/Object.freeze({
  __proto__: null,
  load: load
});

const index = 9;
let component_cache;
const component = async () => component_cache ??= (await import('./_page.svelte-Nti2ew-H.js')).default;
const server_id = "src/routes/users/[userId]/(sub-pages)/comments-left/+page.server.ts";
const imports = ["_app/immutable/nodes/9.CT7yc--w.js","_app/immutable/chunks/CWj6FrbW.js","_app/immutable/chunks/TlERTnYi.js","_app/immutable/chunks/DIeogL5L.js","_app/immutable/chunks/CeeueTis.js","_app/immutable/chunks/GGSi--Df.js","_app/immutable/chunks/D8xadzcL.js","_app/immutable/chunks/XGGrSNVo.js","_app/immutable/chunks/B-eBv7WL.js","_app/immutable/chunks/DaXfFcw6.js","_app/immutable/chunks/69_IOA4Y.js","_app/immutable/chunks/CNt2kpLD.js","_app/immutable/chunks/D6Y3kr68.js"];
const stylesheets = ["_app/immutable/assets/DefaultErrBlock.BZ3oWHWt.css","_app/immutable/assets/ListIsEmptyComponent.BdtJwJof.css","_app/immutable/assets/PostBriefDataPreview.Cw8945DA.css","_app/immutable/assets/9.CvWWPRpw.css"];
const fonts = [];

export { component, fonts, imports, index, _page_server_ts as server, server_id, stylesheets };
//# sourceMappingURL=9-DpBDsWps.js.map
