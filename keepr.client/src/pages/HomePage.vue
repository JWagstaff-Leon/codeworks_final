<template>
    <div v-if="!keeps" class="w-100 h-100 flex-grow-1 align-items-center justify-content-center d-flex">
        <div class="spinner-border text-secondary"></div>
    </div>
    <div class="mx-5">
        <div class="masonry-with-columns">
            <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
    </div>
    <KeepModal id="keep-modal" />
</template>

<script>
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { keepsService } from "../services/KeepsService.js";
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { Modal } from 'bootstrap';
export default {
  name: 'Home',

    beforeUnmount()
    {
        AppState.activeKeeps = null;
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

    async mounted()
    {
        try
        {
            AppState.searchTerm = "";
            AppState.activeKeeps = null;
            await keepsService.getAll();
        }
        catch(error)
        {
            logger.error("[HomePage.vue > mounted]", error.message);
            Pop.toast(error.message, "error");
        }
    },

    setup()
    {
        return {
            keeps: computed(() => AppState.activeKeeps?.filter(keep => keep.name.toLowerCase().includes(AppState.searchTerm.toLowerCase()))),
            openModal: computed(() => AppState.openModal)
        }
    }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 4 200px;
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
