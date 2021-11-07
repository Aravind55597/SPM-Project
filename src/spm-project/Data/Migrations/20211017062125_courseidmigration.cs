using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class courseidmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "ProgressTracker",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgressTracker_CourseId",
                table: "ProgressTracker",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressTracker_Course_CourseId",
                table: "ProgressTracker",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressTracker_Course_CourseId",
                table: "ProgressTracker");

            migrationBuilder.DropIndex(
                name: "IX_ProgressTracker_CourseId",
                table: "ProgressTracker");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "ProgressTracker");
        }
    }
}
