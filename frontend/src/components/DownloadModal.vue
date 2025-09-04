<script setup>
import { ref, watch } from "vue";
import { useUserStore } from "@/stores/userStore";

const props = defineProps({
  show: { type: Boolean, default: false }
});

const emit = defineEmits(["close"]);
const store = useUserStore();

const users = ref([]);
const startDate = ref("");
const endDate = ref("");
const errorMessage = ref("");
const showError = ref(false);

async function getSummary(start = null, end = null) {
  try {
    users.value = await store.getSummary(start, end);
  } catch (err) {
    errorMessage.value = err.message;
    showError.value = true;
  }
}

const resetFilter = async () => {
  startDate.value = "";
  endDate.value = "";
  await getSummary();
};


watch(
  () => props.show,
  async (val) => {
    if (val) {
      await getSummary(); 
    }
  }
);
</script>


<template>
    

<!-- Main modal -->
<div v-if="show" tabindex="-1"  class="fixed inset-0 flex items-center overflow-y-auto overflow-x-hidden justify-center w-full  max-h-full bg-black/50">
    <div class="relative w-full max-w-lg max-h-full">
        <div class="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
            <!--Boton de cerrar-->
            <button @click="emit('close')" type="button" class="absolute top-3 end-2.5 z-10 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-hide="popup-modal">
                <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                    <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"/>
                </svg>
                <span class="sr-only">Cerrar modal</span>
            </button>
            <!-- Header con filtros -->
        <div class="flex flex-col gap-2 p-4 border-b border-gray-200 pt-6">
          <h2 class="text-lg font-semibold text-gray-700 dark:text-gray-200 mb-2">Filtros</h2>
          <div class="flex gap-2">
            <input
              type="date"
              v-model="startDate"
              class="border px-2 py-1 rounded w-1/2"
            />
            <input
              type="date"
              v-model="endDate"
              class="border px-2 py-1 rounded w-1/2"
            />
          </div>
          <div class="flex gap-2">
            <button
              @click="getSummary(startDate, endDate)" 
              class="bg-white-200 text-white px-4 py-1 rounded-full hover:bg-gray-100"
            >
              <svg class="h-6 w-6" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path opacity="0.4" d="M14.3206 19.0698C14.3206 19.6798 13.9205 20.4798 13.4105 20.7898L12.0005 21.6998C10.6905 22.5098 8.87054 21.5998 8.87054 19.9798V14.6298C8.87054 13.9198 8.47055 13.0098 8.06055 12.5098L4.22052 8.46976C3.71052 7.95976 3.31055 7.05977 3.31055 6.44977V4.12976C3.31055 2.91976 4.22057 2.00977 5.33057 2.00977H18.6705C19.7805 2.00977 20.6906 2.91975 20.6906 4.02975V6.24976C20.6906 7.05976 20.1805 8.06976 19.6805 8.56976" stroke="#292D32" stroke-width="1.5" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M16.0692 16.5201C17.8365 16.5201 19.2692 15.0874 19.2692 13.3201C19.2692 11.5528 17.8365 10.1201 16.0692 10.1201C14.3018 10.1201 12.8691 11.5528 12.8691 13.3201C12.8691 15.0874 14.3018 16.5201 16.0692 16.5201Z" stroke="#292D32" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M19.8691 17.1201L18.8691 16.1201" stroke="#292D32" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>
            </button>
            <!--resetear-->
            <button
              v-if="startDate || endDate"
              @click="resetFilter"
              class="bg-red-300 text-white px-4 py-1 rounded-full hover:bg-red-200"
            >
              <svg class="h-6 w-6" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M21.6309 14.75C21.6309 15.64 21.3809 16.48 20.9409 17.2C20.1209 18.58 18.6109 19.5 16.8809 19.5C15.1509 19.5 13.6409 18.57 12.8209 17.2C12.3809 16.49 12.1309 15.64 12.1309 14.75C12.1309 12.13 14.2609 10 16.8809 10C19.5009 10 21.6309 12.13 21.6309 14.75Z" stroke="#ff0000" stroke-width="1.5" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M18.1487 15.99L15.6387 13.48" stroke="#ff0000" stroke-width="1.5" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M18.1291 13.5098L15.6191 16.0198" stroke="#ff0000" stroke-width="1.5" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"></path> <path opacity="0.4" d="M20.6905 4.02002V6.23999C20.6905 7.04999 20.1805 8.06001 19.6805 8.57001L17.9205 10.12C17.5905 10.04 17.2405 10 16.8805 10C14.2605 10 12.1305 12.13 12.1305 14.75C12.1305 15.64 12.3805 16.48 12.8205 17.2C13.1905 17.82 13.7005 18.35 14.3205 18.73V19.07C14.3205 19.68 13.9205 20.49 13.4105 20.79L12.0005 21.7C10.6905 22.51 8.87055 21.6 8.87055 19.98V14.63C8.87055 13.92 8.46055 13.01 8.06055 12.51L4.22055 8.46997C3.72055 7.95997 3.31055 7.05001 3.31055 6.45001V4.12C3.31055 2.91 4.22055 2 5.33055 2H18.6705C19.7805 2 20.6905 2.91002 20.6905 4.02002Z" stroke="#ff0000" stroke-width="1.5" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>
            </button>
          </div>
        </div>
        <!-- Tabla con datos -->
      <div class="p-4">
        <div class="overflow-y-auto max-h-96 border rounded">
          <table class="min-w-full text-sm text-left text-gray-500 dark:text-gray-300 border">
            <thead class="bg-gray-100 dark:bg-gray-600 text-gray-700 dark:text-gray-200">
              <tr>
                <th class="px-4 py-2 border">ID</th>
                <th class="px-4 py-2 border">Nombre</th>
                <th class="px-4 py-2 border">Posición</th>
                <th class="px-4 py-2 border">Registros</th>
                <th class="px-4 py-2 border">Estado</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="user in users" :key="user.id"
                class="hover:bg-gray-50 dark:hover:bg-gray-600">
                <td class="px-4 py-2 border">{{ user.id }}</td>
                <td class="px-4 py-2 border">{{ user.name }}</td>
                <td class="px-4 py-2 border">{{ user.position }}</td>
                <td class="px-4 py-2 border">{{ user.records }}</td>
                <td class="px-4 py-2 border">
                  <span :class="user.status === 'activo' ? 'text-green-600 font-semibold' : 'text-red-600 font-semibold'">
                    {{ user.status }}
                  </span>
                </td>
              </tr>
              <tr v-if="users.length === 0">
                <td colspan="5" class="text-center py-3 text-gray-400">No hay datos disponibles</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <!--Footer de descargas-->
            <div class="p-4 md:p-5 text-center">
                <svg class="mx-auto mb-4 text-gray-400 w-12 h-12 dark:text-gray-200" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g id="Arrow / Arrow_Circle_Down"> <path id="Vector" d="M9 13L12 16M12 16L15 13M12 16V8M21 12C21 7.02944 16.9706 3 12 3C7.02944 3 3 7.02944 3 12C3 16.9706 7.02944 21 12 21C16.9706 21 21 16.9706 21 12Z" stroke="#a39f9f" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path> </g> </g></svg>
               <h3 class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">¿En que formato deseas hacer la descarga?</h3>
                <button @click="store.dowloadPdf(startDate,endDate)"  data-modal-hide="popup-modal" type="button" class=" mr-4 text-white bg-gray-50 hover:bg-gray-100 focus:ring-4 focus:outline-none dark:focus:ring-red-800 font-medium rounded-lg text-sm inline-flex items-center px-5 py-2.5 text-center">
                    <svg class="h-5" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 56 64" enable-background="new 0 0 56 64" xml:space="preserve" fill="#000000"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g> <path fill="#8C181A" d="M5.1,0C2.3,0,0,2.3,0,5.1v53.8C0,61.7,2.3,64,5.1,64h45.8c2.8,0,5.1-2.3,5.1-5.1V20.3L37.1,0H5.1z"></path> <path fill="#6B0D12" d="M56,20.4v1H43.2c0,0-6.3-1.3-6.1-6.7c0,0,0.2,5.7,6,5.7H56z"></path> <path opacity="0.5" fill="#FFFFFF" enable-background="new " d="M37.1,0v14.6c0,1.7,1.1,5.8,6.1,5.8H56L37.1,0z"></path> </g> <path fill="#FFFFFF" d="M14.9,49h-3.3v4.1c0,0.4-0.3,0.7-0.8,0.7c-0.4,0-0.7-0.3-0.7-0.7V42.9c0-0.6,0.5-1.1,1.1-1.1h3.7 c2.4,0,3.8,1.7,3.8,3.6C18.7,47.4,17.3,49,14.9,49z M14.8,43.1h-3.2v4.6h3.2c1.4,0,2.4-0.9,2.4-2.3C17.2,44,16.2,43.1,14.8,43.1z M25.2,53.8h-3c-0.6,0-1.1-0.5-1.1-1.1v-9.8c0-0.6,0.5-1.1,1.1-1.1h3c3.7,0,6.2,2.6,6.2,6C31.4,51.2,29,53.8,25.2,53.8z M25.2,43.1 h-2.6v9.3h2.6c2.9,0,4.6-2.1,4.6-4.7C29.9,45.2,28.2,43.1,25.2,43.1z M41.5,43.1h-5.8V47h5.7c0.4,0,0.6,0.3,0.6,0.7 s-0.3,0.6-0.6,0.6h-5.7v4.8c0,0.4-0.3,0.7-0.8,0.7c-0.4,0-0.7-0.3-0.7-0.7V42.9c0-0.6,0.5-1.1,1.1-1.1h6.2c0.4,0,0.6,0.3,0.6,0.7 C42.2,42.8,41.9,43.1,41.5,43.1z"></path> </g></svg>
                            <span class="flex-1 ms-3 text-black whitespace-nowrap">Pdf</span>

                </button>
                <button @click="store.downloadExcel(startDate,endDate)" data-modal-hide="popup-modal" type="button" class="text-white bg-gray-50 hover:bg-gray-100 focus:ring-4 focus:outline-none dark:focus:ring-red-800 font-medium rounded-lg text-sm inline-flex items-center px-5 py-2.5 text-center">
                    <svg class="h-5" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" fill="#000000"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"><defs><linearGradient id="a" x1="4.494" y1="-2092.086" x2="13.832" y2="-2075.914" gradientTransform="translate(0 2100)" gradientUnits="userSpaceOnUse"><stop offset="0" stop-color="#18884f"></stop><stop offset="0.5" stop-color="#117e43"></stop><stop offset="1" stop-color="#0b6631"></stop></linearGradient></defs><title>file_type_excel</title><path d="M19.581,15.35,8.512,13.4V27.809A1.192,1.192,0,0,0,9.705,29h19.1A1.192,1.192,0,0,0,30,27.809h0V22.5Z" style="fill:#185c37"></path><path d="M19.581,3H9.705A1.192,1.192,0,0,0,8.512,4.191h0V9.5L19.581,16l5.861,1.95L30,16V9.5Z" style="fill:#21a366"></path><path d="M8.512,9.5H19.581V16H8.512Z" style="fill:#107c41"></path><path d="M16.434,8.2H8.512V24.45h7.922a1.2,1.2,0,0,0,1.194-1.191V9.391A1.2,1.2,0,0,0,16.434,8.2Z" style="opacity:0.10000000149011612;isolation:isolate"></path><path d="M15.783,8.85H8.512V25.1h7.271a1.2,1.2,0,0,0,1.194-1.191V10.041A1.2,1.2,0,0,0,15.783,8.85Z" style="opacity:0.20000000298023224;isolation:isolate"></path><path d="M15.783,8.85H8.512V23.8h7.271a1.2,1.2,0,0,0,1.194-1.191V10.041A1.2,1.2,0,0,0,15.783,8.85Z" style="opacity:0.20000000298023224;isolation:isolate"></path><path d="M15.132,8.85H8.512V23.8h6.62a1.2,1.2,0,0,0,1.194-1.191V10.041A1.2,1.2,0,0,0,15.132,8.85Z" style="opacity:0.20000000298023224;isolation:isolate"></path><path d="M3.194,8.85H15.132a1.193,1.193,0,0,1,1.194,1.191V21.959a1.193,1.193,0,0,1-1.194,1.191H3.194A1.192,1.192,0,0,1,2,21.959V10.041A1.192,1.192,0,0,1,3.194,8.85Z" style="fill:url(#a)"></path><path d="M5.7,19.873l2.511-3.884-2.3-3.862H7.758L9.013,14.6c.116.234.2.408.238.524h.017c.082-.188.169-.369.26-.546l1.342-2.447h1.7l-2.359,3.84,2.419,3.905H10.821l-1.45-2.711A2.355,2.355,0,0,1,9.2,16.8H9.176a1.688,1.688,0,0,1-.168.351L7.515,19.873Z" style="fill:#fff"></path><path d="M28.806,3H19.581V9.5H30V4.191A1.192,1.192,0,0,0,28.806,3Z" style="fill:#33c481"></path><path d="M19.581,16H30v6.5H19.581Z" style="fill:#107c41"></path></g></svg>
                            <span class="flex-1 ms-3 text-black whitespace-nowrap">Excel</span>


                </button>
            </div>
        </div>
    </div>
</div>

</template>