using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmergencyPatients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "wardIdentifier",
                table: "EW_Patients",
                newName: "WardIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WardIdentifier",
                table: "EW_Patients",
                newName: "wardIdentifier");
        }
    }
}
