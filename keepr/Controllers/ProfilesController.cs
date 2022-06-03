using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using keepr.Services;
using keepr.Models;
using Microsoft.AspNetCore.Authorization;
using CodeWorks.Auth0Provider;
using System.Threading.Tasks;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _serv;
        private readonly VaultsService _vServ;
        private readonly KeepsService _kServ;

        public ProfilesController(ProfilesService serv, VaultsService vServ, KeepsService kServ)
        {
            _serv = serv;
            _vServ = vServ;
            _kServ = kServ;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> GetById(string id)
        {
            try
            {
                Profile found = _serv.GetById(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetKeeps(string id)
        {
            try
            {
                List<Keep> found = _kServ.GetByUserId(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        [Authorize]
        public async Task<ActionResult<List<Vault>>> GetVaults(string id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Vault> found = _vServ.GetByUserId(id, userInfo.Id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}