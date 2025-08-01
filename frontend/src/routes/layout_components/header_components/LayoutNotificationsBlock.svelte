<script lang="ts">
    import { goto } from "$app/navigation";
    import { onMount } from "svelte";
    import { ApiNotifications } from "../../../ts/backend-services";
    import type { NotificationItem } from "./notifications";
    import type { Err } from "../../../ts/common/errs/err";
    import NotificationItemsList from "./notifications_block_components/NotificationItemsList.svelte";
    import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";

    let anyUnread = $state(false);
    let notificationsListOpen = $state(false);
    let fetchingErrs: Err[] = $state([]);
    let notifications: NotificationItem[] = $state([]);
    let buttonElement: SVGElement;
    function handleClickOutside(event: any) {
        if (!buttonElement.contains(event.target)) {
            notificationsListOpen = false;
        }
    }
    async function fetchNotifications() {
        const res = await ApiNotifications.fetchJsonResponse<{
            notifications: NotificationItem[];
        }>("/notifications", { method: "GET" });
        if (res.isSuccess) {
            notifications = res.data.notifications;
            anyUnread = notifications.some((n) => !n.viewed);
            fetchingErrs = [];
        } else {
            fetchingErrs = res.errors;
            notifications = [];
        }
    }
    function openButtonClicked() {
        if (!notificationsListOpen) {
            fetchNotifications();
            notificationsListOpen = true;
        }
    }
    onMount(() => {
        document.addEventListener("click", handleClickOutside);
        fetchNotifications();

        return () => {
            document.removeEventListener("click", handleClickOutside);
        };
    });
</script>

<svg
    class="block-item unselectable"
    onclick={() => openButtonClicked()}
    bind:this={buttonElement}
    xmlns:xlink="http://www.w3.org/1999/xlink"
    xmlns="http://www.w3.org/2000/svg"
    viewBox="0 0 24 24"
    fill="none"
