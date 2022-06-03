using System;
using System.Collections.Generic;
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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _serv;
        private readonly VaultKeepsService _vkServ;

        public VaultsController(VaultsService serv, VaultKeepsService vkServ)
        {
            _serv = serv;
            _vkServ = vkServ;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault found = _serv.GetById(id, userInfo?.Id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<KeepVaultKeepVM>>> GetVaultKeeps(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _serv.GetById(id, userInfo?.Id); //verify vault exists is acessable by the user
                List<KeepVaultKeepVM> found = _vkServ.GetByVaultId(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault data)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                data.CreatorId = userInfo.Id;
                Vault created = _serv.Create(data);
                created.Creator = userInfo;
                return Ok(created);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault update)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                update.Id = id;
                update.CreatorId = userInfo.Id;
                Vault edited = _serv.Edit(update);
                return Ok(edited);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault removed = _serv.Remove(id, userInfo.Id);
                return Ok(removed);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}