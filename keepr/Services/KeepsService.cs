using System;
using System.Collections.Generic;
using keepr.Repositories;
using keepr.Models;

namespace keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal List<Keep> GetAll()
        {
            return _repo.GetAll();
        }

        internal Keep GetById(int id)
        {
            Keep found = _repo.GetById(id);
            if(found == null)
            {
                throw new Exception("Could not find keep with that id.");
            }
            return _repo.IncreaseViewsByOne(found);
        }

        private Keep GetByIdNoView(int id)
        {
            Keep found = _repo.GetById(id);
            if(found == null)
            {
                throw new Exception("Could not find keep with that id.");
            }
            return found;
        }

        internal Keep Create(Keep data)
        {
            return _repo.Create(data);
        }

        internal Keep Edit(Keep update)
        {
            Keep edited = GetByIdNoView(update.Id);
            if(edited.CreatorId != update.CreatorId)
            {
                throw new Exception("You do not have permission to edit this keep.");
            }
            edited.Name = update.Name ?? edited.Name;
            edited.Description = update.Description ?? edited.Description;
            edited.Img = update.Img ?? edited.Img;
            return _repo.Edit(edited);
        }

        internal Keep Remove(int id, string userId)
        {
            Keep removed = GetByIdNoView(id);
            if(removed.CreatorId != userId)
            {
                throw new Exception("You do not have permission to delete this keep.");
            }
            _repo.Remove(id);
            return removed;
        }
    }
}