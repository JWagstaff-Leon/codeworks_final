<template>
    <div class="my-3 rounded-3 position-relative action" @click="makeActive" :title="`Open details for ${keep.name}`">
        <img :src="keep.img" class="rounded-3 elevation-4 card-image" />
        <div class="position-absolute card-text d-flex justify-content-between align-items-end w-100 px-2">
            <h4 class="text-light m-0 fw-bold no-select">{{keep.name}}</h4>
            <img v-if="!isProfile" :src="keep.creator.picture" class="profile-image rounded-circle action" @click.stop="openProfile" :title="`Open ${keep.creator.name}'s profile`" />
        </div>
    </div>
</template>

<script>
import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService.js';
export default
{
    props:
    {
        keep:
        {
            type: Object,
            required: true
        },

        isProfile:
        {
            type: Boolean,
            default: false
        }
    },

    setup(props)
    {
        const router = useRouter();
        return {
            makeActive()
            {
                keepsService.setActive(props.keep);
            },

            openProfile()
            {
                router.push({name: "Profile", params: {id: props.keep.creator.id}});
            }
        }
    }
}
</script>

<style lang="scss" scoped>
.card-image
{
    object-fit: cover;
    max-height: 30rem;
    width: 100%;
}

.card-text
{
    bottom: 0px;
    padding-bottom: 0.5rem;
    padding-top: 0.5rem;
    background-color: rgba(0, 0, 0, 0.5);
    border-bottom-left-radius: 0.3rem;
    border-bottom-right-radius: 0.3rem;
}

.profile-image
{
    height: 2rem;
    width: 2rem;
    object-fit: cover;
}
</style>