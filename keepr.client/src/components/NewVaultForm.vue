<template>
    <form @submit.prevent="submitForm" class="d-flex flex-column">
        <label for="new-vault-title">Title</label>
        <input name="new-vault-title" class="form-control mb-3" type="text" required placeholder="New Vault Title" />
        <label for="new-vault-image">Image Url</label>
        <input name="new-vault-image" class="form-control mb-3" type="url" required placeholder="New Vault Image URL" />

        <div class="d-flex">
            <input type="checkbox" name="new-vault-private" />
            <label for="new-vault-private" class="ms-1">Private?</label>
        </div>
        <small class="text-secondary">Private Vaults can only be seen by you</small>
        <button class="btn btn-primary align-self-end px-3 py-1" type="submit">Create</button>
    </form>
</template>

<script>
export default
{
    setup()
    {
        const newData = ref({});
        return {
            async submitForm()
            {

            }
        }
    }
}
</script>

<style lang="scss" scoped>

</style>