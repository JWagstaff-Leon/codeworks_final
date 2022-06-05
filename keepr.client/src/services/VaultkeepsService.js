import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultkeepsService
{
    async getUsersByKeepId(id)
    {
        const res = await api.get("api/keeps/" + id + "/vaults");
        logger.log("[VaultkeepsService > getUsersByKeepId > response]", res.data);
        AppState.activeVaultkeeps = res.data;
    }
    
    async create(vaultId, keepId)
    {
        const res = await api.post("api/vaultkeeps", { vaultId, keepId });
        logger.log("[VaultkeepsService > create > response]", res.data);
        AppState.activeVaultkeeps.push(res.data);
        AppState.activeKeep.kept += 1;
    }
}

export const vaultkeepsService = new VaultkeepsService();