import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService
{
    async getAll()
    {
        const res = await api.get("api/keeps");
        logger.log("[KeepsService > getAll > response]", res.data);
        keeps = res.data;
    }
}

export const keepsService = new KeepsService();