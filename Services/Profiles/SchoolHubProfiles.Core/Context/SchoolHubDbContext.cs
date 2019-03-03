using Microsoft.EntityFrameworkCore;
using SchoolHubProfiles.Core.Models.Mapping;
using SchoolHubProfiles.Core.Models.Staffs;
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
        }
}
