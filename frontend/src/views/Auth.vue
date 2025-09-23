<script setup>
import { useSessionStore } from '@/stores/sessionStore';
import { useUserStore } from '@/stores/userStore';
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';

const session = useSessionStore();
const userStore = useUserStore()
const router = useRouter()

onMounted(() =>{

    userStore.getAllUser()
});

const selectUser = (user) =>{
  session.setUser(user);
  router.push('/')
}

</script>


<template>

     <div class="p-4">
    <h1 class="text-xl font-bold mb-4">Selecciona un usuario</h1>
    <ul class="space-y-2">
      <li
        v-for="user in userStore.users"
        :key="user.id"
        class="flex justify-between items-center border p-2 rounded cursor-pointer hover:bg-gray-100"
        @click="selectUser(user)"
      >
        <span>{{ user.name }} - {{ user.position }}</span>
        <span class="text-sm text-gray-500">ID: {{ user.id }}</span>
      </li>
    </ul>
  </div>

</template>