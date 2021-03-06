﻿using System;
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
        [ProducesResponseType(typeof(long),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Register([FromBody] CreateUserDto model)
        {
            try
            {
                var checkUser = await _userAppService.GetUserByEmailAddress(model.EmailAddress);
                if (checkUser != null)
                    return BadRequest($"User with {model.EmailAddress} already exists");

                var userId = await _userAppService.InsertUser(model);
                if(userId < 1)
                {
                    return BadRequest("Your registration failed, please contact administrator or try again");
                }
                return Ok($"User with the Id of {userId} created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(UserLoginResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login(string username, string email, string password)
        {
            try
            {

                var nUser = await _userAppService.Login(username,email,password);
                if (nUser == null)
                    return Unauthorized();
                var token = await _userAppService.GenerateToken(nUser);
                var loginResponse = new UserLoginResponse
                {
                    Success = true,
                    Token = token,
                    UserId = nUser.Id,
                    ExpiryDate = DateTime.Now.AddDays(2)
                };
                return Ok(loginResponse);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser(UpdateUserDto update)
        {
            try
            {
               var isUpdated = await _userAppService.UpdateUser(update);
                if(isUpdated == false)
                {
                    return BadRequest("User update failed, please try again");
                }
                return Ok(isUpdated);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUser(long Id)
        {
            try
            {
                var delete = await _userAppService.DeleteUser(Id);
                if (delete == true)
                {
                    return Ok("Deleted Successfully");
                }
                return BadRequest("Ooops!! user cannot be deleted, confirm you have the permissions");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveUserByEmail(string email)
        {
            try
            {
                var user = await _userAppService.GetUserByEmailAddress(email);
                if (user == null)
                    return BadRequest();
                return Ok(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveUserById(long Id)
        {
            try
            {
                var user = await _userAppService.RetrieveUser(Id);
                if (user == null)
                    return BadRequest();
                return Ok(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveAllUsers()
        {
            try
            {
                var user = await _userAppService.RetrieveAllUsers();
                if (user == null)
                    return BadRequest();
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
