<script setup>
import { ref, watch } from 'vue';
import FilterIcon from '@/assets/FilterIcon.vue';
import DeleteFilterIcon from '@/assets/DeleteFilterIcon.vue';


const props = defineProps({
  startDate: String,
  endDate: String
})

const emit = defineEmits(["update:startDate","update:endDate","filter", "reset"])

const localStartDate = ref(props.startDate || '')
const localEndDate = ref(props.endDate || '')

watch(localStartDate, (date) => emit('update:startDate',date));
watch(localEndDate, (date) => emit('update:endDate',date));

watch(() => props.startDate, (value) => {
  localStartDate.value = value;
});
watch(() => props.endDate, (value) => {
  localEndDate.value = value;
});

const emitFilter = () => emit("filter");
const emitReset = () => emit("reset");

</script>

<template>
  <div class="flex flex-wrap items-center gap-2">
    <h2 class="text-lg font-bold">Filtrar</h2>

    <!-- Inputs de fecha -->
    <input
      type="date"
      v-model="localStartDate"
      class="border px-2 py-1 rounded"
    />
    <input
      type="date"
      v-model="localEndDate"
      class="border px-2 py-1 rounded"
    />

    <!-- Botón Filtrar -->
    <button
      @click="emitFilter"
      
      class="bg-white-200 text-white px-4 py-1 rounded-full hover:bg-gray-100 disabled:opacity-50 disabled:cursor-not-allowed"
    >
      <FilterIcon class="h-6 w-6" />
    </button>

    <!-- Botón Reset -->
    <button
      v-if="localStartDate || localEndDate"
      @click="emitReset"
      class="bg-red-300 text-white px-4 py-1 rounded-full hover:bg-red-200"
    >
      <DeleteFilterIcon class="h-6 w-6" />
    </button>
  </div>
</template>