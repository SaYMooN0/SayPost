<script lang="ts">
    let { userId, username }: { userId: string; username: string } = $props<{
        userId: string;
        username: string;
    }>();
</script>

<div class="item">
    {username}
</div>

<style>
</style>
