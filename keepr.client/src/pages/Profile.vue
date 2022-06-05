<template>
    <div v-if="loading" class="w-100 h-100 flex-grow-1 align-items-center justify-content-center d-flex">
        <div class="spinner-border text-secondary"></div>
    </div>
    <div v-else>
        <div class="d-flex flex-column mx-5">
            <div class="d-flex mt-5 align-items-center">
                <img :src="profile?.picture" class="profile-image rounded-2">
                <div class="d-flex flex-column ms-5 text-black">
                    <h1>{{profile?.name}}</h1>
                    <h3>Vaults: {{vaults?.length}}</h3>
                    <h3>Keeps: {{keeps?.length}}</h3>
                </div>
            </div>
            <h1 class="mt-5 text-black">Vaults <i v-if="isCurrentUser" class="mdi mdi-plus text-primary fs-2 action" title="Create new vault" @click="newItemModal(true)"></i></h1>
            <div class="d-flex flex-wrap">
                <VaultCard v-for="v in vaults" :key="v.id" :vault="v" />
            </div>
            <h1 class="mt-5 text-black">Keeps <i v-if="isCurrentUser" class="mdi mdi-plus text-primary fs-2 action" title="Create new keep" @click="newItemModal(false)"></i></h1>
            <div class="masonry-with-columns">
                <KeepCard v-for="k in keeps" :key="k.id" :keep="k" :isProfile="true" />
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
        try
        {
            const loader = new Loader();
            loader.step(profilesService.getById, [this.route.params.id]);
            loader.step(vaultsService.getByProfile, [this.route.params.id]);
            loader.step(keepsService.getByProfile, [this.route.params.id]);
            await loader.load();
            this.loading = false;
        }
        catch(error)
        {
            logger.error("[Profile.vue > mounted]", error.message);
            Pop.toast(error.message, "error");
        }
    },

    beforeUnmount()
    {
        AppState.activeProfile = null;
        AppState.activeVaults = null;
        AppState.activeKeeps = null;
        AppState.openModal = false;
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
        }
    },

    setup()
    {
        const route = useRoute();
        const loading = ref(true);
        const newItemIsVault = ref(false);
        return {
            route,
            loading,
            newItemIsVault,
            isCurrentUser: computed(() => AppState.account?.id === route.params.id),
            profile: computed(() => AppState.activeProfile),
            vaults: computed(() => AppState.activeVaults),
            keeps: computed(() => AppState.activeKeeps),
            openModal: computed(() => AppState.openModal),
            newItemModal(isVault)
            {
                newItemIsVault.value = isVault;
                Modal.getOrCreateInstance(document.getElementById("new-item-modal")).show();
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
  columns: 6 200px;
  column-gap: 1.75rem;
  div {
    width: 150px;
    display: inline-block;
    width: 100%;
  }
}

.spinner-border
{
    height: 15vh;
    width: 15vh;
}
</style>