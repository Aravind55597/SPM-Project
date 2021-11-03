using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class adedtf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FalseOption",
                table: "QuizQuestion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrueOption",
                table: "QuizQuestion",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FalseOption",
                table: "QuizQuestion");

            migrationBuilder.DropColumn(
                name: "TrueOption",
                table: "QuizQuestion");
        }
    }
}
