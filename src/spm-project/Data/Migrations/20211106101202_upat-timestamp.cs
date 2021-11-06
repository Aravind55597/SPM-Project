using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class upattimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseClass_Quiz_GradedQuizId",
                table: "CourseClass");

            migrationBuilder.RenameColumn(
                name: "UpdateTimeStamp",
                table: "Quiz",
                newName: "UpdateTimestamp");

            migrationBuilder.RenameColumn(
                name: "CreationTimeStamp",
                table: "Quiz",
                newName: "CreationTimestamp");

            migrationBuilder.RenameColumn(
                name: "UpdateTimeStamp",
                table: "CourseClass",
                newName: "UpdateTimestamp");

            migrationBuilder.RenameColumn(
                name: "CreationTimeStamp",
                table: "CourseClass",
                newName: "CreationTimestamp");

            migrationBuilder.RenameColumn(
                name: "UpdateTimeStamp",
                table: "Chapter",
                newName: "UpdateTimestamp");

            migrationBuilder.RenameColumn(
                name: "CreationTimeStamp",
                table: "Chapter",
                newName: "CreationTimestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "UserAnswer",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "UserAnswer",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "Resource",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "Resource",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTimestamp",
                table: "QuizQuestion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "QuizQuestion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "Quiz",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "Quiz",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "ProgressTracker",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "ProgressTracker",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTimestamp",
                table: "LMSUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "LMSUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "CourseClass",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "CourseClass",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "Course",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "Course",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "ClassEnrollmentRecord",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "ClassEnrollmentRecord",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "Chapter",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "Chapter",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClass_Quiz_GradedQuizId",
                table: "CourseClass",
                column: "GradedQuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseClass_Quiz_GradedQuizId",
                table: "CourseClass");

            migrationBuilder.DropColumn(
                name: "CreationTimestamp",
                table: "QuizQuestion");

            migrationBuilder.DropColumn(
                name: "UpdateTimestamp",
                table: "QuizQuestion");

            migrationBuilder.DropColumn(
                name: "CreationTimestamp",
                table: "LMSUser");

            migrationBuilder.DropColumn(
                name: "UpdateTimestamp",
                table: "LMSUser");

            migrationBuilder.RenameColumn(
                name: "UpdateTimestamp",
                table: "Quiz",
                newName: "UpdateTimeStamp");

            migrationBuilder.RenameColumn(
                name: "CreationTimestamp",
                table: "Quiz",
                newName: "CreationTimeStamp");

            migrationBuilder.RenameColumn(
                name: "UpdateTimestamp",
                table: "CourseClass",
                newName: "UpdateTimeStamp");

            migrationBuilder.RenameColumn(
                name: "CreationTimestamp",
                table: "CourseClass",
                newName: "CreationTimeStamp");

            migrationBuilder.RenameColumn(
                name: "UpdateTimestamp",
                table: "Chapter",
                newName: "UpdateTimeStamp");

            migrationBuilder.RenameColumn(
                name: "CreationTimestamp",
                table: "Chapter",
                newName: "CreationTimeStamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "UserAnswer",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "UserAnswer",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "Resource",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "Resource",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimeStamp",
                table: "Quiz",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimeStamp",
                table: "Quiz",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "ProgressTracker",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "ProgressTracker",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimeStamp",
                table: "CourseClass",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimeStamp",
                table: "CourseClass",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "Course",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "Course",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimestamp",
                table: "ClassEnrollmentRecord",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimestamp",
                table: "ClassEnrollmentRecord",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTimeStamp",
                table: "Chapter",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTimeStamp",
                table: "Chapter",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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
