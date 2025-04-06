import { Err, ErrWithExtraData } from "./common/errs/err";


export type ResponseResult<T> =
    | { isSuccess: true; data: T }
    | { isSuccess: false; errors: Err[] };

class BackendService {
    private _baseUrl: string;
    constructor(baseUrl: string) {
        this._baseUrl = baseUrl;
    }
    public async jsonFetch<T>(url: string, options?: RequestInit): Promise<ResponseResult<T>> {
        try {
            const response = await fetch(this._baseUrl + url, options);

            if (response.ok) {
                const result = (await response.json()) as T;
                return { isSuccess: true, data: result };
            }

            //checking if response is of json type to parse errs
            if (response.headers.get("content-type")?.includes("application/json")) { 
                const json = await response.json();
                
                if (!Array.isArray(json.errors)) {
                    return {
                        isSuccess: false,
                        errors: [new Err("Invalid error format", -1, "Expected 'errors' array in response")]
                    };
                }

                const errArray: Err[] = json.errors.map((e: any) => {
                    if ("derivedErrType" in e) {
                        switch (e.derivedErrType) {
                            case "errWithExtraData":
                                return new ErrWithExtraData(e.message, e.extraData, e.code, e.details);
                            default:
                                throw new Error("Unknown error type: " + e.derivedErrType);
                        }
                    } else {
                        return new Err(e.message, e.code, e.details);
                    }
                });
                return { isSuccess: false, errors: errArray };
            } else {
                return {
                    isSuccess: false,
                    errors: [new Err("Unknown error", -1, "Response not in JSON format")]
                };
            }

        } catch (e: any) {
            console.log(e);
            return {
                isSuccess: false,
                errors: [new Err("Unknown error", -1, "Error: " + e.message)]
            };
        }
    }
}

export const ApiAuth = new BackendService('/api/auth');
export const ApiNotifications = new BackendService('/api/notifications');
export const ApiMain = new BackendService('/api/main');