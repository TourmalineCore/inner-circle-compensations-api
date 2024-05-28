using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class RenameCompensationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCompensation",
                table: "Compensations",
                newName: "CompensationRequestedForYearAndMonth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompensationRequestedForYearAndMonth",
                table: "Compensations",
                newName: "CompensationRequestedForYearAndMonth");
        }
    }
}
