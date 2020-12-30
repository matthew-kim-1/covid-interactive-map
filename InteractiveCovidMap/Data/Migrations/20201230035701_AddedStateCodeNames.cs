using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CovidTracking.Data.Migrations
{
    public partial class AddedStateCodeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StateCodeNames",
                columns: table => new
                {
                    StateCodeNameId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fips = table.Column<int>(type: "integer", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    StateName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateCodeNames", x => x.StateCodeNameId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentStates_Fips",
                table: "CurrentStates",
                column: "Fips");

            migrationBuilder.CreateIndex(
                name: "IX_StateCodeNames_Fips",
                table: "StateCodeNames",
                column: "Fips");

            migrationBuilder.CreateIndex(
                name: "IX_StateCodeNames_StateCodeNameId",
                table: "StateCodeNames",
                column: "StateCodeNameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StateCodeNames");

            migrationBuilder.DropIndex(
                name: "IX_CurrentStates_Fips",
                table: "CurrentStates");
        }
    }
}
