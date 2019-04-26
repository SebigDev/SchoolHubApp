using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private IHostingEnvironment _hostingEnvironment;
        public StudentsController(IStudentAppService studentAppService, IHostingEnvironment hostingEnvironment)
        {
            _studentAppService = studentAppService;
            _hostingEnvironment = hostingEnvironment;
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

                return BadRequest(ex);
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
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(StudentDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveStudentsById(long id)
        {
            try
            {
                var students = await _studentAppService.RetrieveStudentById(id);
                return Ok(students);
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true),]
        [Route("[action]")]
        [HttpPost, DisableRequestSizeLimit]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> StudentPhotoUpload(long studentId, IFormFile files)
        {
            try
            {
                var file = files;
                var completeName = string.Empty;

                var nStudent = await _studentAppService.RetrieveStudentById(studentId);

                string folderName = "Contents\\images";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(files.FileName);
                    string ext = Path.GetExtension(files.FileName);
                    var nFileName = fileName.Replace(fileName, $"{nStudent.Firstname}-{nStudent.Lastname}");
                    completeName = nFileName + ext;
                    string fullPath = Path.Combine(newPath, completeName);

                    byte[] imagePath = Encoding.ASCII.GetBytes(fullPath);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var student = new StudentDto();
                    var mStudent = nStudent;
                    if (mStudent != null)
                    {
                        mStudent.Id = studentId;
                        mStudent.Image = imagePath;
                    }
                    await _studentAppService.SavePicture(mStudent);
                }

                return Ok(completeName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet, DisableRequestSizeLimit]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RetrievePhotoByStudentId(long studentId)
       {
            try
            {
                var photo = await _studentAppService.RetrievePhotoByStudentId(studentId);
                return Ok(photo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}