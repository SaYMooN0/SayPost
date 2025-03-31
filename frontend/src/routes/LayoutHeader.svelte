<script lang="ts">
    import AuthView from "../components/AuthView.svelte";
    import { AuthStoreData } from "../stores/auth-store.svelte";
    import LayoutHeaderLink from "./LayoutHeaderLink.svelte";
</script>

<div class="layout-header-container">
    <a data-sveltekit-preload-data="tap" href="/">Logo</a>
    <LayoutHeaderLink href="/my-posts" text="My posts">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M12 4V20"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M4 12H20"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    </LayoutHeaderLink>
    <LayoutHeaderLink href="/new-post" text="New post">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M12 4V20"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M4 12H20"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    </LayoutHeaderLink>
    <input class="browse-input" />
    <div class="notifications-btn">Notifications</div>
    <AuthView {loading} {authenticated} {unauthenticated} />
</div>

{#snippet loading()}
    <div class="profile-view-loader">Loading</div>
{/snippet}
{#snippet authenticated(authData: AuthStoreData)}
    
<h1>{authData.Username}</h1>
{/snippet}
{#snippet unauthenticated()}
    <h1>Unauth</h1>
{/snippet}

<style>
    .layout-header-container {
        display: flex;
        flex-direction: row;
        align-items: center;
        background-color: darkseagreen;
        height: 48px;
    }
</style>
