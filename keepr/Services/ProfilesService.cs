using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class ProfilesService
    {
        private readonly AccountsRepository _repo;

        public ProfilesService(AccountsRepository repo)
        {
            _repo = repo;
        }

        internal Profile GetById(string id)
        {
            return _repo.GetById(id);
        }
    }
}