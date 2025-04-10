<script lang="ts">
    let { email } = $props<{ email: string }>();
</script>

<p class="dialog-title">Thank you for registering</p>
<img src="email-sent.svg" alt="email sent" class="unselectable"/>
<h1>
    Confirmation link has been sent to <span>{email}</span>
</h1>
<p class="second-p">Please check your inbox</p>

<style>
    h1 {
        text-align: center;
        font-size: 2.5rem;
    }
    h1 > span {
        color: var(--accent-main);
        font-weight: 600;
        text-decoration: underline;
    }
    img {
        height: 6rem;
    }
    .second-p {
        margin-top: 0.5rem;
        text-align: center;
        color: var(--gray);
        font-size: 1.5rem;
        font-weight: 500;
    }
</style>
