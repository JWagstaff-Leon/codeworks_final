<template>
    <div class="my-3 rounded-3 position-relative action vault-card" @click="openVault" :title="`Go to vault ${vault.name} page`">
        <img :src="vault.img" class="rounded-3 elevation-4 card-image" />
        <div class="position-absolute card-text w-100 px-2">
            <h4 class="text-dark m-0 fw-bold no-select">{{vault.name}}</h4>
        </div>
    </div>
</template>

<script>
import { useRouter } from 'vue-router'
export default
{
    props:
    {
        vault:
        {
            type: Object,
            required: true
        }
    },

    setup(props)
    {
        const router = useRouter();
        return {
            openProfile()
            {
                router.push({name: "Vault", params: {id: props.vault.id}});
            }
        }
    }
}
</script>

<style lang="scss" scoped>
.vault-card
{
    height: 10rem;
    width: 10rem;
}

.card-image
{
    object-fit: cover;
    
    width: 100%;
}

.card-text
{
    bottom: 0.75rem;
}
</style>