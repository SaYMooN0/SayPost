import { StringUtils } from "../utils/string-utils";

const UnspecifiedErrCode = 0;
export class Err {
    protected _message: string;
    protected _code: number;
    protected _details?: string;
    public constructor(message: string, code: number | undefined = undefined, details: string | undefined = undefined) {
        this._message = message;
        this._code = code ?? UnspecifiedErrCode;
        this._details = details;
    }

    public get message(): string {
        return this._message;
    }
    public get code(): number {
        return this._code;
    }
    public get details(): string | undefined {
        return this._details;
    }

    public static fromPlain(e: any): Err {
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
    }
}
export class ErrWithExtraData extends Err {
    private _extraData: Record<string, any>;
    public constructor(
        message: string,
        extraData: Record<string, any>,
        code: number | undefined = undefined,
        details: string | undefined = undefined
    ) {
        super(message, code, details);
        this._extraData = extraData;
    }
    public get ExtraData(): Record<string, any> {
        return this._extraData;
    }
};
export namespace Err {
    function isWithExtraData(err: Err | ErrWithExtraData): err is ErrWithExtraData {
        return 'extraData' in err;
    }
}