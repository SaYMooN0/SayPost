import { A as ApiMain } from './backend-services-gK9Cr6ja.js';
import './date-utils-BMgSYFUH.js';

const load = async ({ params, fetch }) => {
  const { userId } = params;
  const response = await ApiMain.serverFetchJsonResponse(fetch, `/users/${userId}/list-published-posts`, {
    method: "GET"
  });
  if (!response.isSuccess) {
    return { title: "published posts", errors: response.errors };
  }
  return {
    title: `published posts (${response.data.posts.length})`,
    posts: response.data.posts
  };
};

var _page_server_ts = /*#__PURE__*/Object.freeze({
  __proto__: null,
  load: load
});

const index = 13;
let component_cache;
const component = async () => component_cache ??= (await import('./_page.svelte-EqqCVKrp.js')).default;
const server_id = "src/routes/users/[userId]/(sub-pages)/published-posts/+page.server.ts";
const imports = ["_app/immutable/nodes/13.DPAs9Hxs.js","_app/immutable/chunks/CWj6FrbW.js","_app/immutable/chunks/TlERTnYi.js","_app/immutable/chunks/DIeogL5L.js","_app/immutable/chunks/CeeueTis.js","_app/immutable/chunks/GGSi--Df.js","_app/immutable/chunks/D8xadzcL.js","_app/immutable/chunks/XGGrSNVo.js","_app/immutable/chunks/B-eBv7WL.js","_app/immutable/chunks/DIfM9HrB.js","_app/immutable/chunks/D6Y3kr68.js","_app/immutable/chunks/mvY8ht0J.js","_app/immutable/chunks/DaXfFcw6.js","_app/immutable/chunks/69_IOA4Y.js"];
const stylesheets = ["_app/immutable/assets/DefaultErrBlock.BZ3oWHWt.css","_app/immutable/assets/PostPreviewComponent.Bbp7ycQN.css","_app/immutable/assets/ListIsEmptyComponent.BdtJwJof.css","_app/immutable/assets/13.2fqWR3lq.css"];
const fonts = [];

export { component, fonts, imports, index, _page_server_ts as server, server_id, stylesheets };
//# sourceMappingURL=13-BilwJMbh.js.map
