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
            <div class="marks">
                {#each labelMarks as mark}
                    <div
                        class="mark"
                        style="left: {(100 * (mark - min)) / (max - min)}%"
                    >
                        <div class="mark-line" />
                        {mark}
                    </div>
                {/each}
            </div>
        </div>
    {/if}
</div>

<style>
    .slider-container {
        display: flex;
        flex-direction: column;
        gap: 0.25rem;
        width: 100%;
    }

    .slider {
        width: 100%;
        appearance: none;
        height: 0.375rem;
        border-radius: 1rem;
        background: var(--back-second);
        outline: none;
    }

    .slider::-webkit-slider-thumb {
        width: 1rem;
        height: 1rem;
        border-radius: 50%;
        background: var(--accent-main);
        transition: background 0.2s ease;
        cursor: pointer;
        appearance: none;
    }

    .slider::-moz-range-thumb {
        width: 1rem;
        height: 1rem;
        border-radius: 50%;
        background: var(--accent-main);
        cursor: pointer;
    }

    .marks {
        display: flex;
        justify-content: space-between;
        color: var(--gray);
        font-size: 0.8rem;
    }

    .mark {
        position: relative;
        text-align: center;
        transform: translateX(-50%);
        flex: 0 0 auto;
    }

    .slider-track {
        position: relative;
        width: 100%;
    }

    .mark-line {
        position: absolute;
        top: -1rem;
        left: 50%;
        width: 0.125rem;
        height: 1rem;
        background: var(--back-second);
    }
</style>
