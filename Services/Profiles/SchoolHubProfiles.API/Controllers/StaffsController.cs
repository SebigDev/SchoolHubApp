using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolHubProfiles.Application.Services.Staffs;
using SchoolHubProfiles.Core.DTOs.Staffs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SchoolHubProfiles.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffAppService _staffAppService;
        private IHostingEnvironment _hostingEnvironment;

        public StaffsController(IStaffAppService staffAppService, IHostingEnvironment hostingEnvironment)
        {
            _staffAppService = staffAppService;
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffDto model)
        {
            try
            {
                var staff = await _staffAppService.InsertStaff(model);
                if(staff < 1)
                {
                    return BadRequest("Staff could not be created");
                }
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                    return NotFound("Staff does not exist");
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(StaffQualificationResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveStaffByUserId(long userId)
        {
            try
            {
                var staff = await _staffAppService.RetriveStaffByUserId(userId);
                if (staff == null)
                    return NotFound($"No Staff with User Id: {userId} Found");
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StaffDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveStaffByStaffByUserType(int userType)
        {
            try
            {
                var staff = await _staffAppService.RetrieveStaffByUserType(userType);
                if (staff == null)
                    return NotFound("No Staff Found");
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
            }

        }

       // [ApiExplorerSettings(IgnoreApi = true),]
        [Route("[action]")]
        [HttpPost, DisableRequestSizeLimit]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> StaffPhotoUpload(long staffId, IFormFile files)
        {
            try
            {
                var file = files;
                var completeName = string.Empty;

                var nStaff = await _staffAppService.RetriveStaffById(staffId);

                string folderName = @"Contents/images";
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
                    var nFileName = fileName.Replace(fileName, $"{nStaff.Staff.Firstname}-{nStaff.Staff.Lastname}");
                    completeName = nFileName + ext;
                    string fullPath = Path.Combine(newPath, completeName);
                    var pathToDb = $"{folderName}/{completeName}";

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                         await file.CopyToAsync(stream);
                    }
                    var staff = new StaffDto();

                    if (nStaff != null)
                    {
                        staff.Id = staffId;
                        staff.ImagePath = pathToDb;
                    }
                    await _staffAppService.SavePicture(staff);
                }

                return Ok(completeName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       // [ApiExplorerSettings(IgnoreApi = true)]
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPhoto(long staffId)
        {
            try
            {
                var photo = await _staffAppService.RetriveStaffById(staffId);
                var ipAddress = HttpContext.Request.Host.ToString();
                var scheme = HttpContext.Request.Scheme.ToString();

                var contextAddress = $"{scheme}://{ipAddress}";
                var imagePath = $"{contextAddress}/{photo.Staff.ImagePath}";

                if (photo != null)
                {
                    return Ok(imagePath);
                }
                   
                return BadRequest("No Photo Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateStaff(StaffDto update)
        {
            try
            {
                var updateStatus = await _staffAppService.UpdateStaff(update);
                if (updateStatus == false)
                    return BadRequest("Staff Update Failed");
                return Ok(updateStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddQualification([FromBody] AddQualificationDto model)
        {
            try
            {
                var staff = await _staffAppService.AddQualification(model);
                if (staff < 1)
                    return BadRequest("Qualification could not be added");
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                if (staff == null)
                    return NotFound("No Qualifications Found");
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}