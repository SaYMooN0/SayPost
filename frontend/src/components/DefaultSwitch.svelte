<script lang="ts">
    let {
        id = "",
        fieldName = "",
        isChecked = $bindable(),
    }: { id: string; fieldName: string; isChecked: boolean } = $props<{
        type?: string;
        fieldName?: string;
        isChecked: boolean;
    }>();
</script>

<div class="switch">
    <input type="checkbox" {id} name={fieldName} bind:checked={isChecked} />
    <span class="slider"></span>
</div>

<style>
    .switch {
        --button-width: 2.75em;
        --button-height: 1.75em;
        --toggle-diameter: 1.125em;
        --button-toggle-offset: calc(
            (var(--button-height) - var(--toggle-diameter)) / 2
        );

        display: flex;
        align-items: center;
        height: var(--button-height);
        background-color: var(--back-second);
    }

    .slider {
        position: relative;
        display: inline-block;
        width: var(--button-width);
        height: var(--button-height);
        border-radius: calc(var(--button-height) / 2);
        background-color: var(--back-second);
        transition: 0.3s all ease-in-out;
    }

    .slider::after {
        position: absolute;
        top: var(--button-toggle-offset);
        display: inline-block;
        width: var(--toggle-diameter);
        height: var(--toggle-diameter);
        border-radius: calc(var(--toggle-diameter) / 2);
        background-color: var(--back-main);
        transition: 0.3s all ease-in-out;
        transform: translateX(var(--button-toggle-offset));
        content: "";
    }

    .switch input[type="checkbox"]:checked + .slider {
        background-color: var(--primary);
    }

    .switch input[type="checkbox"]:checked + .slider::after {
        transform: translateX(
            calc(
                var(--button-width) - var(--toggle-diameter) -
                    var(--button-toggle-offset)
            )
        );
    }

    .switch input[type="checkbox"] {
        display: none;
    }
</style>
