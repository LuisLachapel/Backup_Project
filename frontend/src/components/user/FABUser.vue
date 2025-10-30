<script setup>
import { ref } from 'vue';
import ModalAddUser from './ModalAddUser.vue';
import { useUserStore } from '@/stores/userStore';
import ErrorModal from '../ErrorModal.vue';
import DownloadModal from '../DownloadModal.vue';
import ShareModal from '@/components/ShareModal.vue';

const show = ref(false)
const store = useUserStore();
const showCreateModal = ref(false)
const showDownlodadModal = ref(false)
const showShareModal = ref(false)
const showError = ref(false)
const errorMessage = ref("")

const CreateUser = async (user) => {
    try {
        await store.createUser(user)
        showCreateModal.value = false
    } catch (error) {
        errorMessage.value = error.message
        showError.value = true
    }
}


</script>


<template>


    <div @mouseenter="show = true" @mouseleave="show = false" data-dial-init class="fixed bottom-6 end-12 group">
        <div v-if="show"class="flex flex-col items-center  mb-4 space-y-2">
            
            <!--Enviar por email-->
            <button type="button" @click="showShareModal = true" 
                class="w-[68px] h-[68px] text-gray-500 bg-white rounded-full border border-gray-200 dark:border-gray-600 hover:text-gray-900 shadow-xs dark:hover:text-white dark:text-gray-400 hover:bg-gray-50 dark:bg-gray-700 dark:hover:bg-gray-600 focus:ring-4 focus:ring-gray-300 focus:outline-none dark:focus:ring-gray-400">
                <svg class="w-4 h-4 mx-auto mb-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg"
                    fill="currentColor" viewBox="0 0 18 18">
                    <path
                        d="M14.419 10.581a3.564 3.564 0 0 0-2.574 1.1l-4.756-2.49a3.54 3.54 0 0 0 .072-.71 3.55 3.55 0 0 0-.043-.428L11.67 6.1a3.56 3.56 0 1 0-.831-2.265c.006.143.02.286.043.428L6.33 6.218a3.573 3.573 0 1 0-.175 4.743l4.756 2.491a3.58 3.58 0 1 0 3.508-2.871Z" />
                </svg>
                <span class="block mb-px text-xs font-medium">Enviar</span>
            </button>

            <!--
            <button type="button"
                class="w-[68px] h-[68px] text-gray-500 bg-white rounded-full border border-gray-200 dark:border-gray-600 hover:text-gray-900 shadow-xs dark:hover:text-white dark:text-gray-400 hover:bg-gray-50 dark:bg-gray-700 dark:hover:bg-gray-600 focus:ring-4 focus:ring-gray-300 focus:outline-none dark:focus:ring-gray-400">
                <svg class="w-4 h-4 mx-auto mb-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg"
                    fill="currentColor" viewBox="0 0 20 20">
                    <path d="M5 20h10a1 1 0 0 0 1-1v-5H4v5a1 1 0 0 0 1 1Z" />
                    <path
                        d="M18 7H2a2 2 0 0 0-2 2v6a2 2 0 0 0 2 2v-3a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v3a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2Zm-1-2V2a2 2 0 0 0-2-2H5a2 2 0 0 0-2 2v3h14Z" />
                </svg>
                <span class="block mb-px text-xs font-medium">Print</span>
            </button>
            -->

            <!--Descargar-->
            <button type="button" @click="showDownlodadModal = true"
                class="w-[68px] h-[68px] text-gray-500 bg-white rounded-full border border-gray-200 dark:border-gray-600 hover:text-gray-900 shadow-xs dark:hover:text-white dark:text-gray-400 hover:bg-gray-50 dark:bg-gray-700 dark:hover:bg-gray-600 focus:ring-4 focus:ring-gray-300 focus:outline-none dark:focus:ring-gray-400">
                <svg class="w-4 h-4 mx-auto mb-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg"
                    fill="currentColor" viewBox="0 0 20 20">
                    <path
                        d="M14.707 7.793a1 1 0 0 0-1.414 0L11 10.086V1.5a1 1 0 0 0-2 0v8.586L6.707 7.793a1 1 0 1 0-1.414 1.414l4 4a1 1 0 0 0 1.416 0l4-4a1 1 0 0 0-.002-1.414Z" />
                    <path
                        d="M18 12h-2.55l-2.975 2.975a3.5 3.5 0 0 1-4.95 0L4.55 12H2a2 2 0 0 0-2 2v4a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2v-4a2 2 0 0 0-2-2Zm-3 5a1 1 0 1 1 0-2 1 1 0 0 1 0 2Z" />
                </svg>
                <span class="block mb-px text-xs font-medium">descargar</span>
            </button>

            <!--Crear posición
            <button type="button" @click="showCreateModal = true"
                class="w-[68px] h-[68px] text-gray-500 bg-white rounded-full border border-gray-200 dark:border-gray-600 hover:text-gray-900 shadow-xs dark:hover:text-white dark:text-gray-400 hover:bg-gray-50 dark:bg-gray-700 dark:hover:bg-gray-600 focus:ring-4 focus:ring-gray-300 focus:outline-none dark:focus:ring-gray-400">
                <svg class="w-5 h-5 mx-auto mb-1 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg"
                    width="24" height="24" fill="none" viewBox="0 0 24 24">
                    <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                        d="M15 9h3m-3 3h3m-3 3h3m-6 1c-.306-.613-.933-1-1.618-1H7.618c-.685 0-1.312.387-1.618 1M4 5h16a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1V6a1 1 0 0 1 1-1Zm7 5a2 2 0 1 1-4 0 2 2 0 0 1 4 0Z" />
                </svg>


                <span class="block mb-px text-xs font-medium">Cargo</span>
            </button>
            -->

            <!--Crear Usuario-->
            <button type="button" @click="showCreateModal = true"
                class="w-[68px] h-[68px] text-gray-500 bg-white rounded-full border border-gray-200 dark:border-gray-600 hover:text-gray-900 shadow-xs dark:hover:text-white dark:text-gray-400 hover:bg-gray-50 dark:bg-gray-700 dark:hover:bg-gray-600 focus:ring-4 focus:ring-gray-300 focus:outline-none dark:focus:ring-gray-400">
                <svg class="w-5 h-5 mx-auto mb-1 text-gray-800 dark:text-white" aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                    <path stroke="currentColor" stroke-linecap="square" stroke-linejoin="round" stroke-width="2"
                        d="M7 19H5a1 1 0 0 1-1-1v-1a3 3 0 0 1 3-3h1m4-6a3 3 0 1 1-6 0 3 3 0 0 1 6 0Zm7.441 1.559a1.907 1.907 0 0 1 0 2.698l-6.069 6.069L10 19l.674-3.372 6.07-6.07a1.907 1.907 0 0 1 2.697 0Z" />
                </svg>

                <span class="block mb-px text-xs font-medium">Crear</span>
            </button>


        </div>
        <button type="button"
            aria-controls="speed-dial-menu-text-inside-button" aria-expanded="false"
            class="flex items-center justify-center text-white bg-blue-700 rounded-full w-16 h-16 hover:bg-blue-800 dark:bg-blue-600 dark:hover:bg-blue-700 focus:ring-4 focus:ring-blue-300 focus:outline-none dark:focus:ring-blue-800">
            <svg class="w-5 h-5 transition-transform group-hover:rotate-45" aria-hidden="true"
                xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 18 18">
                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M9 1v16M1 9h16" />
            </svg>
            <span class="sr-only">Open actions menu</span>
        </button>
    </div>

    <ModalAddUser :user="null" :show="showCreateModal" @close="showCreateModal = false" @saved="CreateUser" />
    <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />
    <DownloadModal :show="showDownlodadModal" @close="showDownlodadModal = false"/>
    <ShareModal :show="showShareModal" @close="showShareModal = false"/>


</template>