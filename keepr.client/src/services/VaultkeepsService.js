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

    async remove(id)
    {
        const res = await api.delete("api/vaultkeeps/" + id);
        logger.log("[VaultkeepsService > remove > response]", res.data);
        AppState.activeKeeps = AppState.activeKeeps.filter(keep => keep.id != res.data.keepId);
    }
}

export const vaultkeepsService = new VaultkeepsService();