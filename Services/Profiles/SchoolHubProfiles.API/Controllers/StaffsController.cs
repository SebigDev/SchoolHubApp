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
        [ProducesResponseType(typeof(StaffQualificationResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveStaffById(long staffId)
        {
            try
            {
                var staff = await _staffAppService.RetriveStaffById(staffId);
                if (staff == null)
                    return NotFound();
                return Ok(staff);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StaffDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveStaffByStaffByUserType(string userType)
        {
            try
            {
                var staff = await _staffAppService.RetrieveStaffByStaffByUserType(userType);
                return Ok(staff);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StaffDto>), (int)HttpStatusCode.OK)]
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

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddQualification([FromBody] AddQualificationDto model)
        {
            try
            {
                var staff = await _staffAppService.AddQualification(model);
                return Ok(staff);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<QualificationDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetQualificationsByStaffId(long staffId)
        {
            try
            {
                var staff = await _staffAppService.GetQualificationsByStaffId(staffId);
                return Ok(staff);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}