using Microsoft.EntityFrameworkCore;
using SchoolHubProfiles.Core.Models.Classes;
using SchoolHubProfiles.Core.Models.Mapping;
using SchoolHubProfiles.Core.Models.Qualifications;
using SchoolHubProfiles.Core.Models.Staffs;
using SchoolHubProfiles.Core.Models.Students;
using SchoolHubProfiles.Core.Models.Subjects;
using SchoolHubProfiles.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.Context
{
        public class SchoolHubDbContext : DbContext
        {
            public SchoolHubDbContext(DbContextOptions<SchoolHubDbContext> dbContextOptions)
                : base(dbContextOptions) { }

            public DbSet<User> User { get; set; }
            public DbSet<Staff> Staff { get; set; }
            public DbSet<StaffUserMap> StaffUserMap { get; set; }
            public DbSet<Qualification> Qualification { get; set; }
            public DbSet<StaffQualificationMap> StaffQualificationMap{ get; set; }
            public DbSet<ClassName> ClassName { get; set; }
            public DbSet<Subject> Subject { get; set; }
            public DbSet<ClassSubjectMap> ClassSubjectMap { get; set; }
            public DbSet<StaffClassMap> StaffClassMap { get; set; }
            public DbSet<Student> Student { get; set; }
            public DbSet<StudentClassMap> StudentClassMap { get; set; }
    }
}
