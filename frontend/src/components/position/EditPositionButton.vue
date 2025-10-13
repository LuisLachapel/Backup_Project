<script setup>
import { usePositionStore } from '@/stores/positionStore';
import { ref } from 'vue';
import ErrorModal from '@/components/ErrorModal.vue';
import ModalAddPosition from './ModalAddPosition.vue';
import EditIcon from '@/assets/EditIcon.vue';


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
     <EditIcon class="w-6 h-6" @click="showEditModal = true" />

    <!-- Modal para editar -->
    <ModalAddPosition :position="props.position" :show="showEditModal" @close="showEditModal = false" @saved="updatePosition" />

    <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />

</template>