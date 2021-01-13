using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CovidTracking.Data.Migrations
{
    public partial class AddedCountyCodeNameAndCountyDatePositive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountyCodeNames",
                columns: table => new
                {
                    CountyCodeNameId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountyFips = table.Column<int>(type: "integer", nullable: false),
                    CountyName = table.Column<string>(type: "text", nullable: true),
                    StateFips = table.Column<int>(type: "integer", nullable: false),
                    StateName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountyCodeNames", x => x.CountyCodeNameId);
                });

            migrationBuilder.CreateTable(
                name: "CountyDatePositives",
                columns: table => new
                {
                    CountyDatePositiveId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Positive = table.Column<int>(type: "integer", nullable: false),
                    CountyCodeNameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountyDatePositives", x => x.CountyDatePositiveId);
                    table.ForeignKey(
                        name: "FK_CountyDatePositives_CountyCodeNames_CountyCodeNameId",
                        column: x => x.CountyCodeNameId,
                        principalTable: "CountyCodeNames",
                        principalColumn: "CountyCodeNameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountyCodeNames_CountyCodeNameId",
                table: "CountyCodeNames",
                column: "CountyCodeNameId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyCodeNames_CountyFips",
                table: "CountyCodeNames",
                column: "CountyFips");

            migrationBuilder.CreateIndex(
                name: "IX_CountyCodeNames_StateFips",
                table: "CountyCodeNames",
                column: "StateFips");

            migrationBuilder.CreateIndex(
                name: "IX_CountyDatePositives_CountyCodeNameId",
                table: "CountyDatePositives",
                column: "CountyCodeNameId");

            migrationBuilder.CreateIndex(
                name: "IX_CountyDatePositives_CountyDatePositiveId",
                table: "CountyDatePositives",
                column: "CountyDatePositiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountyDatePositives");

            migrationBuilder.DropTable(
                name: "CountyCodeNames");
        }
    }
}
