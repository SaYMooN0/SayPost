import { Err } from "./err";

export type PlainErrType = {
    message: string;
    code: number | null;
    details: string | null;
    derivedErrType?: string;
}

declare global {
	interface Array<T> {
		ToErrInstances(this: PlainErrType[]): Err[];
	}
}

Array.prototype.ToErrInstances = function (this: PlainErrType[]): Err[] {
	return this.map((e) => Err.fromPlain(e));
};
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
