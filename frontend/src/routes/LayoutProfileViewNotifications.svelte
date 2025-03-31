<script lang="ts">
    let anyUnread = false;
</script>

<svg
    xmlns:xlink="http://www.w3.org/1999/xlink"
    xmlns="http://www.w3.org/2000/svg"
    viewBox="0 0 24 24"
    fill="none"
>
    {#if anyUnread}
        <path
            d="M12 3H4C2.89543 3 2 3.89543 2 5V20C2 21.1046 2.89543 22 4 22H19C20.1046 22 21 21.1046 21 20V12"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        >
        </path>
        <path
            d="M2 14H8V14.5C8 16.433 9.567 18 11.5 18C13.433 18 15 16.433 15 14.5V14H21"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        >
        </path>
        <path
            stroke="currentColor"
            stroke-width="1.5"
            d="M22 5.5C22 7.433 20.433 9 18.5 9C16.567 9 15 7.433 15 5.5C15 3.567 16.567 2 18.5 2C20.433 2 22 3.567 22 5.5Z"
        >
        </path>
    {:else}
        <path
            d="M2.5 13H8.5V13.5C8.5 15.433 10.067 17 12 17C13.933 17 15.5 15.433 15.5 13.5V13H21.5"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        >
        </path>
        <path
            d="M4.5 2.5H19.5C20.6046 2.5 21.5 3.39543 21.5 4.5V19.5C21.5 20.6046 20.6046 21.5 19.5 21.5H4.5C3.39543 21.5 2.5 20.6046 2.5 19.5V4.5C2.5 3.39543 3.39543 2.5 4.5 2.5Z"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        >
        </path>
    {/if}
</svg>

<style>
    svg {
        height: 32px;
        width: 32px;
        color: var(--accent-main);
    }
</style>
