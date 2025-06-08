import { p as push, m as copy_payload, n as assign_payload, l as pop, q as bind_props, x as ensure_array_like, v as attr_class, t as attr, w as stringify, u as escape_html, y as await_block, A as attr_style } from './index2-DGUJ8ZVV.js';
import { g as goto } from './client-Dg0CqvnW.js';
import './client2-BT_TsMl5.js';
import { A as AuthView } from './AuthView-BQEuqrR5.js';
import { D as DefaultErrBlock, E as ErrView } from './DefaultErrBlock-DroY93XM.js';
import { S as StringUtils, D as DateUtils, E as Err } from './date-utils-BMgSYFUH.js';
import { B as BaseDialogWithCloseButton } from './BaseDialogWithCloseButton-C-4-7gO8.js';
import { G as GrayLabelWithOnclick } from './GrayLabelWithOnclick-DkBFCD6X.js';
import { A as ApiMain } from './backend-services-gK9Cr6ja.js';
import './exports-CuVW_EtN.js';
import './t-plain-err-CtrWZBuG.js';

function PageAuthNeeded($$payload) {
  $$payload.out += `<div class="svelte-c0oooz">To access this page you need to be logged in <button>Sign in</button></div>`;
}
function PostTitleEditingView($$payload, $$props) {
  push();
  let { postId, title, updateParentValue } = $$props;
  function isInEditingState() {
    return isEditing;
  }
  let isEditing = false;
  {
    $$payload.out += "<!--[!-->";
    $$payload.out += `<h1 class="post-title svelte-vb4oax">${escape_html(title)} <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-vb4oax"><path d="M14.0737 3.88545C14.8189 3.07808 15.1915 2.6744 15.5874 2.43893C16.5427 1.87076 17.7191 1.85309 18.6904 2.39232C19.0929 2.6158 19.4769 3.00812 20.245 3.79276C21.0131 4.5774 21.3972 4.96972 21.6159 5.38093C22.1438 6.37312 22.1265 7.57479 21.5703 8.5507C21.3398 8.95516 20.9446 9.33578 20.1543 10.097L10.7506 19.1543C9.25288 20.5969 8.504 21.3182 7.56806 21.6837C6.63212 22.0493 5.6032 22.0224 3.54536 21.9686L3.26538 21.9613C2.63891 21.9449 2.32567 21.9367 2.14359 21.73C1.9615 21.5234 1.98636 21.2043 2.03608 20.5662L2.06308 20.2197C2.20301 18.4235 2.27297 17.5255 2.62371 16.7182C2.97444 15.9109 3.57944 15.2555 4.78943 13.9445L14.0737 3.88545Z" stroke="currentColor" stroke-width="1.5" stroke-linejoin="round"></path><path d="M13 4L20 11" stroke="currentColor" stroke-width="1.5" stroke-linejoin="round"></path><path d="M14 22L22 22" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path></svg></h1>`;
  }
  $$payload.out += `<!--]-->`;
  bind_props($$props, { isInEditingState });
  pop();
}
function TagOperatingDisplay($$payload, $$props) {
  push();
  let {
    tag,
    isTagAdded,
    isTagRemovingState,
    btnOnClick
  } = $$props;
  $$payload.out += `<div class="tag-display svelte-g22bwk"><span class="svelte-g22bwk">#${escape_html(tag)}</span> `;
  if (isTagRemovingState) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<svg class="remove-btn svelte-g22bwk" viewBox="0 0 24 24" fill="none"><path d="M20 12L4 12" stroke="currentColor" stroke-width="2.6" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
  } else if (isTagAdded) {
    $$payload.out += "<!--[1-->";
    $$payload.out += `<svg viewBox="0 0 24 24" fill="none" class="svelte-g22bwk"><path d="M5 14.5C5 14.5 6.5 14.5 8.5 18C8.5 18 14.0588 8.83333 19 7" stroke="currentColor" stroke-width="2.6" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
  } else {
    $$payload.out += "<!--[!-->";
    $$payload.out += `<svg class="add-btn svelte-g22bwk" viewBox="0 0 24 24" fill="none"><path d="M12 4V20M20 12H4" stroke="currentColor" stroke-width="2.6" stroke-linecap="round" stroke-linejoin="round"></path></svg>`;
  }
  $$payload.out += `<!--]--></div>`;
  pop();
}
function TagsDialogSearchBar($$payload, $$props) {
  push();
  let tagSearchInput = "";
  $$payload.out += `<div class="search-bar svelte-xbwg21"><svg class="search-icon svelte-xbwg21" viewBox="0 0 24 24" fill="none"><path d="M17.5 17.5L22 22" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M20 11C20 6.02944 15.9706 2 11 2C6.02944 2 2 6.02944 2 11C2 15.9706 6.02944 20 11 20C15.9706 20 20 15.9706 20 11Z" stroke="currentColor" stroke-width="2.5" stroke-linejoin="round"></path></svg> <input placeholder="Search for tag..."${attr("value", tagSearchInput)}${attr("name", "tag-search-" + StringUtils.rndStr(12))} class="svelte-xbwg21"> <svg class="reset-button svelte-xbwg21" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2.5"><path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12"></path></svg></div>`;
  pop();
}
function PostTagsChoosingDialog($$payload, $$props) {
  push();
  let { updateParent, postId } = $$props;
  let dialogElement;
  let tagsToChooseFrom = [];
  let errors = [];
  let chosenTags = [];
  function open(tags) {
    tagsToChooseFrom = [];
    errors = [];
    chosenTags = [...tags];
    dialogElement.open();
  }
  function removeTag(tag) {
    chosenTags = chosenTags.filter((t) => t !== tag);
  }
  function addTag(tag) {
    if (!chosenTags.includes(tag)) {
      chosenTags.push(tag);
    }
  }
  BaseDialogWithCloseButton($$payload, {
    dialogId: "post-tags-choosing",
    children: ($$payload2) => {
      const each_array = ensure_array_like(tagsToChooseFrom);
      const each_array_1 = ensure_array_like(chosenTags);
      $$payload2.out += `<div class="main-part svelte-xe4unw">`;
      TagsDialogSearchBar($$payload2);
      $$payload2.out += `<!----> <label class="chosen-tags-label svelte-xe4unw">Tags chosen: (${escape_html(chosenTags.length)})</label> <div class="tags-ops-list svelte-xe4unw"><!--[-->`;
      for (let $$index = 0, $$length = each_array.length; $$index < $$length; $$index++) {
        let tag = each_array[$$index];
        TagOperatingDisplay($$payload2, {
          tag,
          isTagAdded: chosenTags.includes(tag),
          isTagRemovingState: false,
          btnOnClick: () => addTag(tag)
        });
      }
      $$payload2.out += `<!--]--></div> <div class="tags-ops-list svelte-xe4unw"><!--[-->`;
      for (let $$index_1 = 0, $$length = each_array_1.length; $$index_1 < $$length; $$index_1++) {
        let tag = each_array_1[$$index_1];
        TagOperatingDisplay($$payload2, {
          tag,
          isTagAdded: true,
          isTagRemovingState: true,
          btnOnClick: () => removeTag(tag)
        });
      }
      $$payload2.out += `<!--]--></div></div> <div class="bottom-part svelte-xe4unw"><label class="continue-typing svelte-xe4unw">If you don't find the tag you need continue entering the name of the
            tag</label> `;
      if (errors.length != 0) {
        $$payload2.out += "<!--[-->";
        DefaultErrBlock($$payload2, { errList: errors });
      } else {
        $$payload2.out += "<!--[!-->";
      }
      $$payload2.out += `<!--]--> <button class="save-btn svelte-xe4unw">Save</button></div>`;
    },
    $$slots: { default: true }
  });
  bind_props($$props, { open });
  pop();
}
function PostTagsEditingView($$payload, $$props) {
  push();
  let { postId, tags, updateParentValue } = $$props;
  PostTagsChoosingDialog($$payload, {
    updateParent: (newTags, newLastModified) => updateParentValue(newTags, newLastModified),
    postId
  });
  $$payload.out += `<!----> `;
  if (tags.length === 0) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<div class="no-tags unselectable svelte-10312ev">This post has no tags <button class="add-tags-btn svelte-10312ev">Add tags</button></div>`;
  } else {
    $$payload.out += "<!--[!-->";
    const each_array = ensure_array_like(tags);
    $$payload.out += `<div class="tags svelte-10312ev"><!--[-->`;
    for (let $$index = 0, $$length = each_array.length; $$index < $$length; $$index++) {
      let tag = each_array[$$index];
      $$payload.out += `<div class="tag svelte-10312ev">#${escape_html(tag)}</div>`;
    }
    $$payload.out += `<!--]--> <button class="change-tags-btn svelte-10312ev"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-10312ev"><path d="M15.5 12C15.5 13.933 13.933 15.5 12 15.5C10.067 15.5 8.5 13.933 8.5 12C8.5 10.067 10.067 8.5 12 8.5C13.933 8.5 15.5 10.067 15.5 12Z" stroke="currentColor" stroke-width="2"></path><path d="M21.011 14.0965C21.5329 13.9558 21.7939 13.8854 21.8969 13.7508C22 13.6163 22 13.3998 22 12.9669V11.0332C22 10.6003 22 10.3838 21.8969 10.2493C21.7938 10.1147 21.5329 10.0443 21.011 9.90358C19.0606 9.37759 17.8399 7.33851 18.3433 5.40087C18.4817 4.86799 18.5509 4.60156 18.4848 4.44529C18.4187 4.28902 18.2291 4.18134 17.8497 3.96596L16.125 2.98673C15.7528 2.77539 15.5667 2.66972 15.3997 2.69222C15.2326 2.71472 15.0442 2.90273 14.6672 3.27873C13.208 4.73448 10.7936 4.73442 9.33434 3.27864C8.95743 2.90263 8.76898 2.71463 8.60193 2.69212C8.43489 2.66962 8.24877 2.77529 7.87653 2.98663L6.15184 3.96587C5.77253 4.18123 5.58287 4.28891 5.51678 4.44515C5.45068 4.6014 5.51987 4.86787 5.65825 5.4008C6.16137 7.3385 4.93972 9.37763 2.98902 9.9036C2.46712 10.0443 2.20617 10.1147 2.10308 10.2492C2 10.3838 2 10.6003 2 11.0332V12.9669C2 13.3998 2 13.6163 2.10308 13.7508C2.20615 13.8854 2.46711 13.9558 2.98902 14.0965C4.9394 14.6225 6.16008 16.6616 5.65672 18.5992C5.51829 19.1321 5.44907 19.3985 5.51516 19.5548C5.58126 19.7111 5.77092 19.8188 6.15025 20.0341L7.87495 21.0134C8.24721 21.2247 8.43334 21.3304 8.6004 21.3079C8.76746 21.2854 8.95588 21.0973 9.33271 20.7213C10.7927 19.2644 13.2088 19.2643 14.6689 20.7212C15.0457 21.0973 15.2341 21.2853 15.4012 21.3078C15.5682 21.3303 15.7544 21.2246 16.1266 21.0133L17.8513 20.034C18.2307 19.8187 18.4204 19.711 18.4864 19.5547C18.5525 19.3984 18.4833 19.132 18.3448 18.5991C17.8412 16.6616 19.0609 14.6226 21.011 14.0965Z" stroke="currentColor" stroke-width="2" stroke-linecap="round"></path></svg> Change tags</button></div>`;
  }
  $$payload.out += `<!--]-->`;
  pop();
}
function PostContentEditingHeadingView($$payload, $$props) {
  push();
  let { isEditing, initial, editingValue = void 0 } = $$props;
  $$payload.out += `<div class="heading svelte-1hamxme">`;
  if (isEditing) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<textarea rows="1" class="svelte-1hamxme">`;
    const $$body = escape_html(editingValue.value);
    if ($$body) {
      $$payload.out += `${$$body}`;
    }
    $$payload.out += `</textarea>`;
  } else {
    $$payload.out += "<!--[!-->";
    $$payload.out += `<h2 class="svelte-1hamxme">${escape_html(initial.value)}</h2>`;
  }
  $$payload.out += `<!--]--></div>`;
  bind_props($$props, { editingValue });
  pop();
}
function PostContentEditingListView($$payload, $$props) {
  push();
  let { isEditing, initial, editingValue = void 0 } = $$props;
  $$payload.out += `<div class="list svelte-qn6h01">`;
  if (isEditing) {
    $$payload.out += "<!--[-->";
    if (editingValue.items.length > 0) {
      $$payload.out += "<!--[-->";
      const each_array = ensure_array_like(editingValue.items);
      $$payload.out += `<!--[-->`;
      for (let i = 0, $$length = each_array.length; i < $$length; i++) {
        each_array[i];
        $$payload.out += `<div class="list-editing-item svelte-qn6h01"><span class="bullet svelte-qn6h01"></span> <input${attr("value", editingValue.items[i])}${attr("placeholder", `Item #${i + 1}`)} class="svelte-qn6h01"> <svg class="remove-btn svelte-qn6h01" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M6.70914 7.78222C7.76637 6.59403 8.29499 5.99994 9 5.99994C9.70501 5.99994 10.2336 6.59403 11.2909 7.78222L13.891 10.7044C15.297 12.2846 16 13.0747 16 13.9999C16 14.9252 15.297 15.7153 13.891 17.2955L11.2909 20.2177C10.2336 21.4058 9.70501 21.9999 9 21.9999C8.29499 21.9999 7.76637 21.4058 6.70914 20.2177L4.10902 17.2955C2.70301 15.7153 2 14.9252 2 13.9999C2 13.0747 2.70301 12.2846 4.10902 10.7044L6.70914 7.78222Z" stroke="currentColor" stroke-width="2"></path><path d="M15 4.99994H22" stroke="currentColor" stroke-width="2" stroke-linecap="round"></path></svg></div>`;
      }
      $$payload.out += `<!--]-->`;
    } else {
      $$payload.out += "<!--[!-->";
      $$payload.out += `<label class="empty-list-label svelte-qn6h01">Empty list</label>`;
    }
    $$payload.out += `<!--]--> <button${attr_class("add-btn unselectable svelte-qn6h01", void 0, { "list-empty": editingValue.items.length === 0 })}><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-qn6h01"><path d="M6.70914 7.78228C7.76637 6.59409 8.29499 6 9 6C9.70501 6 10.2336 6.59409 11.2909 7.78228L13.891 10.7045C15.297 12.2847 16 13.0747 16 14C16 14.9253 15.297 15.7153 13.891 17.2955L11.2909 20.2177C10.2336 21.4059 9.70501 22 9 22C8.29499 22 7.76637 21.4059 6.70914 20.2177L4.10902 17.2955C2.70301 15.7153 2 14.9253 2 14C2 13.0747 2.70301 12.2847 4.10902 10.7045L6.70914 7.78228Z" stroke="currentColor" stroke-width="2"></path><path d="M18.5 9L18.5 2M15 5.5H22" stroke="currentColor" stroke-width="2" stroke-linecap="round"></path></svg> Add list item</button>`;
  } else {
    $$payload.out += "<!--[!-->";
    const each_array_1 = ensure_array_like(initial.items);
    $$payload.out += `<!--[-->`;
    for (let $$index_1 = 0, $$length = each_array_1.length; $$index_1 < $$length; $$index_1++) {
      let item = each_array_1[$$index_1];
      $$payload.out += `<div class="list-display-item svelte-qn6h01"><span class="bullet svelte-qn6h01"></span> ${escape_html(item)}</div>`;
    }
    $$payload.out += `<!--]-->`;
  }
  $$payload.out += `<!--]--></div>`;
  bind_props($$props, { editingValue });
  pop();
}
function PostContentEditingParagraphView($$payload, $$props) {
  push();
  let { isEditing, initial, editingValue = void 0 } = $$props;
  $$payload.out += `<div class="paragraph svelte-vscvc9">`;
  if (isEditing) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<textarea rows="1" class="svelte-vscvc9">`;
    const $$body = escape_html(editingValue.value);
    if ($$body) {
      $$payload.out += `${$$body}`;
    }
    $$payload.out += `</textarea>`;
  } else {
    $$payload.out += "<!--[!-->";
    $$payload.out += `<p class="svelte-vscvc9">${escape_html(initial.value)}</p>`;
  }
  $$payload.out += `<!--]--></div>`;
  bind_props($$props, { editingValue });
  pop();
}
function PostContentEditingQuoteView($$payload, $$props) {
  push();
  let { isEditing, initial, editingValue = void 0 } = $$props;
  $$payload.out += `<div class="quote svelte-11go838">`;
  if (isEditing) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<textarea rows="1" placeholder="Quote text" class="svelte-11go838">`;
    const $$body = escape_html(editingValue.text);
    if ($$body) {
      $$payload.out += `${$$body}`;
    }
    $$payload.out += `</textarea> <label class="svelte-11go838">Author: <input${attr("value", editingValue.author)} class="svelte-11go838"></label>`;
  } else {
    $$payload.out += "<!--[!-->";
    $$payload.out += `<p class="quote-text svelte-11go838">${escape_html(initial.text)}</p> `;
    if (initial.author) {
      $$payload.out += "<!--[-->";
      $$payload.out += `<label class="quote-author svelte-11go838">${escape_html(initial.author)}</label>`;
    } else {
      $$payload.out += "<!--[!-->";
    }
    $$payload.out += `<!--]-->`;
  }
  $$payload.out += `<!--]--></div>`;
  bind_props($$props, { editingValue });
  pop();
}
function PostContentEditingSubheadingView($$payload, $$props) {
  push();
  let { isEditing, initial, editingValue = void 0 } = $$props;
  $$payload.out += `<div class="subheading svelte-1tw7l70">`;
  if (isEditing) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<textarea rows="1" class="svelte-1tw7l70">`;
    const $$body = escape_html(editingValue.value);
    if ($$body) {
      $$payload.out += `${$$body}`;
    }
    $$payload.out += `</textarea>`;
  } else {
    $$payload.out += "<!--[!-->";
    $$payload.out += `<h3 class="svelte-1tw7l70">${escape_html(initial.value)}</h3>`;
  }
  $$payload.out += `<!--]--></div>`;
  bind_props($$props, { editingValue });
  pop();
}
function ContentEditingRightSideButtons($$payload, $$props) {
  let {
    isEditing,
    isNewlyAdded
  } = $$props;
  $$payload.out += `<div${attr_class("btns-container svelte-189yo57", void 0, { "show": isEditing })}>`;
  if (!isNewlyAdded && isEditing) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<svg class="cancel svelte-189yo57" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M18 6L12 12M12 12L6 18M12 12L18 18M12 12L6 6" stroke="currentColor" stroke-width="1.9" stroke-linecap="round" stroke-linejoin="round" class="svelte-189yo57"></path></svg>`;
  } else if (!isNewlyAdded) {
    $$payload.out += "<!--[1-->";
    $$payload.out += `<svg class="edit svelte-189yo57" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M8.17151 19.8284L19.8284 8.17157C20.3736 7.62632 20.6462 7.3537 20.792 7.0596C21.0693 6.50005 21.0693 5.8431 20.792 5.28354C20.6462 4.98945 20.3736 4.71682 19.8284 4.17157C19.2831 3.62632 19.0105 3.3537 18.7164 3.20796C18.1568 2.93068 17.4999 2.93068 16.9403 3.20796C16.6462 3.3537 16.3736 3.62632 15.8284 4.17157L4.17151 15.8284C3.59345 16.4064 3.30442 16.6955 3.15218 17.063C2.99994 17.4305 2.99994 17.8393 2.99994 18.6568V20.9999H5.34308C6.16059 20.9999 6.56934 20.9999 6.93688 20.8477C7.30442 20.6955 7.59345 20.4064 8.17151 19.8284Z" stroke="currentColor" stroke-width="1.9" stroke-linecap="round" stroke-linejoin="round" class="svelte-189yo57"></path><path d="M12 21H18" stroke="currentColor" stroke-width="1.9" stroke-linecap="round" stroke-linejoin="round" class="svelte-189yo57"></path><path d="M14.5 5.5L18.5 9.5" stroke="currentColor" stroke-width="1.9" stroke-linecap="round" stroke-linejoin="round" class="svelte-189yo57"></path></svg>`;
  } else {
    $$payload.out += "<!--[!-->";
  }
  $$payload.out += `<!--]--> <svg class="delete svelte-189yo57" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M19.5 5.5L18.8803 15.5251C18.7219 18.0864 18.6428 19.3671 18.0008 20.2879C17.6833 20.7431 17.2747 21.1273 16.8007 21.416C15.8421 22 14.559 22 11.9927 22C9.42312 22 8.1383 22 7.17905 21.4149C6.7048 21.1257 6.296 20.7408 5.97868 20.2848C5.33688 19.3626 5.25945 18.0801 5.10461 15.5152L4.5 5.5" stroke="currentColor" stroke-width="1.9" stroke-linecap="round" class="svelte-189yo57"></path><path d="M3 5.5H21M16.0557 5.5L15.3731 4.09173C14.9196 3.15626 14.6928 2.68852 14.3017 2.39681C14.215 2.3321 14.1231 2.27454 14.027 2.2247C13.5939 2 13.0741 2 12.0345 2C10.9688 2 10.436 2 9.99568 2.23412C9.8981 2.28601 9.80498 2.3459 9.71729 2.41317C9.32164 2.7167 9.10063 3.20155 8.65861 4.17126L8.05292 5.5" stroke="currentColor" stroke-width="1.9" stroke-linecap="round" class="svelte-189yo57"></path><path d="M9.5 16.5L9.5 10.5" stroke="currentColor" stroke-width="1.9" stroke-linecap="round" class="svelte-189yo57"></path><path d="M14.5 16.5L14.5 10.5" stroke="currentColor" stroke-width="1.9" stroke-linecap="round" class="svelte-189yo57"></path></svg></div>`;
}
function AddPostItemButton($$payload, $$props) {
  push();
  let isMenuOpen = false;
  $$payload.out += `<div class="add-content-wrapper unselectable svelte-y5q64w"><button class="add-btn svelte-y5q64w"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-y5q64w"><path d="M2.5 12C2.5 7.52166 2.5 5.28249 3.89124 3.89124C5.28249 2.5 7.52166 2.5 12 2.5C16.4783 2.5 18.7175 2.5 20.1088 3.89124C21.5 5.28249 21.5 7.52166 21.5 12C21.5 16.4783 21.5 18.7175 20.1088 20.1088C18.7175 21.5 16.4783 21.5 12 21.5C7.52166 21.5 5.28249 21.5 3.89124 20.1088C2.5 18.7175 2.5 16.4783 2.5 12Z" stroke="currentColor" stroke-width="2.3"></path><path d="M9.48581 12.5068C10.0159 11.9753 11.3066 9.99591 12.0519 10.0054C12.7899 10.085 13.969 12.0056 14.4895 12.5099M11.9721 17.0039L11.9763 10.0108M15.9931 7.00289L8.00098 6.99805" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path></svg> Add content item</button> <div${attr_class("menu svelte-y5q64w", void 0, { "open": isMenuOpen })}><label class="svelte-y5q64w">Choose type of the content item</label> <div class="options svelte-y5q64w"><div class="option svelte-y5q64w"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-y5q64w"><path d="M3 3H21" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M3 9H11" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M3 15H21" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M3 21H11" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path></svg> Paragraph</div> <div class="option svelte-y5q64w"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-y5q64w"><path d="M8 5L20 5" stroke="currentColor" stroke-width="2.3" stroke-linecap="round"></path><path d="M4 5H4.00898" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M4 12H4.00898" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M4 19H4.00898" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M8 12L20 12" stroke="currentColor" stroke-width="2.3" stroke-linecap="round"></path><path d="M8 19L20 19" stroke="currentColor" stroke-width="2.3" stroke-linecap="round"></path></svg> List</div> <div class="option svelte-y5q64w"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-y5q64w"><path d="M10 8C10 9.88562 10 10.8284 9.41421 11.4142C8.82843 12 7.88562 12 6 12C4.11438 12 3.17157 12 2.58579 11.4142C2 10.8284 2 9.88562 2 8C2 6.11438 2 5.17157 2.58579 4.58579C3.17157 4 4.11438 4 6 4C7.88562 4 8.82843 4 9.41421 4.58579C10 5.17157 10 6.11438 10 8Z" stroke="currentColor" stroke-width="2.3"></path><path d="M10 7L10 11.4821C10 15.4547 7.48429 18.8237 4 20" stroke="currentColor" stroke-width="2.3" stroke-linecap="round"></path><path d="M22 8C22 9.88562 22 10.8284 21.4142 11.4142C20.8284 12 19.8856 12 18 12C16.1144 12 15.1716 12 14.5858 11.4142C14 10.8284 14 9.88562 14 8C14 6.11438 14 5.17157 14.5858 4.58579C15.1716 4 16.1144 4 18 4C19.8856 4 20.8284 4 21.4142 4.58579C22 5.17157 22 6.11438 22 8Z" stroke="currentColor" stroke-width="2.3"></path><path d="M22 7L22 11.4821C22 15.4547 19.4843 18.8237 16 20" stroke="currentColor" stroke-width="2.3" stroke-linecap="round"></path></svg> Quote</div> <div class="option svelte-y5q64w"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-y5q64w"><path d="M4 5V19" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M14 5V19" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M17 19H18.5M20 19H18.5M18.5 19V11L17 12" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M4 12L14 12" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path></svg> Heading</div> <div class="option svelte-y5q64w"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-y5q64w"><path d="M3.5 5V19" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M13.5 5V19" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M16.5 11V15H20.5M20.5 15V19M20.5 15V11" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path><path d="M3.5 12L13.5 12" stroke="currentColor" stroke-width="2.3" stroke-linecap="round" stroke-linejoin="round"></path></svg> Subheading</div></div></div></div>`;
  pop();
}
function copyPostContentItem(item) {
  switch (item.$type) {
    case "HeadingContentItem":
    case "SubheadingContentItem":
    case "ParagraphContentItem":
    case "QuoteContentItem":
      return { ...item };
    case "ListContentItem":
      return {
        ...item,
        items: [...item.items]
      };
    default:
      throw new Error(`Unknown content item type: ${item.$type}`);
  }
}
function PostContentEditingView($$payload, $$props) {
  push();
  let { postId, content, updateParentValue } = $$props;
  function isInEditingState() {
    return anyUnsaved;
  }
  let editingState = content.items.map((i) => ({
    editing: copyPostContentItem(i),
    initial: i,
    isNewlyAdded: false,
    isInEditingState: false
  }));
  let editingErrs = [];
  let anyUnsaved = editingState.filter((i) => i.isNewlyAdded ? true : i.isInEditingState).length > 0;
  let $$settled = true;
  let $$inner_payload;
  function $$render_inner($$payload2) {
    $$payload2.out += `<div class="content-editing-view svelte-dqjksl">`;
    if (editingState.length > 0) {
      $$payload2.out += "<!--[-->";
      const each_array = ensure_array_like(editingState);
      $$payload2.out += `<!--[-->`;
      for (let i = 0, $$length = each_array.length; i < $$length; i++) {
        let item = each_array[i];
        $$payload2.out += `<div${attr_class("content-item svelte-dqjksl", void 0, {
          "unsaved": item.isInEditingState,
          "newly-added": item.isNewlyAdded
        })}>`;
        if (item.initial.$type === "HeadingContentItem") {
          $$payload2.out += "<!--[-->";
          PostContentEditingHeadingView($$payload2, {
            isEditing: item.isInEditingState,
            initial: item.initial,
            get editingValue() {
              return editingState[i].editing;
            },
            set editingValue($$value) {
              editingState[i].editing = $$value;
              $$settled = false;
            }
          });
        } else if (item.initial.$type === "SubheadingContentItem") {
          $$payload2.out += "<!--[1-->";
          PostContentEditingSubheadingView($$payload2, {
            isEditing: item.isInEditingState,
            initial: item.initial,
            get editingValue() {
              return editingState[i].editing;
            },
            set editingValue($$value) {
              editingState[i].editing = $$value;
              $$settled = false;
            }
          });
        } else if (item.initial.$type === "ParagraphContentItem") {
          $$payload2.out += "<!--[2-->";
          PostContentEditingParagraphView($$payload2, {
            isEditing: item.isInEditingState,
            initial: item.initial,
            get editingValue() {
              return editingState[i].editing;
            },
            set editingValue($$value) {
              editingState[i].editing = $$value;
              $$settled = false;
            }
          });
        } else if (item.initial.$type === "QuoteContentItem") {
          $$payload2.out += "<!--[3-->";
          PostContentEditingQuoteView($$payload2, {
            isEditing: item.isInEditingState,
            initial: item.initial,
            get editingValue() {
              return editingState[i].editing;
            },
            set editingValue($$value) {
              editingState[i].editing = $$value;
              $$settled = false;
            }
          });
        } else if (item.initial.$type === "ListContentItem") {
          $$payload2.out += "<!--[4-->";
          PostContentEditingListView($$payload2, {
            isEditing: item.isInEditingState,
            initial: item.initial,
            get editingValue() {
              return editingState[i].editing;
            },
            set editingValue($$value) {
              editingState[i].editing = $$value;
              $$settled = false;
            }
          });
        } else {
          $$payload2.out += "<!--[!-->";
          ErrView($$payload2, {
            err: new Err("Unknown post content item type")
          });
        }
        $$payload2.out += `<!--]--> `;
        ContentEditingRightSideButtons($$payload2, {
          isEditing: item.isInEditingState,
          isNewlyAdded: item.isNewlyAdded
        });
        $$payload2.out += `<!----></div>`;
      }
      $$payload2.out += `<!--]-->`;
    } else {
      $$payload2.out += "<!--[!-->";
      $$payload2.out += `<div class="empty-content svelte-dqjksl"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-dqjksl"><path d="M13 21.5V21C13 18.1716 13 16.7574 13.8787 15.8787C14.7574 15 16.1716 15 19 15H19.5M20 13.3431V10C20 6.22876 20 4.34315 18.8284 3.17157C17.6569 2 15.7712 2 12 2C8.22877 2 6.34315 2 5.17157 3.17157C4 4.34314 4 6.22876 4 10L4 14.5442C4 17.7892 4 19.4117 4.88607 20.5107C5.06508 20.7327 5.26731 20.9349 5.48933 21.1139C6.58831 22 8.21082 22 11.4558 22C12.1614 22 12.5141 22 12.8372 21.886C12.9044 21.8623 12.9702 21.835 13.0345 21.8043C13.3436 21.6564 13.593 21.407 14.0919 20.9081L18.8284 16.1716C19.4065 15.5935 19.6955 15.3045 19.8478 14.9369C20 14.5694 20 14.1606 20 13.3431Z" stroke="currentColor" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round"></path></svg> Post is empty. Please add content items</div>`;
    }
    $$payload2.out += `<!--]--> `;
    AddPostItemButton($$payload2);
    $$payload2.out += `<!----> `;
    if (anyUnsaved) {
      $$payload2.out += "<!--[-->";
      $$payload2.out += `<label class="unsaved-label svelte-dqjksl"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-dqjksl"><path d="M5.32171 9.6829C7.73539 5.41196 8.94222 3.27648 10.5983 2.72678C11.5093 2.42437 12.4907 2.42437 13.4017 2.72678C15.0578 3.27648 16.2646 5.41196 18.6783 9.6829C21.092 13.9538 22.2988 16.0893 21.9368 17.8293C21.7376 18.7866 21.2469 19.6548 20.535 20.3097C19.241 21.5 16.8274 21.5 12 21.5C7.17265 21.5 4.75897 21.5 3.46496 20.3097C2.75308 19.6548 2.26239 18.7866 2.06322 17.8293C1.70119 16.0893 2.90803 13.9538 5.32171 9.6829Z" stroke="currentColor" stroke-width="1.5"></path><path d="M11.992 16H12.001" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M12 13L12 8.99997" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path></svg> Post content has some unsaved changes</label> <div class="save-cancel-btns unselectable svelte-dqjksl"><button class="cancel-btn svelte-dqjksl">Cancel</button> <button class="save-btn svelte-dqjksl">Save content changes</button></div>`;
    } else {
      $$payload2.out += "<!--[!-->";
    }
    $$payload2.out += `<!--]--> `;
    DefaultErrBlock($$payload2, { errList: editingErrs });
    $$payload2.out += `<!----></div>`;
  }
  do {
    $$settled = true;
    $$inner_payload = copy_payload($$payload);
    $$render_inner($$inner_payload);
  } while (!$$settled);
  assign_payload($$payload, $$inner_payload);
  bind_props($$props, { isInEditingState });
  pop();
}
function DraftPostPublishingView($$payload, $$props) {
  push();
  let publishingErrs = [];
  $$payload.out += `<button class="publish-btn unselectable svelte-1j0hjai"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-1j0hjai"><path d="M21.0477 3.05293C18.8697 0.707363 2.48648 6.4532 2.50001 8.551C2.51535 10.9299 8.89809 11.6617 10.6672 12.1581C11.7311 12.4565 12.016 12.7625 12.2613 13.8781C13.3723 18.9305 13.9301 21.4435 15.2014 21.4996C17.2278 21.5892 23.1733 5.342 21.0477 3.05293Z" stroke="currentColor" stroke-width="2"></path><path d="M11.5 12.5L15 9" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path></svg> Publish post</button> <div class="errs-wrapper svelte-1j0hjai">`;
  DefaultErrBlock($$payload, { errList: publishingErrs });
  $$payload.out += `<!----></div>`;
  pop();
}
function DraftPostEditingView($$payload, $$props) {
  push();
  let {
    getPostData,
    updateCache
  } = $$props;
  let postData = {
    id: "",
    title: "",
    isPinned: false,
    lastModifiedAt: /* @__PURE__ */ new Date(),
    createdAt: /* @__PURE__ */ new Date(),
    content: { items: [] },
    tags: []
  };
  let fetchingErrs = [];
  async function invokeGetPostData() {
    const res = await getPostData();
    if (Array.isArray(res)) {
      fetchingErrs = res;
    } else {
      fetchingErrs = [];
      postData = res;
    }
  }
  function updateTitle(newTitle, newLastModified) {
    postData.lastModifiedAt = newLastModified;
    postData.title = newTitle;
    updateCache(postData);
  }
  function updateContent(newContent, newLastModified) {
    postData.lastModifiedAt = newLastModified;
    postData.content = newContent;
    updateCache(postData);
  }
  function updateTags(newTags, newLastModified) {
    postData.lastModifiedAt = newLastModified;
    postData.tags = newTags;
    updateCache(postData);
  }
  $$payload.out += `<div class="editing-view svelte-1quseom"><!---->`;
  await_block(
    invokeGetPostData(),
    () => {
    },
    (_) => {
      if (fetchingErrs.length != 0) {
        $$payload.out += "<!--[-->";
        $$payload.out += `<p class="error-p svelte-1quseom">An error has occurred</p> `;
        DefaultErrBlock($$payload, { errList: fetchingErrs });
        $$payload.out += `<!---->`;
      } else {
        $$payload.out += "<!--[!-->";
        $$payload.out += `<label class="last-modified svelte-1quseom">Last modified at: ${escape_html(DateUtils.toLocale(postData.lastModifiedAt))}</label> `;
        PostTitleEditingView($$payload, {
          postId: postData.id,
          title: postData.title,
          updateParentValue: updateTitle
        });
        $$payload.out += `<!----> `;
        PostTagsEditingView($$payload, {
          postId: postData.id,
          tags: postData.tags,
          updateParentValue: updateTags
        });
        $$payload.out += `<!----> `;
        PostContentEditingView($$payload, {
          postId: postData.id,
          content: postData.content,
          updateParentValue: updateContent
        });
        $$payload.out += `<!----> `;
        DraftPostPublishingView($$payload, {
          postId: postData.id
        });
        $$payload.out += `<!---->`;
      }
      $$payload.out += `<!--]-->`;
    }
  );
  $$payload.out += `<!----></div>`;
  pop();
}
function DraftPostItemMoreActionsMenu($$payload, $$props) {
  push();
  let { updatePostPinnedState, deletePost } = $$props;
  let currentPostData = null;
  let x = 0;
  let y = 0;
  function open(event, postData) {
    event.stopPropagation();
    currentPostData = postData;
    x = event.clientX + 10;
    y = event.clientY;
  }
  function close() {
    currentPostData = null;
  }
  if (currentPostData) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<div class="context-menu svelte-9v1dz5"${attr_style(`top: ${stringify(y)}px; left: ${stringify(x)}px;`)}><button class="action-btn svelte-9v1dz5">`;
    if (currentPostData.isPinned) {
      $$payload.out += "<!--[-->";
      $$payload.out += `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-9v1dz5"><path d="M7.5 8C6.95863 8.1281 6.49932 8.14239 5.99268 8.45891C5.07234 9.03388 4.85108 9.71674 5.08821 10.7612C5.94028 14.5139 9.48599 18.0596 13.2388 18.9117C14.2834 19.1489 14.9661 18.928 15.5416 18.0077C15.8411 17.5288 15.8716 17.0081 16 16.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M12 7.79915C12.1776 7.77794 12.3182 7.74034 12.4295 7.68235C13.3997 7.17686 13.9291 5.53361 14.4498 4.60009C14.9311 3.73715 15.1718 3.30567 15.7379 3.10227C16.3041 2.89888 16.6448 3.02205 17.3262 3.26839C18.9197 3.8445 20.1555 5.08032 20.7316 6.6738C20.9779 7.35521 21.1011 7.69591 20.8977 8.26204C20.6943 8.82817 20.2628 9.06884 19.3999 9.55018C18.4608 10.074 16.7954 10.6108 16.2905 11.5898C16.2345 11.6983 16.1978 11.8327 16.1769 12" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M3 21L8 16" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M3 3L21 21" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path></svg> Unpin`;
    } else {
      $$payload.out += "<!--[!-->";
      $$payload.out += `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-9v1dz5"><path d="M3 21L8 16" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M13.2585 18.8714C9.51516 18.0215 5.97844 14.4848 5.12853 10.7415C4.99399 10.1489 4.92672 9.85266 5.12161 9.37197C5.3165 8.89129 5.55457 8.74255 6.03071 8.44509C7.10705 7.77265 8.27254 7.55888 9.48209 7.66586C11.1793 7.81598 12.0279 7.89104 12.4512 7.67048C12.8746 7.44991 13.1622 6.93417 13.7376 5.90269L14.4664 4.59604C14.9465 3.73528 15.1866 3.3049 15.7513 3.10202C16.316 2.89913 16.6558 3.02199 17.3355 3.26771C18.9249 3.84236 20.1576 5.07505 20.7323 6.66449C20.978 7.34417 21.1009 7.68401 20.898 8.2487C20.6951 8.8134 20.2647 9.05346 19.4039 9.53358L18.0672 10.2792C17.0376 10.8534 16.5229 11.1406 16.3024 11.568C16.0819 11.9955 16.162 12.8256 16.3221 14.4859C16.4399 15.7068 16.2369 16.88 15.5555 17.9697C15.2577 18.4458 15.1088 18.6839 14.6283 18.8786C14.1477 19.0733 13.8513 19.006 13.2585 18.8714Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path></svg> Pin`;
    }
    $$payload.out += `<!--]--></button> <button class="action-btn delete-btn svelte-9v1dz5"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-9v1dz5"><path d="M19.5 5.5L18.8803 15.5251C18.7219 18.0864 18.6428 19.3671 18.0008 20.2879C17.6833 20.7431 17.2747 21.1273 16.8007 21.416C15.8421 22 14.559 22 11.9927 22C9.42312 22 8.1383 22 7.17905 21.4149C6.7048 21.1257 6.296 20.7408 5.97868 20.2848C5.33688 19.3626 5.25945 18.0801 5.10461 15.5152L4.5 5.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path><path d="M3 5.5H21M16.0557 5.5L15.3731 4.09173C14.9196 3.15626 14.6928 2.68852 14.3017 2.39681C14.215 2.3321 14.1231 2.27454 14.027 2.2247C13.5939 2 13.0741 2 12.0345 2C10.9688 2 10.436 2 9.99568 2.23412C9.8981 2.28601 9.80498 2.3459 9.71729 2.41317C9.32164 2.7167 9.10063 3.20155 8.65861 4.17126L8.05292 5.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path><path d="M9.5 16.5L9.5 10.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path><path d="M14.5 16.5L14.5 10.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"></path></svg> Delete</button></div>`;
  } else {
    $$payload.out += "<!--[!-->";
  }
  $$payload.out += `<!--]-->`;
  bind_props($$props, { open, close });
  pop();
}
function DraftPostsList($$payload, $$props) {
  push();
  let { posts, currentPostId } = $$props;
  const each_array = ensure_array_like(posts);
  $$payload.out += `<div class="posts-list svelte-u2mpu7"><!--[-->`;
  for (let $$index = 0, $$length = each_array.length; $$index < $$length; $$index++) {
    let post = each_array[$$index];
    $$payload.out += `<a${attr_class("post-link svelte-u2mpu7", void 0, { "is-selected": post.id === currentPostId })}${attr("href", `/new-post/${stringify(post.id)}`)} data-sveltekit-preload-data="hover" data-sveltekit-noscroll=""><div class="hover-indicator svelte-u2mpu7"></div> `;
    if (post.isPinned) {
      $$payload.out += "<!--[-->";
      $$payload.out += `<svg class="pin-indicator svelte-u2mpu7" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path fill-rule="evenodd" clip-rule="evenodd" d="M8.95711 15.0429C9.34763 15.4334 9.34763 16.0666 8.95711 16.4571L3.95711 21.4571C3.56658 21.8476 2.93342 21.8476 2.54289 21.4571C2.15237 21.0666 2.15237 20.4334 2.54289 20.0429L7.54289 15.0429C7.93342 14.6524 8.56658 14.6524 8.95711 15.0429Z" fill="currentColor"></path><path d="M17.7093 2.55346C19.4569 3.18528 20.8147 4.54305 21.4465 6.29064C21.5536 6.58633 21.6798 6.93507 21.7258 7.24165C21.7801 7.60384 21.744 7.94519 21.608 8.32365C21.3168 9.13408 20.6643 9.49626 19.9452 9.89542L18.5273 10.6861C18.0145 10.9721 17.676 11.1618 17.4348 11.3286C17.2 11.4909 17.1333 11.582 17.1056 11.6358C17.0909 11.6641 17.0453 11.8022 17.0573 12.2506C17.0686 12.6742 17.1236 13.2487 17.2024 14.0663C17.3296 15.3845 17.1109 16.6904 16.3502 17.9068C16.2218 18.1123 16.0604 18.3708 15.88 18.563C15.6669 18.7901 15.4215 18.9506 15.1053 19.0787C14.7835 19.2091 14.4907 19.2619 14.1748 19.2478C13.906 19.2358 13.5974 19.1656 13.3398 19.107C11.3641 18.6584 9.47833 17.5121 7.98305 16.0169C6.48778 14.5216 5.34151 12.6359 4.89294 10.6602C4.83436 10.4027 4.76418 10.0941 4.75221 9.82535C4.73813 9.50941 4.79103 9.21658 4.92151 8.89474C5.04969 8.5786 5.21026 8.3331 5.43747 8.12006C5.62978 7.93974 5.88832 7.7784 6.09379 7.65019C7.29324 6.90084 8.58854 6.66967 9.89679 6.78538C10.7321 6.85926 11.321 6.91095 11.754 6.91998C11.968 6.92445 12.2391 6.94301 12.3803 6.86947C12.4326 6.84219 12.5235 6.7753 12.6865 6.53889C12.8534 6.29676 13.0438 5.95698 13.3302 5.44342L14.1046 4.05478C14.5037 3.33567 14.8659 2.68318 15.6763 2.39201C16.0548 2.25603 16.3961 2.2199 16.7583 2.27422C17.0649 2.32019 17.4136 2.44643 17.7093 2.55346Z" fill="currentColor"></path></svg>`;
    } else {
      $$payload.out += "<!--[!-->";
    }
    $$payload.out += `<!--]--> <div class="text svelte-u2mpu7"><label class="post-title svelte-u2mpu7">${escape_html(post.title)}</label> <label class="last-modified svelte-u2mpu7">last modified at ${escape_html(DateUtils.toLocale(post.lastModifiedAt))}</label></div> <svg class="more-btn svelte-u2mpu7" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"><path d="M11.992 12H12.001" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round" class="svelte-u2mpu7"></path><path d="M11.9842 18H11.9932" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round" class="svelte-u2mpu7"></path><path d="M11.9998 6H12.0088" stroke="currentColor" stroke-width="3" stroke-linecap="round" stroke-linejoin="round" class="svelte-u2mpu7"></path></svg></a>`;
  }
  $$payload.out += `<!--]--></div>`;
  pop();
}
var DraftPostsSortOption = /* @__PURE__ */ ((DraftPostsSortOption2) => {
  DraftPostsSortOption2["lastModified"] = "lastModified";
  DraftPostsSortOption2["lastCreated"] = "lastCreated";
  DraftPostsSortOption2["oldestCreated"] = "oldestCreated";
  DraftPostsSortOption2["title"] = "title";
  return DraftPostsSortOption2;
})(DraftPostsSortOption || {});
function DefaultSwitch($$payload, $$props) {
  push();
  let { fieldName = "", isChecked = void 0 } = $$props;
  let id = StringUtils.rndStrWithPref("switch-");
  $$payload.out += `<label class="switch svelte-1m5rhta"${attr("for", id)}><input type="checkbox"${attr("id", id)}${attr("name", fieldName)}${attr("checked", isChecked, true)} class="svelte-1m5rhta"> <span class="slider svelte-1m5rhta"></span></label>`;
  bind_props($$props, { isChecked });
  pop();
}
function DraftPostsListSortingLabel($$payload, $$props) {
  push();
  let {
    sortOption = void 0,
    pinnedPostsOnTop = void 0
  } = $$props;
  let isSelectMenuOpen = false;
  let $$settled = true;
  let $$inner_payload;
  function $$render_inner($$payload2) {
    $$payload2.out += `<div class="sorting-label unselectable svelte-1tdu246">Your draft posts: <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-1tdu246"><path d="M11.0001 8L19.0001 8.00006" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path><path d="M11.0001 12H16.0001" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path><path d="M11.0001 16H14.0001" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path><path d="M11.0001 4H21.0001" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path><path d="M5.5 21V3M5.5 21C4.79977 21 3.49153 19.0057 3 18.5M5.5 21C6.20023 21 7.50847 19.0057 8 18.5" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path></svg> <div${attr_class("context-menu svelte-1tdu246", void 0, { "open": isSelectMenuOpen })}><label class="sort-by-label svelte-1tdu246">Sort by</label> <div${attr_class("sort-option svelte-1tdu246", void 0, {
      "chosen": sortOption == DraftPostsSortOption.lastModified
    })}>Last modified <span class="svelte-1tdu246"></span></div> <div${attr_class("sort-option svelte-1tdu246", void 0, {
      "chosen": sortOption == DraftPostsSortOption.title
    })}>Title <span class="svelte-1tdu246"></span></div> <div${attr_class("sort-option svelte-1tdu246", void 0, {
      "chosen": sortOption == DraftPostsSortOption.lastCreated
    })}>Last created <span class="svelte-1tdu246"></span></div> <div${attr_class("sort-option svelte-1tdu246", void 0, {
      "chosen": sortOption == DraftPostsSortOption.oldestCreated
    })}>Oldest created <span class="svelte-1tdu246"></span></div> <div class="pinned-option-div svelte-1tdu246">Put pinned posts on top: `;
    DefaultSwitch($$payload2, {
      get isChecked() {
        return pinnedPostsOnTop;
      },
      set isChecked($$value) {
        pinnedPostsOnTop = $$value;
        $$settled = false;
      }
    });
    $$payload2.out += `<!----></div></div></div>`;
  }
  do {
    $$settled = true;
    $$inner_payload = copy_payload($$payload);
    $$render_inner($$inner_payload);
  } while (!$$settled);
  assign_payload($$payload, $$inner_payload);
  bind_props($$props, { sortOption, pinnedPostsOnTop });
  pop();
}
function LeftSideWriteNewPostBtn($$payload, $$props) {
  push();
  let creationErrs = [];
  $$payload.out += `<div class="container svelte-yk5jcu"><button class="new-post-btn svelte-yk5jcu"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-yk5jcu"><path d="M5.07579 17C4.08939 4.54502 12.9123 1.0121 19.9734 2.22417C20.2585 6.35185 18.2389 7.89748 14.3926 8.61125C15.1353 9.38731 16.4477 10.3639 16.3061 11.5847C16.2054 12.4534 15.6154 12.8797 14.4355 13.7322C11.8497 15.6004 8.85421 16.7785 5.07579 17Z" stroke="currentColor" stroke-width="1.6" stroke-linecap="round" stroke-linejoin="round"></path><path d="M4 22C4 15.5 7.84848 12.1818 10.5 10" stroke="currentColor" stroke-width="1.6" stroke-linecap="round" stroke-linejoin="round"></path></svg> Write new post</button> `;
  if (creationErrs.length != 0) {
    $$payload.out += "<!--[-->";
    ErrView($$payload, { err: creationErrs[0] });
  } else {
    $$payload.out += "<!--[!-->";
  }
  $$payload.out += `<!--]--></div>`;
  pop();
}
function CreateNewPostBtn($$payload, $$props) {
  push();
  let { createNewPostBtnClicked } = $$props;
  $$payload.out += `<button class="new-post-btn svelte-ur0z4i"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-ur0z4i"><path d="M12 8V16M16 12L8 12" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22C17.5228 22 22 17.5228 22 12Z" stroke="currentColor" stroke-width="1.5"></path></svg> Create new post</button>`;
  pop();
}
function NoDraftPosts($$payload, $$props) {
  push();
  let { refresh, createNewPost } = $$props;
  let postCreationErrs = [];
  async function createNewPostBtnClicked() {
    const res = await createNewPost();
    if (Array.isArray(res)) {
      postCreationErrs = res;
    }
  }
  $$payload.out += `<div class="no-posts svelte-1cbiqjw"><h1 class="svelte-1cbiqjw">You don't have any unpublished posts</h1> `;
  GrayLabelWithOnclick($$payload, { text: "Refresh", onClick: () => refresh() });
  $$payload.out += `<!----> `;
  CreateNewPostBtn($$payload, { createNewPostBtnClicked });
  $$payload.out += `<!----> `;
  if (postCreationErrs.length != 0) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<label class="errs-label svelte-1cbiqjw">Something went wrong during new post creation</label> `;
    DefaultErrBlock($$payload, { errList: postCreationErrs });
    $$payload.out += `<!---->`;
  } else {
    $$payload.out += "<!--[!-->";
  }
  $$payload.out += `<!--]--></div>`;
  pop();
}
function NoPostSelected($$payload, $$props) {
  push();
  let { createNewPost } = $$props;
  let postCreationErrs = [];
  async function createNewPostBtnClicked() {
    const res = await createNewPost();
    if (Array.isArray(res)) {
      postCreationErrs = res;
    }
  }
  $$payload.out += `<div class="no-post-selected svelte-ro6xlc"><h1 class="svelte-ro6xlc">Select a post to edit or create a new one</h1> `;
  CreateNewPostBtn($$payload, { createNewPostBtnClicked });
  $$payload.out += `<!----> `;
  if (postCreationErrs.length != 0) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<div class="errs-div svelte-ro6xlc"><label class="svelte-ro6xlc">Something went wrong during new post creation</label> `;
    DefaultErrBlock($$payload, { errList: postCreationErrs });
    $$payload.out += `<!----></div>`;
  } else {
    $$payload.out += "<!--[!-->";
  }
  $$payload.out += `<!--]--></div>`;
  pop();
}
class NewPostPageState {
  selectedPostId = null;
  draftPostsMainInfo = [];
  postsMainInfoFetchingErrs = [];
  draftPostCache;
  draftPostsSortOption = DraftPostsSortOption.lastModified;
  draftPostsPinnedPostsOnTop = true;
  constructor() {
    this.draftPostCache = /* @__PURE__ */ new Map();
  }
  async fetchDraftPosts() {
    const url = `/draft-posts?sortOption=${this.draftPostsSortOption}&putPinnedOnTop=${this.draftPostsPinnedPostsOnTop}`;
    const response = await ApiMain.fetchJsonResponse(url, { method: "GET" });
    if (response.isSuccess) {
      this.draftPostsMainInfo = response.data.posts;
      if (this.postsMainInfoFetchingErrs.length != 0) {
        this.postsMainInfoFetchingErrs = [];
      }
    } else {
      this.postsMainInfoFetchingErrs = response.errors;
      this.draftPostsMainInfo = [];
    }
  }
  async createNewPost() {
    const response = await ApiMain.fetchJsonResponse("/draft-posts/create", { method: "POST" });
    if (response.isSuccess) {
      this.draftPostsSortOption = DraftPostsSortOption.lastModified;
      goto(`/new-post/${response.data.id}`, {});
      await this.fetchDraftPosts();
    } else {
      return response.errors;
    }
  }
  async getPostFullInfo() {
    const postFomCache = this.draftPostCache.get(this.selectedPostId ?? "");
    if (postFomCache) {
      return postFomCache;
    }
    const response = await ApiMain.fetchJsonResponse(`/draft-posts/${this.selectedPostId}`, { method: "GET" });
    if (response.isSuccess) {
      if (this.selectedPostId) {
        this.draftPostCache.set(this.selectedPostId, response.data);
      }
      return response.data;
    } else {
      return response.errors;
    }
  }
  updateCache(newVal) {
    this.draftPostCache.set(newVal.id, newVal);
    const idx = this.draftPostsMainInfo.findIndex((post) => post.id === newVal.id);
    if (idx !== -1) {
      this.draftPostsMainInfo[idx] = {
        id: newVal.id,
        title: newVal.title,
        isPinned: newVal.isPinned,
        lastModifiedAt: newVal.lastModifiedAt
      };
    }
    if (this.draftPostsSortOption === DraftPostsSortOption.lastModified || this.draftPostsSortOption === DraftPostsSortOption.title) {
      this.fetchDraftPosts();
    }
  }
  updatePostPinnedState(postId, newIsPinnedState) {
    const cachedPost = this.draftPostCache.get(postId);
    if (cachedPost) {
      cachedPost.isPinned = newIsPinnedState;
    }
    const idx = this.draftPostsMainInfo.findIndex((post) => post.id === postId);
    if (idx !== -1) {
      this.draftPostsMainInfo[idx].isPinned = newIsPinnedState;
    }
    if (this.draftPostsPinnedPostsOnTop) {
      this.fetchDraftPosts();
    }
  }
  async deletePost(id) {
    const response = await ApiMain.fetchVoidResponse(`/draft-posts/${id}/delete`, { method: "DELETE" });
    if (response.isSuccess) {
      await this.fetchDraftPosts();
      if (this.selectedPostId === id) {
        this.selectedPostId = null;
      }
    }
  }
}
function unauthenticated($$payload) {
  PageAuthNeeded($$payload);
}
function _page($$payload, $$props) {
  push();
  let pageState = new NewPostPageState();
  function authenticated($$payload2) {
    if (pageState.postsMainInfoFetchingErrs.length != 0) {
      $$payload2.out += "<!--[-->";
      $$payload2.out += `<div class="fetching-err svelte-1rhn31f"><label class="svelte-1rhn31f">Something went wrong...</label> `;
      DefaultErrBlock($$payload2, { errList: pageState.postsMainInfoFetchingErrs });
      $$payload2.out += `<!----></div>`;
    } else if (pageState.draftPostsMainInfo.length == 0) {
      $$payload2.out += "<!--[1-->";
      NoDraftPosts($$payload2, {
        refresh: async () => await pageState.fetchDraftPosts(),
        createNewPost: async () => await pageState.createNewPost()
      });
    } else {
      $$payload2.out += "<!--[!-->";
      $$payload2.out += `<div class="page-content svelte-1rhn31f"><div class="left-side svelte-1rhn31f">`;
      DraftPostItemMoreActionsMenu($$payload2, {
        updatePostPinnedState: (postId, newState) => pageState.updatePostPinnedState(postId, newState),
        deletePost: async (id) => await pageState.deletePost(id)
      });
      $$payload2.out += `<!----> `;
      LeftSideWriteNewPostBtn($$payload2);
      $$payload2.out += `<!----> `;
      DraftPostsListSortingLabel($$payload2, {
        get sortOption() {
          return pageState.draftPostsSortOption;
        },
        set sortOption($$value) {
          pageState.draftPostsSortOption = $$value;
          $$settled = false;
        },
        get pinnedPostsOnTop() {
          return pageState.draftPostsPinnedPostsOnTop;
        },
        set pinnedPostsOnTop($$value) {
          pageState.draftPostsPinnedPostsOnTop = $$value;
          $$settled = false;
        }
      });
      $$payload2.out += `<!----> `;
      DraftPostsList($$payload2, {
        currentPostId: pageState.selectedPostId ?? "",
        posts: pageState.draftPostsMainInfo
      });
      $$payload2.out += `<!----></div> `;
      if (StringUtils.isNullOrWhiteSpace(pageState.selectedPostId)) {
        $$payload2.out += "<!--[-->";
        NoPostSelected($$payload2, {
          createNewPost: async () => await pageState.createNewPost()
        });
      } else {
        $$payload2.out += "<!--[!-->";
        DraftPostEditingView($$payload2, {
          getPostData: async () => await pageState.getPostFullInfo(),
          updateCache: (newVal) => pageState.updateCache(newVal)
        });
      }
      $$payload2.out += `<!--]--></div>`;
    }
    $$payload2.out += `<!--]-->`;
  }
  let $$settled = true;
  let $$inner_payload;
  function $$render_inner($$payload2) {
    AuthView($$payload2, { authenticated, unauthenticated });
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
//# sourceMappingURL=_page.svelte-D9jPjY5e.js.map
