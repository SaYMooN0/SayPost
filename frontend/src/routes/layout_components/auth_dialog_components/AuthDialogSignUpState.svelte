<script lang="ts">
    import DefaultErrBlock from "../../../components/err_blocks/DefaultErrBlock.svelte";
    import GrayLabelWithOnclick from "../../../components/GrayLabelWithOnclick.svelte";
    import { ApiAuth } from "../../../ts/backend-services";
    import { Err } from "../../../ts/common/errs/err";
    import { StringUtils } from "../../../ts/common/utils/string-utils";
    import AuthDialogInput from "./AuthDialogInput.svelte";

    let {
        email = $bindable(),
        changeStateToLogin,
        changeStateToEmailSent,
    } = $props<{
        email: string;
        changeStateToLogin: () => void;
        changeStateToEmailSent: () => void;
    }>();

    let password = $state<string>("");
    let confirmPassword = $state<string>("");
    let errList = $state<Err[]>([]);

    async function signupPressed() {
        errList = [];
        if (StringUtils.isNullOrWhiteSpace(email)) {
            errList.push(
                new Err(
                    "Email is required",
                    null,
                    "Fill the email input field",
                ),
            );
        } else if (!email.includes("@")) {
            errList.push(
                new Err("Invalid email", null, "Email must contain '@'"),
            );
        }
        if (StringUtils.isNullOrWhiteSpace(password)) {
            errList.push(new Err("Password is required"));
        }
        if (StringUtils.isNullOrWhiteSpace(confirmPassword)) {
            errList.push(new Err("Confirm Password is required"));
        } else if (password !== confirmPassword) {
            errList.push(
                new Err(
                    "Passwords don't match",
                    null,
                    "'Password' and 'Confirm Password' fields must match",
                ),
            );
        }
        if (errList.length > 0) {
            return;
        }
        const response = await ApiAuth.fetchJsonResponse<{ email: string }>(
            "/register",
            ApiAuth.requestJsonOptions({
                email,
                password,
                confirmPassword,
            }),
        );
        if (response.isSuccess) {
            changeStateToEmailSent(response.data.email);
        } else {
            errList = response.errors;
        }
    }
</script>

<p class="dialog-title">Create an account</p>
<AuthDialogInput type="email" fieldName="Email" bind:value={email}>
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
        <path
            d="M2 5L8.91302 8.92462C11.4387 10.3585 12.5613 10.3585 15.087 8.92462L22 5"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linejoin="round"
        />
        <path
            d="M10.5 19.5C10.0337 19.4939 9.56682 19.485 9.09883 19.4732C5.95033 19.3941 4.37608 19.3545 3.24496 18.2184C2.11383 17.0823 2.08114 15.5487 2.01577 12.4814C1.99475 11.4951 1.99474 10.5147 2.01576 9.52843C2.08114 6.46113 2.11382 4.92748 3.24495 3.79139C4.37608 2.6553 5.95033 2.61573 9.09882 2.53658C11.0393 2.4878 12.9607 2.48781 14.9012 2.53659C18.0497 2.61574 19.6239 2.65532 20.755 3.79141C21.8862 4.92749 21.9189 6.46114 21.9842 9.52844C21.9939 9.98251 21.9991 10.1965 21.9999 10.5"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M19 17C19 17.8284 18.3284 18.5 17.5 18.5C16.6716 18.5 16 17.8284 16 17C16 16.1716 16.6716 15.5 17.5 15.5C18.3284 15.5 19 16.1716 19 17ZM19 17V17.5C19 18.3284 19.6716 19 20.5 19C21.3284 19 22 18.3284 22 17.5V17C22 14.5147 19.9853 12.5 17.5 12.5C15.0147 12.5 13 14.5147 13 17C13 19.4853 15.0147 21.5 17.5 21.5"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
    </svg>
</AuthDialogInput>
<AuthDialogInput type="password" fieldName="Password" bind:value={password}>
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
        <path
            d="M4.26781 18.8447C4.49269 20.515 5.87613 21.8235 7.55966 21.9009C8.97627 21.966 10.4153 22 12 22C13.5847 22 15.0237 21.966 16.4403 21.9009C18.1239 21.8235 19.5073 20.515 19.7322 18.8447C19.879 17.7547 20 16.6376 20 15.5C20 14.3624 19.879 13.2453 19.7322 12.1553C19.5073 10.485 18.1239 9.17649 16.4403 9.09909C15.0237 9.03397 13.5847 9 12 9C10.4153 9 8.97627 9.03397 7.55966 9.09909C5.87613 9.17649 4.49269 10.485 4.26781 12.1553C4.12105 13.2453 4 14.3624 4 15.5C4 16.6376 4.12105 17.7547 4.26781 18.8447Z"
            stroke="currentColor"
            stroke-width="1.5"
        />
        <path
            d="M7.5 9V6.5C7.5 4.01472 9.51472 2 12 2C14.4853 2 16.5 4.01472 16.5 6.5V9"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M16 15.49V15.5"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M12 15.49V15.5"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M8 15.49V15.5"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
    </svg>
</AuthDialogInput>
<AuthDialogInput
    type="password"
    fieldName="Confirm Password"
    bind:value={confirmPassword}
>
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
        <path
            d="M15.395 21.9009C13.9836 21.966 12.5498 22 10.9709 22C9.39194 22 7.95815 21.966 6.5467 21.9009C4.8693 21.8235 3.4909 20.515 3.26684 18.8447C3.12061 17.7547 3 16.6376 3 15.5C3 14.3624 3.1206 13.2453 3.26684 12.1553C3.4909 10.485 4.8693 9.17649 6.5467 9.09909C7.95815 9.03397 9.39194 9 10.9709 9C12.5498 9 13.9836 9.03397 15.395 9.09909C16.4545 9.14798 17.3946 9.68796 18 10.5"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
        />
        <path
            d="M17 14.978C17 13.8856 17.8954 13 19 13C20.1046 13 21 13.8856 21 14.978C21 15.3718 20.8837 15.7387 20.6831 16.0469C20.0854 16.9656 19 17.8416 19 18.9341V19.4286M19 22H19.012"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M6.5 9V6.5C6.5 4.01472 8.51472 2 11 2C13.4853 2 15.5 4.01472 15.5 6.5V9"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M12 15.49V15.5"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M8 15.49V15.5"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
    </svg>
</AuthDialogInput>
<DefaultErrBlock {errList} />
<span></span>
<GrayLabelWithOnclick
    text={"I already have an account"}
    onClick={() => changeStateToLogin()}
/>
<button class="dialog-main-btn unselectable" onclick={() => signupPressed()}>
    Sign Up
</button>

<style>
    span {
        margin-top: auto;
    }
</style>
