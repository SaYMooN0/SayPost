<div class="loader-container">
    <div class="cube"><div class="cube-inner"></div></div>
    <div class="cube"><div class="cube-inner"></div></div>
    <div class="cube"><div class="cube-inner"></div></div>
</div>

<style>
    .loader-container {
        --uib-size: 3rem;
        --uib-color: var(--accent-main);
        --uib-speed: 1.75s;

        display: flex;
        justify-content: space-between;
        align-items: flex-end;
        width: var(--uib-size);
        height: calc(var(--uib-size) * 0.6);
        padding-bottom: calc(var(--uib-size) * 0.1);
    }

    .cube {
        flex-shrink: 0;
        width: calc(var(--uib-size) * 0.2);
        height: calc(var(--uib-size) * 0.2);
        animation: jump var(--uib-speed) ease-in-out infinite;
    }

    .cube-inner {
        display: block;
        width: 100%;
        height: 100%;
        border-radius: 25%;
        background-color: var(--uib-color);
        transition: background-color 0.3s ease;
        animation: morph var(--uib-speed) ease-in-out infinite;
        transform-origin: center bottom;
    }

    .cube:nth-child(2) {
        animation-delay: calc(var(--uib-speed) * -0.36);

        .cube-inner {
            animation-delay: calc(var(--uib-speed) * -0.36);
        }
    }

    .cube:nth-child(3) {
        animation-delay: calc(var(--uib-speed) * -0.2);

        .cube-inner {
            animation-delay: calc(var(--uib-speed) * -0.2);
        }
    }

    @keyframes jump {
        0% {
            transform: translateY(0);
        }

        30% {
            transform: translateY(0);
            animation-timing-function: ease-out;
        }

        50% {
            transform: translateY(-200%);
            animation-timing-function: ease-in;
        }

        75% {
            transform: translateY(0);
            animation-timing-function: ease-in;
        }
    }

    @keyframes morph {
        0% {
            transform: scaleY(1);
        }

        10% {
            transform: scaleY(1);
        }

        20%,
        25% {
            transform: scaleY(0.6) scaleX(1.3);
            animation-timing-function: ease-in-out;
        }

        30% {
            transform: scaleY(1.15) scaleX(0.9);
            animation-timing-function: ease-in-out;
        }

        40% {
            transform: scaleY(1);
        }

        70%,
        85%,
        100% {
            transform: scaleY(1);
        }

        75% {
            transform: scaleY(0.8) scaleX(1.2);
        }
    }
</style>
