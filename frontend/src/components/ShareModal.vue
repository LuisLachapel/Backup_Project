<script setup>
import { ref } from "vue";
import axios from "axios";
import { watch } from "vue";

const props = defineProps({
  show: { type: Boolean, default: false }
});

const emit = defineEmits(["close"]);

const to = ref("");
const subject = ref("");
const body = ref("");
const files = ref([]);


const handleFiles = (event) => {
  files.value = Array.from(event.target.files);
};

const removeFiles = (index) =>{
  const inputFile = document.getElementById("dropzone-file");
  const dataFiles = new DataTransfer();

  files.value.forEach((file,i) => { //Recorre los archivos agregados por el usuario
    if(i !== index) dataFiles.items.add(file);// aqui se copian todos los archivos menos el que recibes por parametros.

  });

   inputFile.files = dataFiles.files; //se copian los archivos del datafiles sin el archivo a eliminar en el input
  files.value = Array.from(dataFiles.files); // se actuliza la variables files con los archivos no eliminados
}

// Enviar correo
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

    await axios.post("https://localhost:7108/Notification/send", formData, {
      headers: { "Content-Type": "multipart/form-data" },
    });

    alert("Correo enviado correctamente ✅");
    
    emit("close");
  } catch (error) {
    console.error(error);
    alert("Error al enviar el correo ❌");
  }
};

const reset = () =>{
  to.value = "",
  subject.value = "";
  body.value = "";
  files.value = []
  const inputFile = document.getElementById("id");
  if(inputFile) inputFile.value = "";

}

watch(
  () => props.show,
(isShowed) =>{
  if(!isShowed){
    reset();
  }
})
</script>

<template>
  <!-- Main modal -->
  <div
    v-if="show"
    tabindex="-1"
    class="fixed inset-0 flex items-center overflow-y-auto overflow-x-hidden justify-center w-full max-h-full bg-black/50"
  >
    <div class="relative w-full max-w-lg max-h-full">
      <div class="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
        <!-- Boton de cerrar -->
        <button
          @click="emit('close')"
          type="button"
          class="absolute top-3 end-2.5 z-10 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
        >
          <svg
            class="w-3 h-3"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 14 14"
          >
            <path
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"
            />
          </svg>
          <span class="sr-only">Cerrar modal</span>
        </button>

        <!-- Header con inputs -->
        <div class="flex flex-col gap-2 p-4 border-b border-gray-200 pt-6">
          <h2 class="font-medium">Email</h2>
          <div>
            <label class="block mb-2 text-sm font-medium">Para:</label>
            <input
              v-model="to"
              type="email"
              required
              class="w-full p-2.5 border rounded-lg"
              placeholder="Ingrese el correo destinatario"
              maxlength="50"
            />
            <p class="text-sm text-gray-500">{{ to.length }}/50</p>
          </div>

          <div>
            <label class="block mb-2 text-sm font-medium">Asunto</label>
            <input
              v-model="subject"
              type="text"
              class="w-full p-2.5 border rounded-lg"
              placeholder="Asunto"
              maxlength="50"
            />
            <p class="text-sm text-gray-500">{{ subject.length }}/50</p>
          </div>

          <div>
            <label class="block mb-2 text-sm font-medium">Cuerpo</label>
            <textarea
              v-model="body"
              rows="4"
              class="w-full p-2.5 border rounded-lg"
              placeholder="Ingrese la descripción"
              maxlength="200"
            ></textarea>
            <p class="text-sm text-gray-500">{{ body.length }}/200</p>
          </div>
        </div>

        <!-- Dropzone -->
        <div class="p-4">
          <div class="flex items-center justify-center w-full">
            <label
              for="dropzone-file"
              class="flex flex-col items-center justify-center w-full h-64 border-2 border-gray-300 border-dashed rounded-lg cursor-pointer bg-gray-50 dark:hover:bg-gray-800 dark:bg-gray-700 hover:bg-gray-100 dark:border-gray-600 dark:hover:border-gray-500 dark:hover:bg-gray-600"
            >
              <div class="flex flex-col items-center justify-center pt-5 pb-6">
                <svg
                  class="w-8 h-8 mb-4 text-gray-500 dark:text-gray-400"
                  aria-hidden="true"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 20 16"
                >
                  <path
                    stroke="currentColor"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M13 13h3a3 3 0 0 0 0-6h-.025A5.56 5.56 0 0 0 16 6.5 5.5 5.5 0 0 0 5.207 5.021C5.137 5.017 5.071 5 5 5a4 4 0 0 0 0 8h2.167M10 15V6m0 0L8 8m2-2 2 2"
                  />
                </svg>
                <p class="mb-2 text-sm text-gray-500 dark:text-gray-400">
                  <span class="font-semibold">Click to upload</span> or drag and
                  drop
                </p>
                <p class="text-xs text-gray-500 dark:text-gray-400">
                  SVG, PNG, JPG, PDF, DOCX...
                </p>
              </div>
              <input
                id="dropzone-file"
                type="file"
                class="hidden"
                multiple
                @change="handleFiles"
              />
            </label>
          </div>

          <!-- Lista de archivos -->
          <ul v-if="files.length" class="mt-4 text-left text-sm">
            <li
              v-for="(file, index) in files"
              :key="index"
              class="flex justify-between items-center text-gray-700 border p-2 rounded mb-1"
            >
              <span>📎 {{ file.name }} ({{ (file.size / 1024).toFixed(1) }} KB)</span>
              <button
                @click="removeFiles(index)"
                class="ml-2 text-red-500 hover:text-red-700"
              >
                ❌
              </button>
            </li>
          </ul>
        </div>

        <!-- Footer con botón de enviar -->
        <div class="p-4 text-center">
          <button
            @click="sendEmail"
            class="px-6 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition"
          >
            Enviar
          </button>
        </div>
      </div>
    </div>
  </div>
</template>