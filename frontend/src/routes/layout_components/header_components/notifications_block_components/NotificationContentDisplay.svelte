<script lang="ts">
    let { specificData, type }: { specificData: any; type: string } = $props<{
        specificData: any;
        type: string;
    }>();
</script>

<p>{JSON.stringify(specificData)}</p>
<p>{type}</p>
{#if type === "comment"}
    <p>{specificData.comment}</p>
{:else if type === "like"}
    <p>{specificData.like}</p>
{:else if type === "follow"}
    <p>{specificData.follow}</p>
{/if}
