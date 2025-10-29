<script setup>
import { ref, computed, onMounted } from 'vue'
import { useSessionStore } from '@/stores/sessionStore'
import { useRouter } from 'vue-router'
import LogoutIcon from '@/assets/LogoutIcon.vue'

const session = useSessionStore()
const router = useRouter()

const isOpen = defineModel()

// 🔹 Cerrar aside cuando cambie de ruta (solo móvil)
router.afterEach(() => {
  if (window.innerWidth < 640) { // sm breakpoint de Tailwind
    isOpen.value = false
  }
})

const logout = () => {
  session.logout()
  router.push('/auth')
}
// 🧠 Iniciales del usuario
const userInitials = computed(() => {
  const name = session.currentUser?.name || ''
  const parts = name.trim().split(' ')
  if (parts.length === 0) return 'U'
  if (parts.length === 1) return parts[0].substring(0, 2).toUpperCase()
  return (parts[0][0] + parts[1][0]).toUpperCase()
})
</script>

<template>
  <!-- Sidebar -->
  <aside
    :class="[
      'fixed top-0 left-0 z-40 w-64 h-screen transition-transform bg-white dark:bg-gray-800 shadow-lg flex flex-col justify-between',
      isOpen ? 'translate-x-0' : '-translate-x-full sm:translate-x-0'
    ]"
  >
    <!-- Contenido principal -->
    <div>
      <!-- Logo o título -->
      <div class="flex items-center justify-center h-14 border-b border-gray-200 dark:border-gray-700">
        <h1 class="text-lg font-bold text-gray-700 dark:text-white">Backup System</h1>
      </div>

      <!-- Menú-->
      <nav class="flex flex-col p-4 space-y-2">
        <RouterLink :to="{
                        name: 'home'
                    }" active-class="bg-gray-100"
                        class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group">
                        <svg class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                            aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor"
                            viewBox="0 0 22 21">
                            <path
                                d="M16.975 11H10V4.025a1 1 0 0 0-1.066-.998 8.5 8.5 0 1 0 9.039 9.039.999.999 0 0 0-1-1.066h.002Z" />
                            <path
                                d="M12.5 0c-.157 0-.311.01-.565.027A1 1 0 0 0 11 1.02V10h8.975a1 1 0 0 0 1-.935c.013-.188.028-.374.028-.565A8.51 8.51 0 0 0 12.5 0Z" />
                        </svg>
                        <span class="ms-3">Principal</span>
                    </RouterLink>

         <RouterLink :to="{
                        name: 'notes'
                    }" active-class="bg-gray-100"
                        class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group">

                         <svg class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white" viewBox="0 0 16 16" version="1.1" xmlns="http://www.w3.org/2000/svg"
                            xmlns:xlink="http://www.w3.org/1999/xlink" fill="currentColor">
                            <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
                            <g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g>
                            <g id="SVGRepo_iconCarrier">
                                <path d="M4 9h4v2h-4v-2z"></path>
                                <path 
                                    d="M16 2h-1v-2h-10v2h-2v1.25l-0.6 0.75h-1.4v1.75l-1 1.25v9h12l4-5v-9zM2 5h8v2h-8v-2zM11 15h-10v-7h10v7zM12 7h-1v-3h-7v-1h8v4zM14 4.5l-1 1.25v-3.75h-7v-1h8v3.5z">
                                </path>
                            </g>
                        </svg>
                        
                        <span class="ms-3">Registros</span>
                    </RouterLink>

       <RouterLink :to="{ name: 'users' }" active-class="bg-gray-100"
                        class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group">
                        <svg class="shrink-0 w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                            aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor"
                            viewBox="0 0 20 18">
                            <path
                                d="M14 2a3.963 3.963 0 0 0-1.4.267 6.439 6.439 0 0 1-1.331 6.638A4 4 0 1 0 14 2Zm1 9h-1.264A6.957 6.957 0 0 1 15 15v2a2.97 2.97 0 0 1-.184 1H19a1 1 0 0 0 1-1v-1a5.006 5.006 0 0 0-5-5ZM6.5 9a4.5 4.5 0 1 0 0-9 4.5 4.5 0 0 0 0 9ZM8 10H5a5.006 5.006 0 0 0-5 5v2a1 1 0 0 0 1 1h11a1 1 0 0 0 1-1v-2a5.006 5.006 0 0 0-5-5Z" />
                        </svg>
                        <span class="ms-3">Usuarios</span>
                    </RouterLink>

        <RouterLink :to="{ name: 'position' }" active-class="bg-gray-100"
                        class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group">
                        <svg class="shrink-0 w-7 h-7 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                            fill="currentColor" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100"
                            enable-background="new 0 0 100 100" xml:space="preserve">
                            <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
                            <g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g>
                            <g id="SVGRepo_iconCarrier">
                                <path
                                    d="M37.3,31.9h21.8c1.1,0,2-0.9,2-2v-4c0-3.3-2.7-5.9-5.9-5.9H41.3c-3.3,0-5.9,2.7-5.9,5.9v4 C35.3,31,36.2,31.9,37.3,31.9z">
                                </path>
                                <path
                                    d="M70,24.9h-2c-0.6,0-1,0.4-1,1v4c0,4.4-3.6,7.9-7.9,7.9H37.3c-4.4,0-7.9-3.6-7.9-7.9v-4c0-0.6-0.4-1-1-1h-2 c-3.3,0-5.9,2.7-5.9,5.9v40.6c0,3.3,2.7,5.9,5.9,5.9h20c2.8,0,3.1-2.3,3.1-3.1V52.8c0-2.3,1.3-2.8,2-2.8h21.6c2.4,0,2.8-2.1,2.8-2.8 V31C76,27.6,73.3,24.9,70,24.9z">
                                </path>
                                <path
                                    d="M78.4,60.4H56.6c-0.6,0-1.1-0.5-1.1-1.1v-2.2c0-0.6,0.5-1.1,1.1-1.1h21.8c0.6,0,1.1,0.5,1.1,1.1v2.2 C79.5,59.9,79,60.4,78.4,60.4z M78.4,70.2H56.6c-0.6,0-1.1-0.5-1.1-1.1v-2.2c0-0.6,0.5-1.1,1.1-1.1h21.8c0.6,0,1.1,0.5,1.1,1.1v2.2 C79.5,69.7,79,70.2,78.4,70.2z M78.4,80H56.6c-0.6,0-1.1-0.5-1.1-1.1v-2.2c0-0.6,0.5-1.1,1.1-1.1h21.8c0.6,0,1.1,0.5,1.1,1.1v2.2 C79.5,79.5,79,80,78.4,80z">
                                </path>
                            </g>
                        </svg>
                        <span class="ms-3">Roles</span>
                    </RouterLink>

                    <RouterLink :to="{ name: 'permission' }" active-class="bg-gray-100"
                        class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group">
                        <svg class="shrink-0 w-6 h-6 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
                            fill="currentColor" viewBox="-5 -2 24 24" xmlns="http://www.w3.org/2000/svg" width="24"
                            height="24" preserveAspectRatio="xMinYMin" stroke="currentColor">
                            <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
                            <g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g>
                            <g id="SVGRepo_iconCarrier">
                                <path
                                    d="M12 5h-2a3 3 0 1 0-6 0v5h8a2 2 0 0 1 2 2v6a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-6a2 2 0 0 1 2-2V5a5 5 0 1 1 10 0zM7 17a2 2 0 1 0 0-4 2 2 0 0 0 0 4z">
                                </path>
                            </g>
                        </svg>
                        <span class="ms-3">Permisos</span>
                    </RouterLink>
      </nav>
    </div>

    <!-- Perfil del usuario -->
    <div class="border-t border-gray-200 dark:border-gray-700 p-4 flex items-center justify-between gap-3">
      <div class="flex items-center gap-3">
        <!-- Avatar con iniciales -->
        <div
          class="relative inline-flex items-center justify-center w-10 h-10 overflow-hidden bg-gray-100 rounded-full dark:bg-gray-600"
        >
          <span class="font-medium text-gray-600 dark:text-gray-300">
            {{ userInitials }}
          </span>
        </div>

        <!-- Nombre -->
        <div class="flex flex-col">
          <span class="text-sm font-semibold text-gray-800 dark:text-white">
            {{ session.currentUser?.name || "Invitado" }}
          </span>
        </div>
      </div>

      <!-- Logout -->
      <button
        @click="logout"
        class="p-2 rounded-lg hover:bg-red-100 dark:hover:bg-red-900 transition"
        title="Cerrar sesión"
      >
        <LogoutIcon class="w-5 h-5 text-red-600"/>
      </button>
    </div>
  </aside>

  <!-- Overlay móvil -->
  <div
    v-if="isOpen"
    class="fixed inset-0 bg-black-50 bg-opacity-40 sm:hidden"
    @click="isOpen = false"
  ></div>
</template>
