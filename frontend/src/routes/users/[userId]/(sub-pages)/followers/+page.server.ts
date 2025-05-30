import type { PageServerLoad } from "../../$types";
import { ApiMain } from "../../../../../ts/backend-services";
import type { PlainErrType } from "../../../../../ts/common/errs/t-plain-err";

export const load: PageServerLoad = async ({ params, fetch }):
    Promise<
        | { title: string, users: { id: string, username: string }[] }
        | { title: string, errors: PlainErrType[] }
    > => {
    const { userId } = params;
    const response = await ApiMain.serverFetchJsonResponse<
        { users: { id: string, username: string }[] }
    >(
        fetch, `/users/${userId}/list-followers`, { method: 'GET', }
    );

    if (!response.isSuccess) {
        return { title: `followers`, errors: response.errors };
    }
    return { title: `followers`, users: response.data.users };
};