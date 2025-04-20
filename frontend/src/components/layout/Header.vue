<template>


  <nav class="navbar navbar-expand-lg bg-body-tertiary">
    <div class="container-fluid">

      <!-- <a class="navbar-brand" href="#">Navbar</a> -->
      <!-- Ajout de mon Logo ici :  -->


      <img src="@/assets/logo.png" style="width: 40px" class="mx-3" alt="Produitstore" />


      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav"
        aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">

          <li class="nav-item" v-if="registerStore.user && isUser">
            <router-link class="nav-link active" aria-current="page"
              :to="{ name: APP_ROUTE_NAMES.ADDFORDERUNG }">Add-Forderung</router-link>
          </li>


         <!-- Affichage du nom et du rôle de l'utilisateur -->
<li class="nav-item text-center" v-if="registerStore.user">
  <span class="nav-link">
    Willkommen {{ registerStore.user.name }} - 
    <span :class="{ 'text-warning': registerStore.user.rolle === 'Kunde', 'text-success': registerStore.user.rolle === 'Admin' }">
       {{ registerStore.user.rolle }}
    </span>
  </span>
</li>

          



          <li class="nav-item" v-if="registerStore.user && isAdmin">
            <router-link class="nav-link active" aria-current="page"
              :to="{ name: APP_ROUTE_NAMES.AdminDashboard }">Admin Dashboard</router-link>
          </li>

       

        </ul>

        <ul class="d-flex navbar-nav ms-auto">
          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
              <!-- Dropdown -->
              <i class="bi bi-laptop"></i>
            </a>
            <ul class="dropdown-menu">
              <li><button @click="themeStore.setTheme('light')" class="dropdown-item"><i class="bi bi-sun"></i> &nbsp;
                  Hell</button></li>
              <li><button class="dropdown-item" @click="themeStore.setTheme('dark')"> <i
                    class="bi bi-moon-stars-fill"></i> &nbsp; Dunkel</button></li>

            </ul>
          </li>
          <li class="nav-item" v-if="!registerStore.user">
            <router-link class="nav-link active" aria-current="page"
              :to="{ name: APP_ROUTE_NAMES.LOGIN }">Login</router-link>
          </li>
          <li class="nav-item" v-if="!registerStore.user">
            <router-link class="nav-link active" aria-current="page"
              :to="{ name: APP_ROUTE_NAMES.REGISTER }">Register</router-link>
          </li>


          <li class="nav-item" v-if="registerStore.user">
            <button class="btn btn-danger" @click="handleLogout">Logout</button>
          </li>
        </ul>
      </div>
    </div>
  </nav>


</template>

<!-- Ajout du lien  -->
<script setup>


// Import useRouter
import { useRouter } from 'vue-router'
const router = useRouter()
import { APP_ROUTE_NAMES } from '@/constants/routeNames'

// Import registerService
import { useRegisterService } from '@/services/registerService'
const registerStore = useRegisterService()

import { onMounted } from 'vue'

import { useThemeStore } from '@/stores/themeStore';

const themeStore = useThemeStore();


import { computed } from 'vue'




const isAdmin = computed(() => registerStore.user && registerStore.user.rolle === 'Admin')
const isUser = computed(() => registerStore.user && registerStore.user.rolle === 'Kunde')


onMounted(() => {
  registerStore.checkAuth()
})

const handleLogout = () => {
  registerStore.logoutUser()
  router.push({ name: APP_ROUTE_NAMES.LOGIN })
}













</script>
