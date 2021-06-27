using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspirantDatabase.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cathedras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathedras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cathedras_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "Date", nullable: false),
                    Citizenship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Passport = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Workbook = table.Column<bool>(type: "bit", nullable: false),
                    Workplaces = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Contacts = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CathedraId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specialties_Cathedras_CathedraId",
                        column: x => x.CathedraId,
                        principalTable: "Cathedras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CathedraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Cathedras_CathedraId",
                        column: x => x.CathedraId,
                        principalTable: "Cathedras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abiturients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Speciaties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdmissionDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abiturients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abiturients_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enducations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Document = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EndDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Excellent = table.Column<bool>(type: "bit", nullable: false),
                    CountSatisfactoryMarks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enducations_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateExams_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryExams_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aspirants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    ForeignLanguage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnducationForm = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EnducationDirection = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false),
                    Decree = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DissertationTheme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aspirants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aspirants_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aspirants_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aspirants_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamType = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true),
                    SpecialtyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exams_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PassingEntryExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    EntryExamId = table.Column<int>(type: "int", nullable: true),
                    AbiturientId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassingEntryExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassingEntryExams_Abiturients_AbiturientId",
                        column: x => x.AbiturientId,
                        principalTable: "Abiturients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassingEntryExams_EntryExams_EntryExamId",
                        column: x => x.EntryExamId,
                        principalTable: "EntryExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Abstracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspirantId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Place = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Document = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SubjectEdit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlaceEdit = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DateTimeEdit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileNameEdit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileTimeEdit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentEdit = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abstracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abstracts_Aspirants_AspirantId",
                        column: x => x.AspirantId,
                        principalTable: "Aspirants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspirantId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Place = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubjectEdit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlaceEdit = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DateTimeEdit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conferences_Aspirants_AspirantId",
                        column: x => x.AspirantId,
                        principalTable: "Aspirants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diploms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspirantId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: true),
                    CountSatisfactoryMarks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diploms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diploms_Aspirants_AspirantId",
                        column: x => x.AspirantId,
                        principalTable: "Aspirants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diploms_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PassingCandidateExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    AspirantId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassingCandidateExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassingCandidateExams_Aspirants_AspirantId",
                        column: x => x.AspirantId,
                        principalTable: "Aspirants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassingCandidateExams_CandidateExams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "CandidateExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrelProtections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspirantId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Commission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allowmance = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrelProtections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrelProtections_Aspirants_AspirantId",
                        column: x => x.AspirantId,
                        principalTable: "Aspirants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Protections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspirantId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    City = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    University = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Commission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Protections_Aspirants_AspirantId",
                        column: x => x.AspirantId,
                        principalTable: "Aspirants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspirantId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Journal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Page = table.Column<int>(type: "int", nullable: false),
                    SubjectEdit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JournalEdit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumberEdit = table.Column<int>(type: "int", nullable: true),
                    DateEdit = table.Column<DateTime>(type: "date", nullable: true),
                    PageEdit = table.Column<int>(type: "int", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_Aspirants_AspirantId",
                        column: x => x.AspirantId,
                        principalTable: "Aspirants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassingExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    EntryExamId = table.Column<int>(type: "int", nullable: true),
                    AspirantId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassingExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassingExams_Aspirants_AspirantId",
                        column: x => x.AspirantId,
                        principalTable: "Aspirants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassingExams_Exams_EntryExamId",
                        column: x => x.EntryExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abiturients_PersonId",
                table: "Abiturients",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Abstracts_AspirantId",
                table: "Abstracts",
                column: "AspirantId");

            migrationBuilder.CreateIndex(
                name: "IX_Aspirants_PersonId",
                table: "Aspirants",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Aspirants_SpecialtyId",
                table: "Aspirants",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Aspirants_TeacherId",
                table: "Aspirants",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateExams_SpecialtyId",
                table: "CandidateExams",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cathedras_FacultyId",
                table: "Cathedras",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_AspirantId",
                table: "Conferences",
                column: "AspirantId");

            migrationBuilder.CreateIndex(
                name: "IX_Diploms_AspirantId",
                table: "Diploms",
                column: "AspirantId");

            migrationBuilder.CreateIndex(
                name: "IX_Diploms_SpecialtyId",
                table: "Diploms",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Enducations_PersonId",
                table: "Enducations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryExams_SpecialtyId",
                table: "EntryExams",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SpecialtyId",
                table: "Exams",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_TeacherId",
                table: "Exams",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_PassingCandidateExams_AspirantId",
                table: "PassingCandidateExams",
                column: "AspirantId");

            migrationBuilder.CreateIndex(
                name: "IX_PassingCandidateExams_ExamId",
                table: "PassingCandidateExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PassingEntryExams_AbiturientId",
                table: "PassingEntryExams",
                column: "AbiturientId");

            migrationBuilder.CreateIndex(
                name: "IX_PassingEntryExams_EntryExamId",
                table: "PassingEntryExams",
                column: "EntryExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PassingExams_AspirantId",
                table: "PassingExams",
                column: "AspirantId");

            migrationBuilder.CreateIndex(
                name: "IX_PassingExams_EntryExamId",
                table: "PassingExams",
                column: "EntryExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_UserId",
                table: "Persons",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PrelProtections_AspirantId",
                table: "PrelProtections",
                column: "AspirantId");

            migrationBuilder.CreateIndex(
                name: "IX_Protections_AspirantId",
                table: "Protections",
                column: "AspirantId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_AspirantId",
                table: "Publications",
                column: "AspirantId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_CathedraId",
                table: "Specialties",
                column: "CathedraId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CathedraId",
                table: "Teachers",
                column: "CathedraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abstracts");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropTable(
                name: "Diploms");

            migrationBuilder.DropTable(
                name: "Enducations");

            migrationBuilder.DropTable(
                name: "PassingCandidateExams");

            migrationBuilder.DropTable(
                name: "PassingEntryExams");

            migrationBuilder.DropTable(
                name: "PassingExams");

            migrationBuilder.DropTable(
                name: "PrelProtections");

            migrationBuilder.DropTable(
                name: "Protections");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "CandidateExams");

            migrationBuilder.DropTable(
                name: "Abiturients");

            migrationBuilder.DropTable(
                name: "EntryExams");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Aspirants");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cathedras");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
