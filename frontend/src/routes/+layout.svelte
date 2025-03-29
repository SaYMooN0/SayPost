<script>

    let { children } = $props();
    import { authStoreData } from '../stores/auth-store.svelte';

</script>

<div class="page">
    <div class="layout-header-container">
        <a data-sveltekit-preload-data="tap" href="/">Logo</a>
        <a data-sveltekit-preload-data="tap" href="/my-post">My posts</a>
        <a data-sveltekit-preload-data="tap" href="/new-post">New post</a>
        <input class="browse-input"/>
        <div class="notifications-btn">Notifications</div>
        <div>Profile view</div>
        <p>User name: {authStoreData.Email}</p>

    </div>
    
{@render children()}
</div>

<style>
    .page{
        display: flex;
        flex-direction: column;
        width: 100vw;
        background-color: darksalmon;
    }
    .layout-header-container {
        display: flex;
        flex-direction: row;
        align-items: center;
        background-color: darkseagreen;
        height: 48px;
    }
</style>