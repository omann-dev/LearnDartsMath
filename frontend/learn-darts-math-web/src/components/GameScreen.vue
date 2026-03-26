<template>
    <div class="game">
        <h1>Game is running</h1>
        <p>Startscore: {{ currentScore  }}</p>

        <button @click="$emit('reset')">Back</button>
    </div>
</template>

<script setup lang="ts">

defineProps<{
    currentScore: number
}>()

defineEmits<{
    (e: 'reset'): void 
}>()

</script>

<style scoped>

.game {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 1rem;
    max-width: 400px;
}

</style>