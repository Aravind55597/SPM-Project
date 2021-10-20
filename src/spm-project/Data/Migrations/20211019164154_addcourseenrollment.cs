using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class addcourseenrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "ClassEnrollmentRecord",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassEnrollmentRecord_CourseId",
                table: "ClassEnrollmentRecord",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassEnrollmentRecord_Course_CourseId",
                table: "ClassEnrollmentRecord",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassEnrollmentRecord_Course_CourseId",
                table: "ClassEnrollmentRecord");

            migrationBuilder.DropIndex(
                name: "IX_ClassEnrollmentRecord_CourseId",
                table: "ClassEnrollmentRecord");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "ClassEnrollmentRecord");
        }
    }
}