>
    {#if anyUnread}
        <path
            d="M12 3H4C2.89543 3 2 3.89543 2 5V20C2 21.1046 2.89543 22 4 22H19C20.1046 22 21 21.1046 21 20V12"
            stroke="currentColor"
            stroke-width="1.8"
            stroke-linecap="round"
            stroke-linejoin="round"
        >
        </path>
        <path
            d="M2 14H8V14.5C8 16.433 9.567 18 11.5 18C13.433 18 15 16.433 15 14.5V14H21"
            stroke="currentColor"
            stroke-width="1.8"
            stroke-linecap="round"
            stroke-linejoin="round"
        >
        </path>
        <path
            stroke="currentColor"
            stroke-width="1.8"
            d="M22 5.5C22 7.433 20.433 9 18.5 9C16.567 9 15 7.433 15 5.5C15 3.567 16.567 2 18.5 2C20.433 2 22 3.567 22 5.5Z"
        >
        </path>
    {:else}
        <path
            d="M2.5 13H8.5V13.5C8.5 15.433 10.067 17 12 17C13.933 17 15.5 15.433 15.5 13.5V13H21.5"
            stroke="currentColor"
            stroke-width="1.8"
            stroke-linecap="round"
            stroke-linejoin="round"
        >
        </path>
        <path
            d="M4.5 2.5H19.5C20.6046 2.5 21.5 3.39543 21.5 4.5V19.5C21.5 20.6046 20.6046 21.5 19.5 21.5H4.5C3.39543 21.5 2.5 20.6046 2.5 19.5V4.5C2.5 3.39543 3.39543 2.5 4.5 2.5Z"
            stroke="currentColor"
            stroke-width="1.8"
            stroke-linecap="round"
            stroke-linejoin="round"
        >
        </path>
    {/if}
</svg>

<div class="context-menu unselectable" class:open={notificationsListOpen}>
    {#if fetchingErrs.length > 0}
        <DefaultErrBlock errList={fetchingErrs} />
    {:else}
        <NotificationItemsList {notifications} />
    {/if}
    <div class="settings-link" onclick={() => goto("/notifications-settings")}>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M21.3175 7.14139L20.8239 6.28479C20.4506 5.63696 20.264 5.31305 19.9464 5.18388C19.6288 5.05472 19.2696 5.15664 18.5513 5.36048L17.3311 5.70418C16.8725 5.80994 16.3913 5.74994 15.9726 5.53479L15.6357 5.34042C15.2766 5.11043 15.0004 4.77133 14.8475 4.37274L14.5136 3.37536C14.294 2.71534 14.1842 2.38533 13.9228 2.19657C13.6615 2.00781 13.3143 2.00781 12.6199 2.00781H11.5051C10.8108 2.00781 10.4636 2.00781 10.2022 2.19657C9.94085 2.38533 9.83106 2.71534 9.61149 3.37536L9.27753 4.37274C9.12465 4.77133 8.84845 5.11043 8.48937 5.34042L8.15249 5.53479C7.73374 5.74994 7.25259 5.80994 6.79398 5.70418L5.57375 5.36048C4.85541 5.15664 4.49625 5.05472 4.17867 5.18388C3.86109 5.31305 3.67445 5.63696 3.30115 6.28479L2.80757 7.14139C2.45766 7.74864 2.2827 8.05227 2.31666 8.37549C2.35061 8.69871 2.58483 8.95918 3.05326 9.48012L4.0843 10.6328C4.3363 10.9518 4.51521 11.5078 4.51521 12.0077C4.51521 12.5078 4.33636 13.0636 4.08433 13.3827L3.05326 14.5354C2.58483 15.0564 2.35062 15.3168 2.31666 15.6401C2.2827 15.9633 2.45766 16.2669 2.80757 16.8741L3.30114 17.7307C3.67443 18.3785 3.86109 18.7025 4.17867 18.8316C4.49625 18.9608 4.85542 18.8589 5.57377 18.655L6.79394 18.3113C7.25263 18.2055 7.73387 18.2656 8.15267 18.4808L8.4895 18.6752C8.84851 18.9052 9.12464 19.2442 9.2775 19.6428L9.61149 20.6403C9.83106 21.3003 9.94085 21.6303 10.2022 21.8191C10.4636 22.0078 10.8108 22.0078 11.5051 22.0078H12.6199C13.3143 22.0078 13.6615 22.0078 13.9228 21.8191C14.1842 21.6303 14.294 21.3003 14.5136 20.6403L14.8476 19.6428C15.0004 19.2442 15.2765 18.9052 15.6356 18.6752L15.9724 18.4808C16.3912 18.2656 16.8724 18.2055 17.3311 18.3113L18.5513 18.655C19.2696 18.8589 19.6288 18.9608 19.9464 18.8316C20.264 18.7025 20.4506 18.3785 20.8239 17.7307L21.3175 16.8741C21.6674 16.2669 21.8423 15.9633 21.8084 15.6401C21.7744 15.3168 21.5402 15.0564 21.0718 14.5354L20.0407 13.3827C19.7887 13.0636 19.6098 12.5078 19.6098 12.0077C19.6098 11.5078 19.7888 10.9518 20.0407 10.6328L21.0718 9.48012C21.5402 8.95918 21.7744 8.69871 21.8084 8.37549C21.8423 8.05227 21.6674 7.74864 21.3175 7.14139Z"
                stroke="currentColor"
                stroke-width="1.9"
                stroke-linecap="round"
            ></path>
            <path
                d="M15.5195 12C15.5195 13.933 13.9525 15.5 12.0195 15.5C10.0865 15.5 8.51953 13.933 8.51953 12C8.51953 10.067 10.0865 8.5 12.0195 8.5C13.9525 8.5 15.5195 10.067 15.5195 12Z"
                stroke="currentColor"
                stroke-width="1.9"
            ></path>
        </svg>
        Notification settings
    </div>
</div>

<style>
    .block-item {
        padding: 0.25rem;
        box-sizing: border-box;
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

    .settings-link {
        display: grid;
        align-items: center;
        gap: 0.125rem;
        width: 100%;
        padding: 0.125rem 0.5rem;
        border-radius: 0.25rem;
        color: var(--accent-main);
        font-size: 1rem;
        font-weight: 450;
        transition: all 0.1s ease-in;
        cursor: pointer;
        box-sizing: border-box;
        grid-template-columns: auto 1fr;
    }

    .settings-link:hover {
        background-color: var(--back-second);
    }

    .settings-link:active {
        color: var(--accent-hov);
        font-weight: 500;
    }

    .settings-link > svg {
        height: 1.625rem;
        padding: 0.125rem;
        box-sizing: border-box;
    }
</style>
