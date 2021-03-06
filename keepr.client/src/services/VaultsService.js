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

    async getById(id)
    {
        const res = await api.get("api/vaults/" + id);
        logger.log("[VaultsService > getById > response]", res.data);
        AppState.activeVault = res.data;
    }

    async getByProfile(id)
    {
        const res = await api.get("api/profiles/" + id + "/vaults");
        logger.log("[VaultsService > getByProfile > response]", res.data);
        AppState.activeVaults = res.data;
    }

    async create(data)
    {
        const res = await api.post("api/vaults", data);
        logger.log("[VaultsService > create > response]", res.data);
        AppState.userVaults.push(res.data);
        return res.data.id;
    }

    async remove(id)
    {
        const res = await api.delete("api/vaults/" + id);
        logger.log("[VaultsService > remove > response]", res.data);
        AppState.userVaults = AppState.userVaults.filter(vault => vault.id !== res.data.id);
    }
}

export const vaultsService = new VaultsService();