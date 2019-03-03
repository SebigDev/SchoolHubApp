using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolHubProfiles.Application.Services.Staffs;
using SchoolHubProfiles.Application.Services.Users;
using SchoolHubProfiles.Core.DTOs.Staffs;

namespace SchoolHubProfiles.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffAppService _staffAppService;

        public StaffsController(IStaffAppService staffAppService)
        {
            _staffAppService = staffAppService;
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffDto model)
        {
            try
            {
                var staff = await _staffAppService.InsertStaff(model);
                return Ok(staff);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(StaffDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RetrieveStaffById(long Id)
        {
            try
            {
                var staff = await _staffAppService.RetriveStaffById(Id);
                return Ok(staff);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StaffDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RetrieveStaffByStaffByUserType(int type)
        {
            try
            {
                var staff = await _staffAppService.RetrieveStaffByStaffByUserType(type);
                return Ok(staff);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StaffDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RetrieveAllStaffs()
        {
            try
            {
                var staff = await _staffAppService.RetrieveAllStaffs();
                return Ok(staff);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}