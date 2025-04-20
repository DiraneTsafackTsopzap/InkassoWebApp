<!-- src/views/DashboardClient.vue -->

<template>
    <div class="container py-5">
        <div class="row border shadow p-4 my-5 rounded">
            
                <div class="h2 text-start">
                    Bienvenue <span class="text-success">{{ registerStore.user.name }}</span> <span
                        class="text-success">{{ registerStore.user.vorname }}</span> - Offene Forderungen
                </div>


            
            <hr />





            <table class="table table-striped">
                <thead>
                    <tr>


                        <th scope="col">kundename</th>
            
                        

                        <th scope="col">Action</th>

                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(forderung) in forderungsService.forderungen" :key="forderung.forderungId">

                        <td>{{ forderung.kunde?.name || 'N/A' }}</td>
                        
                       


                        <td>


                            <button class="btn btn-outline-danger btn-sm" @click="Download(forderung.forderungId)">
                                Download
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


import { useForderungsService } from '@/services/forderungsService'
const forderungsService = useForderungsService()





const loading = ref(true)

import jsPDF from 'jspdf'
import autoTable from 'jspdf-autotable'





// Appeler la méthode du store dès le montage du composant
onMounted(async () => {

    if (registerStore.user.id) {
        try {

            // Debiteur ici
            await forderungsService.GetForderungBySchuldnerId(registerStore.user.id)
        } catch (error) {
            console.error("Erreur lors du chargement des créances:", error)
        } finally {
            loading.value = false
        }
    }
})


const Download = (forderungId) => {
    const forderung = forderungsService.forderungen.find(f => f.forderungId === forderungId)
    if (!forderung) return

    const doc = new jsPDF()





    // Données : infos générales
    const generalInfo = [
        ['Forderungsart', forderung.forderungsart],
        ['Betrag', `${forderung.betrag} €`],
        ['Status', forderung.status],
        ['Falligskeitdatum', forderung.falligskeitdatum],
        ['Einreichungsdatum', forderung.einreichungsdatum],
        ['Kommentar', forderung.kommentar],
    ]

    // Données client (kunde)
    const KundeInfo = [
        ['Name', forderung.kunde.name],
        ['Vorname', forderung.kunde.vorname],
        ['Email', forderung.kunde.email],
    ]

    // Données débiteur
    const schuldnerInfo = [
        ['Name', forderung.schuldner.name],
        ['Vorname', forderung.schuldner.vorname],
        ['Email', forderung.schuldner.email],
        ['Adresse', forderung.schuldner.adresse],
    ]

    let finalY = 20

    // Titre principal
    doc.setFontSize(16)
    doc.text(`Forderungsdetails`, 14, finalY)
    finalY += 10

    // Tableau 1 - Créance
    autoTable(doc, {
        startY: finalY,
        head: [['Feld', 'Inhalt']],
        body: generalInfo,
    })
    finalY = doc.lastAutoTable.finalY + 10

    // Tableau 2 - Client
    doc.setFontSize(14)
    doc.text('Kundeninformationen', 14, finalY)
    finalY += 5

    autoTable(doc, {
        startY: finalY,
        head: [['Feld', 'Inhalt']],
        body: KundeInfo,
    })
    finalY = doc.lastAutoTable.finalY + 10

    // Tableau 3 - Débiteur
    doc.setFontSize(14)
    doc.text('Schuldnerinformationen', 14, finalY)
    finalY += 5

    autoTable(doc, {
        startY: finalY,
        head: [['Feld', 'Inhalt']],
        body: schuldnerInfo,
    })
    // Section finale : instructions de paiement
    finalY = doc.lastAutoTable.finalY + 15




    doc.setFontSize(14)
    doc.text('Information zur Zahlungen & Kontakt', 14, finalY)
    finalY += 10

    doc.setFontSize(11)
    const instructions = [
"Bitte begleichen Sie diese Forderung, falls dies noch nicht geschehen ist, so schnell wie möglich.",
"Akzeptierte Zahlungsmethoden: Banküberweisung – Ratenzahlung – Barzahlung – Lastschrift.",
"Für Rückfragen zu den Zahlungsmodalitäten kontaktieren Sie uns bitte.",
"",
"E-Mail des Kundenservice: admin@forderung-app.com",
"Telefon: +49 123 456789",
"",
"Für weitere Fragen oder Reklamationen stehen wir Ihnen gerne zur Verfügung.",
"Vielen Dank für Ihr Verständnis."
    ]

    instructions.forEach((line, i) => {
        doc.text(line, 14, finalY + i * 7)
    })

    // Sauvegarde
    doc.save(`forderung_${forderung.kunde.name}.pdf`)
}

</script>
