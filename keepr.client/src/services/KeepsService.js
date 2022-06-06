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
        AppState.keeps = res.data;
    }
    
    async getById(id)
    {
        const res = await api.get("api/keeps/" + id);
        logger.log("[KeepsService > getById > response]", res.data);
        AppState.activeKeep = res.data;
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

    async create(data)
    {
        const res = await api.post("api/keeps", data);
        logger.log("[KeepsService > create > response]", res.data);
        AppState.activeKeeps.push(res.data);
        return res.data.id;
    }

    async remove(id)
    {
        const res = await api.delete("api/keeps/" + id);
        logger.log("[KeepsService > remove > response]");
        AppState.keeps = AppState.keeps.filter(keep => keep.id != res.data.id);
    }

    setActive(id)
    {
        AppState.openModal = true;
        this.getById(id);
        vaultkeepsService.getUsersByKeepId(id);
    }
}

export const keepsService = new KeepsService();