<template>
    <div class="my-2 mx-3 rounded-3 position-relative action vault-card" @click="openVault" :title="`Go to vault ${vault.name}`">
        <img :src="vault.img" class="rounded-3 elevation-4 card-image" />
        <div class="position-absolute card-text w-100 px-2">
            <h4 class="text-light m-0 fw-bold no-select">{{vault.name}}</h4>
        </div>
        <i v-if="vault.isPrivate" class="position-absolute private-icon text-warning mdi mdi-lock"></i>
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
            openVault()
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
    height: 100%;
    width: 100%;
}

.card-text
{
    bottom: 0.75rem;
}

.private-icon
{
    top: 0.25rem;
    right: 0.25rem;
}

@media only screen and (max-width: 768px)
{
    .vault-card
    {
        height: 8rem;
        width: 80vw;
    }
}
</style>