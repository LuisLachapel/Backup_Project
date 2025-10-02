<script setup>
import { ref, onMounted, onUnmounted, watch } from "vue";

const props = defineProps({
  text: { type: String, required: true },
  target: { type: HTMLElement, required: true }
});

const position = ref({ top: 0, left: 0 });
const visible = ref(false);

const updatePosition = () => {
  if (props.target) {
    const rect = props.target.getBoundingClientRect();
    position.value = {
      top: rect.top + window.scrollY - 35, // arriba del target
      left: rect.left + window.scrollX + 20
    };
  }
};

const show = () => {
  visible.value = true;
  updatePosition();
};

const hide = () => {
  visible.value = false;
};

let hideTimeout = null;

const handleMouseEnter = () => {
  clearTimeout(hideTimeout);
  show();
};

const handleMouseLeave = () => {
  hideTimeout = setTimeout(() => hide(), 200);
};

onMounted(() => {
  if (props.target) {
    props.target.addEventListener("mouseenter", handleMouseEnter);
    props.target.addEventListener("mouseleave", handleMouseLeave);
  }
  window.addEventListener("scroll", updatePosition, true);
  window.addEventListener("resize", updatePosition);
});

onUnmounted(() => {
  if (props.target) {
    props.target.removeEventListener("mouseenter", handleMouseEnter);
    props.target.removeEventListener("mouseleave", handleMouseLeave);
  }
  window.removeEventListener("scroll", updatePosition, true);
  window.removeEventListener("resize", updatePosition);
});
</script>

<template>
  <teleport to="body">
    <div
      v-show="visible"
      class="fixed bg-gray-800 text-white text-xs rounded px-2 py-1 whitespace-nowrap z-[9999]"
      :style="{ top: position.top + 'px', left: position.left + 'px' }"
      @mouseenter="show"
      @mouseleave="hide"
    >
      {{ text }}
    </div>
  </teleport>
</template>
