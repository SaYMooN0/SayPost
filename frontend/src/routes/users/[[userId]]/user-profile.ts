export type UserProfile = {
    userId: string;
    profileBanner: UserProfileBanner;
}
export type UserProfileBanner = {
    scale: number;
    design: BannerDesign;
    DesignVariant: DesignVariant;
    colors: string[];
}
enum BannerDesign {
    Waves,
    BlobScene,
    LayeredPeaks,
    LayeredWaves,
    Steps
}
enum DesignVariant {
    Var1,
    Var2,
    Var3,
    Var4,
    Var5
}
//is following