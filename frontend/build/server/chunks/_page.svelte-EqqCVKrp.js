import { p as push, x as ensure_array_like, l as pop } from './index2-DGUJ8ZVV.js';
import { D as DefaultErrBlock, E as ErrView } from './DefaultErrBlock-DroY93XM.js';
import { P as PostPreviewComponent } from './PostPreviewComponent-CKtHevTP.js';
import { L as ListIsEmptyComponent } from './ListIsEmptyComponent-cTEh1X_9.js';
import './t-plain-err-CtrWZBuG.js';
import './date-utils-BMgSYFUH.js';

function _page($$payload, $$props) {
  push();
  let { data } = $$props;
  if (data.errors && data.errors.length > 0) {
    $$payload.out += "<!--[-->";
    DefaultErrBlock($$payload, { errList: data.errors });
  } else if (data.posts === void 0) {
    $$payload.out += "<!--[1-->";
    ErrView($$payload, {
      err: {
        message: "Unable to load user's published posts"
      }
    });
  } else if (data.posts.length === 0) {
    $$payload.out += "<!--[2-->";
    ListIsEmptyComponent($$payload);
  } else {
    $$payload.out += "<!--[!-->";
    const each_array = ensure_array_like(data.posts);
    $$payload.out += `<div class="posts-container svelte-1j584e"><!--[-->`;
    for (let $$index = 0, $$length = each_array.length; $$index < $$length; $$index++) {
      let post = each_array[$$index];
      PostPreviewComponent($$payload, { post });
    }
    $$payload.out += `<!--]--></div>`;
  }
  $$payload.out += `<!--]-->`;
  pop();
}

export { _page as default };
//# sourceMappingURL=_page.svelte-EqqCVKrp.js.map
