<template>
  <div class="container py-5 my-3">
    <div class="row justify-content-center">
      <div class="col-md-6 col-lg-4 border rounded">
        <div class="shadow-sm">
          <div class="card-body p-4">
            <h4 class="text-center mb-4">Login</h4>

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


              <button type="submit" class="btn btn-success w-100">
                Login
              </button>

              <div v-if="error" class="alert alert-danger mt-3 mb-0">{{ error }}</div>
            </form>

            <p class="mt-3 form-label text-center">
              Kein Konto ?
              <router-link :to="APP_ROUTE_NAMES.REGISTER">Konto erstellen</router-link>
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

// Import du store registerService
import { useRegisterService } from '@/services/registerService'
const registerStore = useRegisterService()

// Import Swal
import { useSwal } from '@/utility/useSwal'
const { showSuccess, showError } = useSwal()

// Formulaire et erreurs
const form = ref({
  email: '',
  passwort: ''
})

const errors = ref({
  email: '',
  passwort: ''
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

  return isValid
}

const handleSignUp = async () => {
  if (!validateForm()) {
    return
  }

  try {
    // Appel de loginUser avec le bon formulaire

    console.log(form.value)
    const { mein_user } = await registerStore.loginUser(form.value)

    console.log(mein_user.rolle)
    if (mein_user.rolle === 'Admin') {
      router.push({ name: APP_ROUTE_NAMES.AdminDashboard })
    } else if (mein_user.rolle === 'Schuldner') {
      router.push({ name: APP_ROUTE_NAMES.SCHULDNERDASHBOARD })
    } else {
      // Par défaut, c’est un Client
      router.push({ name: APP_ROUTE_NAMES.DASHBOARD })
    }


  } catch (err) {
    error.value = registerStore.error || 'Une erreur est survenue'
    showError(error.value)  // Afficher l'erreur avec Swal
    console.error(err)
  }
}
</script>