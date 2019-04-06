using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolHubProfiles.Application.Services.Mapping;
using SchoolHubProfiles.Application.Services.Subjects;
using SchoolHubProfiles.Core.DTOs.Subjects;

namespace SchoolHubProfiles.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectAppService _subjectAppService;
        private readonly IMappingService _mappingService;
        public SubjectsController(ISubjectAppService subjectAppService, IMappingService mappingService)
        {
            _subjectAppService = subjectAppService;
            _mappingService = mappingService;
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateSubject(CreateSubjectDto createSubjectDto)
        {
            try
            {
                var nSubject = await _subjectAppService.InsertSubject(createSubjectDto);
                if (nSubject > 0)
                {
                    return Ok(nSubject);
                }
                return BadRequest("Subject Creation Failed");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateClassSubject(long classId, CreateSubjectDto createSubjectDto)
        {
            try
            {
                var nSubject = await _mappingService.MappSubjectClass(classId: classId, createSubjectDto: createSubjectDto);
                if (nSubject > 0)
                {
                    return Ok(nSubject);
                }
                return BadRequest($"Subject Creation For Class with Id {classId} Failed");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSubject(SubjectDto subjectDto)
        {
            try
            {
                var nSubject = await _subjectAppService.UpdateSubject(subjectDto);
                if (nSubject == true)
                {
                    return Ok(nSubject);
                }
                return BadRequest("Subject Update Failed");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteSubject(long Id)
        {
            try
            {
                var nSubject = await _subjectAppService.DeleteSubject(Id);
                if (nSubject == true)
                {
                    return Ok(nSubject);
                }
                return BadRequest("Subject Deletion Failed");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(SubjectDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveSubjectById(long Id)
        {
            try
            {
                var nSubject = await _subjectAppService.RetrieveSubjectById(Id);
                if (nSubject != null)
                {
                    return Ok(nSubject);
                }
                return BadRequest($"No Subject with Id of {Id} found");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(StudentSubjectsResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveAllSubjectsByStudentId(long id)
        {
            try
            {
                var studentSubj = await _subjectAppService.RetrieveSubjectsByStudentId(id);
                return Ok(studentSubj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubjectDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveAllSubjects()
        {
            try
            {
                var nSubject = await _subjectAppService.RetrieveAllSubjects();
                return Ok(nSubject);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(ClassSubjectResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveAllSubjectsByClassId(long Id)
        {
            try
            {
                var nSubject = await _subjectAppService.RetrieveSubjectByClassId(Id);
                return Ok(nSubject);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}