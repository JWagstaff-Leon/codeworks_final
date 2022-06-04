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

    async remmove(id)
    {
        const res = await api.delete("api/keeps/" + id);
        logger.log("[KeepsService > remove > response]");
        AppState.keeps = AppState.keeps.filter(keep => keep.id != res.data.id);
    }
}

export const keepsService = new KeepsService();