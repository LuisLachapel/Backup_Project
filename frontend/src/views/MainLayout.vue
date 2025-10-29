<script setup>
import AsideBar from '@/components/AsideBar.vue'
import OpenNavBar from '@/components/OpenNavBar.vue'
import { ref } from 'vue'
import { RouterView } from 'vue-router'

const openSideBar = ref(false)
</script>


<template>
  <!-- Barra de navegación -->
  <OpenNavBar @toggle-sidebar="openSideBar = !openSideBar" />

  <!-- Aside -->
  <AsideBar v-model="openSideBar" />

  <!-- Contenido principal -->
  <div class="p-4 sm:ml-64 bg-[#f6f7f8] min-h-screen">
    <!-- Aumenta el margen para que no se solape con el nav fijo -->
    <div class="mt-16 sm:mt-20">
      <RouterView />
    </div>
  </div>
</template>

