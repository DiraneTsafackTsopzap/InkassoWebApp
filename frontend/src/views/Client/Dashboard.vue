<!-- src/views/DashboardClient.vue -->

<template>
     <div class="container py-5">
        <div class="row border shadow p-4 my-5 rounded">
            <div class="h2 text-start text-success">
                <div class="h2 text-start text-success">
        Statistiken : 
        Eingereicht : {{ countStatus('Eingereicht') }} | 
        Abgelehnt : {{ countStatus('Abgelehnt') }} | 
        Abgeschlossen : {{ countStatus('Abgeschlossen') }}
      </div>
            </div>
            <hr />
     




        <table class="table table-striped">
  <thead>
    <tr>
  
      <th scope="col">Kommentar</th>
            <th scope="col">Betrag</th>
            <th scope="col">Status</th>
            <th scope="col">EinreichungsDatum</th>
    </tr>
  </thead>
  <tbody>
    <tr v-for="(forderung) in forderungsService.forderungen" :key="forderung.forderungId">
         
            <td>{{ forderung.kommentar }}</td>
            <td>{{ forderung.betrag }}</td>
            <td :class="getStatutClass(forderung.status)">
              {{ forderung.status }}
            </td>
            <td>{{ formatDate(forderung.einreichungsdatum )}}</td>
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



import { useForderungsService } from '@/services/forderungsService'
const forderungsService = useForderungsService()

  const loading = ref(true)
  

const countStatus = (status) => {
  return forderungsService.forderungen.filter(forderung => forderung.status === status).length;
};


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
        await forderungsService.GetForderungenByKundeId(registerStore.user.id)
      } catch (error) {
        console.error("Ein fehler ist aufgetreten:", error)
      } finally {
        loading.value = false
      }
    }
  })
  </script>
