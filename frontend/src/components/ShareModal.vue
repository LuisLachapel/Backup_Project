<script setup>
import { ref, watch } from "vue";
import ErrorModal from "./ErrorModal.vue";
import { useEmailStore } from "@/stores/emailStore";
import DeleteIcon from "@/assets/DeleteIcon.vue";
import CloudIcon from "@/assets/CloudIcon.vue";

const props = defineProps({
  show: { type: Boolean, default: false }
});

const store = useEmailStore();
const emit = defineEmits(["close"]);

const to = ref("");
const subject = ref("");
const body = ref("");
const files = ref([]);

const errorMessage = ref("");
const showError = ref(false);
const showSuccess = ref(false); // ✅ NUEVA bandera para el alert de éxito
const successMessage = ref(""); // ✅ Mensaje dinámico de éxito

const handleFiles = (event) => {
  files.value = Array.from(event.target.files);
};

const removeFiles = (index) => {
  const inputFile = document.getElementById("dropzone-file");
  const dataFiles = new DataTransfer();

  files.value.forEach((file, i) => {
    if (i !== index) dataFiles.items.add(file);
  });

  inputFile.files = dataFiles.files;
  files.value = Array.from(dataFiles.files);
};

const sendEmail = async () => {
  try {
    const formData = new FormData();
    formData.append("to", to.value);
    formData.append("subject", subject.value);
    formData.append("body", body.value);

    if (files.value.length > 0) {
      files.value.forEach((file) => {
        formData.append("attachments", file);
      });
    }

    const response = await store.sendEmail(formData);

    // ✅ Mostrar alerta de éxito
    successMessage.value = response.message || "¡Correo enviado correctamente!";
    showSuccess.value = true;

    // Ocultar el alert después de unos segundos
    setTimeout(() => {
      showSuccess.value = false;
      emit("close");
    }, 3000);

  } catch (error) {
    errorMessage.value = error.message;
    showError.value = true;
  }
};

const reset = () => {
  to.value = "";
  subject.value = "";
  body.value = "";
  files.value = [];
  const inputFile = document.getElementById("id");
  if (inputFile) inputFile.value = "";
};

watch(
  () => props.show,
  (isShowed) => {
    if (!isShowed) {
      reset();
    }
  }
);
</script>

<template>
  <div
    v-if="show"
    tabindex="-1"
    class="fixed inset-0 flex items-center justify-center overflow-y-auto overflow-x-hidden bg-black/50 z-50"
  >
    <div
      class="relative w-[95%] sm:w-[90%] md:w-[80%] lg:max-w-lg max-h-[90vh] my-4 overflow-y-auto rounded-lg bg-white shadow-sm dark:bg-gray-700"
    >
      <div class="relative p-4 sm:p-6">
        <button
          @click="emit('close')"
          type="button"
          class="absolute top-3 right-3 z-10 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
        >
          <DeleteIcon class="w-3 h-3" />
          <span class="sr-only">Cerrar modal</span>
        </button>

        <!-- ✅ ALERTA DE ÉXITO -->
        <div
          v-if="showSuccess"
          class="p-4 mb-4 text-sm text-green-800 rounded-lg bg-green-50 dark:bg-gray-800 dark:text-green-400"
          role="alert"
        >
          <span class="font-medium">✅ Éxito:</span> {{ successMessage }}
        </div>

        <!-- Header -->
        <div class="flex flex-col gap-3 border-b border-gray-200 pb-4">
          <h2 class="font-medium text-lg text-gray-800 dark:text-gray-100">Email</h2>

          <div>
            <label class="block mb-2 text-sm font-medium text-gray-700 dark:text-gray-300">
              Para:
            </label>
            <input
              v-model="to"
              type="email"
              required
              class="w-full p-2.5 border rounded-lg text-sm sm:text-base dark:bg-gray-800 dark:border-gray-600 dark:text-gray-200"
              placeholder="Ingrese el correo destinatario"
              maxlength="50"
            />
            <p class="text-xs sm:text-sm text-gray-500">{{ to.length }}/50</p>
          </div>

          <div>
            <label class="block mb-2 text-sm font-medium text-gray-700 dark:text-gray-300">
              Asunto
            </label>
            <input
              v-model="subject"
              type="text"
              class="w-full p-2.5 border rounded-lg text-sm sm:text-base dark:bg-gray-800 dark:border-gray-600 dark:text-gray-200"
              placeholder="Asunto"
              maxlength="50"
            />
            <p class="text-xs sm:text-sm text-gray-500">{{ subject.length }}/50</p>
          </div>

          <div>
            <label class="block mb-2 text-sm font-medium text-gray-700 dark:text-gray-300">
              Cuerpo
            </label>
            <textarea
              v-model="body"
              rows="4"
              class="w-full p-2.5 border rounded-lg text-sm sm:text-base dark:bg-gray-800 dark:border-gray-600 dark:text-gray-200"
              placeholder="Ingrese la descripción"
              maxlength="200"
            ></textarea>
            <p class="text-xs sm:text-sm text-gray-500">{{ body.length }}/200</p>
          </div>
        </div>

        <!-- Dropzone -->
        <div class="pt-4">
          <div class="flex items-center justify-center w-full">
            <label
              for="dropzone-file"
              class="flex flex-col items-center justify-center w-full h-48 sm:h-56 border-2 border-gray-300 border-dashed rounded-lg cursor-pointer bg-gray-50 hover:bg-gray-100 dark:bg-gray-700 dark:hover:bg-gray-600 dark:border-gray-600 dark:hover:border-gray-500"
            >
              <div class="flex flex-col items-center justify-center pt-5 pb-6">
                <CloudIcon class="w-8 h-8 mb-3 text-gray-500 dark:text-gray-400" />
                <p class="mb-1 text-sm text-gray-500 dark:text-gray-400">
                  <span class="font-semibold">Click para subir</span> o arrastra los archivos
                </p>
                <p class="text-xs text-gray-500 dark:text-gray-400">
                  SVG, PNG, JPG, PDF, DOCX...
                </p>
              </div>
              <input id="dropzone-file" type="file" class="hidden" multiple @change="handleFiles" />
            </label>
          </div>

          <ul v-if="files.length" class="mt-4 text-left text-sm">
            <li
              v-for="(file, index) in files"
              :key="index"
              class="flex justify-between items-center text-gray-700 border p-2 rounded mb-1 dark:text-gray-200 dark:border-gray-600"
            >
              <span>📎 {{ file.name }} ({{ (file.size / 1024).toFixed(1) }} KB)</span>
              <button @click="removeFiles(index)">
                <DeleteIcon class="w-4 h-4 ml-2 text-red-500 hover:text-red-700" />
              </button>
            </li>
          </ul>
        </div>

        <div class="pt-4 text-center">
          <button
            @click="sendEmail"
            class="px-6 py-2.5 sm:py-3 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition text-sm sm:text-base w-full sm:w-auto"
          >
            Enviar
          </button>
        </div>
      </div>
    </div>
  </div>

  <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />
</template>
