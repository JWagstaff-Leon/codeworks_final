import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  userVaults: null,

  keeps: null,
  activeKeep: null,
  activeVaultkeeps: null,
  openModal: false,

  activeProfile: null,
  activeVaults: null,
  activeKeeps: null,

  activeVault: null
})
