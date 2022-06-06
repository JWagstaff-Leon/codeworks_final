<template>
    <div v-if="loading" class="w-100 h-100 flex-grow-1 align-items-center justify-content-center d-flex">
        <div class="spinner-border text-secondary"></div>
    </div>
    <div v-else >
        <div class="d-flex flex-column mx-3 mx-md-5 mt-5">
            <div class="d-flex justify-content-between">
                <h1 class="text-black">{{vault?.name}}</h1>
                <button v-if="isUsersVault" class="btn btn-outline-secondary my-auto" @click="deleteVault">Delete Vault</button>
            </div>
            <h6 v-if="vault" class="text-black">Keeps: {{keeps?.length}}</h6>
            <h1 class="mt-5 text-black">Keeps <i v-if="isCurrentUser" class="mdi mdi-plus text-primary fs-2 action" title="Create new keep" @click="newItemModal(false)"></i></h1>
            <h1 v-if="keeps?.length == 0" class="mt-3 mx-auto text-secondary">Vault has no keeps</h1>
            <h1 v-else-if="filteredKeeps?.length == 0" class="mt-3 mx-auto text-secondary">No keeps matching search</h1>
            <div class="masonry-with-columns">
                <KeepCard v-for="k in filteredKeeps" :key="k.id" :keep="k" :isProfile="true" />
            </div>
        </div>
    </div>
    <KeepModal id="keep-modal" />
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
import { Modal } from 'bootstrap';
export default
{
    async mounted()
    {
        await this.mountedFunc();
    },

    beforeUnmount()
    {
        this.resetPage();
    },

    watch:
    {
        openModal(openModal)
        {
            if(openModal)
            {
                Modal.getOrCreateInstance(document.getElementById("keep-modal")).show();
            }
            else
            {
                Modal.getOrCreateInstance(document.getElementById("keep-modal")).hide();
            }
        },

        'route.params.id'(newVault)
        {
            if(newVault && this.route.name == "Vault")
            {
                this.resetPage();
                this.mountedFunc();
            }
        }
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
                AppState.openModal = false;
            };
        return {
            route,
            loading,
            resetPage,
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.activeKeeps),
            filteredKeeps: computed(() => AppState.activeKeeps?.filter(keep => keep.name.toLowerCase().includes(AppState.searchTerm.toLowerCase()))),
            isUsersVault: computed(() => AppState.account.id === AppState.activeVault?.creatorId),
            openModal: computed(() => AppState.openModal),
            async mountedFunc()
            {
                try
                {
                    this.loading = true;
                    AppState.searchTerm = "";
                    this.resetPage();
                    const loader = new Loader();
                    logger.log("Loading")
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
            },
            async deleteVault()
            {
                try
                {
                    if(await Pop.confirm())
                    {
                        await vaultsService.remove(this.route.params.id);
                        router.push({name: "Home"});
                        Pop.toast("Vault successfully deleted", "success");
                    }
                }
                catch(error)
                {
                    logger.error("[Vault.vue > deleteVault]", error.message);
                    Pop.toast(error.message, "error");
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

.masonry-with-columns {
  columns: 6 150px;
  column-gap: 1.75rem;
  div {
    width: 125px;
    display: inline-block;
    width: 100%;
  }
}
</style>