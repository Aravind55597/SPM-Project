using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class SprintOne18092021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTimestamp",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LMSUserId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LMSUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LMSUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassEnrollmentRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletionStatus = table.Column<bool>(type: "bit", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    LMSUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassEnrollmentRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassEnrollmentRecord_LMSUser_LMSUserId",
                        column: x => x.LMSUserId,
                        principalTable: "LMSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClassTrainerId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseClass_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseClass_LMSUser_ClassTrainerId",
                        column: x => x.ClassTrainerId,
                        principalTable: "LMSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuizQuestionId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    LMSUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAnswer_LMSUser_LMSUserId",
                        column: x => x.LMSUserId,
                        principalTable: "LMSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAnswer_QuizQuestion_QuizQuestionId",
                        column: x => x.QuizQuestionId,
                        principalTable: "QuizQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chapter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapter_CourseClass_CourseClassId",
                        column: x => x.CourseClassId,
                        principalTable: "CourseClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgressTracker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChapterId = table.Column<int>(type: "int", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    LMSUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressTracker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressTracker_Chapter_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgressTracker_LMSUser_LMSUserId",
                        column: x => x.LMSUserId,
                        principalTable: "LMSUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    ChapterId = table.Column<int>(type: "int", nullable: true),
                    QuestionsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quiz_Chapter_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quiz_QuizQuestion_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "QuizQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentUrl = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<int>(type: "int", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChapterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Chapter_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LMSUserId",
                table: "AspNetUsers",
                column: "LMSUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_CourseClassId",
                table: "Chapter",
                column: "CourseClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassEnrollmentRecord_LMSUserId",
                table: "ClassEnrollmentRecord",
                column: "LMSUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CourseId",
                table: "Course",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClass_ClassTrainerId",
                table: "CourseClass",
                column: "ClassTrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClass_CourseId",
                table: "CourseClass",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressTracker_ChapterId",
                table: "ProgressTracker",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressTracker_LMSUserId",
                table: "ProgressTracker",
                column: "LMSUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_ChapterId",
                table: "Quiz",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_QuestionsId",
                table: "Quiz",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ChapterId",
                table: "Resources",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_LMSUserId",
                table: "UserAnswer",
                column: "LMSUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_QuizQuestionId",
                table: "UserAnswer",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_UserId",
                table: "UserAnswer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Department_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_LMSUser_LMSUserId",
                table: "AspNetUsers",
                column: "LMSUserId",
                principalTable: "LMSUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Department_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_LMSUser_LMSUserId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ClassEnrollmentRecord");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "ProgressTracker");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "UserAnswer");

            migrationBuilder.DropTable(
                name: "Chapter");

            migrationBuilder.DropTable(
                name: "QuizQuestion");

            migrationBuilder.DropTable(
                name: "CourseClass");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "LMSUser");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LMSUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreationTimestamp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LMSUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdateTimestamp",
                table: "AspNetUsers");
        }
    }
}
