<template>
    <div class="d-flex flex-column">
        <div class="d-flex">
            <img :src="profile?.picture" alt="">
        </div>
        <div class="d-flex flex-wrap">
            <VaultCard v-for="v in vaults" :key="v.id" :vault="v" />
        </div>
        <div class="masonry-with-columns">
            <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
    </div>
</template>

<script>
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { profilesService } from "../services/ProfilesService.js";
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import Loader from "../utils/Loader.js";
import { vaultsService } from '../services/VaultsService.js';
import { keepsService } from '../services/KeepsService.js';
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
        }
        catch(error)
        {
            logger.error("[Profile.vue > mounted]", error.message);
            Pop.toast(error.message, "error");
        }
    },

    setup()
    {
        const route = useRoute();
        return {
            route,
            profile: computed(() => AppState.activeProfile),
            vaults: computed(() => AppState.activeVaults),
            keeps: computed(() => AppState.activeKeeps)
        }
    }
}
</script>

<style lang="scss" scoped>
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1.75rem;
  div {
    width: 150px;
    display: inline-block;
    width: 100%;
  }
}
</style>