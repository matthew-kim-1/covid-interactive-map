using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidTracking.Data.Migrations
{
    public partial class DeletedDeprecatedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckTimeEt",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "CommercialScore",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "DateChecked",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "Hospitalized",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "NegativeIncrease",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "NegativeRegularScore",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "NegativeScore",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "PosNeg",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "PositiveScore",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "CurrentStates");

            migrationBuilder.RenameColumn(
                name: "Hash",
                table: "CurrentStates",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "CurrentStates",
                newName: "CreatedBy");

            migrationBuilder.AlterColumn<int>(
                name: "TotalTestResults",
                table: "CurrentStates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PositiveIncrease",
                table: "CurrentStates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Positive",
                table: "CurrentStates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Negative",
                table: "CurrentStates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalizedIncrease",
                table: "CurrentStates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Death",
                table: "CurrentStates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CurrentStates",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CurrentStates",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CurrentStates");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CurrentStates");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "CurrentStates",
                newName: "Hash");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CurrentStates",
                newName: "Grade");

            migrationBuilder.AlterColumn<int>(
                name: "TotalTestResults",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PositiveIncrease",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Positive",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Negative",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HospitalizedIncrease",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Death",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckTimeEt",
                table: "CurrentStates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommercialScore",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateChecked",
                table: "CurrentStates",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hospitalized",
                table: "CurrentStates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NegativeIncrease",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NegativeRegularScore",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NegativeScore",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PosNeg",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositiveScore",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "CurrentStates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
