<script lang="ts">
    import { json } from "@sveltejs/kit";
    import BaseDialogWithCloseButton from "../../../../components/dialogs/BaseDialogWithCloseButton.svelte";
    import { BannerDesign, DesignVariant } from "../user-profile";
    import ProfileBannerDisplay from "./ProfileBannerDisplay.svelte";
    import NumberSliderInput from "../../../../components/inputs/NumberSliderInput.svelte";
    import BannerColorInput from "./banner_editing_dialog_components/BannerColorInput.svelte";
    import DesignVariantInputButton from "./banner_editing_dialog_components/DesignVariantInputButton.svelte";
    import DesignSelectInput from "./banner_editing_dialog_components/DesignSelectInput.svelte";
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
            <BannerColorInput
                labelText={`Color #${colorI + 1}`}
                bind:color={chosenColors[colorI]}
            />
        {/each}
    </div>
    <p class="section-label">Banner design</p>
    <div class="designs-container">
        {#each BannerDesign.valsWithNames() as { value, name }}
            <DesignSelectInput
                {name}
                isChosen={chosenDesign == value}
                choose={() => (chosenDesign = value)}
            />
        {/each}
    </div>
    <p class="section-label">Banner design variant</p>
    <div class="variants-container">
        {#each Object.keys(DesignVariant) as variant, i}
            <DesignVariantInputButton
                inputVal={variant}
                labelText="Variant #{i + 1}"
                choose={(val) => (chosenDesignVariant = val)}
                isChosen={chosenDesignVariant == variant}
                {chosenColors}
                {chosenDesign}
            />
        {/each}
    </div>
    <p class="section-label">Banner scale</p>
    <div class="scale-container">
        <NumberSliderInput
            bind:value={chosenScale}
            min={1}
            max={2}
            step={0.01}
            labelMarks={[1, 1.5, 2]}
        />
    </div>
    <button class="save-btn" onclick={() => save()}>Save</button>
</BaseDialogWithCloseButton>

<style>
    :global(#profile-banner-editing) {
        padding: 0 0.25rem;
        border-radius: 1rem;
        background-color: var(--back-main);
        box-shadow:
            rgb(50 50 93 / 25%) 0 2px 5px -1px,
            rgb(0 0 0 / 30%) 0 1px 3px -1px;
    }

    :global(#profile-banner-editing .dialog-content) {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 72rem;
        height: 55rem;
        box-sizing: border-box;
        overflow-y: auto;
    }

    :global(#profile-banner-editing .close-button) {
        top: 1rem;
    }

    .banner-title {
        margin: 0.5rem 0 0 0;
        font-size: 1.5rem;
    }

    .banner-preview-container {
        width: 100%;
    }

    .banner-preview-container > :global(svg) {
        border-radius: 0.25rem;
    }

    .section-label {
        margin: 1rem 0 0.25rem;
        color: var(--gray);
        font-size: 1rem;
        font-weight: 450;
    }

    .colors-container {
        display: flex;
        flex-direction: row;
        gap: 1rem;
    }

    .designs-container {
        display: flex;
        flex-direction: row;
        gap: 1rem;
    }

    .variants-container {
        display: flex;
        flex-direction: row;
        gap: 1rem;
    }
    :global(.dialog-content .selection-indicator) {
        height: 0.75rem;
        width: 0.5rem;
        box-sizing: border-box;
        border: 0.125rem solid var(--gray);
        background-color: transparent;
        padding: 0.25rem;
        border-radius: 2rem;
    }
    :global(.dialog-content .selection-indicator.selected) {
        border-color: var(--accent-main);
    }

    .save-btn {
        margin: 1rem 0 0;
    }
    .scale-container {
        width: 40rem;
        margin-bottom: 12rem;
    }
</style>
