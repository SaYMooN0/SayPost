import { p as push, y as await_block, l as pop, u as escape_html } from './index2-DGUJ8ZVV.js';
import { p as page } from './index3-DT2S3G6t.js';
import { C as CubeLoader } from './CubeLoader-CcsMcLHy.js';
import { a as ApiAuth } from './backend-services-gK9Cr6ja.js';
import { P as PlainErrUtils } from './t-plain-err-CtrWZBuG.js';
import './client2-BT_TsMl5.js';
import './client-Dg0CqvnW.js';
import './exports-CuVW_EtN.js';
import './date-utils-BMgSYFUH.js';

function _page($$payload, $$props) {
  push();
  let userId = page.params.userId;
  let confirmationCode = page.params.confirmationCode;
  async function confirmRegistration() {
    return ApiAuth.fetchVoidResponse("/confirm-registration", ApiAuth.requestJsonOptions({ userId, confirmationCode }));
  }
  $$payload.out += `<div class="view-container svelte-1324r3e"><!---->`;
  await_block(
    confirmRegistration(),
    () => {
      $$payload.out += `<h1 class="loading-h svelte-1324r3e">Confirming your email</h1> `;
      CubeLoader($$payload);
      $$payload.out += `<!---->`;
    },
    (response) => {
      if (response.isSuccess) {
        $$payload.out += "<!--[-->";
        $$payload.out += `<h1 class="svelte-1324r3e">You have successfully confirmed your email</h1> <a href="/new-post" class="new-post-link svelte-1324r3e">Now you can create your own posts</a>`;
      } else {
        $$payload.out += "<!--[!-->";
        $$payload.out += `<h1 class="error-h svelte-1324r3e">An error has occurred during confirmation</h1> <div class="err-view svelte-1324r3e">${escape_html(response.errors[0].message)} `;
        if (PlainErrUtils.HasNonEmptyDetails(response.errors[0])) {
          $$payload.out += "<!--[-->";
          $$payload.out += `<p class="svelte-1324r3e">Details: ${escape_html(response.errors[0].details)}</p>`;
        } else {
          $$payload.out += "<!--[!-->";
        }
        $$payload.out += `<!--]--> `;
        if (PlainErrUtils.HasSpecifiedCode(response.errors[0])) {
          $$payload.out += "<!--[-->";
          $$payload.out += `<p class="svelte-1324r3e">Code: ${escape_html(response.errors[0].code)}</p>`;
        } else {
          $$payload.out += "<!--[!-->";
        }
        $$payload.out += `<!--]--></div>`;
      }
      $$payload.out += `<!--]-->`;
    }
  );
  $$payload.out += `<!----></div>`;
  pop();
}

export { _page as default };
//# sourceMappingURL=_page.svelte-DWj8lkdA.js.map
