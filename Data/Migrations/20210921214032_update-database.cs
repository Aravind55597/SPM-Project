using Microsoft.EntityFrameworkCore.Migrations;

namespace SPM_Project.Data.Migrations
{
    public partial class updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_QuizQuestion_QuestionsId",
                table: "Quiz");

            migrationBuilder.DropIndex(
                name: "IX_Quiz_QuestionsId",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "QuestionsId",
                table: "Quiz");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Quiz",
                newName: "ClassCourseFK");

            migrationBuilder.RenameColumn(
                name: "ViewedResource",
                table: "ProgressTracker",
                newName: "HaveViewedResources");

            migrationBuilder.RenameColumn(
                name: "MaxStudents",
                table: "CourseClass",
                newName: "Slots");

            migrationBuilder.AddColumn<int>(
                name: "Marks",
                table: "QuizQuestion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "QuizQuestion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGraded",
                table: "Quiz",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TimeLimit",
                table: "Quiz",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_QuizId",
                table: "QuizQuestion",
                column: "QuizId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestion_Quiz_QuizId",
                table: "QuizQuestion",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_CourseClass_ClassCourseFK",
                table: "Quiz");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestion_Quiz_QuizId",
                table: "QuizQuestion");

            migrationBuilder.DropIndex(
                name: "IX_QuizQuestion_QuizId",
                table: "QuizQuestion");

            migrationBuilder.DropIndex(
                name: "IX_Quiz_ClassCourseFK",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Marks",
                table: "QuizQuestion");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "QuizQuestion");

            migrationBuilder.DropColumn(
                name: "IsGraded",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "TimeLimit",
                table: "Quiz");

            migrationBuilder.RenameColumn(
                name: "ClassCourseFK",
                table: "Quiz",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "HaveViewedResources",
                table: "ProgressTracker",
                newName: "ViewedResource");

            migrationBuilder.RenameColumn(
                name: "Slots",
                table: "CourseClass",
                newName: "MaxStudents");

            migrationBuilder.AddColumn<int>(
                name: "QuestionsId",
                table: "Quiz",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_QuestionsId",
                table: "Quiz",
                column: "QuestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_QuizQuestion_QuestionsId",
                table: "Quiz",
                column: "QuestionsId",
                principalTable: "QuizQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
