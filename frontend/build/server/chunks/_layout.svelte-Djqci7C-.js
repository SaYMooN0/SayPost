import { p as push, m as copy_payload, n as assign_payload, q as bind_props, l as pop, t as attr, u as escape_html, v as attr_class, w as stringify, x as ensure_array_like } from './index2-DGUJ8ZVV.js';
import { B as BaseDialogWithCloseButton } from './BaseDialogWithCloseButton-C-4-7gO8.js';
import { A as AuthView, g as getAuthData } from './AuthView-BQEuqrR5.js';
import { D as DefaultErrBlock } from './DefaultErrBlock-DroY93XM.js';
import { G as GrayLabelWithOnclick } from './GrayLabelWithOnclick-DkBFCD6X.js';
import { S as StringUtils, D as DateUtils } from './date-utils-BMgSYFUH.js';
import './client-Dg0CqvnW.js';
import './backend-services-gK9Cr6ja.js';
import './t-plain-err-CtrWZBuG.js';
import './exports-CuVW_EtN.js';

function AuthDialogEmailSentState($$payload, $$props) {
  const { email } = $$props;
  $$payload.out += `<p class="dialog-title">Thank you for registering</p> <img src="/email-sent.svg" alt="email sent" class="unselectable svelte-og4hw0"> <h1 class="svelte-og4hw0">Confirmation link has been sent to <span class="svelte-og4hw0">${escape_html(email)}</span></h1> <p class="second-p svelte-og4hw0">Please check your inbox</p>`;
}
function AuthDialogLoggedInState($$payload) {
  $$payload.out += `<h2>It seems like you are already logged in</h2> <button>Log out</button>`;
}
function AuthDialogInput($$payload, $$props) {
  push();
  let {
    type = "text",
    fieldName,
    value = void 0,
    children
  } = $$props;
  let inputName = StringUtils.rndStr(8);
  $$payload.out += `<div class="input-field unselectable svelte-13mbceh"><input required autocomplete="off"${attr("type", type)}${attr("name", inputName)} placeholder=" "${attr("value", value)} class="svelte-13mbceh"> <label${attr("for", inputName)} class="svelte-13mbceh">`;
  children?.($$payload);
  $$payload.out += `<!----> ${escape_html(fieldName)}</label></div>`;
  bind_props($$props, { value });
  pop();
}
function AuthDialogLoginState($$payload, $$props) {
  push();
  let {
    email = void 0,
    changeStateToSignUp,
    closeDialog
  } = $$props;
  let password = "";
  let errList = [];
  let $$settled = true;
  let $$inner_payload;
  function $$render_inner($$payload2) {
    $$payload2.out += `<p class="dialog-title">Enter your account</p> `;
    AuthDialogInput($$payload2, {
      type: "email",
      fieldName: "Email",
      get value() {
        return email;
      },
      set value($$value) {
        email = $$value;
        $$settled = false;
      },
      children: ($$payload3) => {
        $$payload3.out += `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M2 5L8.91302 8.92462C11.4387 10.3585 12.5613 10.3585 15.087 8.92462L22 5" stroke="currentColor" stroke-width="1.5" stroke-linejoin="round"></path><path d="M10.5 19.5C10.0337 19.4939 9.56682 19.485 9.09883 19.4732C5.95033 19.3941 4.37608 19.3545 3.24496 18.2184C2.11383 17.0823 2.08114 15.5487 2.01577 12.4814C1.99475 11.4951 1.99474 10.5147 2.01576 9.52843C2.08114 6.46113 2.11382 4.92748 3.24495 3.79139C4.37608 2.6553 5.95033 2.61573 9.09882 2.53658C11.0393 2.4878 12.9607 2.48781 14.9012 2.53659C18.0497 2.61574 19.6239 2.65532 20.755 3.79141C21.8862 4.92749 21.9189 6.46114 21.9842 9.52844C21.9939 9.98251 21.9991 10.1965 21.9999 10.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M19 17C19 17.8284 18.3284 18.5 17.5 18.5C16.6716 18.5 16 17.8284 16 17C16 16.1716 16.6716 15.5 17.5 15.5C18.3284 15.5 19 16.1716 19 17ZM19 17V17.5C19 18.3284 19.6716 19 20.5 19C21.3284 19 22 18.3284 22 17.5V17C22 14.5147 19.9853 12.5 17.5 12.5C15.0147 12.5 13 14.5147 13 17C13 19.4853 15.0147 21.5 17.5 21.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
      },
      $$slots: { default: true }
    });
    $$payload2.out += `<!----> `;
    AuthDialogInput($$payload2, {
      type: "password",
      fieldName: "Password",
      get value() {
        return password;
      },
      set value($$value) {
        password = $$value;
        $$settled = false;
      },
      children: ($$payload3) => {
        $$payload3.out += `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M4.26781 18.8447C4.49269 20.515 5.87613 21.8235 7.55966 21.9009C8.97627 21.966 10.4153 22 12 22C13.5847 22 15.0237 21.966 16.4403 21.9009C18.1239 21.8235 19.5073 20.515 19.7322 18.8447C19.879 17.7547 20 16.6376 20 15.5C20 14.3624 19.879 13.2453 19.7322 12.1553C19.5073 10.485 18.1239 9.17649 16.4403 9.09909C15.0237 9.03397 13.5847 9 12 9C10.4153 9 8.97627 9.03397 7.55966 9.09909C5.87613 9.17649 4.49269 10.485 4.26781 12.1553C4.12105 13.2453 4 14.3624 4 15.5C4 16.6376 4.12105 17.7547 4.26781 18.8447Z" stroke="currentColor" stroke-width="1.5"></path><path d="M7.5 9V6.5C7.5 4.01472 9.51472 2 12 2C14.4853 2 16.5 4.01472 16.5 6.5V9" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M16 15.49V15.5" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path><path d="M12 15.49V15.5" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path><path d="M8 15.49V15.5" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
      },
      $$slots: { default: true }
    });
    $$payload2.out += `<!----> `;
    DefaultErrBlock($$payload2, { errList });
    $$payload2.out += `<!----> <span class="svelte-1acihsx"></span> `;
    GrayLabelWithOnclick($$payload2, {
      text: "I have forgotten my password",
      onClick: () => {
        console.log("forgot password");
      }
    });
    $$payload2.out += `<!----> `;
    GrayLabelWithOnclick($$payload2, {
      text: "I don't have an account yet",
      onClick: () => changeStateToSignUp()
    });
    $$payload2.out += `<!----> <button class="dialog-main-btn unselectable">Login</button>`;
  }
  do {
    $$settled = true;
    $$inner_payload = copy_payload($$payload);
    $$render_inner($$inner_payload);
  } while (!$$settled);
  assign_payload($$payload, $$inner_payload);
  bind_props($$props, { email });
  pop();
}
function AuthDialogSignUpState($$payload, $$props) {
  push();
  let {
    email = void 0,
    changeStateToLogin,
    changeStateToEmailSent
  } = $$props;
  let password = "";
  let confirmPassword = "";
  let errList = [];
  let $$settled = true;
  let $$inner_payload;
  function $$render_inner($$payload2) {
    $$payload2.out += `<p class="dialog-title">Create an account</p> `;
    AuthDialogInput($$payload2, {
      type: "email",
      fieldName: "Email",
      get value() {
        return email;
      },
      set value($$value) {
        email = $$value;
        $$settled = false;
      },
      children: ($$payload3) => {
        $$payload3.out += `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M2 5L8.91302 8.92462C11.4387 10.3585 12.5613 10.3585 15.087 8.92462L22 5" stroke="currentColor" stroke-width="1.5" stroke-linejoin="round"></path><path d="M10.5 19.5C10.0337 19.4939 9.56682 19.485 9.09883 19.4732C5.95033 19.3941 4.37608 19.3545 3.24496 18.2184C2.11383 17.0823 2.08114 15.5487 2.01577 12.4814C1.99475 11.4951 1.99474 10.5147 2.01576 9.52843C2.08114 6.46113 2.11382 4.92748 3.24495 3.79139C4.37608 2.6553 5.95033 2.61573 9.09882 2.53658C11.0393 2.4878 12.9607 2.48781 14.9012 2.53659C18.0497 2.61574 19.6239 2.65532 20.755 3.79141C21.8862 4.92749 21.9189 6.46114 21.9842 9.52844C21.9939 9.98251 21.9991 10.1965 21.9999 10.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M19 17C19 17.8284 18.3284 18.5 17.5 18.5C16.6716 18.5 16 17.8284 16 17C16 16.1716 16.6716 15.5 17.5 15.5C18.3284 15.5 19 16.1716 19 17ZM19 17V17.5C19 18.3284 19.6716 19 20.5 19C21.3284 19 22 18.3284 22 17.5V17C22 14.5147 19.9853 12.5 17.5 12.5C15.0147 12.5 13 14.5147 13 17C13 19.4853 15.0147 21.5 17.5 21.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
      },
      $$slots: { default: true }
    });
    $$payload2.out += `<!----> `;
    AuthDialogInput($$payload2, {
      type: "password",
      fieldName: "Password",
      get value() {
        return password;
      },
      set value($$value) {
        password = $$value;
        $$settled = false;
      },
      children: ($$payload3) => {
        $$payload3.out += `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M4.26781 18.8447C4.49269 20.515 5.87613 21.8235 7.55966 21.9009C8.97627 21.966 10.4153 22 12 22C13.5847 22 15.0237 21.966 16.4403 21.9009C18.1239 21.8235 19.5073 20.515 19.7322 18.8447C19.879 17.7547 20 16.6376 20 15.5C20 14.3624 19.879 13.2453 19.7322 12.1553C19.5073 10.485 18.1239 9.17649 16.4403 9.09909C15.0237 9.03397 13.5847 9 12 9C10.4153 9 8.97627 9.03397 7.55966 9.09909C5.87613 9.17649 4.49269 10.485 4.26781 12.1553C4.12105 13.2453 4 14.3624 4 15.5C4 16.6376 4.12105 17.7547 4.26781 18.8447Z" stroke="currentColor" stroke-width="1.5"></path><path d="M7.5 9V6.5C7.5 4.01472 9.51472 2 12 2C14.4853 2 16.5 4.01472 16.5 6.5V9" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M16 15.49V15.5" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path><path d="M12 15.49V15.5" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path><path d="M8 15.49V15.5" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
      },
      $$slots: { default: true }
    });
    $$payload2.out += `<!----> `;
    AuthDialogInput($$payload2, {
      type: "password",
      fieldName: "Confirm Password",
      get value() {
        return confirmPassword;
      },
      set value($$value) {
        confirmPassword = $$value;
        $$settled = false;
      },
      children: ($$payload3) => {
        $$payload3.out += `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M15.395 21.9009C13.9836 21.966 12.5498 22 10.9709 22C9.39194 22 7.95815 21.966 6.5467 21.9009C4.8693 21.8235 3.4909 20.515 3.26684 18.8447C3.12061 17.7547 3 16.6376 3 15.5C3 14.3624 3.1206 13.2453 3.26684 12.1553C3.4909 10.485 4.8693 9.17649 6.5467 9.09909C7.95815 9.03397 9.39194 9 10.9709 9C12.5498 9 13.9836 9.03397 15.395 9.09909C16.4545 9.14798 17.3946 9.68796 18 10.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path><path d="M17 14.978C17 13.8856 17.8954 13 19 13C20.1046 13 21 13.8856 21 14.978C21 15.3718 20.8837 15.7387 20.6831 16.0469C20.0854 16.9656 19 17.8416 19 18.9341V19.4286M19 22H19.012" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M6.5 9V6.5C6.5 4.01472 8.51472 2 11 2C13.4853 2 15.5 4.01472 15.5 6.5V9" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M12 15.49V15.5" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path><path d="M8 15.49V15.5" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
      },
      $$slots: { default: true }
    });
    $$payload2.out += `<!----> `;
    DefaultErrBlock($$payload2, { errList });
    $$payload2.out += `<!----> <span class="svelte-1acihsx"></span> `;
    GrayLabelWithOnclick($$payload2, {
      text: "I already have an account",
      onClick: () => changeStateToLogin()
    });
    $$payload2.out += `<!----> <button class="dialog-main-btn unselectable">Sign Up</button>`;
  }
  do {
    $$settled = true;
    $$inner_payload = copy_payload($$payload);
    $$render_inner($$inner_payload);
  } while (!$$settled);
  assign_payload($$payload, $$inner_payload);
  bind_props($$props, { email });
  pop();
}
function LayoutAuthDialog($$payload, $$props) {
  push();
  var DialogState = /* @__PURE__ */ ((DialogState2) => {
    DialogState2[DialogState2["SignUp"] = 0] = "SignUp";
    DialogState2[DialogState2["Login"] = 1] = "Login";
    DialogState2[DialogState2["LoggedIn"] = 2] = "LoggedIn";
    DialogState2[DialogState2["EmailSent"] = 3] = "EmailSent";
    return DialogState2;
  })(DialogState || {});
  let contentState = 0;
  function close() {
    dialogElement.close();
  }
  async function openLogin() {
    if ((await getAuthData()).isAuthenticated()) {
      contentState = 2;
    } else {
      contentState = 1;
    }
    dialogElement.open();
  }
  async function openSignUp() {
    if ((await getAuthData()).isAuthenticated()) {
      contentState = 2;
    } else {
      contentState = 0;
    }
    dialogElement.open();
  }
  let dialogElement;
  let email = "";
  let $$settled = true;
  let $$inner_payload;
  function $$render_inner($$payload2) {
    BaseDialogWithCloseButton($$payload2, {
      dialogId: "auth-dialog",
      children: ($$payload3) => {
        if (contentState === DialogState.SignUp) {
          $$payload3.out += "<!--[-->";
          AuthDialogSignUpState($$payload3, {
            changeStateToLogin: () => contentState = DialogState.Login,
            changeStateToEmailSent: () => contentState = DialogState.EmailSent,
            get email() {
              return email;
            },
            set email($$value) {
              email = $$value;
              $$settled = false;
            }
          });
        } else if (contentState === DialogState.EmailSent) {
          $$payload3.out += "<!--[1-->";
          AuthDialogEmailSentState($$payload3, { email });
        } else if (contentState === DialogState.Login) {
          $$payload3.out += "<!--[2-->";
          AuthDialogLoginState($$payload3, {
            changeStateToSignUp: () => contentState = DialogState.SignUp,
            closeDialog: () => close(),
            get email() {
              return email;
            },
            set email($$value) {
              email = $$value;
              $$settled = false;
            }
          });
        } else if (contentState === DialogState.LoggedIn) {
          $$payload3.out += "<!--[3-->";
          AuthDialogLoggedInState($$payload3);
        } else {
          $$payload3.out += "<!--[!-->";
        }
        $$payload3.out += `<!--]-->`;
      },
      $$slots: { default: true }
    });
  }
  do {
    $$settled = true;
    $$inner_payload = copy_payload($$payload);
    $$render_inner($$inner_payload);
  } while (!$$settled);
  assign_payload($$payload, $$inner_payload);
  bind_props($$props, { close, openLogin, openSignUp });
  pop();
}
function NotificationContentDisplay($$payload, $$props) {
  push();
  let { notification } = $$props;
  $$payload.out += `<p class="svelte-1d1qmvm">`;
  if (notification.type === "CommentLeft") {
    $$payload.out += "<!--[-->";
    $$payload.out += `<a class="userlink svelte-1d1qmvm"${attr("href", `/users/${stringify(notification.commentAuthorId)}`)}>User</a> commented your post <span class="post-title">${escape_html(notification.postTitle)}</span>`;
  } else if (notification.type === "PostLiked") {
    $$payload.out += "<!--[1-->";
    $$payload.out += `<a class="userlink svelte-1d1qmvm"${attr("href", `/users/${stringify(notification.userThatLikedId)}`)}>User</a> liked your post ${escape_html(notification.postId)}`;
  } else if (notification.type === "PostPublished") {
    $$payload.out += "<!--[2-->";
    $$payload.out += `<a class="userlink svelte-1d1qmvm"${attr("href", `/users/${stringify(notification.postAuthorId)}`)}>User</a> published new post ${escape_html(notification.postId)}`;
  } else if (notification.type === "UserGotFollower") {
    $$payload.out += "<!--[3-->";
    $$payload.out += `<a class="userlink svelte-1d1qmvm"${attr("href", `/users/${stringify(notification.followerId)}`)}>User</a> followed you`;
  } else {
    $$payload.out += "<!--[!-->";
    $$payload.out += `Notification of unsupported type`;
  }
  $$payload.out += `<!--]--></p>`;
  pop();
}
function NotificationItemsList($$payload, $$props) {
  push();
  let { notifications = void 0 } = $$props;
  if (notifications.length === 0) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<p class="no-notifications svelte-3fort8">No notifications</p>`;
  } else {
    $$payload.out += "<!--[!-->";
    const each_array = ensure_array_like(notifications);
    $$payload.out += `<div class="notifications-list svelte-3fort8"><!--[-->`;
    for (let $$index = 0, $$length = each_array.length; $$index < $$length; $$index++) {
      let notification = each_array[$$index];
      $$payload.out += `<div class="notification-item svelte-3fort8">`;
      NotificationContentDisplay($$payload, {
        notification: {
          ...notification.typeSpecificData,
          type: notification.type
        }
      });
      $$payload.out += `<!----> <div class="bottom-part svelte-3fort8"><div${attr_class("not-viewed-dot svelte-3fort8", void 0, { "visible": !notification.viewed })}></div> <label class="date svelte-3fort8">${escape_html(DateUtils.toLocale(notification.dateTime))}</label></div></div>`;
    }
    $$payload.out += `<!--]--></div>`;
  }
  $$payload.out += `<!--]-->`;
  bind_props($$props, { notifications });
  pop();
}
function LayoutNotificationsBlock($$payload, $$props) {
  push();
  let notificationsListOpen = false;
  let fetchingErrs = [];
  let notifications = [];
  $$payload.out += `<svg class="block-item unselectable svelte-arupwk" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">`;
  {
    $$payload.out += "<!--[!-->";
    $$payload.out += `<path d="M2.5 13H8.5V13.5C8.5 15.433 10.067 17 12 17C13.933 17 15.5 15.433 15.5 13.5V13H21.5" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path><path d="M4.5 2.5H19.5C20.6046 2.5 21.5 3.39543 21.5 4.5V19.5C21.5 20.6046 20.6046 21.5 19.5 21.5H4.5C3.39543 21.5 2.5 20.6046 2.5 19.5V4.5C2.5 3.39543 3.39543 2.5 4.5 2.5Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path>`;
  }
  $$payload.out += `<!--]--></svg> <div${attr_class("context-menu unselectable svelte-arupwk", void 0, { "open": notificationsListOpen })}>`;
  if (fetchingErrs.length > 0) {
    $$payload.out += "<!--[-->";
    DefaultErrBlock($$payload, { errList: fetchingErrs });
  } else {
    $$payload.out += "<!--[!-->";
    NotificationItemsList($$payload, { notifications });
  }
  $$payload.out += `<!--]--> <div class="settings-link svelte-arupwk"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-arupwk"><path d="M21.3175 7.14139L20.8239 6.28479C20.4506 5.63696 20.264 5.31305 19.9464 5.18388C19.6288 5.05472 19.2696 5.15664 18.5513 5.36048L17.3311 5.70418C16.8725 5.80994 16.3913 5.74994 15.9726 5.53479L15.6357 5.34042C15.2766 5.11043 15.0004 4.77133 14.8475 4.37274L14.5136 3.37536C14.294 2.71534 14.1842 2.38533 13.9228 2.19657C13.6615 2.00781 13.3143 2.00781 12.6199 2.00781H11.5051C10.8108 2.00781 10.4636 2.00781 10.2022 2.19657C9.94085 2.38533 9.83106 2.71534 9.61149 3.37536L9.27753 4.37274C9.12465 4.77133 8.84845 5.11043 8.48937 5.34042L8.15249 5.53479C7.73374 5.74994 7.25259 5.80994 6.79398 5.70418L5.57375 5.36048C4.85541 5.15664 4.49625 5.05472 4.17867 5.18388C3.86109 5.31305 3.67445 5.63696 3.30115 6.28479L2.80757 7.14139C2.45766 7.74864 2.2827 8.05227 2.31666 8.37549C2.35061 8.69871 2.58483 8.95918 3.05326 9.48012L4.0843 10.6328C4.3363 10.9518 4.51521 11.5078 4.51521 12.0077C4.51521 12.5078 4.33636 13.0636 4.08433 13.3827L3.05326 14.5354C2.58483 15.0564 2.35062 15.3168 2.31666 15.6401C2.2827 15.9633 2.45766 16.2669 2.80757 16.8741L3.30114 17.7307C3.67443 18.3785 3.86109 18.7025 4.17867 18.8316C4.49625 18.9608 4.85542 18.8589 5.57377 18.655L6.79394 18.3113C7.25263 18.2055 7.73387 18.2656 8.15267 18.4808L8.4895 18.6752C8.84851 18.9052 9.12464 19.2442 9.2775 19.6428L9.61149 20.6403C9.83106 21.3003 9.94085 21.6303 10.2022 21.8191C10.4636 22.0078 10.8108 22.0078 11.5051 22.0078H12.6199C13.3143 22.0078 13.6615 22.0078 13.9228 21.8191C14.1842 21.6303 14.294 21.3003 14.5136 20.6403L14.8476 19.6428C15.0004 19.2442 15.2765 18.9052 15.6356 18.6752L15.9724 18.4808C16.3912 18.2656 16.8724 18.2055 17.3311 18.3113L18.5513 18.655C19.2696 18.8589 19.6288 18.9608 19.9464 18.8316C20.264 18.7025 20.4506 18.3785 20.8239 17.7307L21.3175 16.8741C21.6674 16.2669 21.8423 15.9633 21.8084 15.6401C21.7744 15.3168 21.5402 15.0564 21.0718 14.5354L20.0407 13.3827C19.7887 13.0636 19.6098 12.5078 19.6098 12.0077C19.6098 11.5078 19.7888 10.9518 20.0407 10.6328L21.0718 9.48012C21.5402 8.95918 21.7744 8.69871 21.8084 8.37549C21.8423 8.05227 21.6674 7.74864 21.3175 7.14139Z" stroke="currentColor" stroke-width="1.9" stroke-linecap="round"></path><path d="M15.5195 12C15.5195 13.933 13.9525 15.5 12.0195 15.5C10.0865 15.5 8.51953 13.933 8.51953 12C8.51953 10.067 10.0865 8.5 12.0195 8.5C13.9525 8.5 15.5195 10.067 15.5195 12Z" stroke="currentColor" stroke-width="1.9"></path></svg> Notification settings</div></div>`;
  pop();
}
function LayoutAccountBlock($$payload, $$props) {
  push();
  let { username, userId } = $$props;
  let isAccountContextMenuOpen = false;
  $$payload.out += `<div class="layout-account-block svelte-y6nb5o">`;
  LayoutNotificationsBlock($$payload);
  $$payload.out += `<!----> <div class="block-item user-profile unselectable svelte-y6nb5o">${escape_html(username)} <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-y6nb5o"><path d="M6.57757 15.4816C5.1628 16.324 1.45336 18.0441 3.71266 20.1966C4.81631 21.248 6.04549 22 7.59087 22H16.4091C17.9545 22 19.1837 21.248 20.2873 20.1966C22.5466 18.0441 18.8372 16.324 17.4224 15.4816C14.1048 13.5061 9.89519 13.5061 6.57757 15.4816Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path><path d="M16.5 6.5C16.5 8.98528 14.4853 11 12 11C9.51472 11 7.5 8.98528 7.5 6.5C7.5 4.01472 9.51472 2 12 2C14.4853 2 16.5 4.01472 16.5 6.5Z" stroke="currentColor" stroke-width="2"></path></svg></div> <div${attr_class("context-menu unselectable svelte-y6nb5o", void 0, { "open": isAccountContextMenuOpen })}><a${attr("href", `/users/${stringify(userId)}`)} class="menu-action svelte-y6nb5o"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M15 10C15 8.34315 13.6569 7 12 7C10.3431 7 9 8.34315 9 10C9 11.6569 10.3431 13 12 13C13.6569 13 15 11.6569 15 10Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M17 18C17 15.2386 14.7614 13 12 13C9.23858 13 7 15.2386 7 18" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M21 13V11C21 7.22876 21 5.34315 19.8284 4.17157C18.6569 3 16.7712 3 13 3H11C7.22876 3 5.34315 3 4.17157 4.17157C3 5.34315 3 7.22876 3 11V13C3 16.7712 3 18.6569 4.17157 19.8284C5.34315 21 7.22876 21 11 21H13C16.7712 21 18.6569 21 19.8284 19.8284C21 18.6569 21 16.7712 21 13Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="square" stroke-linejoin="round"></path></svg> My profile</a> <div class="menu-action svelte-y6nb5o"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M11 3L10.3374 3.23384C7.75867 4.144 6.46928 4.59908 5.73464 5.63742C5 6.67576 5 8.0431 5 10.7778V13.2222C5 15.9569 5 17.3242 5.73464 18.3626C6.46928 19.4009 7.75867 19.856 10.3374 20.7662L11 21" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path><path d="M21 12L11 12M21 12C21 11.2998 19.0057 9.99153 18.5 9.5M21 12C21 12.7002 19.0057 14.0085 18.5 14.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path></svg> Log out</div></div></div>`;
  pop();
}
function LayoutHeaderLink($$payload, $$props) {
  const { children, href, text } = $$props;
  $$payload.out += `<a${attr("href", href)} class="svelte-fngn21">`;
  children($$payload);
  $$payload.out += `<!----> ${escape_html(text)}</a>`;
}
function LayoutSearchBar($$payload) {
  $$payload.out += `<div><form class="form"><button><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="16" height="16" color="#80a7d2" fill="none"><path d="M17 17L21 21" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M19 11C19 6.58172 15.4183 3 11 3C6.58172 3 3 6.58172 3 11C3 15.4183 6.58172 19 11 19C15.4183 19 19 15.4183 19 11Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path></svg></button> <input class="input" placeholder="Search for something..." type="text"> <button class="reset" type="reset"><svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2"><path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12"></path></svg></button></form></div>`;
}
function authenticated($$payload, authData) {
  LayoutAccountBlock($$payload, {
    username: authData.Username ?? "",
    userId: authData.UserId ?? ""
  });
}
function LayoutHeader($$payload, $$props) {
  function unauthenticated($$payload2) {
    $$payload2.out += `<div class="auth-btns-container svelte-1otqyya"><button class="login-btn svelte-1otqyya">Login</button> <button class="signup-btn svelte-1otqyya">Sign Up</button></div>`;
  }
  $$payload.out += `<div class="layout-header-container svelte-1otqyya"><div class="header-left-part svelte-1otqyya"><a class="layout-logo-container svelte-1otqyya" data-sveltekit-preload-data="hover" href="/"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-1otqyya"><path d="M15.0002 17C14.2007 17.6224 13.1504 18 12.0002 18C10.8499 18 9.79971 17.6224 9.00018 17" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path><path d="M2.35157 13.2135C1.99855 10.9162 1.82204 9.76763 2.25635 8.74938C2.69065 7.73112 3.65421 7.03443 5.58132 5.64106L7.02117 4.6C9.41847 2.86667 10.6171 2 12.0002 2C13.3832 2 14.5819 2.86667 16.9792 4.6L18.419 5.64106C20.3462 7.03443 21.3097 7.73112 21.744 8.74938C22.1783 9.76763 22.0018 10.9162 21.6488 13.2135L21.3478 15.1724C20.8473 18.4289 20.5971 20.0572 19.4292 21.0286C18.2613 22 16.5538 22 13.139 22H10.8614C7.44652 22 5.73909 22 4.57118 21.0286C3.40327 20.0572 3.15305 18.4289 2.65261 15.1724L2.35157 13.2135Z" stroke="currentColor" stroke-width="1.5" stroke-linejoin="round"></path></svg></a> `;
  LayoutHeaderLink($$payload, {
    href: "/my-posts",
    text: "My posts",
    children: ($$payload2) => {
      $$payload2.out += `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M16.2627 10.5H7.73725C5.15571 10.5 3.86494 10.5 3.27143 11.3526C2.67793 12.2052 3.11904 13.4258 4.00126 15.867L5.08545 18.867C5.54545 20.1398 5.77545 20.7763 6.2889 21.1381C6.80235 21.5 7.47538 21.5 8.82143 21.5H15.1786C16.5246 21.5 17.1976 21.5 17.7111 21.1381C18.2245 20.7763 18.4545 20.1398 18.9146 18.867L19.9987 15.867C20.881 13.4258 21.3221 12.2052 20.7286 11.3526C20.1351 10.5 18.8443 10.5 16.2627 10.5Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="square"></path><path d="M19 8C19 7.53406 19 7.30109 18.9239 7.11732C18.8224 6.87229 18.6277 6.67761 18.3827 6.57612C18.1989 6.5 17.9659 6.5 17.5 6.5H6.5C6.03406 6.5 5.80109 6.5 5.61732 6.57612C5.37229 6.67761 5.17761 6.87229 5.07612 7.11732C5 7.30109 5 7.53406 5 8" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M16.5 4C16.5 3.53406 16.5 3.30109 16.4239 3.11732C16.3224 2.87229 16.1277 2.67761 15.8827 2.57612C15.6989 2.5 15.4659 2.5 15 2.5H9C8.53406 2.5 8.30109 2.5 8.11732 2.57612C7.87229 2.67761 7.67761 2.87229 7.57612 3.11732C7.5 3.30109 7.5 3.53406 7.5 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
    }
  });
  $$payload.out += `<!----> `;
  LayoutHeaderLink($$payload, {
    href: "/new-post",
    text: "New post",
    children: ($$payload2) => {
      $$payload2.out += `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M5.07579 17C4.08939 4.54502 12.9123 1.0121 19.9734 2.22417C20.2585 6.35185 18.2389 7.89748 14.3926 8.61125C15.1353 9.38731 16.4477 10.3639 16.3061 11.5847C16.2054 12.4534 15.6154 12.8797 14.4355 13.7322C11.8497 15.6004 8.85421 16.7785 5.07579 17Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M4 22C4 15.5 7.84848 12.1818 10.5 10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
    }
  });
  $$payload.out += `<!----></div> `;
  LayoutSearchBar($$payload);
  $$payload.out += `<!----> <div class="header-right-part svelte-1otqyya">`;
  AuthView($$payload, { authenticated, unauthenticated });
  $$payload.out += `<!----></div></div>`;
}
function _layout($$payload, $$props) {
  const { children } = $$props;
  LayoutAuthDialog($$payload, {});
  $$payload.out += `<!----> <div class="page svelte-12cnnd3">`;
  LayoutHeader($$payload);
  $$payload.out += `<!----> <div class="page-content svelte-12cnnd3">`;
  children($$payload);
  $$payload.out += `<!----></div></div>`;
}

export { _layout as default };
//# sourceMappingURL=_layout.svelte-Djqci7C-.js.map
