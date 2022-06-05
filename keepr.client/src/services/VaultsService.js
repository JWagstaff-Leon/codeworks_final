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
}

export const vaultsService = new VaultsService();