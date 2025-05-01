<script lang="ts">
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import { ApiMain } from "../../../../../ts/backend-services";
    import { Err } from "../../../../../ts/common/errs/err";

    let {
        postId,
        updateAfterPublishing,
        anyElementsInEditingState,
    }: {
        postId: string;
        updateAfterPublishing: () => void;
        anyElementsInEditingState: () => boolean;
    } = $props<{
        postId: string;
        updateAfterPublishing: () => void;
        anyElementsInEditingState: () => boolean;
    }>();

    let publishingErrs: Err[] = $state([]);
    async function publishPost() {
        if (anyElementsInEditingState()) {
            publishingErrs = [
                new Err(
                    "Please save or cancel all unsaved changes",
                    null,
                    "Title or content is probably in editing state",
                ),
            ];
            return;
        }
        const response = await ApiMain.fetchJsonResponse<{
            publishedPostId: string;
        }>(`/draft-posts/${postId}/publish`, { method: "POST" });
        if (response.isSuccess) {
            publishingErrs = [];
            console.log(response.data);
            updateAfterPublishing();
        } else {
            publishingErrs = response.errors;
        }
    }
</script>

<button onclick={() => publishPost()} class="publish-btn">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
        <path
            d="M21.0477 3.05293C18.8697 0.707363 2.48648 6.4532 2.50001 8.551C2.51535 10.9299 8.89809 11.6617 10.6672 12.1581C11.7311 12.4565 12.016 12.7625 12.2613 13.8781C13.3723 18.9305 13.9301 21.4435 15.2014 21.4996C17.2278 21.5892 23.1733 5.342 21.0477 3.05293Z"
            stroke="currentColor"
            stroke-width="2"
        ></path>
        <path
            d="M11.5 12.5L15 9"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
        ></path>
    </svg>
    Publish post
</button>
<div class="errs-wrapper">
    <DefaultErrBlock errList={publishingErrs} />
</div>

<style>
    .publish-btn {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 0.5rem;
        width: fit-content;
        padding: 0.375rem 1.25rem;
        margin: 1rem auto;
        border: none;
        border-radius: 4rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1.5rem;
        transition: all 0.1s ease-in;
        cursor: pointer;
    }

    .publish-btn:hover {
        gap: 0.75rem;
        padding: 0.375rem 1.5rem;
        background-color: var(--accent-hov);
    }

    .publish-btn:active {
        transform: scale(0.96);
    }

    .publish-btn svg {
        height: 1.75rem;
        transition: inherit;
    }

    .publish-btn:hover svg {
        transform: rotate(25deg);
    }

    .errs-wrapper {
        margin-bottom: 4rem;
    }
</style>
