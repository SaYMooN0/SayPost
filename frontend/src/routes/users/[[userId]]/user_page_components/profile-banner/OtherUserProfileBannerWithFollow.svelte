<script lang="ts">
    import { ApiFollowings } from "../../../../../ts/backend-services";
    import type { UserProfileBanner } from "../../user-profile";
    import ProfileBannerDisplay from "./ProfileBannerDisplay.svelte";

    let {
        bannerData,
        isFollowedByViewer,
        userId,
        updateFollowersCountOnThePage,
    }: {
        bannerData: UserProfileBanner;
        isFollowedByViewer: boolean;
        userId: string;
        updateFollowersCountOnThePage: (newVal: number) => void;
    } = $props<{
        bannerData: UserProfileBanner;
        isFollowedByViewer: boolean;
        userId: string;
        updateFollowersCountOnThePage: (newVal: number) => void;
    }>();

    async function toggleFollow() {
        const url =
            `/users/${userId}/` + (isFollowedByViewer ? "unfollow" : "follow");
        console.log(url);
        const response = await ApiFollowings.fetchJsonResponse<{
            newIsFollowedByViewer: boolean;
            newFollowersCount: number;
        }>(url, {
            method: "PATCH",
        });
        if (response.isSuccess) {
            isFollowedByViewer = response.data.newIsFollowedByViewer;
            updateFollowersCountOnThePage(response.data.newFollowersCount);
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

