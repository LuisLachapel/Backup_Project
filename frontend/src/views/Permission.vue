<script setup>
import TablePermission from '@/components/permission/TablePermission.vue';
import createButton from '@/components/permission/CreateButton.vue';

</script>


<template>
<h1>Vista de permisos</h1>

<main class="container mx-auto py-2.5">
 <TablePermission/>
</main>


<div class="fixed bottom-6 end-12 group">
    <createButton/>
</div>
</template>