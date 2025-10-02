<script setup>
import { ref } from 'vue'
import { usePermissionStore } from '@/stores/permissionStore'

const store = usePermissionStore()
const isOpen = ref(false)

const toggleAccordion = () => {
    isOpen.value = !isOpen.value
}
</script>

<template>
    <section>
        <!-- Header del accordion -->
        <h2>
            <button type="button" class="flex items-center justify-between w-full py-5 font-medium rtl:text-right 
               text-gray-500 border-b border-gray-200 dark:border-gray-700 dark:text-gray-400 gap-3"
                @click="toggleAccordion">
                <span>Permisos</span>
                <svg class="w-3 h-3 shrink-0 transition-transform duration-200"
                    :class="{ 'rotate-0': isOpen, 'rotate-180': !isOpen }" aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
                    <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                        d="M9 5 5 1 1 5" />
                </svg>
            </button>
        </h2>

        <!-- Contenido del acordeón -->
        <div class="transition-all duration-300"
            :class="isOpen ? 'max-h-screen overflow-visible' : 'max-h-0 overflow-hidden'">
            <div class="py-5 border-b border-gray-200 dark:border-gray-700 
               grid grid-cols-3 gap-4">
                <!-- Lista de permisos -->
                <div v-for="permission in store.permissions" :key="permission.id"
                    class="flex items-center mb-4 gap-2 relative group">
                    <!-- Checkbox -->
                    <input :id="`perm-${permission.id}`" type="checkbox" class="w-4 h-4" />

                    <!-- Label con nombre e icono alineados -->
                    <label :for="`perm-${permission.id}`"
                        class="flex items-center gap-1 cursor-pointer text-sm font-medium whitespace-nowrap">
                        {{ permission.name }}
                        <svg class="w-4 h-4 text-gray-400" xmlns="http://www.w3.org/2000/svg" fill="currentColor"
                            viewBox="0 0 20 20">
                            <path d="M10 .5a9.5 9.5 0 1 0 9.5 9.5A9.51 9.51 0 0 0 10 .5ZM9.5 4a1.5 1.5 0 1 1 0 3 
         1.5 1.5 0 0 1 0-3ZM12 15H8a1 1 0 0 1 0-2h1v-3H8a1 1 0 0 1 0-2h2a1 1 0 0 
         1 1 1v4h1a1 1 0 0 1 0 2Z" />
                        </svg>
                    </label>


                    <!-- Tooltip -->
                    <div class="absolute left-5 bottom-full mb-2 hidden group-hover:block 
                   bg-gray-800 text-white text-xs rounded px-2 py-1 whitespace-nowrap z-50">
                        {{ permission.description }}
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>
