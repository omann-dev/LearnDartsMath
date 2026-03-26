<template>
    <div class="game">
        <h1>{{ currentScore }}</h1>

        <ScoreSelection 
            :selected-score = "selectedScore"
            :selected-modifier="selectedModifier"
            @update:selected-score="selectedScore = $event"
            @update:selected-modifier="selectedModifier = $event"
            />
            
            <p> Score: {{ selectedScore }}  Modifier: {{ selectedModifier }}</p>


        <button @click="$emit('reset')">Back</button>
    </div>
</template>

<script setup lang="ts">


import ScoreSelection from './ScoreSelection.vue';
import { ref } from 'vue'

type Modifier = 'DOUBLE' | 'TRIPPLE'

const selectedScore = ref(0)
const selectedModifier = ref(<Modifier | undefined>(undefined))

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