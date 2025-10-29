<script setup>
import TablePosition from '@/components/position/TablePosition.vue';
import ModalAddPosition from '@/components/position/ModalAddPosition.vue';
import CreateButton from '@/components/position/CreateButton.vue';

</script>

<template>
    
    <main class="container mx-auto py-2.5">
        <TablePosition/>
    </main>

    <div class="fixed bottom-6 end-12 group">
          <CreateButton/>
    </div>
</template>