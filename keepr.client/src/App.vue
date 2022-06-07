<template>
  <header>
    <Navbar />
  </header>
  <main class="d-flex flex-column">
    <router-view />
  </main>
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
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState),
      newItemIsVault: computed(() => AppState.newItemIsVault)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

</style>
