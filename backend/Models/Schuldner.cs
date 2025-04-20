using System;

namespace backend.Models
{

    public class Schuldner
    {
        public Guid SchuldnerId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Passwort { get; set; }

        public string Adresse { get; set; }

        public string Vorname { get; set; }
        public string Rolle { get; set; } = "Schuldner";

    }


}
