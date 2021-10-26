using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class updateclassenrollmenrecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Withdrawm",
                table: "ClassEnrollmentRecord",
                newName: "IsWithdrawal");

            migrationBuilder.RenameColumn(
                name: "Approved",
                table: "ClassEnrollmentRecord",
                newName: "IsEnrollled");

            migrationBuilder.AddColumn<bool>(
                name: "IsAssigned",
                table: "ClassEnrollmentRecord",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAssigned",
                table: "ClassEnrollmentRecord");

            migrationBuilder.RenameColumn(
                name: "IsWithdrawal",
                table: "ClassEnrollmentRecord",
                newName: "Withdrawm");

            migrationBuilder.RenameColumn(
                name: "IsEnrollled",
                table: "ClassEnrollmentRecord",
                newName: "Approved");
        }
    }
}
