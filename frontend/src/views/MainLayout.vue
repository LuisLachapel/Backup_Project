<script setup>
import AsideBar from '@/components/AsideBar.vue';
import { ref } from 'vue';
import { RouterView } from 'vue-router';
import NavBar from '@/components/NavBar.vue';

const openSideBar = ref(false);

</script>


<template>
    <NavBar/> 
    <AsideBar v-model="openSideBar" />
    <div class="p-4 sm:ml-64 bg-[#f6f7f8] min-h-screen">
        <div class="mt-10 "> <!--p-4 border-2 border-gray-200 border-dashed rounded-lg dark:border-gray-700 mt-14-->
            <RouterView></RouterView>
        </div>
    </div>


</template>