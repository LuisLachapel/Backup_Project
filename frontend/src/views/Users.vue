<script setup>
import CardUser from '@/components/user/CardUser.vue';
import FABUser from '@/components/user/FABUser.vue';

</script>

<template>
 <h1>Vista para usuarios</h1>
 <main class="container mx-auto p-2.5">
    <section class="grid grid-cols-3 gap-4">
        <CardUser/>
    </section>
 </main>

 <FABUser/>

</template>