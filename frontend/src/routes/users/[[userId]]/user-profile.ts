export type UserProfile = {
    userId: string;
    profileBanner: UserProfileBanner;
    statistics: UserProfileStatistics;
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
//is following

export type UserProfileStatistics = {
    publishedPostsCount: number;
    followersCount: number;
    commentsLeftCount: number;
    likedPostsCount: number;
}

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