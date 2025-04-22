import { ApiAuth } from "../ts/backend-services";
import { StringUtils } from "../ts/common/utils/string-utils";

export class AuthStoreData {
	private _username: string | null;
	private _userId: string | null;
	private _lastFetched: Date | null;
	constructor(
		username: string | null,
		userId: string | null,
		lastFetched: Date | null
	) {
		this._username = username;
		this._userId = userId;
		this._lastFetched = lastFetched;

	}
	public get Username(): string | null {
		return this._username;
	}
	public get UserId(): string | null {
		return this._userId;
	}
	public get LastFetched(): Date | null {
		return this._lastFetched;
	}

	public isAuthenticated(): boolean {
		return !StringUtils.isNullOrWhiteSpace(this.Username) &&
			!StringUtils.isNullOrWhiteSpace(this.UserId);
	}
	update(
		username: string | null, userId: string | null
	) {
		this._username = username;
		this._userId = userId;
		this._lastFetched = new Date();
	}
	setEmpty() {
		this._username = null;
		this._userId = null;
		this._lastFetched = new Date();
	}
}
const authStoreData = $state(
	new AuthStoreData("", "", new Date(1970, 0, 0))
);


async function fetchAuthData(): Promise<void> {
	const response = await ApiAuth.fetchJsonResponse<{ username: string, userId: string }>(
		"/ping",
		{ method: "POST" }
	);
	if (response.isSuccess) {
		authStoreData.update(response.data.username, response.data.userId);
	} else {
		authStoreData.setEmpty();

	}
}

export async function getAuthData(): Promise<AuthStoreData> {
	let lastFetched = authStoreData.LastFetched;
	const now = new Date();
	const two_minutes = 2 * 60 * 1000;

	if (!lastFetched || (now.getTime() - lastFetched.getTime() > two_minutes)) {
		await fetchAuthData();

	}

	return authStoreData;
}
export async function getAuthDataForced(): Promise<AuthStoreData> {
	await fetchAuthData();
	return authStoreData;
}

export async function logout() {
	const response = await fetch("/api/logout", { method: "POST" });
	authStoreData.setEmpty();
	if (response.ok) {
		window.location.href = "/auth/login";
	} else {
		location.reload();
	}

}

