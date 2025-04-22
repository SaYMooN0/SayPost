<script lang="ts">
    import type { Err } from "../../../../../../ts/common/errs/err";
    import { StringUtils } from "../../../../../../ts/common/utils/string-utils";

    let {
        setErrs,
        setSearchedTags,
    }: {
        setErrs: (newErrs: Err[]) => void;
        setSearchedTags: (newTags: string[]) => void;
    } = $props<{
        setErrs: (newErrs: Err[]) => void;
        setSearchedTags: (newTags: string[]) => void;
    }>();
    
    let tagSearchInput: string = $state("");
    export function setSearchInputEmpty() {
        tagSearchInput = "";
    }
</script>

<div class="search-bar">
    <svg class="search-icon" viewBox="0 0 24 24" fill="none">
        <path
            d="M17.5 17.5L22 22"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M20 11C20 6.02944 15.9706 2 11 2C6.02944 2 2 6.02944 2 11C2 15.9706 6.02944 20 11 20C15.9706 20 20 15.9706 20 11Z"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linejoin="round"
        />
    </svg>
    <input
        placeholder="Search for tag..."
        bind:value={tagSearchInput}
        name={"tag-search-" + StringUtils.rndStr(12)}
    />
    <svg
        class="reset-button"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
        stroke-width="2"
        on:click={() => (tagSearchInput = "")}
    >
        <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M6 18L18 6M6 6l12 12"
        ></path>
    </svg>
</div>

<style>
    .search-bar {
        position: relative;
        display: flex;
        align-items: center;
        height: 2.5rem;
        padding: 0.25rem 1rem;
        border-radius: 1rem;
        background: var(--back-second);
        transition: border-radius 0.45s ease;
        box-sizing: border-box;
    }

    .search-icon {
        width: 1.25rem;
        height: 1.25rem;
    }

    .search-input {
        width: 100%;
        height: 100%;
        border: none;
        background-color: transparent;
        font-size: 1rem;
        padding-inline: 0.5rem;
        padding-block: 0.75rem;
    }

    .search-bar::before {
        position: absolute;
        bottom: 0;
        left: 0;
        width: 100%;
        height: 0.125rem;
        background: var(--primary);
        transition: transform 0.25s ease;
        transform: scaleX(0);
        content: "";
        transform-origin: center;
    }

    .search-bar:focus-within {
        border-radius: 0.25rem;
    }

    .search-input:focus {
        outline: none;
    }

    .search-bar:focus-within::before {
        transform: scale(1);
    }

    .reset-button {
        width: 1.25rem;
        height: 1.25rem;
        margin-top: 0.25rem;
        opacity: 0;
        cursor: pointer;
        visibility: hidden;
    }

    .search-input:not(:placeholder-shown) ~ .reset-button {
        opacity: 1;
        visibility: visible;
    }
</style>
