export class NotificationsStoreItem {
    private _id: string;
    private _text: string;
    private _read: boolean;
    constructor(id: string, text: string, read: boolean) {
        this._id = id;
        this._text = text;
        this._read = read;
    }
    public get Read(): boolean {
        return this._read;
    }
}
export class NotificationsStoreData {
    private _notifications: NotificationsStoreItem[] = [];
    constructor() {
        this._notifications = [];
    }
    update(notifications: NotificationsStoreItem[]) {
        this._notifications = notifications;
    }
    setEmpty() {
        this._notifications = [];
    }
    anyUnread(): boolean {
        return this._notifications.some(n => !n.Read);
    }
}
const notificationStoreData = $state(new NotificationsStoreData());

// export async function getNotificationsData(): Promise<NotificationStoreData | Err> {
// }