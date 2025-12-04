<script setup>
import { onMounted, ref, computed } from 'vue'
import { getUsers } from '@/http-service/users'
import { getGenderName } from '@/data/genders'

const users = ref([])
onMounted(async () => (users.value = (await getUsers()).users))

</script>

<template>
  <div class="p-4 relative">
    <h2 class="text-xl font-semibold">Users List</h2>
    <div v-for="user in users" :key="user.id">
      <p>ID: {{ user.id }}</p>
      <p>First name: {{ user.firstName }}</p>
      <p>Last name: {{ user.lastName }}</p>
      <p>Email: {{ user.email }}</p>
      <p>Date of birth: {{ user.dateOfBirth }}</p>
      <p>Gender: {{ getGenderName(user.gender) }}</p>
      <p>Last updated: {{ user.lastUpdated }}</p>
      <p>Status: {{ user.status }}</p>
    </div>
  </div>
</template>

<style scoped></style>
