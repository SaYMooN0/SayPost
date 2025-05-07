<script lang="ts">
    let {
        isEditing,
        isNewlyAdded,
        startEditing,
        cancelEditing,
        deleteItem,
    }: {
        isEditing: boolean;
        isNewlyAdded: boolean;
        startEditing: () => void;
        cancelEditing: () => void;
        deleteItem: () => void;
    } = $props<{
        isEditing: boolean;
        isNewlyAdded: boolean;
        startEditing: () => void;
        cancelEditing: () => void;
        deleteItem: () => void;
    }>();
</script>

<div class="btns-container">
    {#if !isNewlyAdded && isEditing}
        <svg
            onclick={cancelEditing}
            class="cancel"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M18 6L12 12M12 12L6 18M12 12L18 18M12 12L6 6"
                stroke="currentColor"
                stroke-width="1.9"
                stroke-linecap="round"
                stroke-linejoin="round"
            ></path>
        </svg>
    {:else if !isNewlyAdded}
        <svg
            onclick={startEditing}
            class="edit"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M8.17151 19.8284L19.8284 8.17157C20.3736 7.62632 20.6462 7.3537 20.792 7.0596C21.0693 6.50005 21.0693 5.8431 20.792 5.28354C20.6462 4.98945 20.3736 4.71682 19.8284 4.17157C19.2831 3.62632 19.0105 3.3537 18.7164 3.20796C18.1568 2.93068 17.4999 2.93068 16.9403 3.20796C16.6462 3.3537 16.3736 3.62632 15.8284 4.17157L4.17151 15.8284C3.59345 16.4064 3.30442 16.6955 3.15218 17.063C2.99994 17.4305 2.99994 17.8393 2.99994 18.6568V20.9999H5.34308C6.16059 20.9999 6.56934 20.9999 6.93688 20.8477C7.30442 20.6955 7.59345 20.4064 8.17151 19.8284Z"
                stroke="currentColor"
                stroke-width="1.9"
                stroke-linecap="round"
                stroke-linejoin="round"
            ></path>
            <path
                d="M12 21H18"
                stroke="currentColor"
                stroke-width="1.9"
                stroke-linecap="round"
                stroke-linejoin="round"
            ></path>
            <path
                d="M14.5 5.5L18.5 9.5"
                stroke="currentColor"
                stroke-width="1.9"
                stroke-linecap="round"
                stroke-linejoin="round"
            ></path>
        </svg>
    {/if}
    <svg
        onclick={deleteItem}
        class="delete"
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        fill="none"
    >
        <path
            d="M19.5 5.5L18.8803 15.5251C18.7219 18.0864 18.6428 19.3671 18.0008 20.2879C17.6833 20.7431 17.2747 21.1273 16.8007 21.416C15.8421 22 14.559 22 11.9927 22C9.42312 22 8.1383 22 7.17905 21.4149C6.7048 21.1257 6.296 20.7408 5.97868 20.2848C5.33688 19.3626 5.25945 18.0801 5.10461 15.5152L4.5 5.5"
            stroke="currentColor"
            stroke-width="1.9"
            stroke-linecap="round"
        ></path>
        <path
            d="M3 5.5H21M16.0557 5.5L15.3731 4.09173C14.9196 3.15626 14.6928 2.68852 14.3017 2.39681C14.215 2.3321 14.1231 2.27454 14.027 2.2247C13.5939 2 13.0741 2 12.0345 2C10.9688 2 10.436 2 9.99568 2.23412C9.8981 2.28601 9.80498 2.3459 9.71729 2.41317C9.32164 2.7167 9.10063 3.20155 8.65861 4.17126L8.05292 5.5"
            stroke="currentColor"
            stroke-width="1.9"
            stroke-linecap="round"
        ></path>
        <path
            d="M9.5 16.5L9.5 10.5"
            stroke="currentColor"
            stroke-width="1.9"
            stroke-linecap="round"
        ></path>
        <path
            d="M14.5 16.5L14.5 10.5"
            stroke="currentColor"
            stroke-width="1.9"
            stroke-linecap="round"
        ></path>
    </svg>
</div>

<style>
    .btns-container {
        display: flex;
        flex-direction: column;
        gap: 0.375rem;
    }

    .btns-container svg {
        height: 1.75rem;
        padding: 0.25rem;
        box-sizing: border-box;
        border-radius: 0.25rem;
        cursor: pointer;
    }

    .edit {
        background-color: var(--accent-main);
        color: var(--back-main);
    }

    .btns-container svg:hover {
        transform: scale(1.06);
    }

    .btns-container svg:hover path {
        stroke-width: 2.1;
    }

    .cancel {
        background-color: var(--back-second);
        color: var(--gray);
    }

    .delete {
        background-color: var(--gray);
        color: var(--back-main);
    }
</style>
