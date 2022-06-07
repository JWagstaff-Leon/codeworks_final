import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";
import { vaultkeepsService } from "./VaultkeepsService.js";

class KeepsService
{
    async getAll()
    {
        const res = await api.get("api/keeps");
        logger.log("[KeepsService > getAll > response]", res.data);
        AppState.activeKeeps = res.data;
    }
    
    async getById(id)
    {
        const res = await api.get("api/keeps/" + id);
        logger.log("[KeepsService > getById > response]", res.data);
        // AppState.activeKeep = res.data;
    }

    async getByProfile(id)
    {
        const res = await api.get("api/profiles/" + id + "/keeps");
        logger.log("[KeepsService > getByProfile > response]", res.data);
        AppState.activeKeeps = res.data;
    }

    async getByVault(id)
    {
        const res = await api.get("api/vaults/" + id + "/keeps");
        logger.log("[KeepsService > getByVault > response]", res.data);
        AppState.activeKeeps = res.data;
    }

    async create(data, shouldInsert = true)
    {
        const res = await api.post("api/keeps", data);
        logger.log("[KeepsService > create > response]", res.data);
        if(shouldInsert)
        {
            AppState.activeKeeps.push(res.data);
        }
        return res.data;
    }

    async remove(id)
    {
        const res = await api.delete("api/keeps/" + id);
        logger.log("[KeepsService > remove > response]");
        AppState.activeKeeps = AppState.activeKeeps.filter(keep => keep.id != res.data.id);
    }

    async setActive(keep)
    {
        AppState.openModal = true;
        await vaultkeepsService.getUsersByKeepId(keep.id);
        await this.getById(keep.id);
        keep.views += 1;
        AppState.activeKeep = keep;
    }
}

export const keepsService = new KeepsService();