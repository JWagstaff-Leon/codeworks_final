<template>
  <span class="navbar-text">
    <button
      class="btn selectable text-seconary darken-20 fs-5 text-uppercase my-2 my-lg-0"
      @click="login"
      v-if="!user.isAuthenticated"
    >
      Login
    </button>

    <div class="dropdown my-2 my-lg-0" v-else>
      <div
        class="dropdown-toggle selectable bg-secondary text-light rounded"
        data-bs-toggle="dropdown"
        aria-expanded="false"
        id="authDropdown"
      >
        <div v-if="account.picture">
          <img
            :src="account.picture"
            alt="account photo"
            height="40"
            class="rounded"
          />
          <span class="mx-3 text-white fs-6 d-none d-md-inline">{{ account.name }}</span>
        </div>
      </div>
      <div
        class="dropdown-menu p-0 list-group w-100"
        aria-labelledby="authDropdown"
      >
        <div class="list-group-item list-group-item-action hoverable" @click="newItemModal(true)">
            <i class="mdi mdi-plus text-primary"></i> New Vault
        </div>
        <div class="list-group-item list-group-item-action hoverable" @click="newItemModal(false)">
            <i class="mdi mdi-plus text-primary"></i> New Keep
        </div>
        <router-link :to="{ name: 'Profile', params: {id: account.id} }">
          <div class="list-group-item list-group-item-action hoverable text-secondary">
            Go to my profile
          </div>
        </router-link>
        <div
          class="list-group-item list-group-item-action hoverable text-danger"
          @click="logout"
        >
          <i class="mdi mdi-logout"></i>
          logout
        </div>
      </div>
    </div>
  </span>
</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { AuthService } from "../services/AuthService";
import { Modal } from 'bootstrap';
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup();
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin });
      },
        newItemModal(isVault)
        {
            AppState.newItemIsVault = isVault;
            Modal.getOrCreateInstance(document.getElementById("new-item-modal")).show();
        }
    };
  },
};
</script>


<style lang="scss" scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}

@media only screen and (max-width: 768px)
{
    .dropdown-menu
    {
        right: -0.5rem !important;
        left: unset;
    }
}
</style>
