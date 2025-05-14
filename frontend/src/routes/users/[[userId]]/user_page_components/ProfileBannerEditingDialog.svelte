<script lang="ts">
    import { json } from "@sveltejs/kit";
    import BaseDialogWithCloseButton from "../../../../components/dialogs/BaseDialogWithCloseButton.svelte";
    import { BannerDesign, DesignVariant } from "../user-profile";
    import ProfileBannerDisplay from "./ProfileBannerDisplay.svelte";
    import NumberSliderInput from "../../../../components/inputs/NumberSliderInput.svelte";
    let {
        scale,
        colors,
        design,
        designVariant,
    }: {
        scale: number;
        colors: string[];
        design: BannerDesign;
        designVariant: DesignVariant;
    } = $props<{
        scale: number;
        colors: string[];
        design: BannerDesign;
        designVariant: DesignVariant;
    }>();
    let chosenScale = $state(scale);
    let chosenColors = $state(colors);
    let chosenDesign = $state(design);
    let chosenDesignVariant = $state(designVariant);
    let designColorIndexes = $derived(
        Array.from(
            { length: BannerDesign.colorsInBannerDesignCount(chosenDesign) },
            (_, i) => i,
        ),
    );
    let dialogEl: BaseDialogWithCloseButton;
    export function open() {
        dialogEl.open();
    }
    async function save() {}
</script>

<BaseDialogWithCloseButton
    bind:this={dialogEl}
    dialogId="profile-banner-editing"
>
    <h2 class="banner-title">Profile banner editing</h2>
    <p class="section-label">Banner preview</p>
    <div class="banner-preview-container">
        <ProfileBannerDisplay
            scale={chosenScale}
            colors={chosenColors}
            design={chosenDesign}
            designVariant={chosenDesignVariant}
        />
    </div>
    <p class="section-label">Banner colors</p>
    <div class="colors-container">
        {#each designColorIndexes as colorI}
            <div class="color-input">
                <label class="color-label">Color #{colorI + 1}</label>
                <input type="color" bind:value={chosenColors[colorI]} />
            </div>
        {/each}
    </div>
    <p class="section-label">Banner design</p>
    <div class="designs-container">
        {#each BannerDesign.valsWithNames() as { value, name }}
            <div
                class="design-input"
                class:design-input-chosen={value == chosenDesign}
                onclick={() => (chosenDesign = value)}
            >
                {name}
            </div>
        {/each}
    </div>
    <p class="section-label">Banner design variant</p>
    <div class="variants-container">
        {#each Object.keys(DesignVariant) as variant, i}
            {@const enumVal =
                DesignVariant[variant as keyof typeof DesignVariant]}
            <div onclick={() => (chosenDesignVariant = enumVal)}>
                Variant #{i + 1}
                <ProfileBannerDisplay
                    scale={1}
                    colors={chosenColors}
                    design={chosenDesign}
                    designVariant={enumVal}
                />
            </div>
        {/each}
    </div>
    <p class="section-label">Banner scale</p>
    <NumberSliderInput bind:value={chosenScale} min={1} max={4} step={0.01} labelMarks={[1, 2, 3, 4]} />
    <button class="save-btn" onclick={() => save()}>Save</button>
</BaseDialogWithCloseButton>

<style>
    :global(#profile-banner-editing .dialog-content) {
        display: flex;
        flex-direction: column;
        background-color: darksalmon;
        width: 40rem;
        height: 40rem;
        overflow-y: auto;
    }

    .banner-title {
        font-size: 2rem;
    }

    .banner-preview-container {
        width: 10rem;
    }
</style>
