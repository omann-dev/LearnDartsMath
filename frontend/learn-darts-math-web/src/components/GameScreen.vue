<template>
    <div class="game">
        <h1>{{ currentScore }}</h1>

        <ScoreSelection 
            :selected-score = "selectedScore"
            :selected-modifier="selectedModifier"
            @update:selected-score="onScoreSelected"
            @update:selected-modifier="selectedModifier = $event"
        />

        <button @click="$emit('reset')">Back</button>
    </div>
</template>

<script setup lang="ts">


import ScoreSelection from './ScoreSelection.vue';
import { ref } from 'vue'

type Modifier = 'DOUBLE' | 'TRIPLE'

let thrownScore = ref(0)
let throwCount = ref(0)

const selectedScore = ref(0)
const selectedModifier = ref(<Modifier | undefined>(undefined))

const props = defineProps<{
    currentScore: number
    isGameFinished: boolean
}>()

const emit = defineEmits<{
    (e: 'reset'): void 
    (e: 'update:currentScore', value: number) : void
    (e: 'update:isGameFinished', value: boolean) : void 
}>()


function onScoreSelected(score : number) {
    selectedScore.value = score

    let multiplier = 1
    if (selectedModifier.value === 'DOUBLE') multiplier = 2
    if (selectedModifier.value === 'TRIPLE') multiplier = 3 

    thrownScore.value += score * multiplier
    throwCount.value++

    selectedModifier.value = undefined
    multiplier = 1

    if (throwCount.value === 3) {
        subtractScore()
    }
}

function subtractScore(){
    if (props.currentScore > 0){
        emit('update:currentScore', props.currentScore - thrownScore.value)

        thrownScore.value = 0
        throwCount.value = 0
        selectedModifier.value = undefined
        selectedScore.value = 0
    }
    else emit('update:isGameFinished', true)
}

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