using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class addmcqtf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMultiSelect",
                table: "QuizQuestion",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Option1",
                table: "QuizQuestion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Option2",
                table: "QuizQuestion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Option3",
                table: "QuizQuestion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Option4",
                table: "QuizQuestion",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMultiSelect",
                table: "QuizQuestion");

            migrationBuilder.DropColumn(
                name: "Option1",
                table: "QuizQuestion");

            migrationBuilder.DropColumn(
                name: "Option2",
                table: "QuizQuestion");

            migrationBuilder.DropColumn(
                name: "Option3",
                table: "QuizQuestion");

            migrationBuilder.DropColumn(
                name: "Option4",
                table: "QuizQuestion");
        }
    }
}
