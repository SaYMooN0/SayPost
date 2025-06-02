<script lang="ts">
    import type { Action } from "svelte/action";
    import { DateUtils } from "../../../../ts/common/utils/date-utils";
    import type { NotificationItem } from "../notifications";
    import NotificationContentDisplay from "./NotificationContentDisplay.svelte";
    import { ApiNotifications } from "../../../../ts/backend-services";

    let { notifications = $bindable() }: { notifications: NotificationItem[] } =
        $props<{
            notifications: NotificationItem[];
        }>();

    let markAsViewedNotificationsBuffer = new Set<NotificationItem>();
    let timeout: number | null = null;
    const observeWhenVisible: Action<HTMLElement, () => void> = (
        node,
        callback,
    ) => {
        if (typeof callback !== "function") return;

        const observer = new IntersectionObserver(
            ([entry]) => {
                if (entry.isIntersecting) {
                    callback();
                }
            },
            { threshold: 0.9 },
        );

        observer.observe(node);

        return {
            destroy() {
                observer.disconnect();
            },
        };
    };
    function markAsViewedBatched(notification: NotificationItem) {
        if (
            notification.viewed ||
            markAsViewedNotificationsBuffer.has(notification)
        ) {
            return;
        }

        markAsViewedNotificationsBuffer.add(notification);

        if (!timeout) {
            timeout = setTimeout(async () => {
                const toUpdate = Array.from(markAsViewedNotificationsBuffer);
                const idsToUpdate = toUpdate.map((n) => n.id);
                const response = await ApiNotifications.fetchVoidResponse(
                    "/notifications/view",
                    ApiNotifications.requestJsonOptions(
                        {
                            notificationIds: idsToUpdate,
                        },
                        "PATCH",
                    ),
                );
                if (response.isSuccess) {
                    toUpdate.forEach((n) => (n.viewed = true));
                }

                markAsViewedNotificationsBuffer.clear();
                timeout = null;
            }, 800);
        }
    }
</script>

{#if notifications.length === 0}
    <p class="no-notifications">No notifications</p>
{:else}
    <div class="notifications-list">
        {#each notifications as notification}
            <div class="notification-item">
                <NotificationContentDisplay
                    notification={{
                        ...notification.typeSpecificData,
                        type: notification.type,
                    }}
                />
                <div class="bottom-part">
                    <div
                        class="not-viewed-dot"
                        use:observeWhenVisible={() =>
                            markAsViewedBatched(notification)}
                        class:visible={!notification.viewed}
                    />
                    <label class="date">
                        {DateUtils.toLocale(notification.dateTime)}
                    </label>
                </div>
            </div>
        {/each}
    </div>
{/if}

<style>
    .no-notifications {
        text-align: center;
    }

    .notifications-list {
        display: flex;
        flex-direction: column;
        gap: 0.25rem;
        width: 100%;
        max-height: 16rem;
        overflow-y: auto;
    }

    .notification-item {
        display: flex;
        flex-direction: column;
        gap: 0.25rem;
        width: 100%;
        box-sizing: border-box;
    }

    .bottom-part {
        display: flex;
        align-items: center;
        gap: 0.25rem;
    }

    .not-viewed-dot {
        width: 0.375rem;
        height: 0.375rem;
        margin-left: auto;
        border-radius: 50%;
    }

    .not-viewed-dot.visible {
        background-color: var(--accent-main);
    }

    .bottom-part > .date {
        color: var(--gray);
        font-size: 0.75rem;
        align-self: flex-end;
    }
</style>
