using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class adedsetterformarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_AspNetUsers_UserId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_LMSUser_LMSUserId",
                table: "UserAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswer_LMSUserId",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "LMSUserId",
                table: "UserAnswer");

            migrationBuilder.RenameColumn(
                name: "CreateTimestamp",
                table: "UserAnswer",
                newName: "CreationTimestamp");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserAnswer",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGraded",
                table: "QuizQuestion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_LMSUser_UserId",
                table: "UserAnswer",
                column: "UserId",
                principalTable: "LMSUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_LMSUser_UserId",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "IsGraded",
                table: "QuizQuestion");

            migrationBuilder.RenameColumn(
                name: "CreationTimestamp",
                table: "UserAnswer",
                newName: "CreateTimestamp");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAnswer",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LMSUserId",
                table: "UserAnswer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_LMSUserId",
                table: "UserAnswer",
                column: "LMSUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_AspNetUsers_UserId",
                table: "UserAnswer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_LMSUser_LMSUserId",
                table: "UserAnswer",
                column: "LMSUserId",
                principalTable: "LMSUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
