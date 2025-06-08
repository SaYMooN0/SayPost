import { p as push, t as attr, u as escape_html, w as stringify, l as pop } from './index2-DGUJ8ZVV.js';
import { p as page } from './index3-DT2S3G6t.js';
import './client2-BT_TsMl5.js';
import './client-Dg0CqvnW.js';
import './exports-CuVW_EtN.js';

function _layout($$payload, $$props) {
  push();
  const { children } = $$props;
  $$payload.out += `<div class="layout-header svelte-lylwrj"><a${attr("href", `/users/${stringify(page.params.userId)}`)} class="back-btn svelte-lylwrj" data-sveltekit-preload-data="hover"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-lylwrj"><path d="M8.99996 16.9998L4 11.9997L9 6.99976" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path><path d="M4 12H20" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path></svg></a> User's ${escape_html(page.data.title)}</div> `;
  children($$payload);
  $$payload.out += `<!---->`;
  pop();
}

export { _layout as default };
//# sourceMappingURL=_layout.svelte-BZxISk9u.js.map
