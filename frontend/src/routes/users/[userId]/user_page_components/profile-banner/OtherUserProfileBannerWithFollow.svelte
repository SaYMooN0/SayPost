<script lang="ts">
    import { ApiFollowings } from "../../../../../ts/backend-services";
    import type { UserProfileBanner } from "../../user-profile";
    import ProfileBannerDisplay from "./ProfileBannerDisplay.svelte";

    let {
        bannerData,
        isFollowedByViewer,
        userId,
        updateUserStatistics,
    }: {
        bannerData: UserProfileBanner;
        isFollowedByViewer: boolean;
        userId: string;
        updateUserStatistics: () => void;
    } = $props<{
        bannerData: UserProfileBanner;
        isFollowedByViewer: boolean;
        userId: string;
        updateUserStatistics: () => void;
    }>();

    async function toggleFollow() {
        const url =
            `/users/${userId}/` + (isFollowedByViewer ? "unfollow" : "follow");
        const response = await ApiFollowings.fetchJsonResponse<{
            newIsFollowedByViewer: boolean;
        }>(url, {
            method: "PATCH",
        });
        if (response.isSuccess) {
            isFollowedByViewer = response.data.newIsFollowedByViewer;
            updateUserStatistics();
        } else {
            console.error(response.errors);
        }
    }
</script>

<div class="banner">
    <ProfileBannerDisplay
        scale={bannerData.scale}
        colors={bannerData.colors}
        design={bannerData.design}
        designVariant={bannerData.variant}
    />
    <div class="banner-item">
        <button class="follow-button" onclick={() => toggleFollow()}>
            {#if isFollowedByViewer}
                <span>Following</span>
            {:else}
                <span>Follow</span>
            {/if}
        </button>
    </div>
</div>

