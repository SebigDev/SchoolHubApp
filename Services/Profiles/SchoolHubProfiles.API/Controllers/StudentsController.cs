using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolHubProfiles.Application.Services.Students;
using SchoolHubProfiles.Core.DTOs.Students;

namespace SchoolHubProfiles.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentAppService _studentAppService;
        public StudentsController(IStudentAppService studentAppService)
        {
            _studentAppService = studentAppService;
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateStudent([FromQuery] long classId,[FromBody] CreateStudentDto createStudentDto)
        {
            try
            {
                var student = await _studentAppService.InsertStudent(classId,createStudentDto);
                if(student > 0)
                {
                    return Ok(student);
                }
                return BadRequest("Student Creation Failed");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(StudentClassResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveStudentsByClassId([FromQuery] long classId)
            {
              try
              {
                var students = await _studentAppService.RetrieveStudentsByClassID(classId);
                return Ok(students);
              }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}