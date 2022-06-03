using keepr.Repositories;
using keepr.Models;
using System;
using System.Collections.Generic;

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
            Vault found = _repo.GetById(id);
            if(found == null)
            {
                throw new Exception("Could not find vault with that id.");
            }
            if(userId != found.CreatorId && found.IsPrivate == true)
            {
                throw new Exception("You do not have access to view this vault.");
            }
            return found;
        }

        internal bool OwnsVault(int id, string userId)
        {
            Vault found = _repo.GetById(id);
            if(found == null)
            {
                throw new Exception("Could not find vault with that id.");
            }
            return found.CreatorId == userId;
        }

        internal List<Vault> GetByUserId(string userId, string authId)
        {
            return _repo.GetByUserId(userId, authId);
        }

        internal Vault Create(Vault data)
        {
            return _repo.Create(data);
        }

        internal Vault Edit(Vault update)
        {
            Vault edited = GetById(update.Id, update.CreatorId);
            
            if(edited.CreatorId != update.CreatorId)
            {
                throw new Exception("You do not have permission to edit this vault.");
            }

            edited.Name = update.Name ?? edited.Name;
            edited.Description = update.Description ?? edited.Description;
            edited.IsPrivate = update.IsPrivate ?? edited.IsPrivate;
            return _repo.Edit(edited);
        }

        internal Vault Remove(int id, string userId)
        {
            Vault removed = GetById(id, userId);
            _repo.Remove(id);
            return removed;
        }
    }
}