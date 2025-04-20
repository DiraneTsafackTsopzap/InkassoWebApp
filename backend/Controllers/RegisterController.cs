using backend.Models;
using backend.Repository.KundeRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IKundeRepository kundeRepositories;
        private readonly IConfiguration configuration;
        public RegisterController(IKundeRepository repository)
        {
            this.kundeRepositories = repository;

        }

        [HttpPost]
        public IActionResult Post(Kunde user)
        {
            try
            {
                kundeRepositories.AddKunde(user);
                return Ok(new { Message = "Kunde wurde hinzugefügt" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = $"Ein Fehler ist aufgetreten  : {ex.Message}" });
            }
        }


        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var kundelistes = kundeRepositories.GetAllKunde().ToList();


            if (kundelistes == null || !kundelistes.Any())
            {
                return Ok(new { Message = " Momentan gibt es keine Kunde." });
            }

            return Ok(kundelistes);

        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            kundeRepositories.DeleteKunde(id);
            return Ok("Kunde wurde gelöscht");
        }
    }
}
