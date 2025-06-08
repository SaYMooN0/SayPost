import { p as push, x as ensure_array_like, u as escape_html, l as pop } from './index2-DGUJ8ZVV.js';
import { D as DefaultErrBlock, E as ErrView } from './DefaultErrBlock-DroY93XM.js';
import { L as ListIsEmptyComponent } from './ListIsEmptyComponent-cTEh1X_9.js';
import { P as PostBriefDataPreview } from './PostBriefDataPreview-CyfWHXcZ.js';
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
      err: { message: "Unable to load user's liked posts" }
    });
  } else if (data.posts.length === 0) {
    $$payload.out += "<!--[2-->";
    ListIsEmptyComponent($$payload);
  } else {
    $$payload.out += "<!--[!-->";
    const each_array = ensure_array_like(data.posts);
    $$payload.out += `<div class="posts-container svelte-1ohxlqy"><!--[-->`;
    for (let $$index_1 = 0, $$length = each_array.length; $$index_1 < $$length; $$index_1++) {
      let post = each_array[$$index_1];
      const each_array_1 = ensure_array_like(post.comments);
      $$payload.out += `<div class="post-wrapper svelte-1ohxlqy">`;
      PostBriefDataPreview($$payload, {
        id: post.postId,
        title: post.postTitle,
        authorUsername: post.authorUsername,
        authorId: post.postAuthorId
      });
      $$payload.out += `<!----> <!--[-->`;
      for (let $$index = 0, $$length2 = each_array_1.length; $$index < $$length2; $$index++) {
        let comment = each_array_1[$$index];
        $$payload.out += `<div class="comment svelte-1ohxlqy">${escape_html(comment)}</div>`;
      }
      $$payload.out += `<!--]--></div>`;
    }
    $$payload.out += `<!--]--></div>`;
  }
  $$payload.out += `<!--]-->`;
  pop();
}

export { _page as default };
//# sourceMappingURL=_page.svelte-Nti2ew-H.js.map
