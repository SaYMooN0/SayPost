<script lang="ts">
    import type { StatisticsValue } from "../../../user-profile";
    import StatisticsCard from "./StatisticsCard.svelte";
    const {
        cardValue,
        userId,
    }: { cardValue: StatisticsValue; userId: string } = $props<{
        cardValue: StatisticsValue;
        userId: string;
    }>();
</script>

{#if cardValue.isHidden}
    <StatisticsCard
        card={{
            isHidden: true,
            cardName: "liked posts",
        }}
    />
{:else}
    <StatisticsCard
        card={{
            isHidden: false,
            link: `/users/${userId}/liked-posts`,
            value: cardValue.value.toString(),
            labelText:
                cardValue.value === 1 ? "liked post" : "liked posts",
        }}
    />
{/if}
