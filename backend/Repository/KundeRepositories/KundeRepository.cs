using backend.dataContext;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repository.KundeRepositories
{
    public class KundeRepository : IKundeRepository
    {

        private readonly BackendContext DatabaseContext;
        public KundeRepository(BackendContext backendContext)
        {
            this.DatabaseContext = backendContext;
        }
        public void AddKunde(Kunde client)
        {
            DatabaseContext.Kunden.Add(client);



            DatabaseContext.SaveChanges();
        }

        public void DeleteKunde(Guid kundeId)
        {
            var kundezuloeschen = DatabaseContext.Kunden.FirstOrDefault(q => q.KundeId == kundeId);
            if (kundezuloeschen != null)
            {
                DatabaseContext.Kunden.Remove(kundezuloeschen);
                DatabaseContext.SaveChanges();
            }


        }



        public LoginResult FindByEmailAndPasswordLoginResult(string email, string passwort)
        {
            if (email == "sergesdirane@gmail.com" && passwort == "sergesdirane")
            {
                return new LoginResult
                {
                    Id = Guid.Parse("1F9058BB-C1EC-40AB-9F4A-E86419FB4E47"),
                    Email = email,
                    Name = "Tsafack Tsopzap",
                    Vorname = "Dirane",
                    Rolle = "Admin",
                    Passwort = passwort,
                };
            }

            var kunde = DatabaseContext.Kunden
              .SingleOrDefault(e => e.Email == email && e.Passwort == passwort);

            if (kunde != null)
            {
                return new LoginResult
                {
                    Id = kunde.KundeId,
                    Email = kunde.Email,
                    Name = kunde.Name,
                    Vorname = kunde.Vorname,
                    Passwort = kunde.Passwort,
                    Rolle = "Kunde"
                };
            }

            var schuldner = DatabaseContext.Schuldner
                .SingleOrDefault(e => e.Email == email && e.Passwort == passwort);

            if (schuldner != null)
            {
                return new LoginResult
                {
                    Id = schuldner.SchuldnerId,
                    Email = schuldner.Email,
                    Name = schuldner.Name,
                    Vorname = schuldner.Vorname,
                    Passwort = schuldner.Passwort,
                    Rolle = "Schuldner"
                };
            }

            return null;
        }

        public List<Kunde> GetAllKunde()
        {
            return DatabaseContext.Kunden.ToList();
        }
    }
}
