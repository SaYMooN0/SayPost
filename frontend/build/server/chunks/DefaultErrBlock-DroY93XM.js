import { p as push, x as ensure_array_like, l as pop, u as escape_html, v as attr_class } from './index2-DGUJ8ZVV.js';
import { P as PlainErrUtils } from './t-plain-err-CtrWZBuG.js';

function ErrView($$payload, $$props) {
  push();
  const { err } = $$props;
  $$payload.out += `<div class="err-container svelte-1q9vik7"><div class="err-message svelte-1q9vik7">${escape_html(err.message)} `;
  if (PlainErrUtils.HasSomethingExceptMessage(err)) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-1q9vik7"><path d="M17.9998 15C17.9998 15 13.5809 9.00001 11.9998 9C10.4187 8.99999 5.99985 15 5.99985 15" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
  } else {
    $$payload.out += "<!--[!-->";
  }
  $$payload.out += `<!--]--></div> `;
  if (PlainErrUtils.HasNonEmptyDetails(err)) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<label${attr_class("err-additional svelte-1q9vik7", void 0, { "hidden": true })}>Details: ${escape_html(err.details)}</label>`;
  } else {
    $$payload.out += "<!--[!-->";
  }
  $$payload.out += `<!--]--> `;
  if (PlainErrUtils.HasSpecifiedCode(err)) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<label${attr_class("err-additional svelte-1q9vik7", void 0, { "hidden": true })}>Code: ${escape_html(err.code)}</label>`;
  } else {
    $$payload.out += "<!--[!-->";
  }
  $$payload.out += `<!--]--></div>`;
  pop();
}
function DefaultErrBlock($$payload, $$props) {
  push();
  let { errList } = $$props;
  if (errList.length > 0) {
    $$payload.out += "<!--[-->";
    const each_array = ensure_array_like(errList);
    $$payload.out += `<div class="err-block svelte-gtimd"><!--[-->`;
    for (let $$index = 0, $$length = each_array.length; $$index < $$length; $$index++) {
      let err = each_array[$$index];
      ErrView($$payload, { err });
    }
    $$payload.out += `<!--]--></div>`;
  } else {
    $$payload.out += "<!--[!-->";
  }
  $$payload.out += `<!--]-->`;
  pop();
}

export { DefaultErrBlock as D, ErrView as E };
//# sourceMappingURL=DefaultErrBlock-DroY93XM.js.map
