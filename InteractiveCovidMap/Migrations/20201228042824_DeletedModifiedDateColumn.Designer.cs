﻿// <auto-generated />
using System;
using CovidTracking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CovidTracking.Migrations
{
    [DbContext(typeof(CovidTrackingContext))]
    [Migration("20201228042824_DeletedModifiedDateColumn")]
    partial class DeletedModifiedDateColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CovidTracking.Models.CurrentState", b =>
                {
                    b.Property<int>("CurrentStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DataQualityGrade")
                        .HasColumnType("text");

                    b.Property<int>("Date")
                        .HasColumnType("integer");

                    b.Property<int?>("Death")
                        .HasColumnType("integer");

                    b.Property<int?>("DeathConfirmed")
                        .HasColumnType("integer");

                    b.Property<int>("DeathIncrease")
                        .HasColumnType("integer");

                    b.Property<int?>("DeathProbable")
                        .HasColumnType("integer");

                    b.Property<int>("Fips")
                        .HasColumnType("integer");

                    b.Property<int?>("HospitalizedCumulative")
                        .HasColumnType("integer");

                    b.Property<int?>("HospitalizedCurrently")
                        .HasColumnType("integer");

                    b.Property<int?>("HospitalizedIncrease")
                        .HasColumnType("integer");

                    b.Property<int?>("InIcuCumulative")
                        .HasColumnType("integer");

                    b.Property<int?>("InIcuCurrently")
                        .HasColumnType("integer");

                    b.Property<string>("LastUpdateEt")
                        .HasColumnType("text");

                    b.Property<int?>("Negative")
                        .HasColumnType("integer");

                    b.Property<int?>("NegativeTestsAntibody")
                        .HasColumnType("integer");

                    b.Property<int?>("NegativeTestsPeopleAntibody")
                        .HasColumnType("integer");

                    b.Property<int?>("NegativeTestsViral")
                        .HasColumnType("integer");

                    b.Property<int?>("OnVentilatorCumulative")
                        .HasColumnType("integer");

                    b.Property<int?>("OnVentilatorCurrently")
                        .HasColumnType("integer");

                    b.Property<int?>("Pending")
                        .HasColumnType("integer");

                    b.Property<int?>("Positive")
                        .HasColumnType("integer");

                    b.Property<int?>("PositiveCasesViral")
                        .HasColumnType("integer");

                    b.Property<int?>("PositiveIncrease")
                        .HasColumnType("integer");

                    b.Property<int?>("PositiveTestsAntibody")
                        .HasColumnType("integer");

                    b.Property<int?>("PositiveTestsAntigen")
                        .HasColumnType("integer");

                    b.Property<int?>("PositiveTestsPeopleAntibody")
                        .HasColumnType("integer");

                    b.Property<int?>("PositiveTestsPeopleAntigen")
                        .HasColumnType("integer");

                    b.Property<int?>("PositiveTestsViral")
                        .HasColumnType("integer");

                    b.Property<int?>("ProbableCases")
                        .HasColumnType("integer");

                    b.Property<int?>("Recovered")
                        .HasColumnType("integer");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("StateAbbreviation")
                        .HasColumnType("text");

                    b.Property<string>("StateName")
                        .HasColumnType("text");

                    b.Property<int?>("TotalTestEncountersViral")
                        .HasColumnType("integer");

                    b.Property<int?>("TotalTestResults")
                        .HasColumnType("integer");

                    b.Property<int>("TotalTestResultsIncrease")
                        .HasColumnType("integer");

                    b.Property<string>("TotalTestResultsSource")
                        .HasColumnType("text");

                    b.Property<int?>("TotalTestsAntibody")
                        .HasColumnType("integer");

                    b.Property<int?>("TotalTestsAntigen")
                        .HasColumnType("integer");

                    b.Property<int?>("TotalTestsPeopleAntibody")
                        .HasColumnType("integer");

                    b.Property<int?>("TotalTestsPeopleAntigen")
                        .HasColumnType("integer");

                    b.Property<int?>("TotalTestsPeopleViral")
                        .HasColumnType("integer");

                    b.Property<int?>("TotalTestsViral")
                        .HasColumnType("integer");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("CurrentStateId");

                    b.HasIndex("CurrentStateId");

                    b.ToTable("CurrentStates");
                });
#pragma warning restore 612, 618
        }
    }
}
