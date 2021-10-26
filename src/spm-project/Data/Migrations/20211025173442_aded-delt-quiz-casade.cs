using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class adeddeltquizcasade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestion_Quiz_QuizId",
                table: "QuizQuestion");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestion_Quiz_QuizId",
                table: "QuizQuestion",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestion_Quiz_QuizId",
                table: "QuizQuestion");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestion_Quiz_QuizId",
                table: "QuizQuestion",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
