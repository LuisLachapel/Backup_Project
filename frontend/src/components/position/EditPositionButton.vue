<script setup>
import { usePositionStore } from '@/stores/positionStore';
import { ref } from 'vue';
import ErrorModal from '@/components/ErrorModal.vue';
import ModalAddPosition from './ModalAddPosition.vue';


const props = defineProps({
    position: { type: Object, required: true }
})

const store = usePositionStore()
const showEditModal = ref(false)
const showError = ref(false)
const errorMessage = ref("")


const updatePosition = async (position) => {
    try {
        await store.updatePosition({ ...position, id: props.position.id })
        await store.getAllPosition();
        showEditModal.value = false;

    } catch (error) {
        errorMessage.value = error.message
        showError.value = true
    }
}

</script>

<template>
    <!-- Botón Editar -->
    <svg @click="showEditModal = true" class="w-6 h-6 cursor-pointer text-gray-600 hover:text-yellow-600 inline-block"
        xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="m14.304 4.844 2.852 2.852M7 7H4v12h12v-4.5m2.409-9.91a2.017 2.017 0 0 1 0 2.853l-6.844 6.844L8 14l.713-3.565 6.844-6.844a2.015 2.015 0 0 1 2.852 0Z" />
    </svg>

    <!-- Modal para editar -->
    <ModalAddPosition :position="props.position" :show="showEditModal" @close="showEditModal = false" @saved="updatePosition" />

    <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />

</template>