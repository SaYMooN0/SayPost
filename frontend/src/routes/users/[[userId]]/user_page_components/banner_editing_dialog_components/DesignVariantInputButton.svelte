<script lang="ts">
    import { BannerDesign, DesignVariant } from "../../user-profile";
    import ProfileBannerDisplay from "../ProfileBannerDisplay.svelte";

    let {
        inputVal,
        isChosen,
        labelText,
        choose,
        chosenColors,
        chosenDesign,
    }: {
        inputVal: string;
        isChosen: boolean;
        labelText: string;
        choose: (variant: DesignVariant) => void;
        chosenColors: string[];
        chosenDesign: BannerDesign;
    } = $props<{
        inputVal: string;
        isChosen: boolean;
        labelText: string;
        choose: (variant: DesignVariant) => void;
        chosenDesign: BannerDesign;
        chosenColors: string[];
    }>();
    const enumVal = DesignVariant[inputVal as keyof typeof DesignVariant];
</script>

<div
    class="variant-input"
    class:chosen={isChosen}
    onclick={() => choose(enumVal)}
>
    <label>{labelText}</label>

    <div class="banner">
        <ProfileBannerDisplay
            scale={1}
            colors={chosenColors}
            design={chosenDesign}
            designVariant={enumVal}
        />
    </div>
    <div class="selection-indicator" class:selected={isChosen}></div>
</div>

<style>
    .variant-input {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
        gap: 0.25rem;
        font-size: 1rem;
        padding: 0.25rem;
        font-weight: 500;
        border-radius: 0.75rem;
        box-shadow: rgb(0 0 0 / 15%) 0 1px 4px;
        transition: all 0.04s ease-in;
        cursor: pointer;
        border: 0.125rem solid transparent;
    }
    .banner {
        height: 2rem;
        width: 10rem;
        border-radius: 0.25rem;
    }
    
   .variant-input:hover {
        box-shadow: rgb(0 0 0 / 25%) 0 1px 4px;
        transform: scale(1.02);
    }

    .variant-input.chosen {
        box-shadow: rgb(43 15 189 / 35%) 0 1px 4px;
        transform: scale(1.06);
        border-color: var(--accent-main);
    }
</style>
