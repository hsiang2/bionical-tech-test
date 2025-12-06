<script setup>
import { onMounted, ref } from 'vue'
import { getUsers, updateUser } from '@/http-service/users'
import { getGenderName } from '@/data/genders'
import { tableLabels } from '@/data/labels'
import { calculateAge, formatDate, formatDateTime } from "@/utils/date";
import editUserModal from './edit-user-modal.vue';

const users = ref([])
const isEditModalOpen = ref(false)
const selectedUser = ref(null)
const isSaving = ref(false);
const saveError = ref('')
const isLoadingUsers = ref(true)
const loadError = ref('')

onMounted(async () => {
  isLoadingUsers.value = true
  loadError.value = ''

  try {
    const res = await getUsers()
    if (!res) {
      loadError.value = 'Failed to load users.'
      users.value = []
      return
    }
    users.value = res?.users ?? []
  } catch (error) {
    console.error(error)
    loadError.value = 'Failed to load users.'
  } finally {
    isLoadingUsers.value = false
  }
})

function openEditModal(user) {
  selectedUser.value = { ...user }
  isEditModalOpen.value = true
}

const handleSave = async (payload) => {
  saveError.value = '';
  isSaving.value = true;

  try {
    let updated = await updateUser(payload.id, payload)

    const index = users.value.findIndex((u) => u.id === updated.id)
    if (index !== -1) {
      users.value[index] = updated
    }

    isEditModalOpen.value = false
  } catch (err) {
    saveError.value = err?.message ?? 'Failed to save user'
  } finally {
    isSaving.value = false
  }
}


</script>
<template>
  <div class="container mx-auto px-5 py-5 md:py-[30px] lg:py-[60px]">
    <h2 class="text-3xl md:text-4xl lg:text-5xl font-black text-white mb-2.5 md:mb-5 mt-[100px]">Users</h2>

    <!-- Loading -->
    <div v-if="isLoadingUsers" class="text-white/80 py-10 text-center text-xl">
      Loading users...
    </div>

    <!-- Error -->
    <div v-else-if="loadError" class="text-[#e694b4] py-10 text-center text-xl">
      {{ loadError }}
    </div>

    <!-- No user -->
    <div v-else-if="users.length === 0" class="text-white/80 py-10 text-center text-xl">
      No users found.
    </div>

    <template v-else>
      <!-- Mobile/tablet list -->
      <div class="lg:hidden font-bold flex flex-col gap-2.5 ">
        <div 
          v-for="user in users" :key="user.id"
          class="bg-white rounded-[20px] p-5 flex flex-col gap-5"
        >
          <div class="flex justify-between">
            <div class="w-full">
              <p class="label-text">{{ tableLabels.firstName }}</p>
              <p class="value-text">{{ user.firstName }}</p>
            </div>
            <div class="w-full">
              <p class="label-text">{{ tableLabels.lastName }}</p>
              <p class="value-text">{{ user.lastName }}</p>
            </div>
            <div class="w-full">
              <p class="label-text">{{ tableLabels.email }}</p>
              <p class="value-text">{{ user.email }}</p>
            </div>
          </div>
          <div class="flex justify-between flex-col gap-2.5 md:flex-row">
            <div class="flex justify-between w-full">
              <div class="w-full">
                <p class="label-text">{{ tableLabels.gender }}</p>
                <p class="value-text">{{ getGenderName(user.gender) }}</p>
              </div>
              <div class="w-full">
                <p class="label-text">{{ tableLabels.dateOfBirth }}</p>
                <p class="value-text">{{ formatDate(user.dateOfBirth) }}</p>
              </div>
              <div class="md:w-full">
                <p class="label-text">{{ tableLabels.age }}</p>
                <p class="value-text">{{ calculateAge(user.dateOfBirth) }}</p>
              </div>
            </div>
            <div class="flex justify-between w-full">
              <div class="w-full">
                <p class="label-text">{{ tableLabels.lastUpdated }}</p>
                <p class="value-text">{{ formatDateTime(user.lastUpdated) }}</p>
              </div>
              <div class="w-full flex gap-5">
                <div class="w-full text-center">
                  <p class="label-text">{{ tableLabels.status }}</p>
                  <div v-if="user.status" class="flex justify-center rounded-[5px] bg-[#6F811D] text-white">
                    Active
                  </div>
                  <div v-else class="flex justify-center rounded-[5px] bg-[#B42D60] text-white">
                    Disabled
                  </div>
                </div>
                <button class="cursor-pointer hover:opacity-70" @click="openEditModal(user)">
                  <img src="@/assets/images/icon-edit.svg" alt="Edit icon" class="min-w-[26px]" />
                </button>
              </div>
            
            </div>
          </div>
        </div>
      </div>

      <!-- Desktop table -->
      <div class="hidden lg:block h-[600px] bg-white/50 rounded-[20px] overflow-x-hidden overflow-y-scroll font-bold">
        <table class="w-full text-left">
          <thead>
            <tr class="border-b border-[#A8ADB3] bg-white text-[#B7BCC2]">
              <th class="p-4">{{ tableLabels.firstName }}</th>
              <th class="p-4">{{ tableLabels.lastName }}</th>
              <th class="p-4">{{ tableLabels.email }}</th>
              <th class="p-4">{{ tableLabels.gender }}</th>
              <th class="p-4">{{ tableLabels.dateOfBirth }}</th>
              <th class="p-4">{{ tableLabels.age }}</th>
              <th class="p-4">{{ tableLabels.lastUpdated }}</th>
              <th class="p-4 text-center">{{ tableLabels.status }}</th>
              <th class="p-4">
                <span class="sr-only">Actions</span>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="user in users" :key="user.id"
              class="border-b border-[#A8ADB3] bg-white value-text"
            >
              <td class="p-4">{{ user.firstName }}</td>
              <td class="p-4">{{ user.lastName }}</td>
              <td class="p-4">{{ user.email }}</td>
              <td class="p-4">{{ getGenderName(user.gender) }}</td>
              <td class="p-4">{{ formatDate(user.dateOfBirth) }}</td>
              <td class="p-4">{{ calculateAge(user.dateOfBirth) }}</td>
              <td class="p-4">{{ formatDateTime(user.lastUpdated) }}</td>
              <td>
                <div v-if="user.status" class="flex justify-center rounded-[5px] w-full bg-[#6F811D] text-white">
                  Active
                </div>
                <div v-else class="flex justify-center rounded-[5px] w-full bg-[#B42D60] text-white">
                  Disabled
                </div>
              </td>
              <td class="p-4 min-w-[60px]">
                <button class="cursor-pointer hover:opacity-70" @click="openEditModal(user)">
                    <img src="@/assets/images/icon-edit.svg" alt="Edit icon" class="w-[26px]" />
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </template>
  </div>

  <edit-user-modal 
    v-model:open="isEditModalOpen"
    :user="selectedUser"
    :is-saving="isSaving"
    :error-message="saveError"
    @save="handleSave"
  />

</template>

<style scoped>
@reference "../assets/index.css";

.label-text {
  @apply text-[#B7BCC2] mb-2;
}

.value-text {
  @apply text-[#2E2E2E];
}
</style>
