import { createApp } from 'vue'
import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'

import App from './App.vue'
import router from './router/routes'

// Global CSS
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'
import 'bootstrap-icons/font/bootstrap-icons.css'

// Crée l'application
const app = createApp(App)

// Configure Pinia avec persistance
const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)



// Branche les plugins
app.use(pinia)
app.use(router)

// ⚠️ Important : importer le store APRÈS que Pinia est branché
import { useThemeStore } from '@/stores/themeStore'
const themeStore = useThemeStore()

// Appliquer le thème avant le mount
if (themeStore.theme) {
  document.body.setAttribute('data-bs-theme', themeStore.theme)
}

// Monter l'application
app.mount('#app')
