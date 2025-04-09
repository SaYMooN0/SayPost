<script lang="ts">
    import BaseDialogWithCloseButton from "../../components/dialogs/BaseDialogWithCloseButton.svelte";
    import { getAuthData } from "../../stores/auth-store.svelte";
    import AuthDialogLoggedInState from "./auth_dialog_components/AuthDialogLoggedInState.svelte";
    import AuthDialogLoginState from "./auth_dialog_components/AuthDialogLoginState.svelte";
    import AuthDialogSignUpState from "./auth_dialog_components/AuthDialogSignUpState.svelte";

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
        dialogElement.open();
    }
    export async function openSignUp() {
        if ((await getAuthData()).isAuthenticated()) {
            contentState = DialogState.LoggedIn;
        } else {
            contentState = DialogState.SignUp;
        }
        dialogElement.open();
    }
    let dialogElement: BaseDialogWithCloseButton;
</script>

<BaseDialogWithCloseButton bind:this={dialogElement} dialogId={"auth-dialog"}>
    {#if contentState === DialogState.SignUp}
        <AuthDialogSignUpState
            changeStateToLogin={() => (contentState = DialogState.Login)}
        />
    {:else if contentState === DialogState.Login}
        <AuthDialogLoginState
            changeStateToSignUp={() => (contentState = DialogState.SignUp)}
        />
    {:else if contentState === DialogState.LoggedIn}
        <AuthDialogLoggedInState />
    {/if}
</BaseDialogWithCloseButton>

<style>
    :global(#auth-dialog > .dialog-content) {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 25rem;
        height: 28rem;
        padding: 0.125rem 1rem;
        border-radius: 0.75rem;
        background-color: var(--back-main);
    }

    :global(#auth-dialog .close-button) {
        top: 0.75rem !important;
        right: 0.75rem !important;
    }

    :global(#auth-dialog .dialog-title) {
        margin: 2rem 0;
        font-size: 2.25rem;
        font-weight: 600;
        align-self: center;
    }

    :global(#auth-dialog .dialog-main-btn) {
        width: 9rem;
        padding: 0.25rem 0;
        margin: 1rem 0;
        border: none;
        border-radius: 0.25rem;
        background-color: var(--accent-main);
        color: var(--back-main);
        font-size: 1.75rem;
        transition: all 0.12s ease-in;
        outline: none;
        box-sizing: border-box;
    }
    :global(#auth-dialog .dialog-main-btn:focus) {
        width: 9.5rem;
        background-color: var(--accent-hov);
        box-shadow: rgba(40, 33, 49, 0.5) 0px 1px 4px;
    }
    :global(#auth-dialog .dialog-main-btn:hover) {
        width: 10rem;
        background-color: var(--accent-hov);
    }

    :global(#auth-dialog .dialog-main-btn:active) {
        transform: scale(0.96);
    }
</style>
