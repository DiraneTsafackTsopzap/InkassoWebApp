<template>
  <div class="container py-5 my-3">
    <div class="row justify-content-center">
      <div class="col-md-6 col-lg-4 border rounded">
        <div class="shadow-sm">
          <div class="card-body p-4">
            <h4 class="text-center mb-4">Konto erstellen</h4>

            <form @submit.prevent="handleSignUp">
              <div class="mb-3">
                <label for="email" class="form-label">Email</label>
                <input v-model="form.email" type="email" class="form-control" id="email" required />
                <div v-if="errors.email" class="text-danger">{{ errors.email }}</div>
              </div>

              <div class="mb-4">
                <label for="password" class="form-label">Password</label>
                <input v-model="form.passwort" type="password" class="form-control" id="password" required />
                <div v-if="errors.passwort" class="text-danger">{{ errors.passwort }}</div>
              </div>

              <div class="mb-4">
                <label for="name" class="form-label">Name</label>
                <input v-model="form.name" type="text" class="form-control" id="name" required />
                <div v-if="errors.name" class="text-danger">{{ errors.name }}</div>
              </div>

              <div class="mb-4">
                <label for="vorname" class="form-label">Vorname</label>
                <input v-model="form.vorname" type="text" class="form-control" id="vorname" required />
                <div v-if="errors.vorname" class="text-danger">{{ errors.vorname }}</div>
              </div>

              <button type="submit" class="btn btn-success w-100">
                Konto erstellen
              </button>

              <div v-if="error" class="alert alert-danger mt-3 mb-0">{{ error }}</div>
            </form>

            <p class="mt-3 form-label text-center">
              Already have an account?
              <router-link :to="APP_ROUTE_NAMES.LOGIN">Login here</router-link>
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>





<script setup>
// Import ref
import { ref } from 'vue'

// Import useRouter
import { useRouter } from 'vue-router'
const router = useRouter()
import { APP_ROUTE_NAMES } from '@/constants/routeNames'

// Import registerService
import { useRegisterService } from '@/services/registerService'
const registerStore = useRegisterService()

// Import de Swal
import { useSwal } from '@/utility/useSwal'
const { showSuccess, showError } = useSwal()

// Formulaire et erreurs
const form = ref({
  email: '',
  passwort: '',
  name: '',
  vorname: ''
})

const errors = ref({
  email: '',
  passwort: '',
  name: '',
  vorname: ''
})

const error = ref('')

// Validation des champs
const validateForm = () => {
  let isValid = true
  
  // Validation Email
  if (!form.value.email || !/\S+@\S+\.\S+/.test(form.value.email)) {
    errors.value.email = 'Please enter a valid email address.'
    isValid = false
  } else {
    errors.value.email = ''
  }
  
  // Validation Password
  if (!form.value.passwort || form.value.passwort.length < 6) {
    errors.value.passwort = 'Password must be at least 6 characters long.'
    isValid = false
  } else {
    errors.value.passwort = ''
  }
  
  // Validation Name
  if (!form.value.name) {
    errors.value.name = 'Name is required.'
    isValid = false
  } else {
    errors.value.name = ''
  }
  
  // Validation Vorname
  if (!form.value.vorname) {
    errors.value.vorname = 'Vorname is required.'
    isValid = false
  } else {
    errors.value.vorname = ''
  }
  
  return isValid
}

const handleSignUp = async () => {
  if (!validateForm()) {
    return
  }
  
  try {
    await registerStore.registerUser(form.value)
    
    showSuccess('Account created successfully')
    router.push({ name: APP_ROUTE_NAMES.LOGIN })
  } catch (err) {
    error.value = registerStore.error || 'Une erreur est survenue'
    showError(error.value)  // Afficher l'erreur avec Swal
    console.error(err)
  }
}
</script>
