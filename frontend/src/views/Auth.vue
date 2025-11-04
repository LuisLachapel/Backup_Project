<script setup>
import { ref, onMounted } from 'vue';
import { useUserStore } from '@/stores/userStore';
import { useSessionStore } from '@/stores/sessionStore';
import { useRouter } from 'vue-router';
import PasswordModal from '@/components/PasswordModal.vue';
import { nextTick } from 'vue'


const userStore = useUserStore();
const session = useSessionStore();
const router = useRouter();

const showModal = ref(false);
const selectedUser = ref(null);

onMounted(() => {
  userStore.getAllUser();
});

const selectUser = (user) => {
  selectedUser.value = user;
  showModal.value = true; // mostrar modal
};

const closeModal = () => {
  showModal.value = false;
  selectedUser.value = null;
};

// Lógica cuando se confirma la contraseña
const confirmPassword = async ({ user, password }) => {
  try {
    const response = await userStore.login(user, password);

    // ✅ Aquí response ya ES el JSON del backend
    if (response.statusCode === 200) {
      session.setUser(response.data);
  await nextTick();
  router.push({ name: 'home' });
    } else {
      alert(response.message || 'Error en el inicio de sesión.');
    }
  } catch (error) {
    alert(error.message || 'Contraseña incorrecta o error de conexión.');
  } finally {
    closeModal();
  }
};

</script>

<template>
  <div class="p-4">
    <h1 class="text-xl font-bold mb-4">Selecciona un usuario</h1>
    <section class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
      <div
        @click="selectUser(user)"
        v-for="user in userStore.users"
        :key="user.id"
        class="max-w-sm p-6 bg-white border border-gray-200 rounded-lg shadow-sm cursor-pointer hover:bg-gray-200"
      >
        <div class="flex items-center space-x-3">
          <svg
            class="w-8 h-8 text-gray-800 dark:text-white"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
          >
            <path
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M12 21a9 9 0 1 0 0-18 9 9 0 0 0 0 18Zm0 0a8.949 8.949 0 0 0 4.951-1.488A3.987 3.987 0 0 0 13 16h-2a3.987 3.987 0 0 0-3.951 3.512A8.948 8.948 0 0 0 12 21Zm3-11a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
            />
          </svg>
          <h2 class="text-xl font-bold">{{ user.name }}</h2>
        </div>
        <span class="text-gray-600 text-sm">{{ user.position }}</span>
      </div>
    </section>

    <!-- Modal de contraseña -->
    <PasswordModal
      :show="showModal"
      :user="selectedUser"
      @close="closeModal"
      @confirm="confirmPassword"
    />
  </div>
</template>

