import { p as push, x as ensure_array_like, u as escape_html, l as pop, m as copy_payload, n as assign_payload, t as attr, w as stringify, v as attr_class, y as await_block, q as bind_props } from './index2-DGUJ8ZVV.js';
import { D as DefaultErrBlock, E as ErrView } from './DefaultErrBlock-DroY93XM.js';
import { C as CubeLoader } from './CubeLoader-CcsMcLHy.js';
import { S as StringUtils, E as Err } from './date-utils-BMgSYFUH.js';
import { A as AuthView } from './AuthView-BQEuqrR5.js';
import { A as ApiMain } from './backend-services-gK9Cr6ja.js';
import './t-plain-err-CtrWZBuG.js';

var CommentsSortOption = /* @__PURE__ */ ((CommentsSortOption2) => {
  CommentsSortOption2["Newest"] = "newest";
  CommentsSortOption2["Oldest"] = "oldest";
  return CommentsSortOption2;
})(CommentsSortOption || {});
function CommentView($$payload, $$props) {
  push();
  let { comment } = $$props;
  $$payload.out += `<div class="comment-view svelte-13x5cnj"><div class="comment-content">${escape_html(comment.content)}</div></div>`;
  pop();
}
function NewCommentInput($$payload, $$props) {
  push();
  let commentValue = "";
  let saveErrs = [];
  $$payload.out += `<div class="container svelte-6no4gw"><textarea placeholder="Add a comment..." rows="1" class="svelte-6no4gw">`;
  const $$body = escape_html(commentValue);
  if ($$body) {
    $$payload.out += `${$$body}`;
  }
  $$payload.out += `</textarea> <div class="btns-container svelte-6no4gw"><button class="cancel-btn svelte-6no4gw">Cancel</button> <button class="save-btn svelte-6no4gw"${attr("disabled", StringUtils.isNullOrWhiteSpace(commentValue), true)}>Comment</button></div> `;
  DefaultErrBlock($$payload, { errList: saveErrs });
  $$payload.out += `<!----></div>`;
  pop();
}
function SelectCommentsSorting($$payload, $$props) {
  push();
  let { sortOption = void 0, commentsCount } = $$props;
  let isSelectMenuOpen = false;
  $$payload.out += `<div class="sorting-label unselectable svelte-10r4nks">${escape_html(commentsCount)} comments <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-10r4nks"><path d="M11.0001 8L19.0001 8.00006" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path><path d="M11.0001 12H16.0001" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path><path d="M11.0001 16H14.0001" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path><path d="M11.0001 4H21.0001" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path><path d="M5.5 21V3M5.5 21C4.79977 21 3.49153 19.0057 3 18.5M5.5 21C6.20023 21 7.50847 19.0057 8 18.5" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"></path></svg> <div${attr_class("context-menu svelte-10r4nks", void 0, { "open": isSelectMenuOpen })}><div${attr_class("sort-option svelte-10r4nks", void 0, {
    "chosen": sortOption == CommentsSortOption.Newest
  })}>Newest first <span class="svelte-10r4nks"></span></div> <div${attr_class("sort-option svelte-10r4nks", void 0, {
    "chosen": sortOption == CommentsSortOption.Oldest
  })}>Oldest first <span class="svelte-10r4nks"></span></div></div></div>`;
  bind_props($$props, { sortOption });
  pop();
}
function unauthenticated($$payload) {
  $$payload.out += `<div class="auth-needed svelte-tjm26o">To leave a comment you need to be logged in</div>`;
}
function CommentsSection($$payload, $$props) {
  push();
  let { postId } = $$props;
  let comments = [];
  let commentsFetchingErrs = [];
  let sortOption = CommentsSortOption.Newest;
  async function fetchComments() {
    const url = `/posts/${postId}/comments?sortOption=${sortOption}`;
    const response = await ApiMain.fetchJsonResponse(url, { method: "GET" });
    if (response.isSuccess) {
      comments = response.data.comments;
      commentsFetchingErrs = [];
    } else {
      comments = [];
      commentsFetchingErrs = response.errors;
    }
  }
  function authenticated($$payload2) {
    NewCommentInput($$payload2);
  }
  let $$settled = true;
  let $$inner_payload;
  function $$render_inner($$payload2) {
    $$payload2.out += `<div class="comments-section svelte-tjm26o">`;
    AuthView($$payload2, { authenticated, unauthenticated });
    $$payload2.out += `<!----> <!---->`;
    await_block(
      fetchComments(),
      () => {
        $$payload2.out += `<div class="no-comments loader-wrapper svelte-tjm26o">Loading comments `;
        CubeLoader($$payload2);
        $$payload2.out += `<!----></div>`;
      },
      () => {
        if (comments.length == 0) {
          $$payload2.out += "<!--[-->";
          $$payload2.out += `<div class="no-comments svelte-tjm26o">This comment has no comments yet. Be the first</div>`;
        } else {
          $$payload2.out += "<!--[!-->";
          SelectCommentsSorting($$payload2, {
            commentsCount: comments.length,
            get sortOption() {
              return sortOption;
            },
            set sortOption($$value) {
              sortOption = $$value;
              $$settled = false;
            }
          });
          $$payload2.out += `<!----> `;
          if (commentsFetchingErrs.length === 0) {
            $$payload2.out += "<!--[-->";
            const each_array = ensure_array_like(comments);
            $$payload2.out += `<!--[-->`;
            for (let $$index = 0, $$length = each_array.length; $$index < $$length; $$index++) {
              let c = each_array[$$index];
              CommentView($$payload2, { comment: c });
            }
            $$payload2.out += `<!--]-->`;
          } else {
            $$payload2.out += "<!--[!-->";
            DefaultErrBlock($$payload2, { errList: commentsFetchingErrs });
          }
          $$payload2.out += `<!--]-->`;
        }
        $$payload2.out += `<!--]-->`;
      }
    );
    $$payload2.out += `<!----></div>`;
  }
  do {
    $$settled = true;
    $$inner_payload = copy_payload($$payload);
    $$render_inner($$inner_payload);
  } while (!$$settled);
  assign_payload($$payload, $$inner_payload);
  pop();
}
function PostAuthorView($$payload, $$props) {
  let { authorId } = $$props;
  $$payload.out += `<p class="author svelte-zugpzz">by <a${attr("href", `/users/${stringify(authorId)}`)} data-sveltekit-preload-data="tap" class="svelte-zugpzz">${escape_html(authorId)}</a></p>`;
}
function PostLikesComponent($$payload, $$props) {
  push();
  let { likesCount, isLikedByViewer } = $$props;
  let likesCountText = likesCount === 1 ? "like" : "likes";
  let showNotAuthenticatedEl = false;
  $$payload.out += `<div class="likes-section unselectable svelte-1rs93zo"><label class="count svelte-1rs93zo">${escape_html(likesCount)} ${escape_html(likesCountText)}</label> <div class="like-btn-wrapper svelte-1rs93zo"><svg${attr_class("like-btn svelte-1rs93zo", void 0, { "isLiked": isLikedByViewer })} xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">`;
  if (isLikedByViewer) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<path fill-rule="evenodd" clip-rule="evenodd" d="M1.25 12.625C1.25 11.0372 2.49721 9.75003 4.03571 9.75003C6.08706 9.75003 7.75 11.4663 7.75 13.5834V17.4167C7.75 19.5338 6.08706 21.25 4.03571 21.25C2.49721 21.25 1.25 19.9628 1.25 18.375V12.625ZM4.03571 11.6667C3.52288 11.6667 3.10714 12.0958 3.10714 12.625V18.375C3.10714 18.9043 3.52288 19.3334 4.03571 19.3334C5.06139 19.3334 5.89286 18.4752 5.89286 17.4167V13.5834C5.89286 12.5248 5.06139 11.6667 4.03571 11.6667Z" fill="currentColor"></path><path d="M12.799 3.18499C13.5298 2.54898 14.6525 2.61713 15.2964 3.34567C15.4097 3.48751 15.6704 3.83281 15.807 4.07931C16.4367 5.11969 16.6286 6.35887 16.3392 7.53622C16.3068 7.66781 16.2656 7.80085 16.2023 8.00546L15.9289 8.88818C15.8154 9.25476 15.7468 9.47927 15.7117 9.64734C15.6855 9.74004 15.6985 9.9283 15.9601 9.93979C16.1402 9.95585 16.8457 9.95655 17.2394 9.95655C18.4766 9.95653 19.4811 9.95651 20.2488 10.0581C21.0337 10.162 21.7462 10.3913 22.2315 10.9996C22.3273 11.1197 22.4127 11.2476 22.4867 11.3821C22.8663 12.0718 22.7859 12.8147 22.5573 13.5625C22.335 14.2901 21.9188 15.1861 21.409 16.2837C20.9359 17.3024 20.516 18.2066 20.1401 18.8407C19.7512 19.4967 19.3355 20.0124 18.7609 20.4046C18.0878 20.8641 17.1903 21.1044 16.4221 21.1783C15.6771 21.25 14.7621 21.25 13.6144 21.25C12.2303 21.25 10.5147 21.25 9.64051 21.1362C8.73584 21.0183 7.97376 20.7667 7.3663 20.1781C6.75553 19.5863 6.49136 18.8385 6.36819 17.9508C6.25001 17.0992 6.25003 16.0174 6.25005 14.6802L6.25003 13.5075C6.24964 12.1895 6.24939 11.3282 6.55913 10.5294C6.86812 9.73266 7.45104 9.08753 8.34723 8.09572L12.5574 3.43371C12.6369 3.34545 12.7182 3.25529 12.799 3.18499Z" fill="currentColor"></path>`;
  } else {
    $$payload.out += "<!--[!-->";
    $$payload.out += `<path d="M2 12.5C2 11.3954 2.89543 10.5 4 10.5C5.65685 10.5 7 11.8431 7 13.5V17.5C7 19.1569 5.65685 20.5 4 20.5C2.89543 20.5 2 19.6046 2 18.5V12.5Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path><path d="M15.4787 7.80626L15.2124 8.66634C14.9942 9.37111 14.8851 9.72349 14.969 10.0018C15.0369 10.2269 15.1859 10.421 15.389 10.5487C15.64 10.7065 16.0197 10.7065 16.7791 10.7065H17.1831C19.7532 10.7065 21.0382 10.7065 21.6452 11.4673C21.7145 11.5542 21.7762 11.6467 21.8296 11.7437C22.2965 12.5921 21.7657 13.7351 20.704 16.0211C19.7297 18.1189 19.2425 19.1678 18.338 19.7852C18.2505 19.8449 18.1605 19.9013 18.0683 19.9541C17.116 20.5 15.9362 20.5 13.5764 20.5H13.0646C10.2057 20.5 8.77628 20.5 7.88814 19.6395C7 18.7789 7 17.3939 7 14.6239V13.6503C7 12.1946 7 11.4668 7.25834 10.8006C7.51668 10.1344 8.01135 9.58664 9.00069 8.49112L13.0921 3.96056C13.1947 3.84694 13.246 3.79012 13.2913 3.75075C13.7135 3.38328 14.3652 3.42464 14.7344 3.84235C14.774 3.8871 14.8172 3.94991 14.9036 4.07554C15.0388 4.27205 15.1064 4.37031 15.1654 4.46765C15.6928 5.33913 15.8524 6.37436 15.6108 7.35715C15.5838 7.46692 15.5488 7.5801 15.4787 7.80626Z" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path>`;
  }
  $$payload.out += `<!--]--></svg> <div${attr_class("not-authenticated svelte-1rs93zo", void 0, { "show": showNotAuthenticatedEl })}><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" class="svelte-1rs93zo"><path d="M19.0005 4.99988L5.00049 18.9999M5.00049 4.99988L19.0005 18.9999" stroke="currentColor" stroke-width="3.5" stroke-linecap="round" stroke-linejoin="round"></path></svg> Log in to like</div></div></div>`;
  pop();
}
function AuthorAndLikesSection($$payload, $$props) {
  let {
    authorId,
    likesCount,
    isLikedByViewer
  } = $$props;
  $$payload.out += `<div class="author-and-likes-section svelte-1l9trb7">`;
  PostAuthorView($$payload, { authorId });
  $$payload.out += `<!----> `;
  PostLikesComponent($$payload, { likesCount, isLikedByViewer });
  $$payload.out += `<!----></div>`;
}
function PostContentHeadingView($$payload, $$props) {
  push();
  let { heading } = $$props;
  $$payload.out += `<h2 class="svelte-15ni73o">${escape_html(heading.value)}</h2>`;
  pop();
}
function PostContentListView($$payload, $$props) {
  push();
  let { list } = $$props;
  const each_array = ensure_array_like(list.items);
  $$payload.out += `<div class="items-container svelte-1mnnmly"><!--[-->`;
  for (let $$index = 0, $$length = each_array.length; $$index < $$length; $$index++) {
    let listItem = each_array[$$index];
    $$payload.out += `<div class="list-display-item svelte-1mnnmly"><span class="bullet svelte-1mnnmly"></span> ${escape_html(listItem)}</div>`;
  }
  $$payload.out += `<!--]--></div>`;
  pop();
}
function PostContentParagraphView($$payload, $$props) {
  push();
  let { paragraph } = $$props;
  $$payload.out += `<p class="svelte-kkk3e4">${escape_html(paragraph.value)}</p>`;
  pop();
}
function PostContentQuoteView($$payload, $$props) {
  push();
  let { quote } = $$props;
  $$payload.out += `<div class="quote svelte-1g1da36"><p class="text svelte-1g1da36">${escape_html(quote.text)}</p> `;
  if (!StringUtils.isNullOrWhiteSpace(quote.author)) {
    $$payload.out += "<!--[-->";
    $$payload.out += `<p class="author svelte-1g1da36">${escape_html(quote.author)}</p>`;
  } else {
    $$payload.out += "<!--[!-->";
  }
  $$payload.out += `<!--]--></div>`;
  pop();
}
function PostContentSubheadingView($$payload, $$props) {
  push();
  let { subheading } = $$props;
  $$payload.out += `<h3 class="svelte-1qkasfp">${escape_html(subheading.value)}</h3>`;
  pop();
}
function _page($$payload, $$props) {
  push();
  let { data } = $$props;
  if (data.errors) {
    $$payload.out += "<!--[-->";
    DefaultErrBlock($$payload, { errList: data.errors });
  } else {
    $$payload.out += "<!--[!-->";
    const each_array = ensure_array_like(data.post.content.items);
    $$payload.out += `<h1 class="svelte-1hrqgxk">${escape_html(data.post.title)}</h1> <div class="post-content svelte-1hrqgxk"><!--[-->`;
    for (let $$index = 0, $$length = each_array.length; $$index < $$length; $$index++) {
      let item = each_array[$$index];
      if (item.$type === "HeadingContentItem") {
        $$payload.out += "<!--[-->";
        PostContentHeadingView($$payload, { heading: item });
      } else if (item.$type === "SubheadingContentItem") {
        $$payload.out += "<!--[1-->";
        PostContentSubheadingView($$payload, { subheading: item });
      } else if (item.$type === "ParagraphContentItem") {
        $$payload.out += "<!--[2-->";
        PostContentParagraphView($$payload, { paragraph: item });
      } else if (item.$type === "QuoteContentItem") {
        $$payload.out += "<!--[3-->";
        PostContentQuoteView($$payload, { quote: item });
      } else if (item.$type === "ListContentItem") {
        $$payload.out += "<!--[4-->";
        PostContentListView($$payload, { list: item });
      } else {
        $$payload.out += "<!--[!-->";
        ErrView($$payload, {
          err: new Err("Unknown post content item type")
        });
      }
      $$payload.out += `<!--]-->`;
    }
    $$payload.out += `<!--]--></div> <div class="divider svelte-1hrqgxk"></div> `;
    AuthorAndLikesSection($$payload, {
      postId: data.post.id,
      authorId: data.post.authorId,
      likesCount: data.post.likesCount,
      isLikedByViewer: data.post.isLikedByActor ?? false
    });
    $$payload.out += `<!----> `;
    CommentsSection($$payload, { postId: data.post.id });
    $$payload.out += `<!---->`;
  }
  $$payload.out += `<!--]-->`;
  pop();
}

export { _page as default };
//# sourceMappingURL=_page.svelte-ETQV0MYH.js.map
