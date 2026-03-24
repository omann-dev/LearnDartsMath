<script setup lang="ts">
import { ref } from 'vue'
import { apiFetch } from '@/services/api'

type AuthResponse = {
  token: string
  username: string
  email: string
  displayName: string
}

const usernameOrEmail = ref('')
const password = ref('')
const error = ref('')

async function login() {
  error.value = ''

  try {
    const result = await apiFetch<AuthResponse>('/auth/login', {
      method: 'POST',
      body: JSON.stringify({
        usernameOrEmail: usernameOrEmail.value,
        password: password.value
      })
    })

    localStorage.setItem('token', result.token)
    alert('Login successful!')
  } catch (err: unknown) {
    error.value = err instanceof Error ? err.message : 'Unknown error'
  }
}
</script>

<template>
  <div class="container">
    <h2>Login</h2>

    <input v-model="usernameOrEmail" placeholder="Username or Email" />
    <input v-model="password" type="password" placeholder="Password" />

    <button @click="login">Login</button>

    <p v-if="error" class="error">{{ error }}</p>
  </div>
</template>

<style scoped>
.container {
  padding: 40px;
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.error {
  color: red;
}
</style>