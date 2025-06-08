import { p as push, y as await_block, l as pop } from './index2-DGUJ8ZVV.js';
import { a as ApiAuth } from './backend-services-gK9Cr6ja.js';
import { S as StringUtils } from './date-utils-BMgSYFUH.js';

class AuthStoreData {
  _username;
  _userId;
  _lastFetched;
  constructor(username, userId, lastFetched) {
    this._username = username;
    this._userId = userId;
    this._lastFetched = lastFetched;
  }
  get Username() {
    return this._username;
  }
  get UserId() {
    return this._userId;
  }
  get LastFetched() {
    return this._lastFetched;
  }
  isAuthenticated() {
    return !StringUtils.isNullOrWhiteSpace(this.Username) && !StringUtils.isNullOrWhiteSpace(this.UserId);
  }
  update(username, userId) {
    this._username = username;
    this._userId = userId;
    this._lastFetched = /* @__PURE__ */ new Date();
  }
  setEmpty() {
    this._username = null;
    this._userId = null;
    this._lastFetched = /* @__PURE__ */ new Date();
  }
}
const authStoreData = new AuthStoreData("", "", new Date(1970, 0, 0));
async function fetchAuthData() {
  const response = await ApiAuth.fetchJsonResponse("/ping", { method: "POST" });
  if (response.isSuccess) {
    authStoreData.update(response.data.username, response.data.userId);
  } else {
    authStoreData.setEmpty();
  }
}
async function getAuthData() {
  let lastFetched = authStoreData.LastFetched;
  const now = /* @__PURE__ */ new Date();
  const two_minutes = 2 * 60 * 1e3;
  if (!lastFetched || now.getTime() - lastFetched.getTime() > two_minutes) {
    await fetchAuthData();
  }
  return authStoreData;
}
function AuthView($$payload, $$props) {
  push();
  const {
    loading = null,
    authenticated = null,
    unauthenticated = null
  } = $$props;
  $$payload.out += `<!---->`;
  await_block(
    getAuthData(),
    () => {
      loading?.($$payload);
      $$payload.out += `<!---->`;
    },
    (authData) => {
      if (authData !== null && authData.isAuthenticated()) {
        $$payload.out += "<!--[-->";
        authenticated?.($$payload, authData);
        $$payload.out += `<!---->`;
      } else {
        $$payload.out += "<!--[!-->";
        unauthenticated?.($$payload);
        $$payload.out += `<!---->`;
      }
      $$payload.out += `<!--]-->`;
    }
  );
  $$payload.out += `<!---->`;
  pop();
}

export { AuthView as A, getAuthData as g };
//# sourceMappingURL=AuthView-BQEuqrR5.js.map
