<script lang="ts">
    import type { ListContentItem } from "../../../../../../ts/common/post-content-item";

    export function getValue(): ListContentItem {
        return {
            $type: "ListContentItem",
            items: ["List item 1", "List item 2"]
        };
    }
</script>

<p>
    ListContentItem placeholder
</p>
