using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _repo;

        public ProfilesService(ProfilesRepository repo)
        {
            _repo = repo;
        }

        internal Profile GetById(string id)
        {
            return (Profile)_repo.GetById(id);
        }
    }
}