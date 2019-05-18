using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolHub.Core.Extensions;
using SchoolHubProfiles.Application.Services.Classes;
using SchoolHubProfiles.Core.Context;
using SchoolHubProfiles.Core.DTOs.Students;
using SchoolHubProfiles.Core.Models.Mapping;
using SchoolHubProfiles.Core.Models.Students;

namespace SchoolHubProfiles.Application.Services.Students
{
    public class StudentAppService : IStudentAppService
    {
        private readonly SchoolHubDbContext _schoolHubDbContext;
        private readonly IClassAppService _classAppService;
        public StudentAppService(SchoolHubDbContext schoolHubDbContext, IClassAppService classAppService)
        {
            _schoolHubDbContext = schoolHubDbContext;
            _classAppService = classAppService;
        }

        public async Task<long> InsertStudent(long classId, CreateStudentDto model)
        {

            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (classId < 1)
                throw new ArgumentNullException(nameof(classId));

            var student = new Student
            {
                Firstname = model.Firstname,
                Middlename = model.Middlename,
                Lastname = model.Lastname,
                DateOfBirth = model.DateOfBirth,
                DateOfRegistration = model.DateOfRegistration,
                Gender = model.Gender.GetDescription(),
            };
            await _schoolHubDbContext.Student.AddAsync(student);
            await _schoolHubDbContext.SaveChangesAsync();

            var classMapp = await _classAppService.RetrieveClassById(classId);
           var studentClassMap = new StudentClassMap
            {
                ClassId = classMapp.Id,
                StudentId = student.Id,
            };

            await _schoolHubDbContext.StudentClassMap.AddAsync(studentClassMap);
            await _schoolHubDbContext.SaveChangesAsync();

            return student.Id;
        }
        public Task<IEnumerable<StudentDto>> RetrieveAllStudents()
        {
            throw new NotImplementedException();
        }

        public async Task<StudentDto> RetrieveStudentById(long id)
        {
            StudentDto studentDto = null;
            if (id < 1)
                throw new ArgumentNullException(nameof(id));
            var student = await _schoolHubDbContext.Student.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
                return null;
            studentDto = new StudentDto
            {
                Id = student.Id,
                Firstname = student.Firstname,
                Middlename = student.Middlename,
                Lastname = student.Lastname,
                DateOfBirth = student.DateOfBirth,
                DateOfRegistration = student.DateOfRegistration,
                Gender = student.Gender,
                Age = student.Age,
                ImagePath = student.ImagePath,
                IsActive = student.IsActive
            };
            return studentDto;
        }

        public async Task<StudentClassResponse> RetrieveStudentsByClassID(long classId)
        {
            StudentClassResponse studentClassResponses;
            var students = new List<StudentDto>();
            StudentDto studentDto;

            var studentsMap = await _schoolHubDbContext.StudentClassMap.Where(x => x.ClassId == classId).ToListAsync();
            foreach(var studentMap in studentsMap)
            {
                var student = await _schoolHubDbContext.Student.FirstOrDefaultAsync(x => x.Id == studentMap.StudentId);
                studentDto = new StudentDto
                {
                    Id = student.Id,
                    Firstname = student.Firstname,
                    Middlename = student.Middlename,
                    Lastname = student.Lastname,
                    DateOfBirth = student.DateOfBirth,
                    DateOfRegistration = student.DateOfRegistration,
                    Gender = student.Gender,
                    Age = student.Age,
                    ImagePath = student.ImagePath,
                    IsActive = student.IsActive
                };
                students.Add(studentDto);
            }
            studentClassResponses = new StudentClassResponse
            {
                ClassId = classId,
                Students = students
            };
            return studentClassResponses;
        }

        public async Task SavePicture(StudentDto studentDto)
        {
            var student = await _schoolHubDbContext.Student.FirstOrDefaultAsync(x => x.Id == studentDto.Id);
            if (student != null)
            {
                student.Id = studentDto.Id;
                student.ImagePath = studentDto.ImagePath;
            }
            _schoolHubDbContext.Entry(student).State = EntityState.Modified;
            await _schoolHubDbContext.SaveChangesAsync();
        }

        public async Task<string> RetrievePhotoByStudentId(long studentId)
        {
            var student = await _schoolHubDbContext.Student.FirstOrDefaultAsync(x => x.Id == studentId);
            if (student != null)
            {
                return student.ImagePath;
            }
            return null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~StudentAppService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
             GC.SuppressFinalize(this);
        }
        #endregion
    }
}
