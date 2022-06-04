<template>
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

    watch:
    {
        openModal(openModal)
        {
            if(openModal)
            {
                Modal.getOrCreateInstance(document.getElementById("keep-modal")).show();
            }
        }
    },

    mounted()
    {
        try
        {
            keepsService.getAll();
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
            keeps: computed(() => AppState.keeps),
            openModal: computed(() => AppState.openModal)
        }
    }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 5 200px;
  column-gap: 1.75rem;
  div {
    width: 150px;
    display: inline-block;
    width: 100%;
  } 
  
}
</style>
