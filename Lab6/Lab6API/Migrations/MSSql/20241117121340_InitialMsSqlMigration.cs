using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab6API.Migrations.MSSql
{
    public partial class InitialMsSqlMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentStatusCodes",
                columns: table => new
                {
                    AppointmentStatusCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentStatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentStatusCodes", x => x.AppointmentStatusCode);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PattientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommanagedYn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhsNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PattientId);
                });

            migrationBuilder.CreateTable(
                name: "RecordComponents",
                columns: table => new
                {
                    ComponentCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordComponents", x => x.ComponentCode);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleCode);
                });

            migrationBuilder.CreateTable(
                name: "StaffCategories",
                columns: table => new
                {
                    StaffCategoryCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffCategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffCategories", x => x.StaffCategoryCode);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffBirthDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    StaffDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffQualifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffCategoryCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_Staffs_Roles_RoleCode",
                        column: x => x.RoleCode,
                        principalTable: "Roles",
                        principalColumn: "RoleCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staffs_StaffCategories_StaffCategoryCode",
                        column: x => x.StaffCategoryCode,
                        principalTable: "StaffCategories",
                        principalColumn: "StaffCategoryCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentStartDatetime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AppointmentEndDatetime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AppointmentDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointmentStatusCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_AppointmentStatusCodes_AppointmentStatusCode",
                        column: x => x.AppointmentStatusCode,
                        principalTable: "AppointmentStatusCodes",
                        principalColumn: "AppointmentStatusCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PattientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientRecords",
                columns: table => new
                {
                    PatientRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientRecordComponentDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedByStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRecords", x => x.PatientRecordId);
                    table.ForeignKey(
                        name: "FK_PatientRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PattientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientRecords_RecordComponents_ComponentCode",
                        column: x => x.ComponentCode,
                        principalTable: "RecordComponents",
                        principalColumn: "ComponentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientRecords_Staffs_UpdatedByStaffId",
                        column: x => x.UpdatedByStaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffPatientAssociations",
                columns: table => new
                {
                    AssociationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociationStartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AssociationEndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AssociationDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPatientAssociations", x => x.AssociationId);
                    table.ForeignKey(
                        name: "FK_StaffPatientAssociations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PattientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffPatientAssociations_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentStatusCode",
                table: "Appointments",
                column: "AppointmentStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StaffId",
                table: "Appointments",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecords_ComponentCode",
                table: "PatientRecords",
                column: "ComponentCode");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecords_PatientId",
                table: "PatientRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecords_UpdatedByStaffId",
                table: "PatientRecords",
                column: "UpdatedByStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffPatientAssociations_PatientId",
                table: "StaffPatientAssociations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffPatientAssociations_StaffId",
                table: "StaffPatientAssociations",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_RoleCode",
                table: "Staffs",
                column: "RoleCode");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_StaffCategoryCode",
                table: "Staffs",
                column: "StaffCategoryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "PatientRecords");

            migrationBuilder.DropTable(
                name: "StaffPatientAssociations");

            migrationBuilder.DropTable(
                name: "AppointmentStatusCodes");

            migrationBuilder.DropTable(
                name: "RecordComponents");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "StaffCategories");
        }
    }
}
