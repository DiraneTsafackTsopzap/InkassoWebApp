using backend.dataContext;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repository.ForderungsRepositories
{
    public class ForderungsRepository : IForderungsRepository
    {

        private readonly BackendContext DatabaseContext;




        public ForderungsRepository(BackendContext datacontext)
        {
            DatabaseContext = datacontext;

        }
        public void ForderungHinzufuegen(Forderung forderung)
        {

            var schuldner = DatabaseContext.Schuldner
                .FirstOrDefault(d => d.Email.Trim().ToLower() == forderung.schuldner.Email.Trim().ToLower());

            DatabaseContext.Schuldner.Add(forderung.schuldner);

            forderung.SchuldnerId = forderung.schuldner.SchuldnerId;
            forderung.Status = Forderungsart.Eingereicht;
            DatabaseContext.Forderung.Add(forderung);
            DatabaseContext.SaveChanges();
        }


        public void ForderungLoeschen(Guid id)
        {

            var forderungzuloeschen = GetForderungsById(id);

            if (forderungzuloeschen != null)
            {
                DatabaseContext.Forderung.Remove(forderungzuloeschen);
                DatabaseContext.SaveChanges();
            }
        }



        public List<Forderung> GetAllForderungen()
        {

            var ForderungListe = DatabaseContext.Forderung.Include(c => c.schuldner).Include(a => a.Kunde).ToList();



            return ForderungListe;
        }




        public List<Forderung> GetForderungenByKundeId(Guid userId)
        {
            return DatabaseContext.Forderung.Where(c => c.KundeId == userId).Include(c => c.schuldner).OrderByDescending(c => c.Einreichungsdatum) // si tu as un champ CreatedAt
         .ToList();

        }

        public Forderung GetForderungsById(Guid id)
        {
            return DatabaseContext.Forderung
                .Include(c => c.Kunde)
                .Include(c => c.schuldner)
                .FirstOrDefault(c => c.ForderungId == id);
        }

        public List<Forderung> GetForderungBySchuldnerId(Guid userId)
        {
            var forderungsListe = DatabaseContext.Forderung.Where(c => c.SchuldnerId == userId)
                                                         .Include(c => c.schuldner).Include(a => a.Kunde).
                                                         OrderByDescending(c => c.Einreichungsdatum)
         .ToList();


            return forderungsListe;

        }

        public void UpdateForderung(Guid id, StatusUpdate updatedForderung)
        {
            var forderungToUpdate = GetForderungsById(id);

            if (forderungToUpdate != null)
            {
                forderungToUpdate.Status = updatedForderung.Status;
                DatabaseContext.SaveChanges();
            }
        }


    }
}
