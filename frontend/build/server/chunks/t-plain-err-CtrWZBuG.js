import './date-utils-BMgSYFUH.js';

class PlainErrUtils {
  static HasNonEmptyDetails(err) {
    return !!err.details && err.details.trim().length > 0;
  }
  static HasSpecifiedCode(err) {
    const UnspecifiedErrCode = 0;
    return err.code != null && err.code !== UnspecifiedErrCode;
  }
  static HasSomethingExceptMessage(err) {
    return PlainErrUtils.HasNonEmptyDetails(err) || PlainErrUtils.HasSpecifiedCode(err);
  }
}

export { PlainErrUtils as P };
//# sourceMappingURL=t-plain-err-CtrWZBuG.js.map
