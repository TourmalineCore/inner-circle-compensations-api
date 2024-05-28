using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class RenameDateFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreateCompensation",
                table: "Compensations",
                newName: "CompensationRequestedForYearAndMonth");

            migrationBuilder.RenameColumn(
                name: "DateCompensation",
                table: "Compensations",
                newName: "CompensationRequestedAtUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompensationRequestedForYearAndMonth",
                table: "Compensations",
                newName: "DateCreateCompensation");

            migrationBuilder.RenameColumn(
                name: "CompensationRequestedAtUtc",
                table: "Compensations",
                newName: "DateCompensation");
        }
    }
}
