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

<div class="container" class:chosen={isChosen} onclick={() => choose(enumVal)}>
    <label>{labelText}</label>
    <ProfileBannerDisplay
        scale={1}
        colors={chosenColors}
        design={chosenDesign}
        designVariant={enumVal}
    />
    <div class="selection-indicator" class:selected={isChosen}></div>
</div>
