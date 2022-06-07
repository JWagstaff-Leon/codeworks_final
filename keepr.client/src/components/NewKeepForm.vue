<template>
    <form @submit.prevent="submitForm" class="d-flex flex-column">
        <label for="new-keep-title">Title</label>
        <input v-model="newData.name" name="new-keep-title" class="form-control mb-3" type="text" required placeholder="New Keep Title" />
        <label for="new-keep-image">Image Url</label>
        <input v-model="newData.img" name="new-keep-image" class="form-control mb-3" type="url" required placeholder="New Keep Image URL" />
        <label for="new-keep-description">Description</label>
        <textarea v-model="newData.description" name="new-keep-description" class="form-control mb-3" required placeholder="New Keep Description"></textarea>

        <button class="btn btn-primary align-self-end px-3 py-1" type="submit">Create</button>
    </form>
</template>

<script>
import { ref } from '@vue/reactivity';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
export default
{
    setup()
    {
        const newData = ref({});
        const route = useRoute();
        return {
            newData,
            async submitForm()
            {
                try
                {
                    const shouldInsert = route.name == "Home" || route.name == "Profile" && route.params.id == AppState.account.id;
                    const newKeep = await keepsService.create(newData.value, shouldInsert);
                    Modal.getOrCreateInstance(document.getElementById("new-item-modal")).hide();
                    keepsService.setActive(newKeep);
                    Pop.toast("Keep successfully created", "success");
                    newData.value = {};
                }
                catch(error)
                {
                    logger.error("[NewKeepForm.vue > submitForm]", error.message);
                    Pop.toast(error.message, "error");
                }
            }
        }
    }
}
</script>

<style lang="scss" scoped>
textarea
{
    resize: none;
}
</style>