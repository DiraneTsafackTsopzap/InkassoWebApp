using backend.dataContext;
using backend.Models;

namespace backend.Repository.SchuldnerRepositories
{
    public class SchuldnerRepository : ISchuldnerRepository
    {

        private readonly BackendContext backendContext;
        public SchuldnerRepository(BackendContext backendContext)
        {
            this.backendContext = backendContext;
        }
        public void AddSchuldner(Schuldner schuldner)
        {
            backendContext.Schuldner.Add(schuldner);
            backendContext.SaveChanges();
        }
    }
}
