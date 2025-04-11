<script lang="ts">
    import { page } from "$app/state";
    import { onMount } from "svelte";
    import CubeLoader from "../../../../components/loaders/CubeLoader.svelte";
    import { ApiAuth } from "../../../../ts/backend-services";
    import ErrView from "../../../../components/err_blocks/ErrView.svelte";
    import { StringUtils } from "../../../../ts/string-utils";

    let userId: string = page.params.userId;
    let confirmationCode: string = page.params.confirmationCode;

    async function confirmRegistration() {
        return ApiAuth.fetchVoidResponse(
            "/confirm-registration",
            ApiAuth.requestJsonPostOptions({ userId, confirmationCode }),
        );
    }
</script>

<div class="view-container">
    {#await confirmRegistration()}
        <h1 class="loading-h">Confirming your email</h1>
        <CubeLoader />
    {:then response}
        {#if response.isSuccess}
            <h1>You have successfully confirmed your email</h1>
            <a href="/new-post" class="new-post-link">
                Now you can create your own posts
            </a>
        {:else}
            <h1 class="error-h">An error has occurred during confirmation</h1>
            <div class="err-view">
                {response.errors[0].Message}
                {#if !StringUtils.isNullOrWhiteSpace(response.errors[0].Details)}
                    <p class="details">Details: {response.errors[0].Details}</p>
                {/if}
            </div>
        {/if}
    {/await}
</div>

<style>
    .view-container {
        margin: 10rem auto 0 auto;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }
    :global(.view-container > .loader-container) {
        margin-top: 0.75rem;
        --uib-size: 6rem;
    }
    h1 {
        font-size: 2.5rem;
        letter-spacing: 1px;
        font-weight: 500;
        text-align: center;
    }
    .loading-h {
        color: var(--accent-main);
    }
    .new-post-link {
        padding: 0.125rem 1rem;
        border-radius: 0.375rem;

        background-color: transparent;
        color: var(--gray);
        font-size: 1.5rem;
        transition: all 0.12s ease;
    }
    .new-post-link:hover {
        background-color: var(--back-second);
        color: var(--accent-main);
    }

    .new-post-link:active {
        transform: scale(0.98);
    }
    .error-h {
        color: var(--err-red);
    }
    .err-view {
        background-color: var(--err-red-back);
        padding: 0.75rem 1rem;
        border-radius: 0.5rem;
        font-size: 1.25rem;
        color: var(--err-red);
    }
</style>
