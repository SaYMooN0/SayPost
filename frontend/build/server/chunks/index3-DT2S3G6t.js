import './client2-BT_TsMl5.js';
import { z as getContext } from './index2-DGUJ8ZVV.js';

function context() {
  return getContext("__request__");
}
const page$1 = {
  get data() {
    return context().page.data;
  },
  get error() {
    return context().page.error;
  },
  get params() {
    return context().page.params;
  },
  get status() {
    return context().page.status;
  }
};
const page = page$1;

export { page as p };
//# sourceMappingURL=index3-DT2S3G6t.js.map
