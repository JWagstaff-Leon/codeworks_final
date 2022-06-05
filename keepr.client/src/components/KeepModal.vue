<template>
    <div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1">
        <div class="modal-dialog modal-xl modal-dialog-centered">
            <div class="modal-content">
                <div v-if="!activeKeep && !activeVaultkeeps" class="w-100 d-flex flex-column">
                    <button type="button" class="btn-close align-self-end mt-2 me-2" data-bs-dismiss="modal" aria-label="Close" @click="clearActive"></button>
                    <div class="spinner-border text-secondary align-self-center"></div>
                </div>
                <div v-else class="modal-body d-flex">
                    <div class="w-50 d-flex align-items-center justify-content-center">
                        <img :src="activeKeep?.img" class="img-fluid" />
                    </div>
                    <div class="w-50 d-flex flex-column align-items-center">
                        <button type="button" class="btn-close align-self-end" data-bs-dismiss="modal" aria-label="Close" @click="clearActive"></button>
                        <div class="d-flex justify-content-center px-5">
                            <span class="mx-2 text-secondary fs-5 no-select" :title="`Viewed ${activeKeep.views} time${activeKeep.views == 1 ? '' : 's'}`"><i class="mdi mdi-eye text-primary me-1"></i>{{activeKeep?.views}}</span>
                            <span class="mx-2 text-secondary fs-5 no-select" :title="`Kept in ${activeKeep.kept} vault${activeKeep.kept == 1 ? '' : 's'}`"><img src="../assets/img/Kept.svg" class="me-1" />{{activeKeep?.kept}}</span>
                        </div>
                        <h1 class="mt-4 text-dark px-5">{{activeKeep?.name}}</h1>
                        <p class="text-secondary mt-4 fs-5 text-start align-self-start mx-5 flex-grow-1 px-5">{{activeKeep.description}}</p>
                        <div class="d-flex align-items-center w-100 px-5" :class="{'justify-content-between': userAuthenticated, 'justify-content-end': !userAuthenticated}">
                            <div v-if="userAuthenticated" class="dropdown">
                                <button class="btn btn-outline-primary fw-bold dropdown-toggle" data-bs-toggle="dropdown">ADD TO VAULT</button>
                                <ul class="dropdown-menu">
                                    <li v-for="v in userVaults" :key="v.id" class="no-select" :class="{'selectable text-black': !activeVaultkeeps.find(vk => vk.vaultId == v.id), 'no-add text-secondary': activeVaultkeeps.find(vk => vk.vaultId == v.id)}" :title="activeVaultkeeps.find(vk => vk.vaultId == v.id) ? 'Keep is already in this vault' : ''" @click="addToVault(v.id, !activeVaultkeeps.find(vk => vk.vaultId == v.id))">{{v.name}}</li>
                                </ul>
                            </div>
                            <i v-if="isUsersKeep" class="mdi mdi-delete-outline text-secondary delete-keep-button action fs-1" @click="deleteKeep"></i>
                            <div class="d-flex align-items-end selectable p-1" @click="openProfile" :title="`Open ${activeKeep?.creator.name}'s profile`">
                                <img :src="activeKeep?.creator.picture" class="profile-image rounded-2" />
                                <h5 class="text-black ms-3 no-select">{{activeKeep?.creator.name}}</h5>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import Pop from '../utils/Pop.js'
import { keepsService } from '../services/KeepsService.js';
import { vaultkeepsService } from "../services/VaultkeepsService.js";
import { useRouter } from 'vue-router';
export default
{
    setup()
    {
        const activeKeep = computed(() => AppState.activeKeep);
        const clearActive = () => {
                AppState.openModal = false;
                setTimeout(() => { AppState.activeKeep = null; AppState.activeVaultkeeps = null; }, 150);                
            };
        const router = useRouter();
        return {
            activeKeep,
            activeVaultkeeps: computed(() => AppState.activeVaultkeeps),
            userAuthenticated: computed(() => AppState.user.isAuthenticated),
            isUsersKeep: computed(() => AppState.account.id === AppState.activeKeep?.creator.id),
            userVaults: computed(() => AppState.userVaults),
            clearActive,
            async deleteKeep()
            {
                try
                {
                    if(await Pop.confirm())
                    {
                        await keepsService.remmove(activeKeep.value?.id);
                        Pop.toast("Keep successfully deleted", "success");
                        clearActive();
                    }
                }
                catch(error)
                {
                    logger.error("[error prefix]", error.message);
                    Pop.toast(error.message, "error");
                }
            },
            async addToVault(vaultId, isValid)
            {
                if(isValid)
                {
                    await vaultkeepsService.create(vaultId, activeKeep.value?.id);
                    Pop.toast("Successfully added to vault", "success");
                }
            },
            openProfile()
            {
                clearActive();
                setTimeout(() => { router.push({name: "Profile", params: {id: activeKeep.value?.creator.id}}); }, 150);
            }
        }
    }
}
</script>

<style lang="scss" scoped>
@import "../assets/scss/variables.scss";

.spinner-border
{
    height: 15vh;
    width: 15vh;
    margin-top: 5vh;
    margin-bottom: 5vh;
}

.profile-image
{
    height: 2.5rem;
    width: 2.5rem;
}

.delete-keep-button
{
    transition: color 0.5s ease;
}

.delete-keep-button:hover
{
    color: $danger !important;
}

.no-add
{
    background-color: lightgrey;
    cursor: not-allowed;
}

.img-fluid
{
    max-height: 85vh;
}
</style>