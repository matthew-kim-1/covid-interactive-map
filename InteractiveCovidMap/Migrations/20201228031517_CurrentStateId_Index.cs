using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidTracking.Migrations
{
    public partial class CurrentStateId_Index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CurrentStates_CurrentStateId",
                table: "CurrentStates",
                column: "CurrentStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CurrentStates_CurrentStateId",
                table: "CurrentStates");
        }
    }
}
