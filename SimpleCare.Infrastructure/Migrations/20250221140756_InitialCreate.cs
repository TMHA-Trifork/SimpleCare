using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimpleCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BW_Encounters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BW_Encounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BW_IncomingPatients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BW_IncomingPatients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BW_Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonalIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GivenNames = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BW_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BW_Ward",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BW_Ward", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EW_Encounters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EncounterReason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EW_Encounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EW_Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonalIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GivenNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    wardIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EW_Patients", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BW_Ward",
                columns: new[] { "Id", "Identifier", "Name" },
                values: new object[,]
                {
                    { new Guid("2edd4f62-8b8a-437e-9eef-5cb14de87a94"), "M2", "Medical Department 2" },
                    { new Guid("c5e5f332-8c68-4059-94ea-180ca17ab1e4"), "M1", "Medical Department 1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BW_Encounters");

            migrationBuilder.DropTable(
                name: "BW_IncomingPatients");

            migrationBuilder.DropTable(
                name: "BW_Patients");

            migrationBuilder.DropTable(
                name: "BW_Ward");

            migrationBuilder.DropTable(
                name: "EW_Encounters");

            migrationBuilder.DropTable(
                name: "EW_Patients");
        }
    }
}
