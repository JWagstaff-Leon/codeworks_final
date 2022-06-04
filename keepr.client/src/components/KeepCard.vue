<template>
    <div class="my-4 rounded-3">
        <img :src="keep.img" class="rounded-3 elevation-4">
    </div>
</template>

<script>
export default
{
    props:
    {
        keep:
        {
            type: Object,
            required: true
        }
    },

    setup()
    {
        return {
            
        }
    }
}
</script>

<style lang="scss" scoped>
img
{
    object-fit: cover;
    max-height: 30rem;
    width: 100%;
}
</style>