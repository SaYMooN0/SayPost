<script lang="ts">
    import { onMount } from "svelte";
    import { getAuthData } from "../../../../../stores/auth-store.svelte";
    import { ApiMain } from "../../../../../ts/backend-services";

    let {
        postId,
        likesCount,
        isLikedByViewer,
    }: { postId: string; likesCount: number; isLikedByViewer: boolean } =
        $props<{
            postId: string;
            likesCount: number;
            isLikedByViewer: boolean;
        }>();
    let likesCountText = $derived(likesCount === 1 ? "like" : "likes");
    let notAuthenticatedEl: HTMLElement;
    let showNotAuthenticatedEl = $state(false);
    async function toggleLike(event: MouseEvent) {
        const authData = await getAuthData();
        if (!authData.isAuthenticated()) {
            showNotAuthenticatedEl = true;
            event.stopPropagation();
        }
        const url = `/posts/${postId}/` + (isLikedByViewer ? "unlike" : "like");
        const response = await ApiMain.fetchJsonResponse<{
            likesCount: number;
        }>(url, {
            method: "PATCH",
        });

        if (response.isSuccess) {
            isLikedByViewer = !isLikedByViewer;
            likesCount = response.data.likesCount;
        }
    }
    function clickOutsideOfNotAuthenticated(event: any) {
        if (!notAuthenticatedEl.contains(event.target)) {
            showNotAuthenticatedEl = false;
        }
    }
    onMount(() => {
        document.addEventListener("click", clickOutsideOfNotAuthenticated);
        return () => {
            document.removeEventListener(
                "click",
                clickOutsideOfNotAuthenticated,
            );
        };
    });
</script>

