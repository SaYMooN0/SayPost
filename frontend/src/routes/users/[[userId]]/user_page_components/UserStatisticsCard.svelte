<script lang="ts">
    import type { Snippet } from "svelte";

    let {
        label,
        value,
        children,
    }: { label: string; value: string; children: Snippet } = $props<{
        label: string;
        value: string;
        children: Snippet;
    }>();
</script>

<div class="card">
    <label class="text">{label}</label>
    <label class="value">
        {@render children()}
        {value}
    </label>
</div>

<style>
    .card{
        background-color: red;
    }
</style>
