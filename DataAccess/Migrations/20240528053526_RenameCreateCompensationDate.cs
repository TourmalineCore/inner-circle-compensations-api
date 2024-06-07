using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class RenameCreateCompensationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreateCompensation",
                table: "Compensations",
                newName: "CompensationRequestedAtUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompensationRequestedAtUtc",
                table: "Compensations",
                newName: "CompensationRequestedAtUtc");
        }
    }
}
