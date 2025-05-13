export type UserProfile = {
    userId: string;
    profileBanner: UserProfileBanner;
}
export type UserProfileBanner = {
    scale: number;
    design: BannerDesign;
    designVariant: DesignVariant;
    colors: string[];
}
export enum BannerDesign {
    Waves = "waves",
    BlobScene = "blobScene",
    LayeredPeaks = "layeredPeaks",
    LayeredWaves = "layeredWaves",
    Steps = "steps"
}
export enum DesignVariant {
    Var1 = "var1",
    Var2 = "var2",
    Var3 = "var3",
    Var4 = "var4",
    Var5 = "var5"
}
//is following