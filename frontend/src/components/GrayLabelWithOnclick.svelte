<script lang="ts">
    const { onClick, text } = $props<{ onClick: () => void; text: string }>();
</script>

<label class="unselectable" onclick={() => onClick()}>{text}</label>

<style>
    label {
        width: fit-content;
        padding: 0.125rem 0.75rem;
        margin-top: 0.25rem;
        border-radius: 0.375rem;
        background-color: transparent;
        color: var(--gray);
        font-size: 1rem;
        transition: all 0.12s ease;
    }

    label:hover {
        background-color: var(--back-second);
        color: var(--accent-main);
    }

    label:active {
        transform: scale(0.98);
    }
</style>
