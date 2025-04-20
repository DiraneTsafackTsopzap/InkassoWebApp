using System;

namespace backend.Models
{
    public class LoginResult
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string Passwort { get; set; }

        public string Name { get; set; }

        public string Vorname { get; set; }

        public string Rolle { get; set; }

    }
}
