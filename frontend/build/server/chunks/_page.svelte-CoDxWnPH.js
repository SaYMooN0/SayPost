import { p as push, m as copy_payload, n as assign_payload, l as pop, x as ensure_array_like, q as bind_props, v as attr_class, t as attr, u as escape_html } from './index2-DGUJ8ZVV.js';
import { g as goto } from './client-Dg0CqvnW.js';
import './client2-BT_TsMl5.js';
import { D as DefaultErrBlock } from './DefaultErrBlock-DroY93XM.js';
import { P as PostPreviewComponent } from './PostPreviewComponent-CKtHevTP.js';
import './date-utils-BMgSYFUH.js';
import './exports-CuVW_EtN.js';
import './t-plain-err-CtrWZBuG.js';

class PostsFilterState {
  dateFrom = "";
  //"YYYY-MM-DD"
  dateTo = "";
  //"YYYY-MM-DD"
  includeTags = [];
  excludeTags = [];
  constructor() {
  }
  reset() {
    this.dateFrom = "";
    this.dateTo = "";
    this.includeTags = [];
    this.excludeTags = [];
  }
  applyQueryParams(params) {
    const tagsParam = params.get("includeTags");
    this.includeTags = tagsParam ? tagsParam.split(",") : [];
    const excludeTagsParam = params.get("excludeTags");
    this.excludeTags = excludeTagsParam ? excludeTagsParam.split(",") : [];
    const from = params.get("dateFrom");
    if (from) {
      const date = new Date(parseInt(from));
      this.dateFrom = date.toISOString().slice(0, 10);
    }
    const to = params.get("dateTo");
    if (to) {
      const date = new Date(parseInt(to));
      this.dateTo = date.toISOString().slice(0, 10);
    }
  }
  getQuery() {
    const params = new URLSearchParams();
    if (this.includeTags.length > 0) {
      params.set("includeTags", this.includeTags.join(","));
    }
    if (this.excludeTags.length > 0) {
      params.set("excludeTags", this.excludeTags.join(","));
    }
    if (this.dateFrom) {
      params.set("dateFrom", new Date(this.dateFrom).getTime().toString());
    }
    if (this.dateTo) {
      params.set("dateTo", new Date(this.dateTo).getTime().toString());
    }
    return params.toString();
  }
}
function FilterTagsSelection($$payload, $$props) {
  push();
  let { selectedTags = void 0 } = $$props;
  let $$settled = true;
  let $$inner_payload;
  function $$render_inner($$payload2) {
    const each_array = ensure_array_like(selectedTags);
    $$payload2.out += `<div class="tags svelte-15tj9u6"><!--[-->`;
    for (let $$index = 0, $$length = each_array.length; $$index < $$length; $$index++) {
      let tag = each_array[$$index];
      $$payload2.out += `<div class="tag svelte-15tj9u6">${escape_html(tag)} <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-15tj9u6"><path d="M19.0005 4.99988L5.00049 18.9999M5.00049 4.99988L19.0005 18.9999" stroke="currentColor" stroke-width="2.4" stroke-linecap="round" stroke-linejoin="round"></path></svg></div>`;
    }
    $$payload2.out += `<!--]--> `;
    {
      $$payload2.out += "<!--[-->";
      $$payload2.out += `<button class="add-tags-btn unselectable svelte-15tj9u6">Add tags</button>`;
    }
    $$payload2.out += `<!--]--></div> `;
    {
      $$payload2.out += "<!--[!-->";
    }
    $$payload2.out += `<!--]-->`;
  }
  do {
    $$settled = true;
    $$inner_payload = copy_payload($$payload);
    $$render_inner($$inner_payload);
  } while (!$$settled);
  assign_payload($$payload, $$inner_payload);
  bind_props($$props, { selectedTags });
  pop();
}
function PostsFilter($$payload, $$props) {
  push();
  let { postsFilter = void 0, applyFilter } = $$props;
  let isVisible = false;
  let $$settled = true;
  let $$inner_payload;
  function $$render_inner($$payload2) {
    $$payload2.out += `<div class="container svelte-o0y004"><p${attr_class("always-shown unselectable svelte-o0y004", void 0, { "filter-not-hidden": isVisible })}>Posts filter <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-o0y004"><path d="M17.9998 15C17.9998 15 13.5809 9.00001 11.9998 9C10.4187 8.99999 5.99985 15 5.99985 15" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path></svg></p> <div${attr_class("filter svelte-o0y004", void 0, { "hidden": true })}><p class="filter-p svelte-o0y004">Published from: <input type="date"${attr("value", postsFilter.dateFrom)} class="svelte-o0y004"> to: <input type="date"${attr("value", postsFilter.dateTo)} class="svelte-o0y004"></p> <p class="filter-p svelte-o0y004">Include tags: `;
    if (postsFilter.includeTags.length === 0) {
      $$payload2.out += "<!--[-->";
      $$payload2.out += `<span class="no-tags-selected svelte-o0y004">(No tags selected)</span>`;
    } else {
      $$payload2.out += "<!--[!-->";
    }
    $$payload2.out += `<!--]--></p> `;
    FilterTagsSelection($$payload2, {
      get selectedTags() {
        return postsFilter.includeTags;
      },
      set selectedTags($$value) {
        postsFilter.includeTags = $$value;
        $$settled = false;
      }
    });
    $$payload2.out += `<!----> <p class="filter-p svelte-o0y004">Exclude tags: `;
    if (postsFilter.excludeTags.length === 0) {
      $$payload2.out += "<!--[-->";
      $$payload2.out += `<span class="no-tags-selected svelte-o0y004">(No tags selected)</span>`;
    } else {
      $$payload2.out += "<!--[!-->";
    }
    $$payload2.out += `<!--]--></p> `;
    FilterTagsSelection($$payload2, {
      get selectedTags() {
        return postsFilter.excludeTags;
      },
      set selectedTags($$value) {
        postsFilter.excludeTags = $$value;
        $$settled = false;
      }
    });
    $$payload2.out += `<!----> <div class="filter-btns svelte-o0y004"><button class="reset svelte-o0y004">Reset</button> <button class="apply svelte-o0y004">Apply</button></div></div></div>`;
  }
  do {
    $$settled = true;
    $$inner_payload = copy_payload($$payload);
    $$render_inner($$inner_payload);
  } while (!$$settled);
  assign_payload($$payload, $$inner_payload);
  bind_props($$props, { postsFilter });
  pop();
}
function _page($$payload, $$props) {
  push();
  let postsFilter = new PostsFilterState();
  let posts = [];
  let postsFetchingErrs = [];
  function applyFilter() {
    goto(`/posts?${postsFilter.getQuery()}`);
  }
  let $$settled = true;
  let $$inner_payload;
  function $$render_inner($$payload2) {
    $$payload2.out += `<div class="page svelte-d988ns">`;
    PostsFilter($$payload2, {
      applyFilter,
      get postsFilter() {
        return postsFilter;
      },
      set postsFilter($$value) {
        postsFilter = $$value;
        $$settled = false;
      }
    });
    $$payload2.out += `<!----> `;
    if (postsFetchingErrs.length != 0) {
      $$payload2.out += "<!--[-->";
      DefaultErrBlock($$payload2, { errList: postsFetchingErrs });
    } else if (posts.length == 0) {
      $$payload2.out += "<!--[1-->";
      $$payload2.out += `<h1>There are no posts</h1>`;
    } else {
      $$payload2.out += "<!--[!-->";
      const each_array = ensure_array_like(posts);
      $$payload2.out += `<div class="posts-container svelte-d988ns"><!--[-->`;
      for (let $$index = 0, $$length = each_array.length; $$index < $$length; $$index++) {
        let post = each_array[$$index];
        PostPreviewComponent($$payload2, { post });
      }
      $$payload2.out += `<!--]--></div>`;
    }
    $$payload2.out += `<!--]--></div>`;
  }
  do {
    $$settled = true;
    $$inner_payload = copy_payload($$payload);
    $$render_inner($$inner_payload);
  } while (!$$settled);
  assign_payload($$payload, $$inner_payload);
  pop();
}

export { _page as default };
//# sourceMappingURL=_page.svelte-CoDxWnPH.js.map