<div class="likes-section unselectable">
    <label class="count">{likesCount} {likesCountText}</label>
    <div class="like-btn-wrapper">
        <svg
            class="like-btn"
            class:isLiked={isLikedByViewer}
            onclick={(e) => toggleLike(e)}
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
        >
            {#if isLikedByViewer}
                <path
                    fill-rule="evenodd"
                    clip-rule="evenodd"
                    d="M1.25 12.625C1.25 11.0372 2.49721 9.75003 4.03571 9.75003C6.08706 9.75003 7.75 11.4663 7.75 13.5834V17.4167C7.75 19.5338 6.08706 21.25 4.03571 21.25C2.49721 21.25 1.25 19.9628 1.25 18.375V12.625ZM4.03571 11.6667C3.52288 11.6667 3.10714 12.0958 3.10714 12.625V18.375C3.10714 18.9043 3.52288 19.3334 4.03571 19.3334C5.06139 19.3334 5.89286 18.4752 5.89286 17.4167V13.5834C5.89286 12.5248 5.06139 11.6667 4.03571 11.6667Z"
                    fill="currentColor"
                />
                <path
                    d="M12.799 3.18499C13.5298 2.54898 14.6525 2.61713 15.2964 3.34567C15.4097 3.48751 15.6704 3.83281 15.807 4.07931C16.4367 5.11969 16.6286 6.35887 16.3392 7.53622C16.3068 7.66781 16.2656 7.80085 16.2023 8.00546L15.9289 8.88818C15.8154 9.25476 15.7468 9.47927 15.7117 9.64734C15.6855 9.74004 15.6985 9.9283 15.9601 9.93979C16.1402 9.95585 16.8457 9.95655 17.2394 9.95655C18.4766 9.95653 19.4811 9.95651 20.2488 10.0581C21.0337 10.162 21.7462 10.3913 22.2315 10.9996C22.3273 11.1197 22.4127 11.2476 22.4867 11.3821C22.8663 12.0718 22.7859 12.8147 22.5573 13.5625C22.335 14.2901 21.9188 15.1861 21.409 16.2837C20.9359 17.3024 20.516 18.2066 20.1401 18.8407C19.7512 19.4967 19.3355 20.0124 18.7609 20.4046C18.0878 20.8641 17.1903 21.1044 16.4221 21.1783C15.6771 21.25 14.7621 21.25 13.6144 21.25C12.2303 21.25 10.5147 21.25 9.64051 21.1362C8.73584 21.0183 7.97376 20.7667 7.3663 20.1781C6.75553 19.5863 6.49136 18.8385 6.36819 17.9508C6.25001 17.0992 6.25003 16.0174 6.25005 14.6802L6.25003 13.5075C6.24964 12.1895 6.24939 11.3282 6.55913 10.5294C6.86812 9.73266 7.45104 9.08753 8.34723 8.09572L12.5574 3.43371C12.6369 3.34545 12.7182 3.25529 12.799 3.18499Z"
                    fill="currentColor"
                />
            {:else}
                <path
                    d="M2 12.5C2 11.3954 2.89543 10.5 4 10.5C5.65685 10.5 7 11.8431 7 13.5V17.5C7 19.1569 5.65685 20.5 4 20.5C2.89543 20.5 2 19.6046 2 18.5V12.5Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M15.4787 7.80626L15.2124 8.66634C14.9942 9.37111 14.8851 9.72349 14.969 10.0018C15.0369 10.2269 15.1859 10.421 15.389 10.5487C15.64 10.7065 16.0197 10.7065 16.7791 10.7065H17.1831C19.7532 10.7065 21.0382 10.7065 21.6452 11.4673C21.7145 11.5542 21.7762 11.6467 21.8296 11.7437C22.2965 12.5921 21.7657 13.7351 20.704 16.0211C19.7297 18.1189 19.2425 19.1678 18.338 19.7852C18.2505 19.8449 18.1605 19.9013 18.0683 19.9541C17.116 20.5 15.9362 20.5 13.5764 20.5H13.0646C10.2057 20.5 8.77628 20.5 7.88814 19.6395C7 18.7789 7 17.3939 7 14.6239V13.6503C7 12.1946 7 11.4668 7.25834 10.8006C7.51668 10.1344 8.01135 9.58664 9.00069 8.49112L13.0921 3.96056C13.1947 3.84694 13.246 3.79012 13.2913 3.75075C13.7135 3.38328 14.3652 3.42464 14.7344 3.84235C14.774 3.8871 14.8172 3.94991 14.9036 4.07554C15.0388 4.27205 15.1064 4.37031 15.1654 4.46765C15.6928 5.33913 15.8524 6.37436 15.6108 7.35715C15.5838 7.46692 15.5488 7.5801 15.4787 7.80626Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            {/if}
        </svg>
        <div
            class="not-authenticated"
            bind:this={notAuthenticatedEl}
            class:show={showNotAuthenticatedEl}
        >
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                onclick={() => (showNotAuthenticatedEl = false)}
                fill="none"
            >
                <path
                    d="M19.0005 4.99988L5.00049 18.9999M5.00049 4.99988L19.0005 18.9999"
                    stroke="currentColor"
                    stroke-width="3.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            </svg>
            Log in to like
        </div>
    </div>
</div>

<style>
    .likes-section {
        display: flex;
        flex-direction: row;
        align-items: center;
    }

    .count {
        margin-right: 0.5rem;
        font-size: 1.25rem;
        font-weight: 450;
        text-align: end;
    }

    .like-btn-wrapper {
        position: relative;
    }

    .like-btn {
        width: 2.25rem;
        height: 2.25rem;
        padding: 0.25rem;
        border-radius: 0.25rem;
        background-color: var(--back-second);
        color: var(--accent-main);
        transition: all 0.08s ease-in;
        box-sizing: border-box;
    }

    .like-btn:hover {
        padding: 0.125rem;
        color: var(--accent-hov);
        transform: scale(1.02);
    }

    .like-btn:active {
        transform: scale(0.98);
    }

    .not-authenticated {
        position: absolute;
        top: -10%;
        width: max-content;
        padding: 0.5rem;
        background-color: var(--back-second);
        font-size: 0;
        text-align: center;
        opacity: 0;
        transition:
            all 0.1s ease-in,
            top 0.12s ease-out,
            opacity 0.2s ease-in;
        transform: translateX(-60%);
    }

    .not-authenticated.show {
        top: -140%;
        display: block;
        padding: 0.75rem 1.75rem;
        border-radius: 0.5rem;
        font-size: 1rem;
        font-weight: 450;
        opacity: 1;
        box-shadow: rgb(0 0 0 / 10%) 0 3px 4px;
    }

    .not-authenticated > svg {
        position: absolute;
        top: 0.25rem;
        right: 0.25rem;
        height: 0;
        transition: inherit;
    }

    .not-authenticated.show > svg {
        height: 0.75rem;
        padding: 0.25rem;
        border: 0;
        border-radius: 1rem;
        background-color: var(--gray);
        color: var(--back-main);
        cursor: pointer;
    }

    .not-authenticated.show > svg:hover {
        transform: scale(1.08);
    }
</style>
