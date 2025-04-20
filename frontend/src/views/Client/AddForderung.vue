<template>
    <div class="container py-5">
        <div class="row border shadow p-4 my-5 rounded">
            <div class="h2 text-start text-success">
                Forderung einreichen
            </div>
            <hr />

            <form @submit.prevent="submitForm">

                <div class="h2 text-center text-success">
                    Angaben zum Schuldner
                </div>
                <div class="row">

                    <div class="col-md-6">
                        <label for="name" class="form-label text-muted">Name</label>
                        <input v-model="form.schuldner.name" type="text"  class="form-control" />
                    </div>


                    <div class="col-md-6">
                        <label for="vorname" class="form-label text-muted">Vorname</label>
                        <input v-model="form.schuldner.vorname" type="text"  class="form-control" />
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-6">
                        <label for="email" class="form-label text-muted">Email</label>
                        <input type="email" v-model="form.schuldner.email" class="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label for="adresse" class="form-label text-muted">Adresse</label>
                        <input type="text" v-model="form.schuldner.adresse" class="form-control" />
                    </div>
                </div>

                
                &nbsp;

                <hr />
                <div class="h2 text-center text-success">
                    ForderungsDetails
                </div>
                &nbsp;

                <div class="row">

                    <div class="col-md-4">
                        <label for="adresse" class="form-label text-muted">Montant</label>
                        <input type="number"  v-model="form.betrag" class="form-control" />
                    </div>

                    <div class="col-md-4">
                        <label for="adresse" class="form-label text-muted">Type Creance</label>
                        <select v-model="form.forderungsart" class="form-select" >
                            <option disabled value="">Veuillez sélectionner un type</option>
                            <option v-for="option in Forderungs_CATEGORIES" :key="option" :value="option">
                                {{ option }}
                            </option>
                        </select>
                    </div>
                    <div class="col-md-4">
                        <label for="adresse" class="form-label text-muted">FalligskeitDatum</label>
                        <input v-model="form.falligskeitdatum" type="date" id="email" class="form-control" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md">
                        <label for="commentaire" class="form-label text-muted">Kommentar</label>
                        <textarea  v-model="form.kommentar" id="commentaire" class="form-control" rows="4"
                            placeholder="Geben Sie Ihren Kommentar ein..."></textarea>
                    </div>

                </div>


                &nbsp;

                <!-- Bouton de soumission -->
                <div class="col-md-12 d-flex justify-content-end gap-2">

                       <!-- Bouton de soumission -->
      <button type="submit" class="btn btn-success">Einreichen</button>
      <!-- Bouton d'annulation -->
      <router-link :to="APP_ROUTE_NAMES.DASHBOARD">
        <button type="button" class="btn btn-danger">Stornieren</button>
      </router-link>

   
    </div>
            </form>
        </div>
    </div>
</template>


<script setup>
import { reactive, watch , watchEffect  } from 'vue'
import { useRoute, useRouter } from 'vue-router'

import { Forderungs_CATEGORIES } from '@/constants/appConstant'


// import des alert messages
import { useSwal } from '@/utility/useSwal'
const { showSuccess, showError, showConfirm } = useSwal()

// import des services Firestore


// import de route de redirection
import { APP_ROUTE_NAMES } from '@/constants/routeNames'
const router = useRouter()

// route params by id
const route = useRoute();

// Import registerService
import { useRegisterService } from '@/services/registerService'
const registerStore = useRegisterService()



import { useForderungsService } from '@/services/forderungsService'

const forderungsService = useForderungsService()


const form = reactive({


    // kundeid
    kundeId : registerStore.user.id,


   
  // Debiteur en tant qu'objet : faire attention  au Template
  schuldner: {
    name: '',
    vorname: '',
    email: '',
    passwort :'',
    adresse : ''
   // tu peux générer ça côté backend si tu veux
  },

  // ForderungsDetails
  betrag: '',
  forderungsart: '',
  falligskeitdatum: '',
  kommentar: ''




})

function generatePassword(vorname) {
    // Vérifie si le prénom est présent et génère le mot de passe
    if (vorname) {
        return `${vorname}123456`;  // Le prénom suivi de "123456"
    }
    return 'DefaultPassword123';  // Mot de passe par défaut si aucun prénom n'est fourni
}


watch(() => form.schuldner.vorname, (newVorname) => {
    form.schuldner.passwort = generatePassword(newVorname);
});




// Fonction pour réinitialiser le formulaire
function resetForm() {
    form.schuldner.name = '';
    form.schuldner.vorname = '';
    form.schuldner.email = '';
    form.schuldner.adresse =''
    form.schuldner.passwort =''
    form.betrag = '';
    form.forderungsart = '';
    form.falligskeitdatum = '';
    form.kommentar = '';
}



async function submitForm() {

    console.log(form)
  await forderungsService.createForderung(form)
  resetForm();
  router.push({ name: APP_ROUTE_NAMES.DASHBOARD })
}


</script>