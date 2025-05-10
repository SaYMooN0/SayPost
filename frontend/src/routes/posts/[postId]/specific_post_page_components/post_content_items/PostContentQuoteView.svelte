<script lang="ts">
    import type {
        PostContentItem,
        QuoteContentItem,
    } from "../../../../../ts/common/post-content-item";
    import { StringUtils } from "../../../../../ts/common/utils/string-utils";
    let { quote }: { quote: QuoteContentItem } = $props<{
        quote: PostContentItem;
    }>();
</script>

<div class="quote">
    <p class="text">{quote.text}</p>
    {#if !StringUtils.isNullOrWhiteSpace(quote.author)}
        <p class="author">{quote.author}</p>
    {/if}
</div>

<style>
    .quote {
        position: relative;
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
        font-size: var(--post-content-paragraph-font-size);
        font-weight: var(--post-content-paragraph-font-weight);
        margin: 0;

    }

    .text {
        max-width: 90%;
        padding: 0.5rem;
        margin: 0;
        border-radius: 0.5rem;
        background-color: var(--back-second);
        color: var(--text-main);
        font-size: inherit;
        font-weight: inherit;
        font-style: italic;
        align-self: center;
        box-sizing: border-box;
        overflow-wrap: break-word;
        word-break: break-word;
    }

    .author {
        color: var(--gray);
        align-self: flex-end;
        word-break: break-all;
    }
</style>
