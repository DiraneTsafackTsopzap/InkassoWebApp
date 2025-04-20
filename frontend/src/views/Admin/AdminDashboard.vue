<!-- src/views/DashboardClient.vue -->

<template>
  <div class="container py-5">
    <div class="row border shadow p-4 my-5 rounded">
      <div class="h2 text-start text-success">
        <div class="h2 text-start text-success">

          Admin - DashBoard
        </div>
      </div>
      <hr />





      <table class="table table-striped">
        <thead>
          <tr>


            <th scope="col">Kundename</th>
            <th scope="col">EinreichungsDatum</th>
            <th scope="col">ForderungsArt</th>
            <th scope="col">Status</th>
            <th scope="col">Action</th>

          </tr>
        </thead>
        <tbody>
          <tr v-for="(forderung) in forderungsService.forderungen" :key="forderung.forderungId">

            <td>{{ forderung.kunde?.name || 'N/A' }}</td>

            <td>{{ formatDate(forderung.einreichungsdatum) }}</td>
            <td>{{ forderung.forderungsArt }}</td>
            <td :class="getStatutClass(forderung.status)">
              {{ forderung.status }}
            </td>


            <td>
              <router-link class="btn btn-outline-primary btn-sm me-2" :to="`/forderungsdetails/${forderung.forderungId}`">
                Détails
              </router-link>
              <button class="btn btn-outline-danger btn-sm" @click="onDeleteCreance(forderung.forderungId)">
                Löschen
              </button>
            </td>





          </tr>

        </tbody>
      </table>
    </div>

  </div>

</template>

<script setup>
import { onMounted, ref } from 'vue'
// Import registerService
import { useRegisterService } from '@/services/registerService'
const registerStore = useRegisterService()
import { APP_ROUTE_NAMES } from '@/constants/routeNames'


import { useForderungsService } from '@/services/forderungsService'
const forderungsService = useForderungsService()

// Une variable locale pour suivre l'état de chargement
const loading = ref(true)

// Une fonction utilitaire pour formater la date
const formatDate = (dateUtc) => {
  return new Date(dateUtc).toLocaleString('de-DE');
}


const getStatutClass = (statut) => {
  switch (statut) {
    case 'Eingereicht':
      return 'text-warning fw-bold';
      case 'Bearbeitung':
      return 'text-primary  fw-bold'; 
    case 'Abgelehnt':
      return 'text-danger fw-bold';
    case 'Abgeschlossen':  // Nouveau cas pour le statut réussi
      return 'text-success fw-bold';  // Peut-être un vert plus clair
    default:
      return '';
  }
}







// Appeler la méthode du store dès le montage du composant

onMounted(async () => {
  if (registerStore.user.id) {
    try {
      await forderungsService.GetAllForderungen();
    } catch (error) {
      console.error("Erreur lors du chargement des créances:", error)
    } finally {
      loading.value = false
    }
  }
})


async function onDeleteCreance(id) {
  if (confirm("Bist du sicher ? ")) {
    try {
      await creanceService.deleteCreance(id)
      alert("Forderung erfolgreich gelöscht")
      await forderungsService.GetAllForderungen() 
    } catch (error) {
      console.error("Ein fehler ist aufgetreten :", error)
    }
  }
}

</script>
