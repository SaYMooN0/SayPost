<script lang="ts">
    import { DesignVariant } from "../../user-profile";

    let {
        designVariant,
        choose,
    }: {
        designVariant: DesignVariant;
        choose: (variant: DesignVariant) => void;
    } = $props<{
        designVariant: DesignVariant;
        choose: (variant: DesignVariant) => void;
    }>();
    const enumVal = DesignVariant[designVariant as keyof typeof DesignVariant];
</script>
