<script setup>
import { reactive, watch } from 'vue'
import { genders } from '@/data/genders'

const props = defineProps({
  open: {
    type: Boolean,
    required: true
  },
  user: {
    type: Object,
    default: null
  },
  isSaving: {
    type: Boolean,
    default: false
  },
  errorMessage: {
    type: String,
    default: ''
  }
})

const emit = defineEmits(['update:open', 'save'])

const form = reactive({
  firstName: '',
  lastName: '',
  email: '',
  gender: '',
  dateOfBirth: '',
  status: true,
})

const errors = reactive({
  firstName: '',
  lastName: '',
  email: '',
  dateOfBirth: ''
})

const clearErrors = () => {
  Object.keys(errors).forEach((key) => {
    errors[key] = ''
  })
}

watch(
  () => props.user,
  (user) => {
    if (!user) return

    form.firstName = user.firstName ?? ''
    form.lastName = user.lastName ?? ''
    form.email = user.email ?? ''
    form.gender = user.gender ?? ''
    form.dateOfBirth = user.dateOfBirth ? String(user.dateOfBirth).slice(0, 10) : ''
    form.status = Boolean(user.status)

    clearErrors()
  },
  { immediate: true }
)

const validate = () => {
  clearErrors()
  let valid = true

  if (!form.firstName.trim()) {
    errors.firstName = 'First name is required'
    valid = false
  }

  if (!form.lastName.trim()) {
    errors.lastName = 'Last name is required'
    valid = false
  }

  if (!form.email.trim()) {
    errors.email = 'Email is required'
    valid = false
  } else {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    if (!emailRegex.test(form.email)) {
      errors.email = `Please enter a valid email address`
      valid = false
    }
  }

  if (!form.dateOfBirth) {
    errors.dateOfBirth = 'Date of birth is required'
    valid = false
  }

  return valid
}

const close = () => {
  emit('update:open', false)
}

const submit = () => {
  if (!validate()) return

  emit('save', {
    ...props.user,
    firstName: form.firstName.trim(),
    lastName: form.lastName.trim(),
    email: form.email.trim(),
    gender: form.gender,
    dateOfBirth: form.dateOfBirth,
    status: form.status,
  })
}
</script>


<template>
 <div 
    v-if="open"
    class="fixed inset-0 bg-black/70 backdrop-blur-[10px] flex items-start justify-center z-50 pt-2.5 md:pt-[50px] lg:pt-[60px]"
  >
    <div class="bg-[#CAE2F2] p-5 md:p-[30px] rounded-[20px] w-[400px] md:w-[650px] max-h-[700px] overflow-scroll">
        <h1 class="text-xl md:text-2xl lg:text-3xl font-black text-[#092E58] mb-5 md:mb-7">Edit User</h1>

        <form class="space-y-5" @submit.prevent="submit" novalidate>
            <div>
                <label class="label-base">
                    First Name*
                </label>
                <input
                    v-model="form.firstName"
                    type="text"
                    class="w-full rounded-md border px-3 py-2 text-sm focus:outline-none focus:ring-2"
                    :class="[
                        'input-base',
                        errors.firstName ? 'input-error' : 'input-normal'
                    ]"
                />
                <p v-if="errors.firstName" class="error-text">
                    {{ errors.firstName }}
                </p>
            </div>

            <div>
                <label class="label-base">
                    Last Name*
                </label>
                <input
                    v-model="form.lastName"
                    type="text"
                    :class="[
                        'input-base',
                        errors.lastName ? 'input-error' : 'input-normal'
                    ]"
                />
                <p v-if="errors.lastName" class="error-text">
                    {{ errors.lastName }}
                </p>
            </div>

            <div>
                <label class="label-base">
                  Email*
                </label>
                <input
                    v-model="form.email"
                    type="email"
                    :class="[
                        'input-base',
                        errors.email ? 'input-error' : 'input-normal'
                    ]"
                />
                <p v-if="errors.email" class="error-text">
                {{ errors.email }}
                </p>
            </div>

            <div>
                <label class="label-base">
                  Gender
                </label>
                <select
                    v-model="form.gender"
                   class="input-base input-normal appearance-none" 
                >
                    <option
                        v-for="gender in genders"
                        :key="gender.value"
                        :value="gender.value"
                    >
                        {{ gender.name }}
                    </option>
                </select>
            </div>

            <div>
                <label class="label-base">
                  Date of Birth*
                </label>
                <input
                    v-model="form.dateOfBirth"
                    type="date"
                    :class="[
                        'input-base',
                        errors.dateOfBirth ? 'input-error' : 'input-normal'
                    ]"
                />
                <p v-if="errors.dateOfBirth" class="error-text">
                    {{ errors.dateOfBirth }}
                </p>
            </div>

            <div>
                <label class="label-base">
                  Status
                </label>
                <div class="flex gap-6 mt-1">
                    <label class="flex items-center gap-2">
                        <input
                            type="radio"
                            v-model="form.status"
                            :value="true"
                            class="
                              h-6 w-6
                              appearance-none
                              rounded-full
                              bg-white
                              ring-3 ring-white ring-offset-0
                              checked:bg-[#498BCA]
                              checked:ring-white
                            "
                        />
                        <span class="md:text-lg lg:text-xl text-[#092E58]">Active</span>
                    </label>

                    <label class="flex items-center gap-2">
                        <input
                            type="radio"
                            v-model="form.status"
                            :value="false"
                            class="
                              h-6 w-6
                              appearance-none
                              rounded-full
                              bg-white
                              ring-3 ring-white ring-offset-0
                              checked:bg-[#498BCA]
                              checked:ring-white
                            "
                        />
                        <span class="md:text-lg lg:text-xl text-[#092E58]">Disabled</span>
                    </label>
                </div>
            </div>
            <p v-if="errorMessage" class="error-text">
              {{ errorMessage }}
            </p>

            <div class="flex justify-between mt-10 md:text-lg lg:text-xl font-bold">
                <button 
                    @click="close"
                    class="px-5 py-2 bg-white rounded-[10px] cursor-pointer hover:opacity-70 text-[#092E58]"
                >
                    CANCEL
                </button>
                <button 
                    type="submit"
                    class="px-4 py-2 bg-[#498BCA] rounded-[10px] text-white cursor-pointer hover:opacity-70 disabled:opacity-50"
                    :disabled="isSaving"
                >
                  <span v-if="isSaving">SAVING...</span>
                  <span v-else>SAVE</span>
                </button>
            </div>
        </form>
    </div>
  </div>

</template>