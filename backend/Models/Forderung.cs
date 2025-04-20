using System;

namespace backend.Models
{

    public class Forderung
    {

        public Guid ForderungId { get; set; } = Guid.NewGuid();

        public Guid KundeId { get; set; }
        public Kunde Kunde { get; set; }

        public string Kommentar { get; set; }

        public string Forderungsart { get; set; }
        public string Falligskeitdatum { get; set; }

        public decimal Betrag { get; set; }
        public DateTime Einreichungsdatum { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(
    DateTime.UtcNow,
    TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")
);

        public Forderungsart Status { get; set; }

        public Guid SchuldnerId { get; set; }
        public Schuldner schuldner { get; set; }



    }

    public enum Forderungsart
    {
        Eingereicht,
        Bearbeitung,
        Abgelehnt,
        Abgeschlossen,




    }

}


//{
//    "kundeId": "GUID-du-client",
//  "montant": 1500.00,
//  "commentaire": "Détails de la créance",
//  "debiteur": {
//        "name": "Jean",
//    "vorname": "Dupont",
//    "email": "jean.dupont@example.com",
//    "password": "motdepasse123"
//  }
//}
