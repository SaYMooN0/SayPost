import { A as ApiMain } from './backend-services-gK9Cr6ja.js';
import './date-utils-BMgSYFUH.js';

const load = async ({ params, fetch }) => {
  const { userId } = params;
  const response = await ApiMain.serverFetchJsonResponse(fetch, `/users/${userId}/profile-data`, {
    method: "GET"
  });
  if (!response.isSuccess) {
    return { errors: response.errors };
  }
  return { pageUser: response.data };
};

var _page_server_ts = /*#__PURE__*/Object.freeze({
  __proto__: null,
  load: load
});

const index = 8;
let component_cache;
const component = async () => component_cache ??= (await import('./_page.svelte-e-KXMtTm.js')).default;
const server_id = "src/routes/users/[userId]/+page.server.ts";
const imports = ["_app/immutable/nodes/8.B3dRu-aJ.js","_app/immutable/chunks/CWj6FrbW.js","_app/immutable/chunks/TlERTnYi.js","_app/immutable/chunks/DIeogL5L.js","_app/immutable/chunks/CeeueTis.js","_app/immutable/chunks/GGSi--Df.js","_app/immutable/chunks/XGGrSNVo.js","_app/immutable/chunks/DVQ-NCSO.js","_app/immutable/chunks/B3uS-3am.js","_app/immutable/chunks/DqFbyC95.js","_app/immutable/chunks/BC8zxhgT.js","_app/immutable/chunks/CrUDN0xC.js","_app/immutable/chunks/B-eBv7WL.js","_app/immutable/chunks/mvY8ht0J.js","_app/immutable/chunks/D8xadzcL.js","_app/immutable/chunks/ByBu9AUQ.js","_app/immutable/chunks/D6Y3kr68.js","_app/immutable/chunks/4kLUYOtx.js","_app/immutable/chunks/oFYcwlI3.js"];
const stylesheets = ["_app/immutable/assets/DefaultErrBlock.BZ3oWHWt.css","_app/immutable/assets/BaseDialogWithCloseButton.DNNh1bvf.css","_app/immutable/assets/8.Bdzm0hrM.css"];
const fonts = [];

export { component, fonts, imports, index, _page_server_ts as server, server_id, stylesheets };
//# sourceMappingURL=8-CQEpjEPr.js.map
