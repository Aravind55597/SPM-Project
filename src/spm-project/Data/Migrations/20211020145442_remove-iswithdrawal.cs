using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class removeiswithdrawal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWithdrawal",
                table: "ClassEnrollmentRecord");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWithdrawal",
                table: "ClassEnrollmentRecord",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
