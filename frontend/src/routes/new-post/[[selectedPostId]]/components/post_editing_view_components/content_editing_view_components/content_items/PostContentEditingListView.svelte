<script lang="ts">
    import type {
        ListContentItem,
        PostContentItem,
    } from "../../../../../../../ts/common/post-content-item";

    let {
        isEditing,
        initial,
        editingValue = $bindable<ListContentItem>(),
    }: {
        isEditing: boolean;
        initial: ListContentItem;
        editingValue: ListContentItem;
    } = $props<{
        isEditing: boolean;
        initial: ListContentItem;
        editingValue: PostContentItem;
    }>();

    function addItem() {
        editingValue.items.push("");
    }

    function removeItem(index: number) {
        editingValue.items.splice(index, 1);
    }
</script>

<div class="list">
    {#if isEditing}
        {#if editingValue.items.length > 0}
            {#each editingValue.items as item, i}
                <div class="list-editing-item">
                    <span class="bullet"></span>
                    <input
                        bind:value={editingValue.items[i]}
                        placeholder={`Item #${i + 1}`}
                    />
                    <svg
                        class="remove-btn"
                        onclick={() => removeItem(i)}
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 24 24"
                        fill="none"
                    >
                        <path
                            d="M6.70914 7.78222C7.76637 6.59403 8.29499 5.99994 9 5.99994C9.70501 5.99994 10.2336 6.59403 11.2909 7.78222L13.891 10.7044C15.297 12.2846 16 13.0747 16 13.9999C16 14.9252 15.297 15.7153 13.891 17.2955L11.2909 20.2177C10.2336 21.4058 9.70501 21.9999 9 21.9999C8.29499 21.9999 7.76637 21.4058 6.70914 20.2177L4.10902 17.2955C2.70301 15.7153 2 14.9252 2 13.9999C2 13.0747 2.70301 12.2846 4.10902 10.7044L6.70914 7.78222Z"
                            stroke="currentColor"
                            stroke-width="2"
                        ></path>
                        <path
                            d="M15 4.99994H22"
                            stroke="currentColor"
                            stroke-width="2"
                            stroke-linecap="round"
                        ></path>
                    </svg>
                </div>
            {/each}
        {:else}
            <label class="empty-list-label">Empty list</label>
        {/if}
        <button
            class="add-btn unselectable"
            class:list-empty={editingValue.items.length === 0}
            onclick={addItem}
        >
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M6.70914 7.78228C7.76637 6.59409 8.29499 6 9 6C9.70501 6 10.2336 6.59409 11.2909 7.78228L13.891 10.7045C15.297 12.2847 16 13.0747 16 14C16 14.9253 15.297 15.7153 13.891 17.2955L11.2909 20.2177C10.2336 21.4059 9.70501 22 9 22C8.29499 22 7.76637 21.4059 6.70914 20.2177L4.10902 17.2955C2.70301 15.7153 2 14.9253 2 14C2 13.0747 2.70301 12.2847 4.10902 10.7045L6.70914 7.78228Z"
                    stroke="currentColor"
                    stroke-width="2"
                ></path>
                <path
                    d="M18.5 9L18.5 2M15 5.5H22"
                    stroke="currentColor"
                    stroke-width="2"
                    stroke-linecap="round"
                ></path>
            </svg>
            Add list item
        </button>
    {:else}
        {#each initial.items as item}
            <div class="list-display-item">
                <span class="bullet"></span>
                {item}
            </div>
        {/each}
    {/if}
</div>

<style>
    .list {
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
        font-size: var(--post-content-paragraph-font-size);
        font-weight: var(--post-content-paragraph-font-weight);
    }

    .list-display-item,
    .list-editing-item {
        display: grid;
        grid-template-columns: auto 1fr auto;
        gap: 0.5rem;
        overflow-wrap: anywhere;
    }

    .bullet {
        display: block;
        width: 0.375rem;
        height: 0.375rem;
        margin-top: calc((1em - 0.25rem) / 2);
        margin-left: 0.25rem;
        border-radius: 1rem;
        background-color: var(--accent-main);
        align-self: flex-start;
        user-select: none;
    }

    input {
        padding: 0 0.5rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.25rem;
        background-color: var(--back-main);
        color: var(--text-main);
        font-size: inherit;
        font-weight: inherit;
        flex-grow: 1;
        outline: none;
    }

    input:hover {
        border-color: var(--gray);
    }

    input:focus {
        border-color: var(--accent-main);
        background-color: transparent;
    }

    .remove-btn {
        width: 1.75rem;
        height: 1.75rem;
        padding: 0.25rem 0;
        border-radius: 0.25rem;
        background-color: var(--gray);
        color: inherit;
        color: var(--back-main);
        transition: all 0.08s ease-in;
        cursor: pointer;
        box-sizing: border-box;
    }

    .remove-btn:active {
        background-color: var(--accent-hov);
        transform: scale(0.94);
    }

    .remove-btn:hover {
        background-color: var(--accent-main);
        color: var(--back-main);
    }

    .empty-list-label {
        color: var(--gray);
        align-self: center;
        font-size: 1rem;
    }

    .add-btn {
        display: flex;
        align-items: center;
        gap: 0.25rem;
        margin-right: 0;
        margin-left: auto;
        border: none;
        background-color: transparent;
        color: var(--accent-main);
        font-size: 1.25rem;
        font-weight: var(--post-content-paragraph-font-weight);
        transition:
            transform 0.08s ease-in,
            border-bottom-color 0.02s ease-in,
            margin 0.82s ease-in;
        cursor: pointer;
        border-bottom: 0.125rem solid transparent;
    }

    .add-btn > svg {
        height: 1.5rem;
        box-sizing: border-box;
        color: inherit;
    }

    .add-btn:hover {
        border-bottom-color: var(--accent-main);
        transform: scale(1.06);
    }

    .add-btn:active {
        color: var(--accent-hov);
        transform: scale(1.02);
    }

    .add-btn.list-empty {
        margin-right: auto;
    }
</style>
