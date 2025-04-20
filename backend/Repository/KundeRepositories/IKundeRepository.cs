using backend.Models;
using System;
using System.Collections.Generic;

namespace backend.Repository.KundeRepositories
{
    public interface IKundeRepository
    {
        List<Kunde> GetAllKunde();
        void AddKunde(Kunde newKunde);

        void DeleteKunde(Guid kundeId);
        LoginResult FindByEmailAndPasswordLoginResult(string email, string password);
    }
}
