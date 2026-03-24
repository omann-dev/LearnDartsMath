<template>
  <div>
    <nav class="nav">
      <router-link to="/">Home</router-link>
      <router-link to="/login">Login</router-link>
      <router-link to="/register">Register</router-link>
    </nav>

    <router-view />
  </div>
</template>

<style>
.nav {
  display: flex;
  gap: 20px;
  padding: 20px;
  background: #111;
}

a {
  color: white;
  text-decoration: none;
}
</style>