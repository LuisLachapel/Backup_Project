<script setup>
import { ref } from 'vue';
import { usePermissionStore } from '@/stores/permissionStore';
import ErrorModal from '@/components/ErrorModal.vue';
import ModalAddPermission from './ModalAddPermission.vue';
import EditIcon from '@/assets/EditIcon.vue';

const props = defineProps({
    permission: {type: Object, required: true}
})

const store = usePermissionStore()
const showEditModal = ref(false)
const showError = ref(false)
const errorMessage = ref("")

const update = async (permission) =>{
    try {
        await store.update({...permission,id:permission.id})
        await store.getPermissions();
        showEditModal.value = false
    } catch (error) {
        errorMessage.value = error.message;
        showError.value = true;
    }
}

</script>

<template>
    
     <!-- Botón Editar-->
      <EditIcon @click="showEditModal = true" class="w-6 h-6 "/>
    
    <!--Modal para edición-->
    <ModalAddPermission :permission="permission" :show="showEditModal" @close="showEditModal = false" @save="update"/>
    <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />
</template>