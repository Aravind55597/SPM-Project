﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SPM_Project.Data;

namespace SPM_Project.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211023092639_update-schema")]
    partial class updateschema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("LMSUserId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdateTimestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("LMSUserId")
                        .IsUnique()
                        .HasFilter("[LMSUserId] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.Chapter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseClassId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateTimeStamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseClassId");

                    b.ToTable("Chapter");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.ClassEnrollmentRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CompletionStatus")
                        .HasColumnType("bit");

                    b.Property<int?>("CourseClassId")
                        .HasColumnType("int");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAssigned")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEnrollled")
                        .HasColumnType("bit");

                    b.Property<int?>("LMSUserId")
                        .HasColumnType("int");

                    b.Property<decimal>("PercentageScore")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdateTimestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseClassId");

                    b.HasIndex("CourseId");

                    b.HasIndex("LMSUserId");

                    b.ToTable("ClassEnrollmentRecord");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PassingPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdateTimestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.CourseClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClassTrainerId")
                        .HasColumnType("int");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndClass")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndRegistration")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GradedQuizId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Slots")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartClass")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartRegistration")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdateTimeStamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClassTrainerId");

                    b.HasIndex("CourseId");

                    b.HasIndex("GradedQuizId");

                    b.ToTable("CourseClass");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.LMSUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LMSUser");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.ProgressTracker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChapterId")
                        .HasColumnType("int");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<bool>("HaveViewedResources")
                        .HasColumnType("bit");

                    b.Property<int?>("LMSUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateTimestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.HasIndex("CourseId");

                    b.HasIndex("LMSUserId");

                    b.ToTable("ProgressTracker");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChapterId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsGraded")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TimeLimit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdateTimeStamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.ToTable("Quiz");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.QuizQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsGraded")
                        .HasColumnType("bit");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuizId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("QuizQuestion");

                    b.HasDiscriminator<string>("QuestionType");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChapterId")
                        .HasColumnType("int");

                    b.Property<int>("Content")
                        .HasColumnType("int");

                    b.Property<string>("ContentUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdateTimestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTimestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int?>("QuizQuestionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuizQuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnswer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPM_Project.EntityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SPM_Project.EntityModels.ApplicationUser", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.LMSUser", "LMSUser")
                        .WithOne("ApplicationUser")
                        .HasForeignKey("SPM_Project.EntityModels.ApplicationUser", "LMSUserId");

                    b.Navigation("LMSUser");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.Chapter", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.CourseClass", "CourseClass")
                        .WithMany("Chapters")
                        .HasForeignKey("CourseClassId");

                    b.Navigation("CourseClass");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.ClassEnrollmentRecord", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.CourseClass", "CourseClass")
                        .WithMany("ClassEnrollmentRecords")
                        .HasForeignKey("CourseClassId");

                    b.HasOne("SPM_Project.EntityModels.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("SPM_Project.EntityModels.LMSUser", "LMSUser")
                        .WithMany("Enrollments")
                        .HasForeignKey("LMSUserId");

                    b.Navigation("Course");

                    b.Navigation("CourseClass");

                    b.Navigation("LMSUser");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.Course", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.Course", null)
                        .WithMany("PreRequisites")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.CourseClass", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.LMSUser", "ClassTrainer")
                        .WithMany("ClassesTrained")
                        .HasForeignKey("ClassTrainerId");

                    b.HasOne("SPM_Project.EntityModels.Course", "Course")
                        .WithMany("CourseClass")
                        .HasForeignKey("CourseId");

                    b.HasOne("SPM_Project.EntityModels.Quiz", "GradedQuiz")
                        .WithMany()
                        .HasForeignKey("GradedQuizId");

                    b.Navigation("ClassTrainer");

                    b.Navigation("Course");

                    b.Navigation("GradedQuiz");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.ProgressTracker", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.Chapter", "Chapter")
                        .WithMany("ProgressTrackers")
                        .HasForeignKey("ChapterId");

                    b.HasOne("SPM_Project.EntityModels.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("SPM_Project.EntityModels.LMSUser", "LMSUser")
                        .WithMany("ProgressTrackers")
                        .HasForeignKey("LMSUserId");

                    b.Navigation("Chapter");

                    b.Navigation("Course");

                    b.Navigation("LMSUser");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.Quiz", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.Chapter", "Chapter")
                        .WithMany("Quizzes")
                        .HasForeignKey("ChapterId");

                    b.Navigation("Chapter");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.QuizQuestion", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.Resource", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.Chapter", "Chapter")
                        .WithMany("Resources")
                        .HasForeignKey("ChapterId");

                    b.Navigation("Chapter");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.UserAnswer", b =>
                {
                    b.HasOne("SPM_Project.EntityModels.QuizQuestion", "QuizQuestion")
                        .WithMany("UserAnswers")
                        .HasForeignKey("QuizQuestionId");

                    b.HasOne("SPM_Project.EntityModels.LMSUser", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserId");

                    b.Navigation("QuizQuestion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.Chapter", b =>
                {
                    b.Navigation("ProgressTrackers");

                    b.Navigation("Quizzes");

                    b.Navigation("Resources");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.Course", b =>
                {
                    b.Navigation("CourseClass");

                    b.Navigation("PreRequisites");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.CourseClass", b =>
                {
                    b.Navigation("Chapters");

                    b.Navigation("ClassEnrollmentRecords");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.LMSUser", b =>
                {
                    b.Navigation("ApplicationUser");

                    b.Navigation("ClassesTrained");

                    b.Navigation("Enrollments");

                    b.Navigation("ProgressTrackers");

                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.Quiz", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("SPM_Project.EntityModels.QuizQuestion", b =>
                {
                    b.Navigation("UserAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
