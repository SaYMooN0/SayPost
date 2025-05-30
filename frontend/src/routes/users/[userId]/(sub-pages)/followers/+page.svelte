<script lang="ts">
    import DefaultErrBlock from "../../../../../components/err_blocks/DefaultErrBlock.svelte";
    import ErrView from "../../../../../components/err_blocks/ErrView.svelte";
    import UserList from "../shared_components/UserList.svelte";
    import type { PageProps } from "./$types";

    let { data }: PageProps = $props();

</script>

{#if data.errors && data.errors.length > 0}
    <DefaultErrBlock errList={data.errors} />
{:else if data.users === undefined}
    <ErrView err={{ message: "Unable to load users followers" }} />
{:else}
    <UserList users={data.users} />
{/if}