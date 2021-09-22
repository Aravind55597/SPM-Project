using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class _22092021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseClassId",
                table: "ClassEnrollmentRecord",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassEnrollmentRecord_CourseClassId",
                table: "ClassEnrollmentRecord",
                column: "CourseClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassEnrollmentRecord_CourseClass_CourseClassId",
                table: "ClassEnrollmentRecord",
                column: "CourseClassId",
                principalTable: "CourseClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassEnrollmentRecord_CourseClass_CourseClassId",
                table: "ClassEnrollmentRecord");

            migrationBuilder.DropIndex(
                name: "IX_ClassEnrollmentRecord_CourseClassId",
                table: "ClassEnrollmentRecord");

            migrationBuilder.DropColumn(
                name: "CourseClassId",
                table: "ClassEnrollmentRecord");
        }
    }
}
