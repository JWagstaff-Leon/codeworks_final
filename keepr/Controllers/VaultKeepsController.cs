using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using keepr.Services;
using keepr.Models;
using Microsoft.AspNetCore.Authorization;
using CodeWorks.Auth0Provider;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _serv;
        private readonly VaultsService _vServ;

        public VaultKeepsController(VaultKeepsService serv, VaultsService vServ)
        {
            _serv = serv;
            _vServ = vServ;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep data)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                if(!_vServ.OwnsVault(data.VaultId, userInfo.Id))
                {
                    throw new Exception("You cannot add to this vault.");
                }
                data.CreatorId = userInfo.Id;
                VaultKeep created = _serv.Create(data);
                return Ok(created);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                VaultKeep removed = _serv.Remove(id, userInfo.Id);
                return Ok(removed);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}