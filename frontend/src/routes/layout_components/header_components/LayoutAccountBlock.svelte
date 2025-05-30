<script lang="ts">
    import { onMount } from "svelte";
    import LayoutNotificationsBlock from "./LayoutNotificationsBlock.svelte";

    let { username, userId }: { username: string; userId: string } = $props<{
        username: string;
        userId: string;
    }>();

    let isAccountContextMenuOpen = $state(false);
    let buttonElement: HTMLElement;
    function handleClickOutside(event: any) {
        if (!buttonElement.contains(event.target as Node)) {
            let parent = event.target.parentElement;
            while (parent) {
                if (parent === buttonElement) return;
                parent = parent.parentElement;
            }
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
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M16.5 6.5C16.5 8.98528 14.4853 11 12 11C9.51472 11 7.5 8.98528 7.5 6.5C7.5 4.01472 9.51472 2 12 2C14.4853 2 16.5 4.01472 16.5 6.5Z"
                stroke="currentColor"
                stroke-width="2"
            />
        </svg>
    </div>
    <div
        class="context-menu unselectable"
        class:open={isAccountContextMenuOpen}
    >
        <a
            href="/users/{userId}"
            class="menu-action"
        >
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M15 10C15 8.34315 13.6569 7 12 7C10.3431 7 9 8.34315 9 10C9 11.6569 10.3431 13 12 13C13.6569 13 15 11.6569 15 10Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                ></path>
                <path
                    d="M17 18C17 15.2386 14.7614 13 12 13C9.23858 13 7 15.2386 7 18"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                ></path>
                <path
                    d="M21 13V11C21 7.22876 21 5.34315 19.8284 4.17157C18.6569 3 16.7712 3 13 3H11C7.22876 3 5.34315 3 4.17157 4.17157C3 5.34315 3 7.22876 3 11V13C3 16.7712 3 18.6569 4.17157 19.8284C5.34315 21 7.22876 21 11 21H13C16.7712 21 18.6569 21 19.8284 19.8284C21 18.6569 21 16.7712 21 13Z"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="square"
                    stroke-linejoin="round"
                ></path>
            </svg>
            My profile
        </a>
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
        height: 2.5rem;
        border: 0.125rem solid var(--back-second);
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
        display: flex;
        align-items: center;
        gap: 0.25rem;
        padding: 0.25rem 0.75rem;
        font-size: var(--header-font-size);
        font-weight: 450;
    }

    .user-profile > svg {
        height: 100%;
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
        text-decoration: none;
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
