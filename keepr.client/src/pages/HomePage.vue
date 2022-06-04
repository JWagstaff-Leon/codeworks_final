<template>
  <div>
      
  </div>
</template>

<script>
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { keepsService } from "../services/KeepsService.js";
export default {
  name: 'Home',

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
  }
}
</script>

<style scoped lang="scss">

</style>
