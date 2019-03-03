using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolHub.Core.Enums;
using SchoolHubProfiles.Application.Services.Users;
using SchoolHubProfiles.Core.DTOs.Staffs;
using SchoolHubProfiles.Core.DTOs.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolHubProfiles.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateUserDto),(int)HttpStatusCode.Created)]
        public async Task<IActionResult> Register([FromBody] CreateUserDto model)
        {
            try
            {
                var userId = await _userAppService.InsertUser(model);
                return Ok($"User with the Id of {userId} created successfully");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Login(string username, string email, string password)
        {
            try
            {
                var login = await _userAppService.Login(username,email,password);
                if(login != null)
                {
                    return Ok(login);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

      
    }
}
