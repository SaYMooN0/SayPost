<script lang="ts">
    import BaseDialog from "../components/BaseDialog.svelte";
    import { getAuthData } from "../stores/auth-store.svelte";

    enum DialogState {
        SignUp,
        Login,
        LoggedIn,
    }
    let contentState: DialogState = $state(DialogState.SignUp);
    export function close() {
        dialogElement.close();
    }
    export async function openLogin() {
        if ((await getAuthData()).isAuthenticated()) {
            contentState = DialogState.LoggedIn;
        } else {
            contentState = DialogState.Login;
        }
    }
    export async function openSignUp() {
        if ((await getAuthData()).isAuthenticated()) {
            contentState = DialogState.LoggedIn;
        } else {
            contentState = DialogState.SignUp;
        }
    }
    let dialogElement: BaseDialog;
</script>

<BaseDialog bind:this={dialogElement} dialogId={"auth-dialog"}>
    <div class="dialog-content">
        {#if contentState === DialogState.SignUp}
            <h2>Sign Up</h2>
        {:else if contentState === DialogState.Login}
            <h2>Login</h2>
        {:else if contentState === DialogState.LoggedIn}
            <h2>Already Logged In</h2>
        {/if}
    </div>
</BaseDialog>

<style>
    .dialog-content {
    }
</style>
