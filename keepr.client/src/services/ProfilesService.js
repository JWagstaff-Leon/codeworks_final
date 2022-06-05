import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class ProfilesService
{
    async getById(id)
    {
        const res = await api.get("api/profiles/" + id);
        logger.log("[ProfilesService > getById > response]", res.data);
        AppState.activeProfile = res.data;
    }
}

export const profilesService = new ProfilesService();