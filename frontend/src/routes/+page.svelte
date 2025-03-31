<script lang="ts">
    import { ApiMain } from "../ts/backend-services";

    let authMsg = $state("Loading...");
    let notificationsMsg = $state("Loading...");
    let mainMsg = $state("Loading...");
    async function fetchGreetings() {}
</script>

{#await ApiMain.jsonFetch<any>("/test-err-with-extra")}
    <h1>Waiting</h1>
{:then response}
    <h1>
        {JSON.stringify(response, null, 2)}
    </h1>
{:catch}
    <h2>Error</h2>
{/await}
