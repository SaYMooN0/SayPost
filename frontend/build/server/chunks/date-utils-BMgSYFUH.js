var StringUtils;
((StringUtils2) => {
  function isNullOrWhiteSpace(str) {
    return !str || str.trim().length === 0;
  }
  StringUtils2.isNullOrWhiteSpace = isNullOrWhiteSpace;
  function rndStr(length = 8) {
    const characters = "abcdefghijklmnopqrstuvwxyz0123456789";
    let result = "";
    for (let i = 0; i < length; i++) {
      const randomIndex = Math.floor(Math.random() * characters.length);
      result += characters[randomIndex];
    }
    return result;
  }
  StringUtils2.rndStr = rndStr;
  function rndStrWithPref(prefix, length = 8) {
    return prefix + rndStr(length);
  }
  StringUtils2.rndStrWithPref = rndStrWithPref;
})(StringUtils || (StringUtils = {}));
const UnspecifiedErrCode = 0;
class Err {
  _message;
  _code;
  _details;
  constructor(message, code = void 0, details = void 0) {
    this._message = message;
    this._code = code ?? UnspecifiedErrCode;
    this._details = details;
  }
  get message() {
    return this._message;
  }
  get code() {
    return this._code;
  }
  get details() {
    return this._details;
  }
  static fromPlain(e) {
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
class ErrWithExtraData extends Err {
  _extraData;
  constructor(message, extraData, code = void 0, details = void 0) {
    super(message, code, details);
    this._extraData = extraData;
  }
  get ExtraData() {
    return this._extraData;
  }
}
/* @__PURE__ */ ((Err2) => {
})(Err || (Err = {}));

class DateUtils {
  static toLocale(date) {
    const locale = navigator.language || "en-US";
    const timePart = date.toLocaleTimeString(locale, {
      hour: "2-digit",
      minute: "2-digit",
      hour12: false
    });
    const datePart = date.toLocaleDateString(locale, {
      day: "2-digit",
      month: "2-digit",
      year: "numeric"
    });
    return `${timePart} ${datePart}`;
  }
  static isoDateRegex = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:\.\d+)?(?:Z|[+-]\d{2}:\d{2})?$/;
}

export { DateUtils as D, Err as E, StringUtils as S };
//# sourceMappingURL=date-utils-BMgSYFUH.js.map
