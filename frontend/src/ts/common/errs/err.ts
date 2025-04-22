import { StringUtils } from "../utils/string-utils";

const UnspecifiedErrCode = 0;
export class Err {
    protected _message: string;
    protected _code: number;
    protected _details: string | null;
    public constructor(message: string, code: number | null = null, details: string | null = null) {
        this._message = message;
        this._code = code ?? UnspecifiedErrCode;
        this._details = details;
    }

    public get Message(): string {
        return this._message;
    }
    public get Code(): number | null {
        return this._code;
    }
    public get Details(): string | null {
        return this._details;
    }
    public HasNonEmptyDetails(): boolean { return !StringUtils.isNullOrWhiteSpace(this._details); }
    public HasSpecifiedCode(): boolean { return this._code != UnspecifiedErrCode; }
    public HasSomethingExceptMessage(): boolean {
        return this.HasNonEmptyDetails() || this.HasSpecifiedCode();
    }
}
export class ErrWithExtraData extends Err {
    private _extraData: Record<string, any>;
    public constructor(message: string, extraData: Record<string, any>, code: number | null = null, details: string | null = null,) {
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