using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolHubProfiles.Application.Services.Classes;
using SchoolHubProfiles.Core.DTOs.Classes;

namespace SchoolHubProfiles.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassAppService _classAppService;
        public ClassesController(IClassAppService classAppService)
        {
            _classAppService = classAppService;
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateClass(CreateClassDto createClassDto)
        {
            try
            {
                var nClass = await _classAppService.InsertClass(createClassDto);
                if(nClass > 0)
                {
                    return Ok(nClass);
                }
                return BadRequest("Class Creation Failed");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AssignClassToStaff(long staffId, long classId)
        {
            try
            {
                var nClass = await _classAppService.AssignClassesToStaff(staffId, classId);
                if (nClass > 0)
                {
                    return Ok(nClass);
                }
                return BadRequest("Class Assignment Failed");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateClass(ClassDto classDto)
        {
            try
            {
                var nClass = await _classAppService.UpdateClass(classDto);
                if (nClass == true)
                {
                    return Ok(nClass);
                }
                return BadRequest("Class Update Failed");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteClass(long Id)
        {
            try
            {
                var nClass = await _classAppService.DeleteClass(Id);
                if (nClass == true)
                {
                    return Ok(nClass);
                }
                return BadRequest("Class Deletion Failed");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(StaffClassAssIgnedResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveAssignedClassToStaff(long staffId)
        {
            try
            {
                var nClass = await _classAppService.RetrieveClassesForStaff(staffId);
                if (nClass != null)
                {
                    return Ok(nClass);
                }
                return BadRequest($"No Class Assigned to Staff with Id of {staffId} found");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClassDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetriveUnAssignedClasses(long staffId)
        {
            try
            {
                var nClass = await _classAppService.RetriveUnAssignedClasses(staffId);
                if (nClass != null)
                {
                    return Ok(nClass);
                }
                return BadRequest($"No Class Assigned to Staff with Id of {staffId} found");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(ClassDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveClassById(long Id)
        {
            try
            {
                var nClass = await _classAppService.RetrieveClassById(Id);
                if (nClass != null)
                {
                    return Ok(nClass);
                }
                return BadRequest($"No Class with Id of {Id} found");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClassDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveAllClasses()
        {
            try
            {
                var nClass = await _classAppService.RetrieveAllClasses();
                return Ok(nClass);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}