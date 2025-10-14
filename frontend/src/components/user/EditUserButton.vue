<script setup>
import { ref } from 'vue';
import ModalAddUser from './ModalAddUser.vue';
import { useUserStore } from '@/stores/userStore';
import ErrorModal from '../ErrorModal.vue';
import EditIcon from '@/assets/EditIcon.vue';

const props = defineProps({
    user: {type: Object, required: true}
})

const store = useUserStore();
const showEditModal = ref(false)
const showError = ref(false)
const errorMessage = ref("")


const updateUser = async (user) => {
 try {
    await store.updateUser({...user, id: props.user.id})
    await store.getAllUser();
    showEditModal.value = false;
    
 } catch (error) {
    errorMessage.value = error.message
        showError.value = true
 }
}

</script>

<template>
  <div class="inline-block">
    
    <!-- Botón Editar -->
    <EditIcon @click="showEditModal = true" class="w-6 h-6"/>
    

    <!-- Modal para editar -->
    <ModalAddUser
      :user="props.user"
      :show="showEditModal"
      @close="showEditModal = false"
      @saved="updateUser"
    />


    <ErrorModal
  :show="showError" 
      :message="errorMessage" 
      @close="showError = false" 
  />
  </div>
</template>
