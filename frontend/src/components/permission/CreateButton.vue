<script setup>
import { ref } from 'vue';
import { usePermissionStore } from '@/stores/permissionStore';
import ModalAddPermission from './ModalAddPermission.vue';


const show = ref(false)
const showCreateModal = ref(false)
const store = usePermissionStore()
const showError = ref(false)
const errorMessage = ref("")


const create = async (permission) =>{
    try {
        await store.insert(permission)
        showCreateModal.value = false
    } catch (error) {
        errorMessage.value = error.message
        showError.value = true
    }
}

</script>

<template>
 <button @click="showCreateModal = true" type="button"
        aria-controls="speed-dial-menu-text-inside-button" aria-expanded="false"
        class="flex items-center justify-center text-white bg-blue-700 rounded-full w-16 h-16 hover:bg-blue-800 dark:bg-blue-600 dark:hover:bg-blue-700 focus:ring-4 focus:ring-blue-300 focus:outline-none dark:focus:ring-blue-800">
        <svg class="w-5 h-5 transition-transform group-hover:rotate-45" aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 18 18">
            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M9 1v16M1 9h16" />
        </svg>
        <span class="sr-only">Open actions menu</span>
    </button>

    <ModalAddPermission :permission="null" :show="showCreateModal" @close="showCreateModal = false" @save="create" />

</template>