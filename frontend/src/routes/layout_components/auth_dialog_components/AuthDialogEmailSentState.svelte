<script lang="ts">
    const { email } = $props<{ email: string }>();
</script>

<p class="dialog-title">Thank you for registering</p>
<img src="/email-sent.svg" alt="email sent" class="unselectable" />
<h1>
    Confirmation link has been sent to <span>{email}</span>
</h1>
<p class="second-p">Please check your inbox</p>

<style>
    h1 {
        font-size: 2.5rem;
        text-align: center;
    }

    h1 > span {
        color: var(--accent-main);
        font-weight: 600;
        text-decoration: underline;
        word-break: normal;
        overflow-wrap: anywhere;
    }

    img {
        height: 6rem;
    }

    .second-p {
        margin-top: 0.5rem;
        color: var(--gray);
        font-size: 1.5rem;
        font-weight: 500;
        text-align: center;
    }
</style>
