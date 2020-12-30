using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CovidTracking.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<string>(type: "text", nullable: true),
                    StateAbbreviation = table.Column<string>(type: "text", nullable: true),
                    StateName = table.Column<string>(type: "text", nullable: true),
                    Positive = table.Column<int>(type: "integer", nullable: false),
                    ProbableCases = table.Column<int>(type: "integer", nullable: true),
                    Negative = table.Column<int>(type: "integer", nullable: false),
                    Pending = table.Column<int>(type: "integer", nullable: true),
                    TotalTestResultsSource = table.Column<string>(type: "text", nullable: true),
                    TotalTestResults = table.Column<int>(type: "integer", nullable: false),
                    HospitalizedCurrently = table.Column<int>(type: "integer", nullable: true),
                    HospitalizedCumulative = table.Column<int>(type: "integer", nullable: true),
                    InIcuCurrently = table.Column<int>(type: "integer", nullable: true),
                    InIcuCumulative = table.Column<int>(type: "integer", nullable: true),
                    OnVentilatorCurrently = table.Column<int>(type: "integer", nullable: true),
                    OnVentilatorCumulative = table.Column<int>(type: "integer", nullable: true),
                    Recovered = table.Column<int>(type: "integer", nullable: true),
                    DataQualityGrade = table.Column<string>(type: "text", nullable: true),
                    LastUpdateEt = table.Column<string>(type: "text", nullable: true),
                    DateModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CheckTimeEt = table.Column<string>(type: "text", nullable: true),
                    Death = table.Column<int>(type: "integer", nullable: false),
                    Hospitalized = table.Column<int>(type: "integer", nullable: true),
                    DateChecked = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalTestsViral = table.Column<int>(type: "integer", nullable: true),
                    PositiveTestsViral = table.Column<int>(type: "integer", nullable: true),
                    NegativeTestsViral = table.Column<int>(type: "integer", nullable: true),
                    PositiveCasesViral = table.Column<int>(type: "integer", nullable: true),
                    DeathConfirmed = table.Column<int>(type: "integer", nullable: true),
                    DeathProbable = table.Column<int>(type: "integer", nullable: true),
                    TotalTestEncountersViral = table.Column<int>(type: "integer", nullable: true),
                    TotalTestsPeopleViral = table.Column<int>(type: "integer", nullable: true),
                    TotalTestsAntibody = table.Column<int>(type: "integer", nullable: true),
                    PositiveTestsAntibody = table.Column<int>(type: "integer", nullable: true),
                    NegativeTestsAntibody = table.Column<int>(type: "integer", nullable: true),
                    TotalTestsPeopleAntibody = table.Column<int>(type: "integer", nullable: true),
                    PositiveTestsPeopleAntibody = table.Column<int>(type: "integer", nullable: true),
                    NegativeTestsPeopleAntibody = table.Column<int>(type: "integer", nullable: true),
                    TotalTestsPeopleAntigen = table.Column<int>(type: "integer", nullable: true),
                    PositiveTestsPeopleAntigen = table.Column<int>(type: "integer", nullable: true),
                    TotalTestsAntigen = table.Column<int>(type: "integer", nullable: true),
                    PositiveTestsAntigen = table.Column<int>(type: "integer", nullable: true),
                    Fips = table.Column<int>(type: "integer", nullable: false),
                    PositiveIncrease = table.Column<int>(type: "integer", nullable: false),
                    NegativeIncrease = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<int>(type: "integer", nullable: false),
                    TotalTestResultsIncrease = table.Column<int>(type: "integer", nullable: false),
                    PosNeg = table.Column<int>(type: "integer", nullable: false),
                    DeathIncrease = table.Column<int>(type: "integer", nullable: false),
                    HospitalizedIncrease = table.Column<int>(type: "integer", nullable: false),
                    Hash = table.Column<string>(type: "text", nullable: true),
                    CommercialScore = table.Column<int>(type: "integer", nullable: false),
                    NegativeRegularScore = table.Column<int>(type: "integer", nullable: false),
                    NegativeScore = table.Column<int>(type: "integer", nullable: false),
                    PositiveScore = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Grade = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentStates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentStates");
        }
    }
}
