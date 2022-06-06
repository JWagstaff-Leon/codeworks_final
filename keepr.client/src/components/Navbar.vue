<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-primary px-3 justify-content-between flex-nowrap">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="../assets/img/Logo.svg" height="45" />
      </div>
    </router-link>
      <!-- LOGIN COMPONENT HERE -->
        <Search class="flex-grow-1" />
      <Login class="ms-3"/>
  </nav>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
