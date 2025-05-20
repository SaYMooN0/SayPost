<script lang="ts">
    import BaseDialogWithCloseButton from "../../../../components/dialogs/BaseDialogWithCloseButton.svelte";
    import {
        BannerDesign,
        DesignVariant,
        type UserProfileBanner,
    } from "../user-profile";
    import ProfileBannerDisplay from "./ProfileBannerDisplay.svelte";
    import NumberSliderInput from "../../../../components/inputs/NumberSliderInput.svelte";
    import BannerColorInput from "./banner_editing_dialog_components/BannerColorInput.svelte";
    import DesignVariantInputButton from "./banner_editing_dialog_components/DesignVariantInputButton.svelte";
    import DesignSelectInput from "./banner_editing_dialog_components/DesignSelectInput.svelte";
    import { ApiMain } from "../../../../ts/backend-services";
    import type { Err } from "../../../../ts/common/errs/err";
    import DefaultErrBlock from "../../../../components/err_blocks/DefaultErrBlock.svelte";
    let {
        scale,
        colors,
        design,
        designVariant,
        updateValuesOnPage,
    }: {
        scale: number;
        colors: string[];
        design: BannerDesign;
        designVariant: DesignVariant;
        updateValuesOnPage: (newVal: UserProfileBanner) => void;
    } = $props<{
        scale: number;
        colors: string[];
        design: BannerDesign;
        designVariant: DesignVariant;
        updateValuesOnPage: (newVal: UserProfileBanner) => void;
    }>();
    let inputs = $state({ scale, colors: [...colors], design, designVariant });
    let designColorIndexes = $derived(
        Array.from(
            { length: BannerDesign.colorsInBannerDesignCount(inputs.design) },
            (_, i) => i,
        ),
    );
    let dialogEl: BaseDialogWithCloseButton;
    export function open() {
        dialogEl.open();
    }
    async function save() {
        console.log("a-----",{ ...inputs });
        const response = await ApiMain.fetchJsonResponse<UserProfileBanner>(
            `/profile/update-banner/`,
            ApiMain.requestJsonPostOptions({ ...inputs }, "PATCH"),
        );
        if (response.isSuccess) {
            updateValuesOnPage(response.data);
            savingErrs = [];
        } else {
            savingErrs = response.errors;
        }
    }
    let savingErrs: Err[] = $state([]);
</script>

<BaseDialogWithCloseButton
    bind:this={dialogEl}
    dialogId="profile-banner-editing"
>
    <h2 class="banner-title">Profile banner editing</h2>
    <p class="section-label">Banner preview</p>
    <div class="banner-preview-container">
        <ProfileBannerDisplay {...inputs} />
    </div>
    <p class="section-label">Banner colors</p>
    <div class="colors-container">
        {#each designColorIndexes as colorI}
            <BannerColorInput
                labelText={`Color #${colorI + 1}`}
                bind:color={inputs.colors[colorI]}
            />
        {/each}
    </div>
    <p class="section-label">Banner design</p>
    <div class="designs-container">
        {#each BannerDesign.valsWithNames() as { value, name }}
            <DesignSelectInput
                {name}
                isChosen={inputs.design == value}
                choose={() => (inputs.design = value)}
            />
        {/each}
    </div>
    <p class="section-label">Banner design variant</p>
    <div class="variants-container">
        {#each Object.keys(DesignVariant) as variant, i}
            <DesignVariantInputButton
                inputVal={variant}
                labelText="Variant #{i + 1}"
                choose={(val) => (inputs.designVariant = val)}
                isChosen={inputs.designVariant == variant}
                chosenColors={inputs.colors}
                chosenDesign={inputs.design}
            />
        {/each}
    </div>
    <p class="section-label">Banner scale</p>
    <div class="scale-container">
        <NumberSliderInput
            bind:value={inputs.scale}
            min={1}
            max={2}
            step={0.01}
            labelMarks={[1, 1.5, 2]}
        />
    </div>
    <DefaultErrBlock errList={savingErrs} />
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
        width: 68rem;
        height: min(55rem, 90vh);
        box-sizing: border-box;
        overflow-y: auto;
    }

    :global(#profile-banner-editing .close-button) {
        top: 1rem;
    }

    .banner-title {
        margin: 0.5rem 0 0;
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

    .scale-container {
        width: 40rem;
    }

    .save-btn {
        padding: 0.25rem 1rem;
        margin-top: 2rem;
        border: none;
        border-radius: 0.25rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1.25rem;
        font-weight: 520;
        transition: all 0.04s ease-in;
        cursor: pointer;
    }

    .save-btn:hover {
        background-color: var(--accent-hov);
    }

    :global(.dialog-content .selection-indicator) {
        width: 0.5rem;
        height: 0.75rem;
        padding: 0.25rem;
        border: 0.125rem solid var(--gray);
        border-radius: 2rem;
        background-color: transparent;
        box-sizing: border-box;
    }

    :global(.dialog-content .selection-indicator.selected) {
        border-color: var(--accent-main);
    }

    :global(.dialog-content .err-block) {
        margin: 2rem 2rem 0;
        box-sizing: border-box;
    }
</style>
