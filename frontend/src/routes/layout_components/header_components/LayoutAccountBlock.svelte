<script lang="ts">
    import { onMount } from "svelte";
    import LayoutNotificationsBlock from "./LayoutNotificationsBlock.svelte";

    let { username } = $props<{ username: string }>();

    let isAccountContextMenuOpen = $state(false);
    let buttonElement: HTMLElement;
    function handleClickOutside(event: any) {
        if (!buttonElement.contains(event.target)) {
            isAccountContextMenuOpen = false;
        }
    }
    onMount(() => {
        document.addEventListener("click", handleClickOutside);
        return () => {
            document.removeEventListener("click", handleClickOutside);
        };
    });
</script>

<div class="layout-account-block">
    <LayoutNotificationsBlock />
    <div
        class="block-item user-profile unselectable"
        onclick={() => (isAccountContextMenuOpen = !isAccountContextMenuOpen)}
        bind:this={buttonElement}
    >
        {username}
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M6.57757 15.4816C5.1628 16.324 1.45336 18.0441 3.71266 20.1966C4.81631 21.248 6.04549 22 7.59087 22H16.4091C17.9545 22 19.1837 21.248 20.2873 20.1966C22.5466 18.0441 18.8372 16.324 17.4224 15.4816C14.1048 13.5061 9.89519 13.5061 6.57757 15.4816Z"
                stroke="currentColor"
                stroke-width="1.8"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M16.5 6.5C16.5 8.98528 14.4853 11 12 11C9.51472 11 7.5 8.98528 7.5 6.5C7.5 4.01472 9.51472 2 12 2C14.4853 2 16.5 4.01472 16.5 6.5Z"
                stroke="currentColor"
                stroke-width="1.8"
            />
        </svg>
    </div>
    <div
        class="context-menu unselectable"
        class:open={isAccountContextMenuOpen}
    >
        <div class="menu-action">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M10.5 22H6.59087C5.04549 22 3.81631 21.248 2.71266 20.1966C0.453365 18.0441 4.1628 16.324 5.57757 15.4816C8.12805 13.9629 11.2057 13.6118 14 14.4281"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
                <path
                    d="M16.5 6.5C16.5 8.98528 14.4853 11 12 11C9.51472 11 7.5 8.98528 7.5 6.5C7.5 4.01472 9.51472 2 12 2C14.4853 2 16.5 4.01472 16.5 6.5Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                />
                <path
                    d="M18.4332 13.8485C18.7685 13.4851 18.9362 13.3035 19.1143 13.1975C19.5442 12.9418 20.0736 12.9339 20.5107 13.1765C20.6918 13.2771 20.8646 13.4537 21.2103 13.8067C21.5559 14.1598 21.7287 14.3364 21.8272 14.5214C22.0647 14.9679 22.0569 15.5087 21.8066 15.9478C21.7029 16.1298 21.5251 16.3011 21.1694 16.6437L16.9378 20.7194C16.2638 21.3686 15.9268 21.6932 15.5056 21.8577C15.0845 22.0222 14.6214 22.0101 13.6954 21.9859L13.5694 21.9826C13.2875 21.9752 13.1466 21.9715 13.0646 21.8785C12.9827 21.7855 12.9939 21.6419 13.0162 21.3548L13.0284 21.1988C13.0914 20.3906 13.1228 19.9865 13.2807 19.6232C13.4385 19.2599 13.7107 18.965 14.2552 18.375L18.4332 13.8485Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linejoin="round"
                />
            </svg>
            Edit profile
        </div>
        <div class="menu-action">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M11 3L10.3374 3.23384C7.75867 4.144 6.46928 4.59908 5.73464 5.63742C5 6.67576 5 8.0431 5 10.7778V13.2222C5 15.9569 5 17.3242 5.73464 18.3626C6.46928 19.4009 7.75867 19.856 10.3374 20.7662L11 21"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                />
                <path
                    d="M21 12L11 12M21 12C21 11.2998 19.0057 9.99153 18.5 9.5M21 12C21 12.7002 19.0057 14.0085 18.5 14.5"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            </svg>
            Log out
        </div>
    </div>
</div>

<style>
    .layout-account-block {
        position: relative;
        display: inline-flex;
        justify-content: flex-end;
        align-items: center;
        gap: 0.5rem;
        height: 100%;
        margin: 0 2rem 0 auto;
        box-sizing: border-box;
    }

    :global(.layout-account-block > .block-item) {
        border: 0.125rem solid transparent;
        border-radius: 0.5rem;
        background-color: var(--back-second);
        color: var(--accent-main);
        cursor: pointer;
        box-sizing: border-box;
    }

    :global(.layout-account-block > .block-item:hover) {
        border-color: var(--accent-main);
    }

    .user-profile {
        position: relative;
        display: flex;
        align-items: center;
        gap: 0.5rem;
        padding: 0.25rem 0.75rem;
        font-size: var(--header-font-size);
        font-weight: 450;
    }

    .user-profile svg {
        height: 2rem;
    }

    .context-menu {
        position: absolute;
        top: 110%;
        z-index: 1000;
        display: none;
        gap: 0.25rem;
        width: 100%;
        padding: 0.25rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.5rem;
        background: var(--back-main);
        box-sizing: border-box;
    }

    .context-menu.open {
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 0.25rem;
    }

    .menu-action {
        display: grid;
        align-items: center;
        gap: 0.25rem;
        width: 100%;
        padding: 0.125rem 0.5rem;
        border-radius: 0.25rem;
        color: var(--text-main);
        font-size: 1rem;
        font-weight: 450;
        transition: all 0.1s ease-in;
        cursor: pointer;
        box-sizing: border-box;
        grid-template-columns: 1.5rem 1fr;
    }

    .menu-action:hover {
        gap: 0.5rem;
        background-color: var(--back-second);
        color: var(--accent-main);

    }

    .menu-action:active {
        color: var(--accent-hov);
        font-weight: 500;

    }
</style>
