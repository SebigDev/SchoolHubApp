using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolHubProfiles.Application.Services.Authentication;
using SchoolHubProfiles.Core.DTOs.Auth;

namespace SchoolHubProfiles.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationAppService _authenticationAppService;

        public AuthenticationController(IAuthenticationAppService authenticationAppService)
        {
            _authenticationAppService = authenticationAppService;
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangePassword(string email, string oldPassword, string newPassword)
        {
            try
            {
                var pwdChange = await _authenticationAppService.ChangePassword(email, oldPassword,newPassword);
                if(pwdChange == null)
                {
                    return BadRequest();
                }
                return Ok(pwdChange);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                var pwdReset = await _authenticationAppService.ResetPassword(request);
                if(pwdReset == false)
                {
                    return BadRequest();
                }
                return Ok(pwdReset);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}