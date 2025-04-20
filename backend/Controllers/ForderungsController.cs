using backend.Models;
using backend.Repository.ForderungsRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForderungsController : ControllerBase
    {

        private readonly IForderungsRepository ForderungsRepositories;

        public ForderungsController(IForderungsRepository forderung)
        {
            this.ForderungsRepositories = forderung;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {


            var forderungsListe = ForderungsRepositories.GetAllForderungen().ToList();

            if (forderungsListe == null || !forderungsListe.Any())
            {
                return Ok(new { Message = "Momentan gibt es keine Forderung" });
            }

            return Ok(forderungsListe);

        }

        [Authorize(Roles = "Kunde")]
        [HttpPost]
        public IActionResult Post(Forderung forderung)
        {
            try
            {
                var kundeIdClaim = Guid.Parse(User.FindFirst("id")!.Value);

                if (kundeIdClaim == null)
                {
                    return Unauthorized(new { Message = "Die Benutzer-ID kann nicht abgerufen werden." });
                }




                forderung.KundeId = kundeIdClaim;

                this.ForderungsRepositories.ForderungHinzufuegen(forderung);

                return Ok(new { Message = "Forderung wurde hinzugefügt" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Ein Fehler ist aufgetreten : {ex.Message}" });
            }
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            ForderungsRepositories.ForderungLoeschen(id);
            return Ok("Forderung wurde erfolgreich gelöscht");
        }


        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(Guid id)
        {
            var creance = ForderungsRepositories.GetForderungsById(id);

            if (creance == null)
            {
                return NotFound(new { Message = "Forderung nicht gefunden." });
            }

            return Ok(creance);
        }












        [HttpPut("{id}/statut")]
        [Authorize(Roles = "Admin")]
        public IActionResult ModifierStatut(Guid id, [FromBody] StatusUpdate dto)
        {
            try
            {
                var creance = ForderungsRepositories.GetForderungsById(id);
                if (creance == null)
                {
                    return NotFound(new { Message = "Créance non trouvée." });
                }


                creance.Status = dto.Status;
                ForderungsRepositories.UpdateForderung(id, dto);

                return Ok(new { Message = "Statut Change avec succès" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erreur serveur : {ex.Message}" });
            }
        }


        [Authorize(Roles = "Kunde")]
        [HttpGet("bykunde/{userId}")]
        public IActionResult GetCreanceByClientId(Guid userId)
        {
            var creances = ForderungsRepositories.GetForderungenByKundeId(userId);

            if (creances == null || !creances.Any())
            {
                return Ok(new { Message = "Aucune créance trouvée pour cet utilisateur." });
            }

            return Ok(creances);
        }


        [Authorize(Roles = "Schuldner")]
        [HttpGet("byschuldner/{userId}")]
        public IActionResult GetCreanceByDebiteurId(Guid userId)
        {
            var creances = ForderungsRepositories.GetForderungBySchuldnerId(userId);

            if (creances == null || !creances.Any())
            {
                return Ok(new { Message = "Aucune créance trouvée pour cet utilisateur." });
            }

            return Ok(creances);
        }




        // Afficher les creances en fonction du debiteur


    }
}


// Add-Migration migration
// Update-Database



// L'application marche si et seulement si les criteres suivantes st respectes :
// 1- creation dun client
// 2-