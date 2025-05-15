<script lang="ts">
    import { json } from "@sveltejs/kit";
    import BaseDialogWithCloseButton from "../../../../components/dialogs/BaseDialogWithCloseButton.svelte";
    import { BannerDesign, DesignVariant } from "../user-profile";
    import ProfileBannerDisplay from "./ProfileBannerDisplay.svelte";
    import NumberSliderInput from "../../../../components/inputs/NumberSliderInput.svelte";
    import BannerColorInput from "./banner_editing_dialog_components/BannerColorInput.svelte";
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
    <NumberSliderInput
        bind:value={chosenScale}
        min={1}
        max={2}
        step={0.01}
        labelMarks={[1, 1.5, 2]}
    />
    <button class="save-btn" onclick={() => save()}>Save</button>
</BaseDialogWithCloseButton>

<style>
    :global(#profile-banner-editing) {
        padding: 0 0.25rem;
        background-color: var(--back-main);
        border-radius: 1rem;
        box-shadow:
            rgba(50, 50, 93, 0.25) 0px 2px 5px -1px,
            rgba(0, 0, 0, 0.3) 0px 1px 3px -1px;
    }
    :global(#profile-banner-editing .dialog-content) {
        display: flex;
        flex-direction: column;
        align-items: center;
        height: 50rem;
        width: 60rem;
        box-sizing: border-box;
        overflow-y: auto;
    }
    :global(#profile-banner-editing .close-button) {
        top: 1rem;
    }

    .banner-title {
        font-size: 2rem;
        margin: 0.5rem 0;
    }

    .banner-preview-container {
        width: 100%;
    }
    .banner-preview-container > :global(svg) {
        border-radius: 0.25rem;
    }
    .section-label {
        font-size: 1rem;
        font-weight: 450;
        color: var(--gray);
        margin: 1rem 0 0.25rem 0;
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
    .design-input {
        height: 3rem;
        width: 9rem;
        display: flex;
        align-items: center;
        justify-content: center;
        box-shadow: rgba(0, 0, 0, 0.15) 0px 1px 4px;
        cursor: pointer;
        border-radius: 0.75rem;
        transition: all 0.04s ease-in;
    }
    .design-input:hover {
        box-shadow: rgba(0, 0, 0, 0.25) 0px 1px 4px;
        transform: scale(1.02);
    }
    .design-input-chosen {
        box-shadow: rgba(43, 15, 189, 0.35) 0px 1px 4px !important;
        transform: scale(1.06) !important;
    }
    .variants-container {
        display: flex;
        flex-direction: row;
        gap: 1rem;
    }
    .save-btn {
        margin: 1rem 0 0 0;
    }
</style>
