using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class sp12292021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_CourseClass_ClassCourseFK",
                table: "Quiz");

            migrationBuilder.DropIndex(
                name: "IX_Quiz_ClassCourseFK",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "ClassCourseFK",
                table: "Quiz");

            migrationBuilder.AddColumn<int>(
                name: "GradedQuizId",
                table: "CourseClass",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseClass_GradedQuizId",
                table: "CourseClass",
                column: "GradedQuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClass_Quiz_GradedQuizId",
                table: "CourseClass",
                column: "GradedQuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseClass_Quiz_GradedQuizId",
                table: "CourseClass");

            migrationBuilder.DropIndex(
                name: "IX_CourseClass_GradedQuizId",
                table: "CourseClass");

            migrationBuilder.DropColumn(
                name: "GradedQuizId",
                table: "CourseClass");

            migrationBuilder.AddColumn<int>(
                name: "ClassCourseFK",
                table: "Quiz",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_ClassCourseFK",
                table: "Quiz",
                column: "ClassCourseFK",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_CourseClass_ClassCourseFK",
                table: "Quiz",
                column: "ClassCourseFK",
                principalTable: "CourseClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
