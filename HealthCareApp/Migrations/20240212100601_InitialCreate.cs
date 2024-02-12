using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HealthCareApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientAdmissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAdmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientAdmissions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientAdmissions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientAdmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalReports_PatientAdmissions_PatientAdmissionId",
                        column: x => x.PatientAdmissionId,
                        principalTable: "PatientAdmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Code", "Firstname", "IsDeleted", "Lastname", "Title" },
                values: new object[,]
                {
                    { new Guid("1e2e6559-3442-416c-afda-e35232824ce4"), "NS1", "Ken", false, "Richards", "Nurse" },
                    { new Guid("2f2e6559-3442-416c-afda-e35232824ce4"), "NS2", "Melina", false, "Diericks", "Nurse" },
                    { new Guid("6a2e6559-3442-416c-afda-e35232824ce4"), "DC1", "James", false, "Smith", "Specialist" },
                    { new Guid("7b2e6559-3442-416c-afda-e35232824ce4"), "DC2", "Kevin", false, "May", "Specialist" },
                    { new Guid("8c2e6559-3442-416c-afda-e35232824ce4"), "RES1", "Jane", false, "Johnson", "Resident" },
                    { new Guid("9d2e6559-3442-416c-afda-e35232824ce4"), "RES2", "Michael", false, "Abrams", "Resident" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "DateOfBirth", "Firstname", "Gender", "IsDeleted", "Lastname", "TelephoneNumber" },
                values: new object[,]
                {
                    { new Guid("1200a45e-8914-47bf-9035-e85aaad2b261"), "Address 1", new DateTime(1996, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michels", "Male", false, "Jones", "00023323" },
                    { new Guid("211a94b3-78cd-4a6b-babd-eb8c3a18cfea"), "Address 5", new DateTime(1991, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brock", "Male", false, "Wallace", "1111122222" },
                    { new Guid("2b99eca2-1421-4789-a3e1-00da3d953abe"), "Address 4", new DateTime(1982, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hannah", "Female", false, "Brown", "7777777" },
                    { new Guid("73776586-b4db-4769-9228-f103e8499d4f"), "Address 3", new DateTime(1984, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nick", "Male", false, "Oakenfold", "6666666" },
                    { new Guid("78eecc5e-5e7c-46d7-b472-3a1bddf289ba"), "Address 2", new DateTime(1994, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wolf", "Male", false, "Warren", "555555" },
                    { new Guid("b226f2a3-8f68-4c85-b162-4b2204f8665d"), null, new DateTime(1992, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jenna", "Female", false, "Lynn", null }
                });

            migrationBuilder.InsertData(
                table: "PatientAdmissions",
                columns: new[] { "Id", "AdmissionDateTime", "DoctorId", "IsCancelled", "IsUrgent", "PatientId" },
                values: new object[,]
                {
                    { new Guid("11aea768-8434-4468-9cd7-8034a105f31a"), new DateTime(2023, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b2e6559-3442-416c-afda-e35232824ce4"), false, false, new Guid("b226f2a3-8f68-4c85-b162-4b2204f8665d") },
                    { new Guid("21aea768-8434-4468-9cd7-8034a105f31a"), new DateTime(2020, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b2e6559-3442-416c-afda-e35232824ce4"), false, false, new Guid("78eecc5e-5e7c-46d7-b472-3a1bddf289ba") },
                    { new Guid("31aea768-8434-4468-9cd7-8034a105f31a"), new DateTime(2020, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6a2e6559-3442-416c-afda-e35232824ce4"), false, false, new Guid("73776586-b4db-4769-9228-f103e8499d4f") },
                    { new Guid("41aea768-8434-4468-9cd7-8034a105f31a"), new DateTime(2023, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6a2e6559-3442-416c-afda-e35232824ce4"), false, false, new Guid("2b99eca2-1421-4789-a3e1-00da3d953abe") },
                    { new Guid("51aea768-8434-4468-9cd7-8034a105f31a"), new DateTime(2021, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6a2e6559-3442-416c-afda-e35232824ce4"), false, false, new Guid("211a94b3-78cd-4a6b-babd-eb8c3a18cfea") },
                    { new Guid("81aea768-8434-4468-9cd7-8034a105f31a"), new DateTime(2023, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b2e6559-3442-416c-afda-e35232824ce4"), false, false, new Guid("1200a45e-8914-47bf-9035-e85aaad2b261") }
                });

            migrationBuilder.InsertData(
                table: "MedicalReports",
                columns: new[] { "Id", "DateCreated", "Description", "PatientAdmissionId" },
                values: new object[,]
                {
                    { new Guid("13024ef8-c1e7-4fea-b4e3-abf161b88196"), new DateTime(2024, 2, 12, 15, 35, 56, 26, DateTimeKind.Local).AddTicks(1684), "Description 4", new Guid("31aea768-8434-4468-9cd7-8034a105f31a") },
                    { new Guid("23024ef8-c1e7-4fea-b4e3-abf161b88196"), new DateTime(2024, 2, 12, 15, 35, 56, 26, DateTimeKind.Local).AddTicks(1688), "Description 5", new Guid("41aea768-8434-4468-9cd7-8034a105f31a") },
                    { new Guid("33024ef8-c1e7-4fea-b4e3-abf161b88196"), new DateTime(2024, 2, 12, 15, 35, 56, 26, DateTimeKind.Local).AddTicks(1674), "Description 2", new Guid("11aea768-8434-4468-9cd7-8034a105f31a") },
                    { new Guid("53024ef8-c1e7-4fea-b4e3-abf161b88196"), new DateTime(2024, 2, 12, 15, 35, 56, 26, DateTimeKind.Local).AddTicks(1680), "Description 3", new Guid("21aea768-8434-4468-9cd7-8034a105f31a") },
                    { new Guid("63024ef8-c1e7-4fea-b4e3-abf161b88196"), new DateTime(2024, 2, 12, 15, 35, 56, 26, DateTimeKind.Local).AddTicks(1692), "Description 6", new Guid("51aea768-8434-4468-9cd7-8034a105f31a") },
                    { new Guid("93024ef8-c1e7-4fea-b4e3-abf161b88196"), new DateTime(2024, 2, 12, 15, 35, 56, 26, DateTimeKind.Local).AddTicks(1669), "Description 1", new Guid("81aea768-8434-4468-9cd7-8034a105f31a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReports_PatientAdmissionId",
                table: "MedicalReports",
                column: "PatientAdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAdmissions_DoctorId",
                table: "PatientAdmissions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAdmissions_PatientId",
                table: "PatientAdmissions",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalReports");

            migrationBuilder.DropTable(
                name: "PatientAdmissions");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
