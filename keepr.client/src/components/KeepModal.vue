<template>
    <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div v-if="!activeKeep" class="w-100 d-flex flex-column">
                    <button type="button" class="btn-close align-self-end mt-2 me-2" data-bs-dismiss="modal" aria-label="Close" @click="clearActive"></button>
                    <div class="spinner-border text-secondary align-self-center"></div>
                </div>
                <div v-else class="modal-body d-flex">
                    <div class="w-50">
                        <img :src="activeKeep?.img" class="img-fluid" />
                    </div>
                    <div class="w-50 d-flex flex-column align-items-center px-5">
                        <button type="button" class="btn-close align-self-end" data-bs-dismiss="modal" aria-label="Close" @click="clearActive"></button>
                        <div class="d-flex justify-content-center">
                            <span class="mx-2 text-secondary fs-5 no-select" :title="`Viewed ${activeKeep.views} time${activeKeep.views == 1 ? '' : 's'}`"><i class="mdi mdi-eye text-primary me-1"></i>{{activeKeep?.views}}</span>
                            <span class="mx-2 text-secondary fs-5 no-select" :title="`Kept in ${activeKeep.kept} vault${activeKeep.kept == 1 ? '' : 's'}`"><img src="../assets/img/Kept.svg" class="me-1" />{{activeKeep?.kept}}</span>
                        </div>
                        <h1 class="mt-4 text-dark">{{activeKeep?.name}}</h1>
                        <p class="text-secondary mt-4 fs-5 text-start align-self-start mx-5 flex-grow-1">{{activeKeep.description}}</p>
                        <div class="d-flex justify-content-between align-items-center w-100">
                            <button class="btn btn-outline-primary fw-bold">ADD TO VAULT <i class="mdi mdi-menu-down"></i></button>
                            <i v-if="isUsersKeep" class="mdi mdi-delete-outline text-secondary delete-keep-button action fs-1" @click="deleteKeep"></i>
                            <div class="d-flex align-items-end">
                                <img :src="activeKeep?.creator.picture" class="profile-image rounded-2" />
                                <h5 class="text-black ms-3">{{activeKeep?.creator.name}}</h5>
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
import { keepsService } from '../services/KeepsService.js'
export default
{
    setup()
    {
        const activeKeep = computed(() => AppState.activeKeep);
        const clearActive = () => {
                AppState.openModal = false;
                setTimeout(() => {AppState.activeKeep = null;}, 150);                
            };
        return {
            activeKeep,
            isUsersKeep: computed(() => AppState.account.id === AppState.activeKeep?.creator.id),
            clearActive,
            async deleteKeep()
            {
                try
                {
                    if(await Pop.confirm())
                    {
                        keepsService.remmove(activeKeep.value?.id);
                        Pop.toast("Keep successfully deleted", "success");
                        clearActive();
                    }
                }
                catch(error)
                {
                    logger.error("[error prefix]", error.message);
                    Pop.toast(error.message, "error");
                }
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
</style>