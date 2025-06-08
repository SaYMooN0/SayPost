import { E as Err, D as DateUtils } from './date-utils-BMgSYFUH.js';

class BackendService {
  _baseUrl;
  constructor(baseUrl) {
    this._baseUrl = baseUrl;
  }
  requestJsonOptions(data, method = "POST") {
    return {
      method,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data)
    };
  }
  async fetchJsonResponse(url, options) {
    try {
      const response = await fetch(this._baseUrl + url, {
        ...options,
        credentials: "include"
      });
      console.log(response);
      if (response.ok) {
        const text = await response.text();
        const data = BackendService.parseWithDates(text);
        return { isSuccess: true, data };
      }
      const errors = await this.parseErrResponse(response);
      return { isSuccess: false, errors };
    } catch (e) {
      return {
        isSuccess: false,
        errors: [new Err("Unknown error", -1, "Error: " + e.message)]
      };
    }
  }
  async fetchVoidResponse(url, options) {
    try {
      const response = await fetch(this._baseUrl + url, {
        ...options,
        credentials: "include"
      });
      if (response.ok) {
        return { isSuccess: true };
      }
      const errors = await this.parseErrResponse(response);
      return { isSuccess: false, errors };
    } catch (e) {
      return {
        isSuccess: false,
        errors: [new Err("Unknown error", -1, "Error: " + e.message)]
      };
    }
  }
  async serverFetchJsonResponse(fetchFunc, url, options) {
    try {
      const response = await fetchFunc(this._baseUrl + url, {
        ...options,
        credentials: "include"
      });
      console.log(response);
      if (response.ok) {
        const text = await response.text();
        const data = BackendService.parseWithDates(text);
        return { isSuccess: true, data };
      }
      const contentType = response.headers.get("content-type");
      if (contentType?.includes("application/json")) {
        const json = await response.json();
        if (Array.isArray(json.errors)) {
          return {
            isSuccess: false,
            errors: json.errors
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
    } catch (e) {
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
  static parseWithDates(json) {
    return JSON.parse(json, (key, value) => {
      if (typeof value === "string" && DateUtils.isoDateRegex.test(value)) {
        return new Date(value);
      }
      return value;
    });
  }
  async parseErrResponse(response) {
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
const ApiAuth = new BackendService("/api/auth");
const ApiMain = new BackendService("/api/main");

export { ApiMain as A, ApiAuth as a };
//# sourceMappingURL=backend-services-gK9Cr6ja.js.map
