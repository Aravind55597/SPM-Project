using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class remvgradedquizcouseclss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "CourseClassId",
                table: "Quiz",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_CourseClassId",
                table: "Quiz",
                column: "CourseClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_CourseClass_CourseClassId",
                table: "Quiz",
                column: "CourseClassId",
                principalTable: "CourseClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_CourseClass_CourseClassId",
                table: "Quiz");

            migrationBuilder.DropIndex(
                name: "IX_Quiz_CourseClassId",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "CourseClassId",
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
    }
}
