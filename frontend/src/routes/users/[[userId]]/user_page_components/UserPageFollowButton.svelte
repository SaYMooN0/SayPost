<script lang="ts">
    import { ApiFollowings } from "../../../../ts/backend-services";

    let {
        isFollowedByViewer,
        userId,
    }: { isFollowedByViewer: boolean; userId: string } = $props<{
        isFollowedByViewer: boolean;
        userId: string;
    }>();
    async function toggleFollow() {
        const url =
            `/users/${userId}/` + (isFollowedByViewer ? "unfollow" : "follow");
        const response = await ApiFollowings.fetchJsonResponse<{
            isFollowed: boolean;
        }>(url, {
            method: "PATCH",
        });
        if (response.isSuccess) {
            isFollowedByViewer = response.data.isFollowed;
        } else {
            console.error(response.errors);
        }
    }
</script>

<button class="follow-button" onclick={() => toggleFollow()}>
    {#if isFollowedByViewer}
        <span>Following</span>
    {:else}
        <span>Follow</span>
    {/if}
</button>
