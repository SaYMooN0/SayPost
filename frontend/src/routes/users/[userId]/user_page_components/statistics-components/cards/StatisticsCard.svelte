<script lang="ts">
    const {
        card,
    }: {
        card:
            | {
                  isHidden: false;
                  link: string;
                  value: string;
                  labelText: string;
              }
            | { isHidden: true; cardName: string };
    } = $props<{
        card:
            | {
                  isHidden: false;
                  link: string;
                  value: string;
                  labelText: string;
              }
            | { isHidden: true; cardName: string };
    }>();
</script>

{#if card.isHidden}
    <div class="card card-hidden">
        <div class="hidden-card-blur">
            To see user's {card.cardName} you need to follow this user
        </div>
        <p class="value unselectable">{123}</p>
        <label class="text unselectable">{card.cardName} </label>
    </div>
{:else}
    <a
        href={card.link}
        class="card card-not-hidden"
        data-sveltekit-preload-data="hover"
    >
        <p class="value">{card.value}</p>
        <label class="text">{card.labelText} </label>
    </a>
{/if}

<style>
    .card {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        width: 13rem;
        height: 6rem;
        border-radius: 0.25rem;
        text-decoration: none;
        box-shadow:
            rgb(60 64 67 / 30%) 0 1px 2px 0,
            rgb(60 64 67 / 15%) 0 1px 3px 1px;
        transition: all 0.2s cubic-bezier(0.23, 1, 0.32, 1);
        cursor: pointer;
        overflow: hidden;
    }

    .card-not-hidden:hover {
        box-shadow:
            rgb(60 64 67 / 30%) 0 1px 2px 0,
            rgb(60 64 67 / 15%) 0 1px 3px 1px,
            rgb(0 0 0 / 10%) 0 5px 10px;
        transform: scale(1.1);
    }

    .card > p {
        margin: 0;
        color: var(--accent-main);
        font-size: 2.25rem;
        font-weight: 550;
        cursor: inherit;
    }

    .card > label {
        color: var(--accent-main);
        font-size: 1.5rem;
        font-weight: 450;
        cursor: inherit;
    }

    .card-hidden {
        position: relative;
    }

    .hidden-card-blur {
        position: absolute;
        z-index: 0;
        display: flex;
        justify-content: center;
        align-items: center;
        width: 100%;
        height: 100%;
        padding: 0.25rem;
        background: rgb(225 205 255 / 10%);
        color: var(--text-main);
        font-size: 1.125rem;
        text-align: center;
        cursor: default;
        backdrop-filter: blur(10px);
        backdrop-filter: blur(10px);
        box-sizing: border-box;
        text-wrap: pretty;
    }
</style>
