import { q as bind_props, p as push, l as pop, t as attr, w as stringify } from './index2-DGUJ8ZVV.js';
import { S as StringUtils } from './date-utils-BMgSYFUH.js';

async function tick() {
}
function BaseDialog($$payload, $$props) {
  push();
  const {
    dialogId = StringUtils.rndStrWithPref("dialog-"),
    children
  } = $$props;
  let show = false;
  let dialogElement;
  async function open() {
    show = true;
    await tick();
    dialogElement.showModal();
  }
  function close() {
    dialogElement.close();
  }
  if (show) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<dialog${attr("id", dialogId)} class="base-dialog svelte-oypo1d"><div class="dialog-content"${attr("id", `${stringify(dialogId)}-dialog-content`)}>`;
    children($$payload);
    $$payload.out += `<!----></div></dialog>`;
  } else {
    $$payload.out += "<!--[!-->";
  }
  $$payload.out += `<!--]-->`;
  bind_props($$props, { open, close });
  pop();
}
function BaseDialogWithCloseButton($$payload, $$props) {
  const { dialogId = null, children } = $$props;
  async function open() {
    await dialogElement.open();
  }
  function close() {
    dialogElement.close();
  }
  let dialogElement;
  BaseDialog($$payload, {
    dialogId,
    children: ($$payload2) => {
      $$payload2.out += `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" class="close-button svelte-15hmxw0" aria-label="Close dialog" fill="none" role="button" tabindex="0"><path d="M19.0005 4.99988L5.00049 18.9999M5.00049 4.99988L19.0005 18.9999" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
      children($$payload2);
      $$payload2.out += `<!---->`;
    },
    $$slots: { default: true }
  });
  bind_props($$props, { open, close });
}

export { BaseDialogWithCloseButton as B };
//# sourceMappingURL=BaseDialogWithCloseButton-C-4-7gO8.js.map
