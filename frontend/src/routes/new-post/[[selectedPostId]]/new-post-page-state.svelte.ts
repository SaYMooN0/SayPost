import { goto } from "$app/navigation";
import { ApiMain } from "../../../ts/backend-services";
import type { Err } from "../../../ts/common/errs/err";
import { DraftPostsSortOption, type DraftPostFullInfo, type DraftPostMainInfo } from "./draft-posts";

export class NewPostPageState {
    selectedPostId: string | null = $state(null);

    draftPostsMainInfo: DraftPostMainInfo[] = $state([]);
    postsMainInfoFetchingErrs: Err[] = $state([]);

    draftPostCache: Map<string, DraftPostFullInfo>;

    draftPostsSortOption = $state(DraftPostsSortOption.lastModified);
    draftPostsPinnedPostsOnTop = $state(true);

    constructor() {
        this.draftPostCache = new Map();
        $effect(() => {
            this.draftPostsSortOption;
            this.draftPostsPinnedPostsOnTop;

            this.fetchDraftPosts();
        });
    }
    async fetchDraftPosts(): Promise<void> {
        const url = `/draft-posts?sortOption=${this.draftPostsSortOption}&putPinnedOnTop=${this.draftPostsPinnedPostsOnTop}`;
        const response = await ApiMain.fetchJsonResponse<{
            posts: DraftPostMainInfo[];
        }>(url, {
            method: "GET",
        });
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
    async createNewPost(): Promise<Err[] | void> {
        const response = await ApiMain.fetchJsonResponse<DraftPostFullInfo>(
            "/draft-posts/create", { method: "POST" },
        );
        if (response.isSuccess) {
            this.draftPostsSortOption = DraftPostsSortOption.lastModified;
            goto(`/new-post/${response.data.id}`, { noScroll: true, });
            await this.fetchDraftPosts();
        } else {
            return response.errors;
        }
    }
    async getPostFullInfo(): Promise<DraftPostFullInfo | Err[]> {
        const postFomCache = this.draftPostCache.get(this.selectedPostId ?? "");
        if (postFomCache) { return postFomCache; }

        const response = await ApiMain.fetchJsonResponse<DraftPostFullInfo>(
            `/draft-posts/${this.selectedPostId}`, { method: "GET", },
        );
        if (response.isSuccess) {
            if (this.selectedPostId) {
                this.draftPostCache.set(this.selectedPostId, response.data);
            }
            return response.data;
        } else { return response.errors; }
    }
    updateCache(newVal: DraftPostFullInfo) {
        console.log("updateCache");
        this.draftPostCache.set(newVal.id, newVal);
        const idx = this.draftPostsMainInfo.findIndex((post) => post.id === newVal.id);

        if (idx !== -1) {
            this.draftPostsMainInfo[idx] = {
                id: newVal.id,
                title: newVal.title,
                isPinned: newVal.isPinned,
                lastModifiedAt: newVal.lastModifiedAt,
            };
        }
    }
    updatePostPinnedState(postId: string, newIsPinnedState: boolean) {
        const cachedPost = this.draftPostCache.get(postId);
        if (cachedPost) {
            cachedPost.isPinned = newIsPinnedState;
        }

        const idx = this.draftPostsMainInfo.findIndex(post => post.id === postId);
        if (idx !== -1) {
            this.draftPostsMainInfo[idx].isPinned = newIsPinnedState;
        }

        if (this.draftPostsPinnedPostsOnTop) {
            this.fetchDraftPosts();
        }
    }
    async deletePost(id: string): Promise<void | Err[]> {
        const response = await ApiMain.fetchVoidResponse(
            `/draft-posts/${id}/delete`, { method: "DELETE" },
        );
        if (response.isSuccess) {
            await this.fetchDraftPosts();
            if (this.selectedPostId === id) {
                this.selectedPostId = null;
            }
        }
    }
}