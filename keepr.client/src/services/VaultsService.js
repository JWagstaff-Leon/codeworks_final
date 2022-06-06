import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService
{
    async getUserVaults()
    {
        const res = await api.get("account/vaults");
        logger.log("[VaultsService > getUserVaults > response]", res.data);
        AppState.userVaults = res.data;
    }

    async getByProfile(id)
    {
        const res = await api.get("api/profiles/" + id + "/vaults");
        logger.log("[VaultsService > getByProfile > response]", res.data);
        AppState.activeVaults = res.data;
    }

    async create(data)
    {
        const res = await api.post("api/vaults", data)
        logger.log("[VaultsService > create > response]", res.data);
        return res.data.id;
    }
}

export const vaultsService = new VaultsService();