using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidTracking.Data.Migrations
{
    public partial class ModifiedCurrentState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "CurrentStates");

            migrationBuilder.RenameColumn(
                name: "StateAbbreviation",
                table: "CurrentStates",
                newName: "PostalCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "CurrentStates",
                newName: "StateAbbreviation");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "CurrentStates",
                type: "text",
                nullable: true);
        }
    }
}
