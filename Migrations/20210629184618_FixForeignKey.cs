using Microsoft.EntityFrameworkCore.Migrations;

namespace AspirantDatabase.Migrations
{
    public partial class FixForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassingEntryExams_EntryExams_EntryExamId",
                table: "PassingEntryExams");

            migrationBuilder.DropForeignKey(
                name: "FK_PassingExams_Exams_EntryExamId",
                table: "PassingExams");

            migrationBuilder.DropIndex(
                name: "IX_PassingExams_EntryExamId",
                table: "PassingExams");

            migrationBuilder.DropIndex(
                name: "IX_PassingEntryExams_EntryExamId",
                table: "PassingEntryExams");

            migrationBuilder.DropColumn(
                name: "EntryExamId",
                table: "PassingExams");

            migrationBuilder.DropColumn(
                name: "EntryExamId",
                table: "PassingEntryExams");

            migrationBuilder.CreateIndex(
                name: "IX_PassingExams_ExamId",
                table: "PassingExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PassingEntryExams_ExamId",
                table: "PassingEntryExams",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_PassingEntryExams_EntryExams_ExamId",
                table: "PassingEntryExams",
                column: "ExamId",
                principalTable: "EntryExams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassingExams_Exams_ExamId",
                table: "PassingExams",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassingEntryExams_EntryExams_ExamId",
                table: "PassingEntryExams");

            migrationBuilder.DropForeignKey(
                name: "FK_PassingExams_Exams_ExamId",
                table: "PassingExams");

            migrationBuilder.DropIndex(
                name: "IX_PassingExams_ExamId",
                table: "PassingExams");

            migrationBuilder.DropIndex(
                name: "IX_PassingEntryExams_ExamId",
                table: "PassingEntryExams");

            migrationBuilder.AddColumn<int>(
                name: "EntryExamId",
                table: "PassingExams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntryExamId",
                table: "PassingEntryExams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PassingExams_EntryExamId",
                table: "PassingExams",
                column: "EntryExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PassingEntryExams_EntryExamId",
                table: "PassingEntryExams",
                column: "EntryExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_PassingEntryExams_EntryExams_EntryExamId",
                table: "PassingEntryExams",
                column: "EntryExamId",
                principalTable: "EntryExams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PassingExams_Exams_EntryExamId",
                table: "PassingExams",
                column: "EntryExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
