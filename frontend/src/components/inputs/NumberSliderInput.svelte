<script lang="ts">
    let {
        min,
        max,
        step,
        value = $bindable<number>(min),
        labelMarks,
    }: {
        min: number;
        max: number;
        step: number;
        value: number;
        labelMarks: number[];
    } = $props<{
        min: number;
        max: number;
        step: number;
        value: number;
        labelMarks: number[];
    }>();
</script>

<div class="slider-container unselectable">
    <input class="slider" type="range" bind:value {min} {max} {step} />

    {#if labelMarks.length > 0}
        <div class="slider-track">
            {#each labelMarks as mark}
                <div
                    onclick={() => (value = mark)}
                    class="mark"
                    class:active={mark == value}
                    style="left: {(max - min) * (mark - min) * 100}%"
                >
                    <label class="mark-label">{mark}</label>
                </div>
            {/each}
        </div>
    {/if}
</div>

<style>
    .slider-container {
        display: flex;
        flex-direction: column;
        gap: 0.25rem;
        width: 100%;
        position: relative;
    }

    .slider {
        width: 100%;
        appearance: none;
        height: 0.375rem;
        border-radius: 0.125rem;
        background: var(--back-second);
        box-sizing: border-box;
        outline: none;
        border: none;
        margin: 0;
        z-index: 6;
    }

    .slider::-webkit-slider-thumb {
        width: 1rem;
        height: 1rem;
        border-radius: 50%;
        background: var(--accent-main);
        transition: background 0.2s ease;
        cursor: pointer;
        appearance: none;
        z-index: 15;
    }

    .slider::-moz-range-thumb {
        width: 1rem;
        height: 1rem;
        border-radius: 50%;
        background: var(--accent-main);
        cursor: pointer;
        z-index: 15;
    }
    .slider-track {
        width: calc(100% - 0.75rem);
        height: 1rem;
        display: flex;
        justify-content: space-between;
        color: var(--gray);
        font-size: 0.8rem;
        position: relative;
        z-index: 3;
    }

    .mark {
        position: absolute;
        flex: 0 0 auto;
        align-items: center;
        z-index: 1;
        width: 0.75rem;
        font-weight: 460;
        color: var(--gray);
        top: 0.125rem;
        transition: all 0.2s ease;
        cursor: pointer;
    }
    .mark-label {
        display: block;
        position: absolute;
        left: 50%;
        transform: translateX(-50%);
        color: inherit;
        font-weight: inherit;
        cursor: inherit;
    }
    .mark:hover:not(.active) {
        color: var(--text-main);
        transform: scale(1.08) translateY(-0.125rem);
        font-weight: 550;
    }
    .mark.active {
        color: var(--accent-main);
        font-weight: 600;
    }
</style>
