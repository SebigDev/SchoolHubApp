﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolHubProfiles.Core.Context;

namespace SchoolHubProfiles.Core.Migrations
{
    [DbContext(typeof(SchoolHubDbContext))]
    partial class SchoolHubDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SchoolHubProfiles.Core.Models.Mapping.StaffQualificationMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("StaffId");

                    b.HasKey("Id");

                    b.ToTable("StaffQualificationMap");
                });

            modelBuilder.Entity("SchoolHubProfiles.Core.Models.Mapping.StaffUserMap", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateMapped");

                    b.Property<long>("StaffId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("StaffUserMap");
                });

            modelBuilder.Entity("SchoolHubProfiles.Core.Models.Qualifications.Qualification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Certficate");

                    b.Property<DateTime>("DateObtained");

                    b.Property<string>("Institution");

                    b.Property<long>("StaffId");

                    b.HasKey("Id");

                    b.ToTable("Qualification");
                });

            modelBuilder.Entity("SchoolHubProfiles.Core.Models.Staffs.Staff", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<DateTime>("DateEmployed");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<DateTime>("DateOfRegistration");

                    b.Property<DateTime?>("DateUpdated");

                    b.Property<string>("Firstname");

                    b.Property<string>("Gender");

                    b.Property<bool?>("IsActive");

                    b.Property<bool>("IsUpdate");

                    b.Property<string>("Lastname");

                    b.Property<string>("Middlename");

                    b.Property<long>("UserId");

                    b.Property<string>("UserType");

                    b.HasKey("Id");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("SchoolHubProfiles.Core.Models.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress");

                    b.Property<bool?>("IsAdmin");

                    b.Property<bool>("IsEmailConfirmed");

                    b.Property<bool>("IsUpdated");

                    b.Property<string>("Password");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<DateTime>("RegisteredOn");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<int>("UserType");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}