<template>
    <div v-if="loading" class="w-100 h-100 flex-grow-1 align-items-center justify-content-center d-flex">
        <div class="spinner-border text-secondary"></div>
    </div>
    <div v-else>
        <div class="d-flex flex-column mx-3 mx-md-5">
            <div class="d-flex mt-5 align-items-center">
                <img :src="profile?.picture" :alt="'Profile picture of ' + profile.name" class="profile-image rounded-2">
                <div class="d-flex flex-column ms-3 ms-md-5 text-black">
                    <h1>{{profile?.name}}</h1>
                    <h3>Vaults: {{vaults?.length}}</h3>
                    <h3>Keeps: {{keeps?.length}}</h3>
                </div>
            </div>
            <h1 class="mt-5 text-black">Vaults <i v-if="isCurrentUser" role="button" class="mdi mdi-plus text-primary fs-2 action" title="Create new vault" @click="newItemModal(true)"></i></h1>
            <div class="d-flex flex-wrap vault-cards-container">
                <h1 v-if="vaults.length == 0" class="mt-3 mx-auto text-secondary">User has no vaults</h1>
                <h1 v-else-if="filteredVaults.length == 0" class="mt-3 mx-auto text-secondary">No vaults matching search</h1>
                <VaultCard v-for="v in filteredVaults" :key="v.id" :vault="v" />
            </div>
            <h1 class="mt-5 text-black">Keeps <i v-if="isCurrentUser" role="button" class="mdi mdi-plus text-primary fs-2 action" title="Create new keep" @click="newItemModal(false)"></i></h1>
                <h1 v-if="keeps.length == 0" class="mt-3 mx-auto text-secondary">User has no keeps</h1>
                <h1 v-else-if="filteredKeeps.length == 0" class="mt-3 mx-auto text-secondary">No keeps matching search</h1>
            <div class="masonry-with-columns">
                <KeepCard v-for="k in filteredKeeps" :key="k.id" :keep="k" :isProfile="true" />
            </div>
        </div>
        <KeepModal id="keep-modal" />
        <NewItemModal id="new-item-modal">
            <template #header>
                <h1 v-if="newItemIsVault" class="text-black">New Vault</h1>
                <h1 v-else class="text-black">New Keep</h1>
            </template>
            <template #body>
                <NewVaultForm v-if="newItemIsVault" />
                <NewKeepForm v-else />
            </template>
        </NewItemModal>        
    </div>
</template>

<script>
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { profilesService } from "../services/ProfilesService.js";
import { computed, ref } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import Loader from "../utils/Loader.js";
import { vaultsService } from '../services/VaultsService.js';
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

        'route.params.id'(newProfile)
        {
            if(newProfile)
            {
                this.resetPage();
                this.mountedFunc();
            }
        }
    },

    setup()
    {
        const route = useRoute();
        const loading = ref(true);
        const newItemIsVault = ref(false);
        const resetPage = () =>
            {
                AppState.activeProfile = null;
                AppState.activeVaults = null;
                AppState.activeKeeps = null;
                AppState.openModal = false;
            }
        return {
            route,
            loading,
            newItemIsVault,
            isCurrentUser: computed(() => AppState.account?.id === route.params.id),
            profile: computed(() => AppState.activeProfile),
            vaults: computed(() => AppState.activeVaults),
            filteredVaults: computed(() => AppState.activeVaults?.filter(vault => vault.name.toLowerCase().includes(AppState.searchTerm.toLowerCase()))),
            keeps: computed(() => AppState.activeKeeps),
            filteredKeeps: computed(() => AppState.activeKeeps?.filter(keep => keep.name.toLowerCase().includes(AppState.searchTerm.toLowerCase()))),
            openModal: computed(() => AppState.openModal),
            newItemModal(isVault)
            {
                newItemIsVault.value = isVault;
                Modal.getOrCreateInstance(document.getElementById("new-item-modal")).show();
            },
            resetPage,
            async mountedFunc()
            {
                try
                {
                    this.loading = true;
                    AppState.searchTerm = "";
                    this.resetPage();
                    const loader = new Loader();
                    loader.step(profilesService.getById, [this.route.params.id]);
                    loader.step(vaultsService.getByProfile, [this.route.params.id]);
                    loader.step(keepsService.getByProfile, [this.route.params.id]);
                    await loader.load();
                    this.loading = false;
                }
                catch(error)
                {
                    logger.error("[Profile.vue > mountedFunc]", error.message);
                    Pop.toast(error.message, "error");
                }
            }
        }
    }
}
</script>

<style lang="scss" scoped>
.profile-image
{
    height: 13rem;
    width: 13rem;
    object-fit: cover;
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

.spinner-border
{
    height: 15vh;
    width: 15vh;
}

.vault-cards-container
{
    justify-content: unset;
}

@media only screen and (max-width: 768px)
{
    .profile-image
    {
        height: 7.5rem;
        width: 7.5rem;
        object-fit: cover;
    }

    .vault-cards-container
    {
        justify-content: center;
    }
}
</style>