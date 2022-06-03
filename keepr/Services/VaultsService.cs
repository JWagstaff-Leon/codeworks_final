using keepr.Repositories;
using keepr.Models;

namespace keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault GetById(int id, string userId)
        {

        }

        internal Vault Create(Vault data)
        {

        }

        internal Vault Edit(Vault update)
        {

        }

        internal Vault Remove(int id, string userId)
        {
            
        }
    }
}