import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService
{
    async getAll()
    {
        const res = await api.get("api/keeps");
        logger.log("[KeepsService > getAll > response]", res.data);
        AppState.keeps = res.data;
    }
    
    async getById(id)
    {
        const res = await api.get("api/keeps/" + id);
        logger.log("[KeepsService > getById > response]", res.data);
        AppState.activeKeep = res.data;
    }
}

export const keepsService = new KeepsService();