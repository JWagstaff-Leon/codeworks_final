using System;
using System.Collections.Generic;
using keepr.Repositories;
using keepr.Models;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;

        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }

        internal VaultKeep GetById(int id)
        {
            VaultKeep found = _repo.GetById(id);
            if(found == null)
            {
                throw new Exception("Could not find vaultkeep with that id.");
            }
            return found;
        }

        internal List<KeepVaultKeepVM> GetByVaultId(int id)
        {
            return _repo.GetByVaultId(id);
        }

        internal List<VaultKeep> GetByKeepAndUserIds(int keepId, string userId)
        {
            return _repo.GetByKeepAndUserIds(keepId, userId);
        }

        internal VaultKeep Create(VaultKeep data)
        {
            return _repo.Create(data);
        }

        internal VaultKeep Remove(int id, string userId)
        {
            VaultKeep removed = GetById(id);
            if(removed.CreatorId != userId)
            {
                throw new Exception("You do not have permission to delete this vaultkeep.");
            }
            _repo.Remove(id);
            return removed;
        }
    }
}