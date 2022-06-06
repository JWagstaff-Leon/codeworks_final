<template>
    <div v-if="loading" class="w-100 h-100 flex-grow-1 align-items-center justify-content-center d-flex">
        <div class="spinner-border text-secondary"></div>
    </div>
    <div v-else>
        <div class="d-flex flex-column mx-5 mt-5">
            <h1 class="text-black">{{activeVault?.name}}</h1>
        </div>
    </div>
</template>

<script>
import { computed, ref } from "@vue/reactivity";
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import Loader from '../utils/Loader.js';
import { vaultsService } from '../services/VaultsService.js';
import { useRoute, useRouter } from 'vue-router';
import { keepsService } from '../services/KeepsService.js';
export default
{
    async mounted()
    {
        await this.mountedFunc();
    },

    setup()
    {
        const route = useRoute();
        const router = useRouter();
        const loading = ref(true);
        const resetPage = () => 
            {
                AppState.activeVault = null;
                AppState.activeKeeps = null;
            };
        return {
            route,
            loading,
            resetPage,
            activeVault: computed(() => AppState.activeVault),
            async mountedFunc()
            {
                try
                {
                    this.loading = true;
                    this.resetPage();
                    const loader = new Loader();
                    loader.step(vaultsService.getById, [this.route.params.id]);
                    loader.step(keepsService.getByVault, [this.route.params.id]);
                    await loader.load();
                    this.loading = false;
                }
                catch(error)
                {
                    this.loading = false;
                    logger.error("[Vault.vue > mountedFunc]", error.message);
                    router.push({name: "Home"});
                    await Pop.confirm("You do not have permission to view this vault", "You have been redirected to the home page", "info", "Okay", false);
                }
            }
        }
    }
}
</script>

<style lang="scss" scoped>
.spinner-border
{
    height: 15vh;
    width: 15vh;
}
</style>