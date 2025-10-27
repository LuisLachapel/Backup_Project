<script setup>
import { ref, watch } from 'vue'
import FilterIcon from '@/assets/FilterIcon.vue'
import DeleteFilterIcon from '@/assets/DeleteFilterIcon.vue'

const props = defineProps({
  startDate: String,
  endDate: String
})

const emit = defineEmits(['update:startDate', 'update:endDate', 'filter', 'reset'])

const localStartDate = ref(props.startDate || '')
const localEndDate = ref(props.endDate || '')

watch(localStartDate, date => emit('update:startDate', date))
watch(localEndDate, date => emit('update:endDate', date))

watch(() => props.startDate, value => (localStartDate.value = value))
watch(() => props.endDate, value => (localEndDate.value = value))

const emitFilter = () => emit('filter')
const emitReset = () => emit('reset')
</script>

<template>
  <div
    class="mb-6 rounded-lg border border-gray-200 bg-[#f6f7f8] p-4 shadow-sm dark:border-gray-700 dark:bg-gray-800"
  >
    <div
      class="grid grid-cols-1 gap-4 md:grid-cols-2 lg:flex lg:items-end lg:gap-4"
    >
      <!-- Fecha inicio -->
      <div class="w-full lg:flex-1">
        <label
          class="block text-sm font-medium text-gray-600 dark:text-gray-300"
          for="from-date"
        >
          Desde
        </label>
        <input
          id="from-date"
          type="date"
          v-model="localStartDate"
          class="mt-1 w-full rounded-md border border-gray-300 bg-white text-sm text-gray-700 
                 placeholder:text-gray-400 focus:border-blue-500 focus:ring-blue-500 
                 dark:border-gray-600 dark:bg-gray-700 dark:text-gray-100 dark:placeholder:text-gray-400"
        />
      </div>

      <!-- Fecha fin -->
      <div class="w-full lg:flex-1">
        <label
          class="block text-sm font-medium text-gray-600 dark:text-gray-300"
          for="to-date"
        >
          Hasta
        </label>
        <input
          id="to-date"
          type="date"
          v-model="localEndDate"
          class="mt-1 w-full rounded-md border border-gray-300 bg-white text-sm text-gray-700 
                 placeholder:text-gray-400 focus:border-blue-500 focus:ring-blue-500 
                 dark:border-gray-600 dark:bg-gray-700 dark:text-gray-100 dark:placeholder:text-gray-400"
        />
      </div>

      <!-- Botones -->
      <div class="flex gap-2 w-full lg:w-auto mt-2 lg:mt-0">
        <button
      @click="emitFilter"
      
      class="bg-white-200 text-white px-4 py-1 rounded-full hover:bg-gray-100 disabled:opacity-50 disabled:cursor-not-allowed"
    >
      <FilterIcon class="h-6 w-6" />
    </button>


        <button
      v-if="localStartDate || localEndDate"
      @click="emitReset"
      class="bg-red-300 text-white px-4 py-1 rounded-full hover:bg-red-200"
    >
      <DeleteFilterIcon class="h-6 w-6" />
    </button>
      </div>
    </div>
  </div>
</template>
