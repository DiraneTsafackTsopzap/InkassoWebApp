using System;

namespace backend.Models
{
    public class Kunde
    {
        public Guid KundeId { get; set; } = Guid.NewGuid();
        public string Email { get; set; }

        public string Passwort { get; set; }

        public string Name { get; set; }

        public string Vorname { get; set; }

        public string Rolle { get; set; } = "Kunde";

    }
}
