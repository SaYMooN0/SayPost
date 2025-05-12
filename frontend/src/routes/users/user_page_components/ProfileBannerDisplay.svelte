<script lang="ts">
    import type { UserProfileBanner } from "../[[userId]]/user-profile";
    import type { Snippet } from "svelte";

    const {
        children,
        bannerData,
    }: { children: Snippet; bannerData: UserProfileBanner } = $props<{
        children: Snippet;
        bannerData: UserProfileBanner;
    }>();
</script>

<svg
    class="banner"
    viewBox="0 0 600 120"
    xmlns="http://www.w3.org/2000/svg"
    xmlns:xlink="http://www.w3.org/1999/xlink"
    version="1.1"
>
    <rect x="0" y="0" width="600" height="120" fill={bannerData.colors[0]}>
    </rect>
    <g fill={bannerData.colors[1]}>
        <circle r="62" cx="153" cy="106"></circle>
        <circle r="46" cx="301" cy="33"></circle>
        <circle r="45" cx="521" cy="56"> </circle>
        <circle r="50" cx="35" cy="24"></circle>
        <circle r="54" cx="407" cy="112"></circle>
    </g>
    <div class="banner-item">
        {@render children()}
    </div>
</svg>

<style>
    .banner {
        position: relative;
        width: 40rem;
        aspect-ratio: 5/1;
    }
    .banner-item {
        position: absolute;
        right: 2rem;
        bottom: 2rem;
    }
</style>
