<script lang="ts">
    import type { UserProfileBanner } from "../../user-profile";
    import ProfileBannerEditingDialog from "./ProfileBannerEditingDialog.svelte";
    import ProfileBannerDisplay from "./ProfileBannerDisplay.svelte";

    let { bannerData }: { bannerData: UserProfileBanner } = $props<{
        bannerData: UserProfileBanner;
    }>();
    let bannerEditableCopy = $state(bannerData);
    let bannerEditingDialog: ProfileBannerEditingDialog;
</script>

<div class="banner">
    <ProfileBannerDisplay
        scale={bannerEditableCopy.scale}
        colors={bannerEditableCopy.colors}
        design={bannerEditableCopy.design}
        designVariant={bannerEditableCopy.variant}
    />
    <div class="banner-item">
        <button
            class="edit-banner-btn"
            onclick={() => bannerEditingDialog.open()}
        >
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M7.49478 13.753C10.5833 10.1644 17.5788 3.15478 20.5387 3.00445C22.3699 2.82906 18.7218 9.32547 10.0785 16.4339M11.4581 10.0449L13.7157 12.3249M3 20.8546C3.70948 18.3472 3.26187 19.5794 3.50407 16.6919C3.63306 16.2644 3.89258 14.9377 5.51358 14.2765C7.35618 13.5249 8.70698 14.6611 9.05612 15.195C10.0847 16.3102 10.2039 17.6952 9.05612 19.2774C7.9083 20.8596 4.50352 21.2527 3 20.8546Z"
                    stroke="currentColor"
                    stroke-width="1.8"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                ></path>
            </svg>
            Edit
        </button>
        <ProfileBannerEditingDialog
            bind:this={bannerEditingDialog}
            scale={bannerEditableCopy.scale}
            colors={bannerEditableCopy.colors}
            design={bannerEditableCopy.design}
            designVariant={bannerEditableCopy.variant}
            updateValuesOnPage={(newVal) => {
                bannerEditableCopy = newVal;
            }}
        />
    </div>
</div>

<style>
    .edit-banner-btn {
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: center;
        gap: 0.125rem;
        padding: 0.375rem 0.75rem;
        border: none;
        border-radius: 0.25rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1rem;
        font-weight: 420;
        
        box-shadow:
            rgba(48, 48, 99, 0.25) 0 2px 5px -1px,
            rgba(12, 9, 18, 0.3) 0 1px 3px -1px;
        cursor: pointer;
        outline: none;
        transition: all 0.08s ease-in;

    }

    .edit-banner-btn:focus,
    .edit-banner-btn:hover {
        transform: scale(1.04);
        font-weight: 460;


    }

    .edit-banner-btn > svg {
        width: 1.25rem;
        height: 1.25rem;
        color: inherit;
        transition: all 0.12s ease-in-out;
    }
    .edit-banner-btn:focus > svg,
    .edit-banner-btn:hover > svg {
        transform: rotate(-10deg) scale(1.05);
    }
</style>
