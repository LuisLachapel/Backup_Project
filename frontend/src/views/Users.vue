<script setup>
import CardUser from '@/components/user/CardUser.vue';
import FABUser from '@/components/user/FABUser.vue';

</script>

<template>
    
 <main class="container mx-auto p-2.5">
     <h2 class="text-2xl font-bold mb-4">Listado de Usuarios</h2>
    <section class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        <CardUser/>
    </section>
 </main>

 <FABUser/>

</template>