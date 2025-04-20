<!-- src/views/DetailsCreance.vue -->
<template>
  <div class="container py-5">
    <div class="row border shadow p-4 my-5 rounded">
      <div class="h2 text-start text-success">
        Details der Forderung - Client
      </div>
      <hr />

      <table class="table table-striped w-100" v-if="forderung">
        <thead>
          <tr>
            <th scope="col">KundeName</th>
            <th scope="col">KundeVorname</th>
            <th scope="col">Email</th>
            <th scope="col">Password</th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{{ forderung.kunde.name }}</td>
            <td>{{ forderung.kunde.vorname }}</td>
            <td>{{ forderung.kunde.email }}</td>
            <td>{{ forderung.kunde.passwort }}</td>

          </tr>
        </tbody>
      </table>


      <div class="h2 text-start text-success">
        Details der Forderung - Schuldner
      </div>
      <hr />

      <table class="table table-striped w-100" v-if="forderung">
        <thead>
          <tr>
            <th scope="col">name</th>
            <th scope="col">vorname</th>
            <th scope="col">email</th>
            <th scope="col">Adresse</th>
            <th scope="col">Password</th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{{ forderung.schuldner.name }}</td>
            <td>{{ forderung.schuldner.vorname }}</td>
            <td>{{ forderung.schuldner.email }}</td>
            <td>{{ forderung.schuldner.adresse }}</td>
            <td>{{ forderung.schuldner.passwort }}</td>
      

          </tr>
        </tbody>
      </table>


      <div class="h2 text-start text-success">
        Weiteren Informationen
      </div>
      <hr />


      <table class="table table-striped w-100" v-if="forderung">
        <thead>
          <tr>
            <th scope="col">Kommentar</th>
            <th scope="col">betrag</th>
            <th scope="col">forderungsArt</th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{{ forderung.kommentar }}</td>
            <td>{{ forderung.betrag }}</td>
            <td>{{ forderung.forderungsart }}</td>

          </tr>
        </tbody>
      </table>


      <hr />


      &nbsp;

  
      <div class="col-md-12 d-flex justify-content-end gap-2">
     
          <select id="statut" v-model="selectedStatut" class="form-select w-50">
            <option disabled value="">Choisir un statut</option>
            <option value="Bearbeitung">Bearbeitung</option>
            <option value="Abgelehnt">Abgelehnt</option>
            <option value="Abgeschlossen">Abgeschlossen</option>
        
          </select>

        
        <button type="submit" class="btn btn-success" @click="updateStatut" >Status Aktualiesieren </button>
     
        <router-link :to="{ name: APP_ROUTE_NAMES.AdminDashboard }">
  <button type="button" class="btn btn-danger">Stornieren</button>

</router-link>


      </div>

      </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'



import { APP_ROUTE_NAMES } from '@/constants/routeNames'
// Import useRouter
import { useRouter } from 'vue-router'
const router = useRouter()
import { useForderungsService } from '@/services/forderungsService'
const forderungsService = useForderungsService()

const route = useRoute()
const forderungsId = route.params.id


const forderung = ref(null)


const selectedStatut = ref('')

// Charger les données
onMounted(async () => {
  await loadForderung()
})

// Fonction de chargement des données
async function loadForderung() {
  try {
    const data = await forderungsService.GetForderungsById(forderungsId)
    forderung.value = data
  
  } catch (error) {
    console.error('Ein fehler ist aufgetreten', error)
  }
}

// Méthode pour mettre à jour le statut
async function updateStatut() {
  if (!selectedStatut.value) {
    alert("Bitte Status auswählen")
    return
  }

  try {
    await forderungsService.UpdateForderung(forderungsId, selectedStatut.value)

    router.push({ name: APP_ROUTE_NAMES.AdminDashboard })

    await loadForderung() 
    // Recharger les données après update
    selectedStatut.value = '' // reset le champ
  } catch (error) {
    console.error("Ein fehler ist aufgetreten", error)
  }
}
</script>