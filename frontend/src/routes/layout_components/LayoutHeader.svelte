<script lang="ts">
    import AuthView from "../../components/AuthView.svelte";
    import { AuthStoreData } from "../../stores/auth-store.svelte";
    import LayoutAccountBlock from "./header_components/LayoutAccountBlock.svelte";
    import LayoutHeaderLink from "./header_components/LayoutHeaderLink.svelte";
    import LayoutSearchBar from "./header_components/LayoutSearchBar.svelte";

    const { openLogin, openSignUp } = $props<{
        openLogin: () => void;
        openSignUp: () => void;
    }>();
</script>

<div class="layout-header-container">
    <div class="header-left-part">
        <a
            class="layout-logo-container"
            data-sveltekit-preload-data="hover"
            href="/"
        >
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M15.0002 17C14.2007 17.6224 13.1504 18 12.0002 18C10.8499 18 9.79971 17.6224 9.00018 17"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                />
                <path
                    d="M2.35157 13.2135C1.99855 10.9162 1.82204 9.76763 2.25635 8.74938C2.69065 7.73112 3.65421 7.03443 5.58132 5.64106L7.02117 4.6C9.41847 2.86667 10.6171 2 12.0002 2C13.3832 2 14.5819 2.86667 16.9792 4.6L18.419 5.64106C20.3462 7.03443 21.3097 7.73112 21.744 8.74938C22.1783 9.76763 22.0018 10.9162 21.6488 13.2135L21.3478 15.1724C20.8473 18.4289 20.5971 20.0572 19.4292 21.0286C18.2613 22 16.5538 22 13.139 22H10.8614C7.44652 22 5.73909 22 4.57118 21.0286C3.40327 20.0572 3.15305 18.4289 2.65261 15.1724L2.35157 13.2135Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linejoin="round"
                />
            </svg>
        </a>
        <LayoutHeaderLink href="/my-posts" text="My posts">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M16.2627 10.5H7.73725C5.15571 10.5 3.86494 10.5 3.27143 11.3526C2.67793 12.2052 3.11904 13.4258 4.00126 15.867L5.08545 18.867C5.54545 20.1398 5.77545 20.7763 6.2889 21.1381C6.80235 21.5 7.47538 21.5 8.82143 21.5H15.1786C16.5246 21.5 17.1976 21.5 17.7111 21.1381C18.2245 20.7763 18.4545 20.1398 18.9146 18.867L19.9987 15.867C20.881 13.4258 21.3221 12.2052 20.7286 11.3526C20.1351 10.5 18.8443 10.5 16.2627 10.5Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="square"
                />
                <path
                    d="M19 8C19 7.53406 19 7.30109 18.9239 7.11732C18.8224 6.87229 18.6277 6.67761 18.3827 6.57612C18.1989 6.5 17.9659 6.5 17.5 6.5H6.5C6.03406 6.5 5.80109 6.5 5.61732 6.57612C5.37229 6.67761 5.17761 6.87229 5.07612 7.11732C5 7.30109 5 7.53406 5 8"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M16.5 4C16.5 3.53406 16.5 3.30109 16.4239 3.11732C16.3224 2.87229 16.1277 2.67761 15.8827 2.57612C15.6989 2.5 15.4659 2.5 15 2.5H9C8.53406 2.5 8.30109 2.5 8.11732 2.57612C7.87229 2.67761 7.67761 2.87229 7.57612 3.11732C7.5 3.30109 7.5 3.53406 7.5 4"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            </svg>
        </LayoutHeaderLink>
        <LayoutHeaderLink href="/new-post" text="New post">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M5.07579 17C4.08939 4.54502 12.9123 1.0121 19.9734 2.22417C20.2585 6.35185 18.2389 7.89748 14.3926 8.61125C15.1353 9.38731 16.4477 10.3639 16.3061 11.5847C16.2054 12.4534 15.6154 12.8797 14.4355 13.7322C11.8497 15.6004 8.85421 16.7785 5.07579 17Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M4 22C4 15.5 7.84848 12.1818 10.5 10"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            </svg>
        </LayoutHeaderLink>
    </div>
    <LayoutSearchBar />
    <div class="header-right-part">
        <AuthView {authenticated} {unauthenticated} />
    </div>
</div>

{#snippet authenticated(authData: AuthStoreData)}
    <LayoutAccountBlock username={authData.Username ?? ""} />
{/snippet}
{#snippet unauthenticated()}
    <div class="auth-btns-container">
        <button class="login-btn" onclick={openLogin}>Login</button>
        <button class="signup-btn" onclick={openSignUp}>Sign Up</button>
    </div>
{/snippet}

<style>
    :global(*){
        --layout-header-height: 3rem;
    }

    .layout-header-container {
        --header-font-size: 1.4rem;

        display: grid;
        align-items: center;
        height: var(--layout-header-height);
        grid-template-columns: 1fr 30rem 1fr;
        box-sizing: border-box;
    }

    .header-left-part,
    .header-right-part {
        display: flex;
        flex-direction: row;
        align-items: center;
        height: inherit;
    }

    .header-left-part {
        display: flex;
        flex-direction: row;
        gap: 0.75rem;
    }

    .layout-logo-container {
        justify-self: center;
        height: 2.5rem;
        padding: 0 1rem;
        box-sizing: border-box;
        aspect-ratio: 1/1;
    }

    .layout-logo-container svg {
        height: 100%;
        color: var(--accent-main);
    }

    .auth-btns-container {
        display: grid;
        align-items: center;
        gap: 1rem;
        height: 2.5rem;
        margin: 0 2rem 0 auto;
        grid-template-columns: 1fr 1fr;
        box-sizing: border-box;
    }

    .auth-btns-container > button {
        height: 2.5rem;
        padding: 0 1.5rem;
        border: 0.25rem solid var(--accent-main);
        border-radius: 2rem;
        font-size: var(--header-font-size);
        font-weight: 550;
        transition: all 0.06s ease-in;
        cursor: pointer;
        outline: none;
    }

    .auth-btns-container > button:hover {
        border-color: var(--accent-hov);
        transform: scale(1.04);
    }

    .auth-btns-container > button:focus {
        transform: scale(1.04);
    }

    .auth-btns-container > button:active {
        transform: scale(0.98);
    }

    .login-btn {
        background-color: var(--back-main);
        color: var(--accent-main);
    }

    .login-btn:hover {
        color: var(--accent-hov);
    }

    .signup-btn {
        background-color: var(--accent-main);
        color: var(--back-main);
    }

    .signup-btn:hover {
        background-color: var(--accent-hov);
    }
</style>
