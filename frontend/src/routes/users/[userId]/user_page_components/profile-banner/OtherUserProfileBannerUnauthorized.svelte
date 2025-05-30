<script lang="ts">
    import type { UserProfileBanner } from "../../user-profile";
    import ProfileBannerDisplay from "./ProfileBannerDisplay.svelte";
    let { bannerData }: { bannerData: UserProfileBanner } = $props<{
        bannerData: UserProfileBanner;
    }>();
</script>

<div class="banner">
    <ProfileBannerDisplay
        scale={bannerData.scale}
        colors={bannerData.colors}
        design={bannerData.design}
        designVariant={bannerData.variant}
    />
    <div class="banner-item">
        <label class="logging-to-follow-msg">Log in to follow users</label>
    </div>
</div>

<style>
    .logging-to-follow-msg {
        padding: 0.375rem 0.75rem;
        border-radius: 0.25rem;
        background-color: var(--back-second);
        font-weight: 450;
    }
</style>