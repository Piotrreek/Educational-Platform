﻿// <auto-generated />
using System;
using EducationPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EducationPlatform.Infrastructure.Migrations
{
    [DbContext(typeof(EducationalPlatformDbContext))]
    [Migration("20230720182426_AdjustDeleteAction")]
    partial class AdjustDeleteAction
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.DidacticMaterial", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AverageRating")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(4, 3)
                        .HasColumnType("decimal(4,3)")
                        .HasDefaultValue(0m);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DidacticMaterialType")
                        .HasColumnType("int");

                    b.Property<string>("Keywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RatingsCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<Guid>("UniversityCourseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("UniversityCourseId");

                    b.ToTable("DidacticMaterials");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.DidacticMaterialOpinion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("DidacticMaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Opinion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("DidacticMaterialId");

                    b.ToTable("DidacticMaterialOpinions");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.Faculty", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UniversityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UniversityId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0151ad19-8241-4952-943b-dcc75d9a7600"),
                            CreatedOn = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = new Guid("715d2298-ba94-4d0c-a94b-fd7b4054ad9f"),
                            CreatedOn = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Employee"
                        },
                        new
                        {
                            Id = new Guid("81a1e319-8958-457a-b59d-27bb0dcf0a06"),
                            CreatedOn = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "User"
                        });
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.University", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.UniversityCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UniversityCourseSession")
                        .HasColumnType("int");

                    b.Property<Guid>("UniversitySubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UniversitySubjectId");

                    b.ToTable("UniversityCourses");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.UniversitySubject", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("FacultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UniversitySubjectDegree")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("UniversitySubjects");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("FacultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UniversityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UniversitySubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("FacultyId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UniversityId");

                    b.HasIndex("UniversitySubjectId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.UserLogin", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsSuccess")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.UserToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("ExpirationDateTimeOffset")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TokenType")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.DidacticMaterial", b =>
                {
                    b.HasOne("EducationalPlatform.Domain.Entities.User", "Author")
                        .WithMany("DidacticMaterials")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationalPlatform.Domain.Entities.UniversityCourse", "UniversityCourse")
                        .WithMany("DidacticMaterials")
                        .HasForeignKey("UniversityCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("UniversityCourse");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.DidacticMaterialOpinion", b =>
                {
                    b.HasOne("EducationalPlatform.Domain.Entities.User", "Author")
                        .WithMany("DidacticMaterialOpinions")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EducationalPlatform.Domain.Entities.DidacticMaterial", "DidacticMaterial")
                        .WithMany("Opinions")
                        .HasForeignKey("DidacticMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("DidacticMaterial");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.Faculty", b =>
                {
                    b.HasOne("EducationalPlatform.Domain.Entities.University", "University")
                        .WithMany("Faculties")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("University");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.UniversityCourse", b =>
                {
                    b.HasOne("EducationalPlatform.Domain.Entities.UniversitySubject", "UniversitySubject")
                        .WithMany("UniversityCourses")
                        .HasForeignKey("UniversitySubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UniversitySubject");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.UniversitySubject", b =>
                {
                    b.HasOne("EducationalPlatform.Domain.Entities.Faculty", "Faculty")
                        .WithMany("UniversitySubjects")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.User", b =>
                {
                    b.HasOne("EducationalPlatform.Domain.Entities.Faculty", "Faculty")
                        .WithMany("Users")
                        .HasForeignKey("FacultyId");

                    b.HasOne("EducationalPlatform.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationalPlatform.Domain.Entities.University", "University")
                        .WithMany("Users")
                        .HasForeignKey("UniversityId");

                    b.HasOne("EducationalPlatform.Domain.Entities.UniversitySubject", "UniversitySubject")
                        .WithMany("Users")
                        .HasForeignKey("UniversitySubjectId");

                    b.Navigation("Faculty");

                    b.Navigation("Role");

                    b.Navigation("University");

                    b.Navigation("UniversitySubject");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.UserLogin", b =>
                {
                    b.HasOne("EducationalPlatform.Domain.Entities.User", null)
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.UserToken", b =>
                {
                    b.HasOne("EducationalPlatform.Domain.Entities.User", null)
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.DidacticMaterial", b =>
                {
                    b.Navigation("Opinions");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.Faculty", b =>
                {
                    b.Navigation("UniversitySubjects");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.University", b =>
                {
                    b.Navigation("Faculties");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.UniversityCourse", b =>
                {
                    b.Navigation("DidacticMaterials");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.UniversitySubject", b =>
                {
                    b.Navigation("UniversityCourses");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EducationalPlatform.Domain.Entities.User", b =>
                {
                    b.Navigation("DidacticMaterialOpinions");

                    b.Navigation("DidacticMaterials");

                    b.Navigation("UserLogins");

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
