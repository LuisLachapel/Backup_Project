<script setup>
import { ref, watch } from 'vue'
import { usePermissionStore } from '@/stores/permissionStore'


const store = usePermissionStore()

const props = defineProps({
    selectedPermissions: {type: Array, default: () => []}
})

const emit = defineEmits(["update:selectedPermissions"]);


const isOpen = ref(false)
const checkedPermissions = ref([])

watch(
    () => props.selectedPermissions,
    (val) => {
        checkedPermissions.value = [...val]
    },
    {immediate: true}
)

const togglePermission = (id) => {
  if (checkedPermissions.value.includes(id)) {
    
    checkedPermissions.value = checkedPermissions.value.filter(p => p !== id)
  } else {
    checkedPermissions.value.push(id)
  }
  emit("update:selectedPermissions", checkedPermissions.value)
}

const toggleAccordion = () => {
    isOpen.value = !isOpen.value
}
</script>

<template>
  <section>
    <!-- Header del accordion -->
    <h2>
      <button type="button" 
        class="flex items-center justify-between w-full py-5 font-medium rtl:text-right 
               text-black-500 border-b border-gray-200 dark:border-gray-700 dark:text-gray-400 gap-3"
        @click="toggleAccordion">
        <span>Permisos</span>
        <svg class="w-3 h-3 shrink-0 transition-transform duration-200"
          :class="{ 'rotate-0': isOpen, 'rotate-180': !isOpen }"
          aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
          <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M9 5 5 1 1 5" />
        </svg>
      </button>
    </h2>

    <!-- Contenido -->
    <div class="transition-all duration-300"
      :class="isOpen ? 'max-h-screen overflow-visible' : 'max-h-0 overflow-hidden'">
      <div class="py-5 border-b border-gray-200 dark:border-gray-700 grid grid-cols-3 gap-4">
        <div v-for="permission in store.permissions" :key="permission.id"
          class="flex items-center mb-4 gap-2 relative group">

          <!-- Checkbox -->
          <input 
            :id="`perm-${permission.id}`" 
            type="checkbox"
            :checked="checkedPermissions.includes(permission.id)"
            @change="togglePermission(permission.id)"
            class="w-4 h-4" 
          />

          <!-- Label -->
          <label :for="`perm-${permission.id}`" class="text-sm font-medium">
            {{ permission.name }}
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