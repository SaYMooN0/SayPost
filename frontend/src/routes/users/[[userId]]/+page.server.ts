import { ApiMain } from "../../../ts/backend-services";
import type { PageServerLoad } from "./$types";
import type { UserProfile } from "./user-profile";

export const load: PageServerLoad = async ({ params, fetch }) => {
    const { userId } = params;
    try {

        const response = await ApiMain.serverFetchJsonResponse<UserProfile>(fetch, `/users/${userId}`, {
            method: 'GET',
        });

        if (!response.isSuccess) {
            return { errors: response.errors };
        }
        return {
            pageUser: response.data
        };
    }
    catch {
        console.log("catch 2");
    }
};