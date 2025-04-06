<script lang="ts">
    import AuthView from "../components/AuthView.svelte";
    import { AuthStoreData } from "../stores/auth-store.svelte";
    import LayoutHeaderLink from "./LayoutHeaderLink.svelte";
    import LayoutProfileViewNotifications from "./LayoutProfileViewNotifications.svelte";
    import LayoutSearchBar from "./LayoutSearchBar.svelte";

    type LayoutHeaderProps = {
        openLogin: () => void;
        openSignUp: () => void;
    };
    let { openLogin, openSignUp }: LayoutHeaderProps = $props();
</script>

<div class="layout-header-container">
    <div class="header-left-part">
        <a
            class="layout-logo-container"
            data-sveltekit-preload-data="tap"
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
                    d="M13.2852 19.3647L5.82243 20.7506C4.39103 21.0164 3.67534 21.1493 3.26303 20.737C2.85072 20.3246 2.98362 19.6089 3.24943 18.1774L4.63523 10.7143C4.85745 9.51762 4.96856 8.91925 5.36302 8.5577C5.75749 8.19616 6.47889 8.1256 7.9217 7.98448C9.31227 7.84847 10.6283 7.37177 12 6L18 12.0005C16.6283 13.3723 16.1513 14.6874 16.0151 16.0781C15.8738 17.5211 15.8031 18.2426 15.4416 18.637C15.0801 19.0314 14.4818 19.1425 13.2852 19.3647Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linejoin="round"
                />
                <path
                    d="M11 15.2105C10.4405 15.1197 9.92895 14.8763 9.52632 14.4737M9.52632 14.4737C9.12368 14.0711 8.8803 13.5595 8.78947 13M9.52632 14.4737L4 20"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                />
                <path
                    d="M12 6C12.7123 4.9491 13.6771 3.1812 15.1065 3.01098C16.0822 2.89479 16.8906 3.70312 18.5072 5.31978L18.6802 5.49277C20.2969 7.10944 21.1052 7.91777 20.989 8.8935C20.8188 10.3229 19.0509 11.2877 18 12"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linejoin="round"
                />
            </svg>
        </LayoutHeaderLink>
    </div>
    <LayoutSearchBar />
    <div class="header-right-part">
        <AuthView {loading} {authenticated} {unauthenticated} />
    </div>
</div>

{#snippet loading()}
    <div class="profile-view-loader">Loading</div>
{/snippet}
{#snippet authenticated(authData: AuthStoreData)}
    <LayoutProfileViewNotifications />
    <h1>{authData.Username}</h1>
{/snippet}
{#snippet unauthenticated()}
    <div class="auth-btns-container">
        <button class="login-btn" onclick={openLogin}>Login</button>
        <button class="signup-btn" onclick={openSignUp}>Sign Up</button>
    </div>
{/snippet}

<style>
    .layout-header-container {
        display: grid;
        grid-template-columns: 1fr 1fr 1fr;
        align-items: center;
        height: 48px;
    }
    .header-left-part,
    .header-right-part {
        height: inherit;
        display: flex;
        flex-direction: row;
    }
    .layout-logo-container {
        height: 100%;
        aspect-ratio: 1/1;
        box-sizing: border-box;
        padding: 2px;
    }
    .layout-logo-container svg {
        height: 100%;
        color: var(--accent-main);
    }
</style>
