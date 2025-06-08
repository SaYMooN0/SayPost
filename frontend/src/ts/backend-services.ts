import { Err, ErrWithExtraData } from "./common/errs/err";
import type { PlainErrType } from "./common/errs/t-plain-err";
import { DateUtils } from "./common/utils/date-utils";


export type ResponseResult<T> =
    | { isSuccess: true; data: T }
    | { isSuccess: false; errors: Err[] };
export type ResponseVoidResult =
    | { isSuccess: true }
    | { isSuccess: false; errors: Err[] };


export type ServerResponseResult<TData> =
    | { isSuccess: true; data: TData }
    | { isSuccess: false; errors: PlainErrType[] };

export type ServerResponseVoidResult =
    | { isSuccess: true }
    | { isSuccess: false; errors: PlainErrType[] };

class BackendService {
    private _baseUrl: string;
    constructor(baseUrl: string) {
        this._baseUrl = baseUrl;
    }
    public requestJsonOptions(data: any, method = "POST"): RequestInit {
        return {
            method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        }
    }
    public async fetchJsonResponse<T>(url: string, options: RequestInit): Promise<ResponseResult<T>> {
        try {
            const response = await fetch(this._baseUrl + url, {
                ...options,
                credentials: 'include'
            });
            if (response.ok) {
                const text = await response.text();
                const data = BackendService.parseWithDates<T>(text);
                return { isSuccess: true, data };
            }

            const errors = await this.parseErrResponse(response);
            return { isSuccess: false, errors };

        } catch (e: any) {
            return {
                isSuccess: false,
                errors: [new Err("Unknown error", -1, "Error: " + e.message)]
            };
        }
    }
    public async fetchVoidResponse(url: string, options: RequestInit): Promise<ResponseVoidResult> {
        try {

            const response = await fetch(this._baseUrl + url, {
                ...options,
                credentials: 'include'
            });
            if (response.ok) {
                return { isSuccess: true };
            }

            const errors = await this.parseErrResponse(response);
            return { isSuccess: false, errors };

        } catch (e: any) {

            return {
                isSuccess: false,
                errors: [new Err("Unknown error", -1, "Error: " + e.message)]
            };
        }
    }
    public async serverFetchJsonResponse<T>(
        fetchFunc: typeof fetch,
        url: string,
        options: RequestInit
    ): Promise<ServerResponseResult<T>> {
        try {
            const response = await fetchFunc(this._baseUrl + url, {
                ...options,
                credentials: 'include'
            });

            if (response.ok) {
                const text = await response.text();
                const data = BackendService.parseWithDates<T>(text);
                return { isSuccess: true, data };
            }

            const contentType = response.headers.get("content-type");
            if (contentType?.includes("application/json")) {
                const json = await response.json();
                if (Array.isArray(json.errors)) {
                    return {
                        isSuccess: false,
                        errors: json.errors as PlainErrType[]
                    };
                }
            }

            return {
                isSuccess: false,
                errors: [{
                    message: "Unknown error",
                    code: -1,
                    details: "Response was not valid JSON with an 'errors' array"
                }]
            };

        } catch (e: any) {
            return {
                isSuccess: false,
                errors: [{
                    message: "Unknown error",
                    code: -1,
                    details: "Exception: " + e.message
                }]
            };
        }
    }

    static parseWithDates<T>(json: string): T {
        return JSON.parse(json, (key, value) => {
            if (typeof value === 'string' && DateUtils.isoDateRegex.test(value)) {
                return new Date(value);
            }
            return value;
        });
    }


    private async parseErrResponse(response: Response): Promise<Err[]> {
        if (response.headers.get("content-type")?.includes("application/json")) {
            const json = await response.json();

            if (!Array.isArray(json.errors)) {
                return [new Err("Invalid error format", -1, "Expected 'errors' array in response")];
            }

            return json.errors.map(Err.fromPlain);
        }

        return [new Err("Unknown error", -1, "Response not in JSON format")];
    }

}

export const ApiAuth = new BackendService('/api/auth');
export const ApiNotifications = new BackendService('/api/notifications');
export const ApiMain = new BackendService('/api/main');
export const ApiFollowings = new BackendService('/api/followings');
