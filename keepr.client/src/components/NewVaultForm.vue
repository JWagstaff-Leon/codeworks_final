<template>
    <form @submit.prevent="submitForm" class="d-flex flex-column">
        <label for="new-vault-title">Title</label>
        <input v-model="newData.name" name="new-vault-title" class="form-control mb-3" type="text" required placeholder="New Vault Title" />
        <label for="new-vault-image">Image Url</label>
        <input v-model="newData.img" name="new-vault-image" class="form-control mb-3" type="url" required placeholder="New Vault Image URL" />
        <label for="new-vault-description">Description</label>
        <textarea v-model="newData.description" name="new-vault-description" class="form-control mb-3" required placeholder="New Vault Description"></textarea>

        <div class="d-flex">
            <input v-model="newData.isPrivate" type="checkbox" name="new-vault-private" />
            <label for="new-vault-private" class="ms-1">Private?</label>
        </div>
        <small class="text-secondary">Private Vaults can only be seen by you</small>
        <button class="btn btn-primary align-self-end px-3 py-1" type="submit">Create</button>
    </form>
</template>

<script>
import { ref } from '@vue/reactivity';
import { vaultsService } from '../services/VaultsService.js';
import { useRouter } from 'vue-router';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
export default
{
    setup()
    {
        const newData = ref({isPrivate: false});
        const router = useRouter();
        return {
            newData,
            async submitForm()
            {
                try
                {
                    const newVaultId = await vaultsService.create(newData.value);
                    Modal.getOrCreateInstance(document.getElementById("new-item-modal")).hide();
                    router.push({name: "Vault", params: { id: newVaultId }});
                    Pop.toast("Vault successfully created", "success");
                }
                catch(error)
                {
                    logger.error("[NewVaultForm.vue > submitForm]", error.message);
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