<script lang="ts">
    import ListIsEmptyComponent from "./ListIsEmptyComponent.svelte";

    const { users }: { users: { id: string; username: string | null }[] } =
        $props<{
            users: { id: string; username: string | null }[];
        }>();
    let displayList = $state(true);
</script>

{#if users.length === 0}
    <ListIsEmptyComponent />
{:else}
    <div class="change-view">
        <svg
            xmlns="http://www.w3.org/2000/svg"
            onclick={() => {
                displayList = true;
            }}
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M2 11.4C2 10.2417 2.24173 10 3.4 10H20.6C21.7583 10 22 10.2417 22 11.4V12.6C22 13.7583 21.7583 14 20.6 14H3.4C2.24173 14 2 13.7583 2 12.6V11.4Z"
                stroke="currentColor"
                stroke-width="1.7"
                stroke-linecap="round"
            />
            <path
                d="M2 3.4C2 2.24173 2.24173 2 3.4 2H20.6C21.7583 2 22 2.24173 22 3.4V4.6C22 5.75827 21.7583 6 20.6 6H3.4C2.24173 6 2 5.75827 2 4.6V3.4Z"
                stroke="currentColor"
                stroke-width="1.7"
                stroke-linecap="round"
            />
            <path
                d="M2 19.4C2 18.2417 2.24173 18 3.4 18H20.6C21.7583 18 22 18.2417 22 19.4V20.6C22 21.7583 21.7583 22 20.6 22H3.4C2.24173 22 2 21.7583 2 20.6V19.4Z"
                stroke="currentColor"
                stroke-width="1.7"
                stroke-linecap="round"
            />
        </svg>
        <svg
            xmlns="http://www.w3.org/2000/svg"
            onclick={() => (displayList = false)}
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M3.88884 9.66294C4.39329 10 5.09552 10 6.49998 10C7.90445 10 8.60668 10 9.11113 9.66294C9.32951 9.51702 9.51701 9.32952 9.66292 9.11114C9.99998 8.60669 9.99998 7.90446 9.99998 6.5C9.99998 5.09554 9.99998 4.39331 9.66292 3.88886C9.51701 3.67048 9.32951 3.48298 9.11113 3.33706C8.60668 3 7.90445 3 6.49998 3C5.09552 3 4.39329 3 3.88884 3.33706C3.67046 3.48298 3.48296 3.67048 3.33705 3.88886C2.99998 4.39331 2.99998 5.09554 2.99998 6.5C2.99998 7.90446 2.99998 8.60669 3.33705 9.11114C3.48296 9.32952 3.67046 9.51702 3.88884 9.66294Z"
                stroke="currentColor"
                stroke-width="1.7"
                stroke-linejoin="round"
            ></path>
            <path
                d="M14.8888 9.66294C15.3933 10 16.0955 10 17.5 10C18.9044 10 19.6067 10 20.1111 9.66294C20.3295 9.51702 20.517 9.32952 20.6629 9.11114C21 8.60669 21 7.90446 21 6.5C21 5.09554 21 4.39331 20.6629 3.88886C20.517 3.67048 20.3295 3.48298 20.1111 3.33706C19.6067 3 18.9044 3 17.5 3C16.0955 3 15.3933 3 14.8888 3.33706C14.6705 3.48298 14.483 3.67048 14.337 3.88886C14 4.39331 14 5.09554 14 6.5C14 7.90446 14 8.60669 14.337 9.11114C14.483 9.32952 14.6705 9.51702 14.8888 9.66294Z"
                stroke="currentColor"
                stroke-width="1.7"
                stroke-linejoin="round"
            ></path>
            <path
                d="M3.88884 20.6629C4.39329 21 5.09552 21 6.49998 21C7.90445 21 8.60668 21 9.11113 20.6629C9.32951 20.517 9.51701 20.3295 9.66292 20.1111C9.99998 19.6067 9.99998 18.9045 9.99998 17.5C9.99998 16.0955 9.99998 15.3933 9.66292 14.8889C9.51701 14.6705 9.32951 14.483 9.11113 14.3371C8.60668 14 7.90445 14 6.49998 14C5.09552 14 4.39329 14 3.88884 14.3371C3.67046 14.483 3.48296 14.6705 3.33705 14.8889C2.99998 15.3933 2.99998 16.0955 2.99998 17.5C2.99998 18.9045 2.99998 19.6067 3.33705 20.1111C3.48296 20.3295 3.67046 20.517 3.88884 20.6629Z"
                stroke="currentColor"
                stroke-width="1.7"
                stroke-linejoin="round"
            ></path>
            <path
                d="M14.8888 20.6629C15.3933 21 16.0955 21 17.5 21C18.9044 21 19.6067 21 20.1111 20.6629C20.3295 20.517 20.517 20.3295 20.6629 20.1111C21 19.6067 21 18.9045 21 17.5C21 16.0955 21 15.3933 20.6629 14.8889C20.517 14.6705 20.3295 14.483 20.1111 14.3371C19.6067 14 18.9044 14 17.5 14C16.0955 14 15.3933 14 14.8888 14.3371C14.6705 14.483 14.483 14.6705 14.337 14.8889C14 15.3933 14 16.0955 14 17.5C14 18.9045 14 19.6067 14.337 20.1111C14.483 20.3295 14.6705 20.517 14.8888 20.6629Z"
                stroke="currentColor"
                stroke-width="1.7"
                stroke-linejoin="round"
            ></path>
        </svg>
    </div>
    <div class="container {displayList ? 'list' : 'grid'}">
        {#each users as u}
            <div
                class="list-item {u.username
                    ? ''
                    : 'unable-to-load'} {displayList
                    ? 'list-view'
                    : 'grid-view'}"
            >
                {#if u.username}
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 24 24"
                        fill="none"
                    >
                        <path
                            d="M16 7C16 9.20914 14.2091 11 12 11C9.79086 11 8 9.20914 8 7C8 4.79086 9.79086 3 12 3C14.2091 3 16 4.79086 16 7Z"
                            stroke="currentColor"
                            stroke-width="1.5"
                        ></path>
                        <path
                            d="M14 14H10C7.23858 14 5 16.2386 5 19C5 20.1046 5.89543 21 7 21H17C18.1046 21 19 20.1046 19 19C19 16.2386 16.7614 14 14 14Z"
                            stroke="currentColor"
                            stroke-width="1.5"
                            stroke-linejoin="round"
                        ></path>
                    </svg>
                    <label>
                        {u.username}
                    </label>
                {:else}
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 24 24"
                        fill="none"
                    >
                        <path
                            d="M14.9994 15L9 9M9.00064 15L15 9"
                            stroke="currentColor"
                            stroke-width="2"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        />
                        <path
                            d="M22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22C17.5228 22 22 17.5228 22 12Z"
                            stroke="currentColor"
                            stroke-width="2"
                        />
                    </svg>
                    <label>Unable to load username</label>
                {/if}
                <a
                    data-sveltekit-preload-data="hover"
                    href="/users/{u.id}"
                    class="visit-btn"
                >
                    Visit profile
                </a>
            </div>
        {/each}
    </div>
{/if}

<style>
    .change-view {
        display: grid;
        gap: 0.25rem;
        width: fit-content;
        height: 2rem;
        padding: 0.125rem;
        margin-left: auto;
        grid-template-columns: 1fr 1fr;
        box-sizing: border-box;
    }

    .change-view svg {
        height: 100%;
        padding: 0.125rem;
        border-radius: 0.25rem;
        background-color: var(--back-second);
        transition: all 0.08s ease-in;
        cursor: pointer;
        aspect-ratio: 1/1;
        box-sizing: border-box;
    }

    .change-view svg:hover {
        color: var(--accent-main);
        transform: scale(1.06);
    }

    .container {
        display: flex;
        align-items: center;
        gap: 0.75rem;
        margin-top: 1rem;
    }

    .container.grid {
        flex-wrap: wrap;
    }

    .container.list {
        flex-direction: column;
    }

    .list-item {
        align-items: center;
        box-shadow:
            rgb(60 64 67 / 10%) 0 1px 2px 0,
            rgb(47 59 67 / 20%) 0 1px 2px 1px;
        box-sizing: border-box;
        interpolate-size: allow-keywords;
    }

    .list-item.grid-view {
        display: flex;
        flex-direction: column;
        max-width: 25%;
        padding: 0.5rem 1rem;
        border-radius: 0.5rem;
        flex: 1 1 auto;
    }

    .list-item.list-view {
        display: grid;
        grid-template-columns: 3rem 1fr auto;
        width: 100%;
        padding: 0.375rem 2rem;
        border-radius: 0.25rem;
        transition: width 0.1s ease-in;
    }

    .list-item > svg {
        width: 3rem;
        color: var(--accent-main);
    }

    .list-item.grid-view > svg {
        width: 2rem;
    }

    .list-item > label {
        margin: 0.125rem 0;
        color: var(--text-main);
        font-size: 1.125rem;
        font-weight: 450;
    }

    .unable-to-load > svg {
        color: var(--gray);
        width: 1.5rem;
        justify-self: center;
    }

    .unable-to-load > label {
        color: var(--gray);
    }

    .visit-btn {
        display: flex;
        align-items: center;
        height: 1.5rem;
        padding: 0.125rem 0.5rem;
        border-radius: 0.25rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 0.875rem;
        text-decoration: none;
        transition: all 0.08s ease-in;
    }

    .visit-btn:hover {
        background-color: var(--accent-hov);
    }

    .list-item.list-view > .visit-btn {
        font-size: 1rem;
    }
</style>
