<template>
    <div class="d-flex justify-content-center">
        <label for="search-bar" class="visually-hidden">Search</label>
        <div class="input-group w-50">
            <input name="serach-bar" type="text" placeholder="Search" class="form-control" v-model="searchTerm" @input="changeSearch"/>
            <span class="input-group-text"><i class="mdi mdi-magnify mdi-18px text-primary"></i></span>
        </div>

    </div>
</template>

<script>
import { ref } from '@vue/reactivity'
import { AppState } from '../AppState.js';
export default
{
    setup()
    {
        const searchTerm = ref("");
        return {
            searchTerm,
            changeSearch()
            {
                AppState.searchTerm = searchTerm.value;
            }
        }
    }
}
</script>

<style lang="scss" scoped>
.input-group-text, .form-control
{
    background: #f7f5f5 !important;
    border: none !important;
}

.form-control:focus
{
    box-shadow: none;
}

.btn
{
    box-shadow: none !important;
}
</style>