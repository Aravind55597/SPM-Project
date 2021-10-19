using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class maelmsfkforappusernullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LMSUser_AspNetUsers_ApplicationUserId",
                table: "LMSUser");

            migrationBuilder.DropIndex(
                name: "IX_LMSUser_ApplicationUserId",
                table: "LMSUser");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "LMSUser");

            migrationBuilder.AddColumn<int>(
                name: "LMSUserId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LMSUserId",
                table: "AspNetUsers",
                column: "LMSUserId",
                unique: true,
                filter: "[LMSUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_LMSUser_LMSUserId",
                table: "AspNetUsers",
                column: "LMSUserId",
                principalTable: "LMSUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_LMSUser_LMSUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LMSUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LMSUserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "LMSUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LMSUser_ApplicationUserId",
                table: "LMSUser",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_LMSUser_AspNetUsers_ApplicationUserId",
                table: "LMSUser",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
