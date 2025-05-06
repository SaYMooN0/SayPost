export type HeadingContentItem = {
    $type: 'HeadingContentItem';
    value: string;
};

export type SubheadingContentItem = {
    $type: 'SubheadingContentItem';
    value: string;
};

export type ParagraphContentItem = {
    $type: 'ParagraphContentItem';
    value: string;
};

export type ListContentItem = {
    $type: 'ListContentItem';
    items: string[];
};

export type QuoteContentItem = {
    $type: 'QuoteContentItem';
    text: string;
    author: string | null;
};

export type PostContentItem =
    | HeadingContentItem
    | SubheadingContentItem
    | ParagraphContentItem
    | ListContentItem
    | QuoteContentItem;


export type PostContent ={
    items: PostContentItem[]
}
export function copyPostContentItem(item: PostContentItem): PostContentItem {
    switch (item.$type) {
        case 'HeadingContentItem':
        case 'SubheadingContentItem':
        case 'ParagraphContentItem':
        case 'QuoteContentItem':
            return { ...item };
        case 'ListContentItem':
            return {
                ...item,
                items: [...item.items],
            };
        default:
            throw new Error(`Unknown content item type: ${(item as any).$type}`);
    }
}
