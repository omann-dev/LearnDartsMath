<script setup lang="ts">
import { ref } from 'vue'
import { startGame, submitTurn } from '@/services/gameApi'
import type { GameState, TurnResult } from '@/types/game'

const startScore = ref(501)
const game = ref<GameState | null>(null)

const enteredScoredPoints = ref<number | null>(null)
const enteredRemainingScore = ref<number | null>(null)

const error = ref('')
const feedback = ref('')
const lastResult = ref<TurnResult | null>(null)
const turnHistory = ref<TurnResult[]>([])

async function startNewGame() {
  error.value = ''
  feedback.value = ''
  lastResult.value = null
  turnHistory.value = []

  try {
    const result = await startGame({
      startScore: startScore.value,
      mode: 'x01'
    })

    game.value = result
    enteredScoredPoints.value = null
    enteredRemainingScore.value = null
  } catch (err: unknown) {
    error.value = err instanceof Error ? err.message : 'Unknown error'
  }
}

async function checkTurn() {
  error.value = ''
  feedback.value = ''
  lastResult.value = null

  if (!game.value) {
    error.value = 'Start a game first.'
    return
  }

  if (enteredScoredPoints.value === null || enteredRemainingScore.value === null) {
    error.value = 'Please enter both values.'
    return
  }

  try {
    const result = await submitTurn({
      trainingSessionId: game.value.trainingSessionId,
      enteredScoredPoints: enteredScoredPoints.value,
      enteredRemainingScore: enteredRemainingScore.value
    })

    lastResult.value = result
    turnHistory.value.push(result)
    game.value.currentScore = result.newCurrentScore

    if (result.isCorrect) {
      feedback.value = `Correct! New score: ${result.newCurrentScore}`
    } else {
      feedback.value = `Wrong. Correct remaining score would be ${result.correctRemainingScore}.`
    }

    enteredScoredPoints.value = null
    enteredRemainingScore.value = null
  } catch (err: unknown) {
    error.value = err instanceof Error ? err.message : 'Unknown error'
  }
}

function resetGame() {
  game.value = null
  enteredScoredPoints.value = null
  enteredRemainingScore.value = null
  error.value = ''
  feedback.value = ''
  lastResult.value = null
  turnHistory.value = []
}
</script>

<template>
  <div class="page">
    <div class="header-card">
      <h1>Training</h1>
      <p>Practice subtracting from 501.</p>
    </div>

    <div v-if="!game" class="card">
      <h2>Start New Session</h2>

      <label for="startScore">Start Score</label>
      <input id="startScore" v-model.number="startScore" type="number" min="1" />

      <button @click="startNewGame">Start Game</button>
    </div>

    <template v-else>
      <div class="score-card">
        <div class="score-label">Current Score</div>
        <div class="score-value">{{ game.currentScore }}</div>
        <div class="session-info">
          Session #{{ game.trainingSessionId }} · {{ game.mode }}
        </div>
      </div>

      <div class="card">
        <div class="card-header">
          <h2>Enter Your Calculation</h2>
          <button class="secondary-button" @click="resetGame">End / Reset</button>
        </div>

        <label for="scoredPoints">Scored Points</label>
        <input
          id="scoredPoints"
          v-model.number="enteredScoredPoints"
          type="number"
          min="0"
          max="180"
          placeholder="e.g. 60"
        />

        <label for="remainingScore">Your Remaining Score</label>
        <input
          id="remainingScore"
          v-model.number="enteredRemainingScore"
          type="number"
          min="0"
          max="501"
          placeholder="e.g. 441"
        />

        <button @click="checkTurn">Check</button>
      </div>

      <div v-if="lastResult" class="card">
        <h2>Last Result</h2>

        <p><strong>Previous Score:</strong> {{ lastResult.previousScore }}</p>
        <p><strong>Your Scored Points:</strong> {{ lastResult.enteredScoredPoints }}</p>
        <p><strong>Your Remaining Score:</strong> {{ lastResult.enteredRemainingScore }}</p>
        <p><strong>Correct Remaining Score:</strong> {{ lastResult.correctRemainingScore }}</p>
        <p><strong>Answer Correct:</strong> {{ lastResult.isCorrect ? 'Yes' : 'No' }}</p>
      </div>

      <div class="card">
        <h2>Turn History</h2>

        <div v-if="turnHistory.length === 0" class="empty-state">
          No entries yet.
        </div>

        <table v-else>
          <thead>
            <tr>
              <th>#</th>
              <th>Previous</th>
              <th>Scored</th>
              <th>Your Remaining</th>
              <th>Correct Remaining</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(turn, index) in turnHistory" :key="index">
              <td>{{ index + 1 }}</td>
              <td>{{ turn.previousScore }}</td>
              <td>{{ turn.enteredScoredPoints }}</td>
              <td>{{ turn.enteredRemainingScore }}</td>
              <td>{{ turn.correctRemainingScore }}</td>
              <td :class="turn.isCorrect ? 'success-text' : 'warning-text'">
                {{ turn.isCorrect ? 'Correct' : 'Wrong' }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </template>

    <p v-if="feedback" :class="lastResult?.isCorrect ? 'success' : 'warning'">
      {{ feedback }}
    </p>

    <p v-if="error" class="error">
      {{ error }}
    </p>
  </div>
</template>

<style scoped>
.page {
  max-width: 900px;
  margin: 0 auto;
  padding: 32px;
}

.header-card,
.card,
.score-card {
  background: #1b1b1b;
  border-radius: 16px;
  padding: 24px;
  margin-bottom: 20px;
}

.score-card {
  text-align: center;
}

.score-label {
  font-size: 1rem;
  color: #bbbbbb;
  margin-bottom: 12px;
}

.score-value {
  font-size: 5rem;
  font-weight: 700;
  line-height: 1;
}

.session-info {
  margin-top: 12px;
  color: #aaaaaa;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 16px;
}

label {
  display: block;
  margin-bottom: 8px;
  margin-top: 14px;
}

input {
  display: block;
  width: 100%;
  padding: 12px;
  border-radius: 10px;
  border: none;
  background: #2a2a2a;
  color: white;
  font-size: 1rem;
  box-sizing: border-box;
}

button {
  margin-top: 18px;
  padding: 12px 18px;
  border: none;
  border-radius: 10px;
  background: #2d6cdf;
  color: white;
  font-size: 1rem;
  cursor: pointer;
}

.secondary-button {
  margin-top: 0;
  background: #444;
}

table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 10px;
}

th,
td {
  text-align: left;
  padding: 12px 10px;
  border-bottom: 1px solid #333;
}

.empty-state {
  color: #aaaaaa;
}

.success {
  color: #57d66b;
  font-weight: 600;
}

.warning {
  color: #ffb347;
  font-weight: 600;
}

.error {
  color: #ff5c5c;
  font-weight: 600;
}

.success-text {
  color: #57d66b;
  font-weight: 600;
}

.warning-text {
  color: #ffb347;
  font-weight: 600;
}
</style>