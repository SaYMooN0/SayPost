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
            cardName: "followers",
        }}
    />
{:else}
    <StatisticsCard
        card={{
            isHidden: false,
            link: `/users/${userId}/followers`,
            value: cardValue.value.toString(),
            labelText: cardValue.value === 1 ? "follower" : "followers",
        }}
    />
{/if}
