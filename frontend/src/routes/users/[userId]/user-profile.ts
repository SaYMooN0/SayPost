export type UserProfile = {
    userId: string;
    isFollowedByViewer: boolean;
    profileBanner: UserProfileBanner;
}
export type UserProfileBanner = {
    scale: number;
    design: BannerDesign;
    variant: DesignVariant;
    colors: string[];
}
export enum BannerDesign {
    Waves = "Waves",
    BlobScene = "BlobScene",
    LayeredPeaks = "LayeredPeaks",
    LayeredWaves = "LayeredWaves",
    Steps = "Steps"
}

export enum DesignVariant {
    Var1 = "Var1",
    Var2 = "Var2",
    Var3 = "Var3",
    Var4 = "Var4",
    Var5 = "Var5"
}

export type UserProfileStatistics = {
    publishedPosts: StatisticsValue;
    followers: StatisticsValue;
    followings: StatisticsValue;
    commentsLeft: StatisticsValue;
    likedPosts: StatisticsValue;
}
export type StatisticsValue =
    | { isHidden: false; value: number }
    | { isHidden: true }

export namespace BannerDesign {
    export function colorsInBannerDesignCount(design: BannerDesign): number {
        switch (design) {
            case BannerDesign.Waves:
            case BannerDesign.BlobScene:
                return 2;
            case BannerDesign.LayeredPeaks:
            case BannerDesign.LayeredWaves:
            case BannerDesign.Steps:
                return 5;
            default:
                return 0;
        }
    }

    export function valsWithNames(): { value: BannerDesign, name: string }[] {
        const result: { value: BannerDesign, name: string }[] = [
            { value: BannerDesign.Waves, name: "Waves" },
            { value: BannerDesign.BlobScene, name: "Blob Scene" },
            { value: BannerDesign.LayeredPeaks, name: "Layered Peaks" },
            { value: BannerDesign.LayeredWaves, name: "Layered Waves" },
            { value: BannerDesign.Steps, name: "Steps" }
        ];
        return result;
    }
}
export type StatisticsVisibility = {
    publishedPostsOnlyForFollowers: boolean;
    followersOnlyForFollowers: boolean;
    followingOnlyForFollowers: boolean;
    likedPostsOnlyForFollowers: boolean;
    leftCommentsOnlyForFollowers: boolean;
};