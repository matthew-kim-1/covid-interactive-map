using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidTracking.Migrations
{
    public partial class DeletedModifiedDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "CurrentStates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "CurrentStates",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
