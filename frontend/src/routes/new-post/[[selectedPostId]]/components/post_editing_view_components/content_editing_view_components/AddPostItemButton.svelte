<script lang="ts">
    import { onMount } from "svelte";
    import type { PostContentItem } from "../../../../../../ts/common/post-content-item";

    let {
        addNewContentItem,
    }: { addNewContentItem: (item: PostContentItem) => void } = $props<{
        addNewContentItem: (item: PostContentItem) => void;
    }>();

    let isMenuOpen = $state(false);
    let buttonElement: HTMLElement;

    function handleClickOutside(event: MouseEvent) {
        if (!buttonElement.contains(event.target as Node)) {
            isMenuOpen = false;
        }
    }

    onMount(() => {
        document.addEventListener("click", handleClickOutside);
        return () => {
            document.removeEventListener("click", handleClickOutside);
        };
    });

    function addParagraph() {
        addNewContentItem({
            $type: "ParagraphContentItem",
            value: "new paragraph",
        });
    }
    function addHeading() {
        addNewContentItem({
            $type: "HeadingContentItem",
            value: "new heading",
        });
    }
    function addSubheading() {
        addNewContentItem({
            $type: "SubheadingContentItem",
            value: "new subheading",
        });
    }
    function addList() {
        addNewContentItem({
            $type: "ListContentItem",
            items: ["item 1", "item 2"],
        });
    }
    function addQuote() {
        addNewContentItem({
            $type: "QuoteContentItem",
            text: "new quote",
            author: null,
        });
    }
</script>

<div class="add-content-wrapper unselectable">
    <button
        class="add-btn"
        onclick={() => (isMenuOpen = !isMenuOpen)}
        bind:this={buttonElement}
    >
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M2.5 12C2.5 7.52166 2.5 5.28249 3.89124 3.89124C5.28249 2.5 7.52166 2.5 12 2.5C16.4783 2.5 18.7175 2.5 20.1088 3.89124C21.5 5.28249 21.5 7.52166 21.5 12C21.5 16.4783 21.5 18.7175 20.1088 20.1088C18.7175 21.5 16.4783 21.5 12 21.5C7.52166 21.5 5.28249 21.5 3.89124 20.1088C2.5 18.7175 2.5 16.4783 2.5 12Z"
                stroke="currentColor"
                stroke-width="2.3"
            />
            <path
                d="M9.48581 12.5068C10.0159 11.9753 11.3066 9.99591 12.0519 10.0054C12.7899 10.085 13.969 12.0056 14.4895 12.5099M11.9721 17.0039L11.9763 10.0108M15.9931 7.00289L8.00098 6.99805"
                stroke="currentColor"
                stroke-width="2.3"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
        Add content item
    </button>
    <div class="menu" class:open={isMenuOpen}>
        <label>Choose type of the content item</label>
        <div class="options">
            <div class="option" onclick={addParagraph}>
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M3 3H21"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M3 9H11"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M3 15H21"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M3 21H11"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                </svg>
                Paragraph
            </div>
            <div class="option" onclick={addList}>
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M8 5L20 5"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                    ></path>
                    <path
                        d="M4 5H4.00898"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M4 12H4.00898"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M4 19H4.00898"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M8 12L20 12"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                    ></path>
                    <path
                        d="M8 19L20 19"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                    ></path>
                </svg>
                List
            </div>
            <div class="option" onclick={addQuote}>
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M10 8C10 9.88562 10 10.8284 9.41421 11.4142C8.82843 12 7.88562 12 6 12C4.11438 12 3.17157 12 2.58579 11.4142C2 10.8284 2 9.88562 2 8C2 6.11438 2 5.17157 2.58579 4.58579C3.17157 4 4.11438 4 6 4C7.88562 4 8.82843 4 9.41421 4.58579C10 5.17157 10 6.11438 10 8Z"
                        stroke="currentColor"
                        stroke-width="2.3"
                    ></path>
                    <path
                        d="M10 7L10 11.4821C10 15.4547 7.48429 18.8237 4 20"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                    ></path>
                    <path
                        d="M22 8C22 9.88562 22 10.8284 21.4142 11.4142C20.8284 12 19.8856 12 18 12C16.1144 12 15.1716 12 14.5858 11.4142C14 10.8284 14 9.88562 14 8C14 6.11438 14 5.17157 14.5858 4.58579C15.1716 4 16.1144 4 18 4C19.8856 4 20.8284 4 21.4142 4.58579C22 5.17157 22 6.11438 22 8Z"
                        stroke="currentColor"
                        stroke-width="2.3"
                    ></path>
                    <path
                        d="M22 7L22 11.4821C22 15.4547 19.4843 18.8237 16 20"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                    ></path>
                </svg>
                Quote
            </div>
            <div class="option" onclick={addHeading}>
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M4 5V19"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M14 5V19"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M17 19H18.5M20 19H18.5M18.5 19V11L17 12"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M4 12L14 12"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                </svg>
                Heading
            </div>
            <div class="option" onclick={addSubheading}>
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                >
                    <path
                        d="M3.5 5V19"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M13.5 5V19"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M16.5 11V15H20.5M20.5 15V19M20.5 15V11"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                    <path
                        d="M3.5 12L13.5 12"
                        stroke="currentColor"
                        stroke-width="2.3"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    ></path>
                </svg>
                Subheading
            </div>
        </div>
    </div>
</div>

<style>
    .add-content-wrapper {
        position: relative;
        display: inline-block;
        margin-top: 1rem;
    }

    .add-btn {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 0.25rem;
        padding: 0.25rem 1rem;
        margin: 0 auto;
        border: none;
        border-radius: 0.5rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1.375rem;
        transition: all 0.08s ease-in;
        cursor: pointer;
    }

    .add-btn > svg {
        height: 1.5rem;
    }

    .add-btn:active {
        transform: scale(0.96);
    }

    .add-content-wrapper:hover .add-btn,
    .add-content-wrapper:has(.open) .add-btn {
        gap: 0.75rem;
        padding: 0.25rem 1.5rem;
        background-color: var(--accent-hov);
    }

    .menu {
        position: absolute;
        top: 110%;
        left: 50%;
        z-index: 1000;
        display: none;
        flex-direction: column;
        align-items: center;
        width: 24rem;
        border: 0.125rem solid var(--back-second);
        border-radius: 0.5rem;
        background-color: var(--back-main);
        box-shadow: 0 4px 8px rgb(0 0 0 / 10%);
        transform: translateX(-50%);
    }

    .menu.open {
        display: flex;
    }

    .menu > label {
        margin: 0.25rem 0;
        color: var(--gray);
        font-size: 1.25rem;
        font-weight: 440;
    }

    .options {
        display: flex;
        justify-content: center;
        gap: 0.5rem;
        padding: 0.5rem 0;
        flex-flow: row wrap;
    }

    .option {
        display: flex;
        align-items: center;
        gap: 0.25rem;
        padding: 0.25rem 0.75rem;
        border-radius: 4rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1.25rem;
        transition: all 0.15s ease;
        cursor: pointer;
    }

    .option > svg {
        height: 1.125rem;
        color: var(--back-main);
    }

    .option:hover {
        background-color: var(--accent-hov);
    }
</style>
