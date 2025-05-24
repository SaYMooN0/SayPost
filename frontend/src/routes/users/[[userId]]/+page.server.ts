import { ApiMain } from "../../../ts/backend-services";
import type { PlainErrType } from "../../../ts/common/errs/t-plain-err";
import type { PageServerLoad } from "./$types";
import type { UserProfile } from "./user-profile";

export const load: PageServerLoad = async ({ params, fetch }) : Promise<{ pageUser: UserProfile } | { errors: PlainErrType[] }> => {
    const { userId } = params;
    const response = await ApiMain.serverFetchJsonResponse<UserProfile>(fetch, `/users/${userId}/profile-data`, {
        method: 'GET',
    });

    if (!response.isSuccess) {
        return { errors: response.errors };
    }
    return { pageUser: response.data };
};