import { u as escape_html, t as attr, w as stringify } from './index2-DGUJ8ZVV.js';

function PostBriefDataPreview($$payload, $$props) {
  const { id, title, authorUsername, authorId } = $$props;
  $$payload.out += `<div class="post svelte-fp5pip"><h1 class="title svelte-fp5pip">${escape_html(title)}</h1> <label class="author svelte-fp5pip">by <a${attr("href", `/users/${stringify(authorId)}`)} data-sveltekit-preload-data="tap" class="svelte-fp5pip">`;
  if (authorUsername) {
    $$payload.out += "<!--[-->";
    $$payload.out += `${escape_html(authorUsername)}`;
  } else {
    $$payload.out += "<!--[!-->";
    $$payload.out += `unable to load username`;
  }
  $$payload.out += `<!--]--></a></label> <a${attr("href", `/posts/${stringify(id)}`)} class="read-btn svelte-fp5pip" data-sveltekit-preload-data="hover">Read</a></div>`;
}

export { PostBriefDataPreview as P };
//# sourceMappingURL=PostBriefDataPreview-CyfWHXcZ.js.map
