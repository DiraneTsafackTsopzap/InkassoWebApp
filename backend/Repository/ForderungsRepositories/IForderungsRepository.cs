using backend.Models;
using System;
using System.Collections.Generic;

namespace backend.Repository.ForderungsRepositories
{
    public interface IForderungsRepository
    {
        List<Forderung> GetAllForderungen();
        void ForderungHinzufuegen(Forderung eleve);

        void ForderungLoeschen(Guid creanceId);

        void UpdateForderung(Guid id, StatusUpdate updatedCreance);

        Forderung GetForderungsById(Guid id);

        List<Forderung> GetForderungenByKundeId(Guid userId);


        List<Forderung> GetForderungBySchuldnerId(Guid userId);
    }
}
