import { Err } from "./err";

export type PlainErrType = {
    message: string;
    code: number | null;
    details: string | null;
    derivedErrType?: string;
}

export class PlainErrUtils {
	static HasNonEmptyDetails(err: PlainErrType): boolean {
		return !!err.details && err.details.trim().length > 0;
	}

	static HasSpecifiedCode(err: PlainErrType): boolean {
		const UnspecifiedErrCode = 0; 
		return err.code != null && err.code !== UnspecifiedErrCode;
	}

	static HasSomethingExceptMessage(err: PlainErrType): boolean {
		return (
			PlainErrUtils.HasNonEmptyDetails(err) ||
			PlainErrUtils.HasSpecifiedCode(err)
		);
	}
}
