<script lang="ts">
    import { DateUtils } from "../../../../ts/common/utils/date-utils";
    import type { NotificationItem } from "../notifications";

    let { notifications = $bindable() }: { notifications: NotificationItem[] } =
        $props<{
            notifications: NotificationItem[];
        }>();
</script>

{#if notifications.length === 0}
    <p class="no-notifications">No notifications</p>
{:else}
    <div class="notifications-list">
        {#each notifications as notification}
            <div class="notification-item">
                <label class="date">
                    {DateUtils.toLocale(notification.dateTime)}
                </label>
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
        width: 100%;
    }

    .notification-item {
        width: 100%;
        background-color: aliceblue;
    }
</style>
