<template>
    <div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header ms-2">
                    <slot name="header"></slot>
                    <i role="button" class="mdi mdi-close mdi-24px text-danger" data-bs-dismiss="modal" aria-label="Close"></i>
                </div>
                <div class="modal-body mx-2">
                    <slot name="body"></slot>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default
{
    setup()
    {
        return {
            
        }
    }
}
</script>

<style lang="scss" scoped>
.modal-header
{
    border: none !important;
}
</style>