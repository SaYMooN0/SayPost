import { p as push, l as pop } from './index2-DGUJ8ZVV.js';
import { D as DefaultErrBlock, E as ErrView } from './DefaultErrBlock-DroY93XM.js';
import { U as UserList } from './UserList-BYI9sijr.js';
import './t-plain-err-CtrWZBuG.js';
import './date-utils-BMgSYFUH.js';
import './ListIsEmptyComponent-cTEh1X_9.js';

function _page($$payload, $$props) {
  push();
  let { data } = $$props;
  if (data.errors && data.errors.length > 0) {
    $$payload.out += "<!--[-->";
    DefaultErrBlock($$payload, { errList: data.errors });
  } else if (data.users === void 0) {
    $$payload.out += "<!--[1-->";
    ErrView($$payload, {
      err: { message: "Unable to load user's followings" }
    });
  } else {
    $$payload.out += "<!--[!-->";
    UserList($$payload, { users: data.users });
  }
  $$payload.out += `<!--]-->`;
  pop();
}

export { _page as default };
//# sourceMappingURL=_page.svelte-1YATr5_y.js.map
