using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApplicationMetroNSK.Migrations;

/// <inheritdoc />
public partial class init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "DataForCalculation",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                TariffRate = table.Column<decimal>(type: "TEXT", nullable: false),
                CoefficientEvening = table.Column<decimal>(type: "TEXT", nullable: false),
                CoefficientNight = table.Column<decimal>(type: "TEXT", nullable: false),
                BonusTNP = table.Column<decimal>(type: "TEXT", nullable: false),
                Bonus = table.Column<decimal>(type: "TEXT", nullable: false),
                RegionalCoefficient = table.Column<decimal>(type: "TEXT", nullable: false),
                LengthOfService = table.Column<decimal>(type: "TEXT", nullable: false),
                Qualification = table.Column<decimal>(type: "TEXT", nullable: false),
                Taxes = table.Column<decimal>(type: "TEXT", nullable: false),
                ProfessionalFees = table.Column<decimal>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DataForCalculation", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Salary",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                HourlyPayment = table.Column<decimal>(type: "TEXT", nullable: false),
                HolidayPayment = table.Column<decimal>(type: "TEXT", nullable: false),
                EveningPayment = table.Column<decimal>(type: "TEXT", nullable: false),
                NightPayment = table.Column<decimal>(type: "TEXT", nullable: false),
                PaymentForQualification = table.Column<decimal>(type: "TEXT", nullable: false),
                LongServicePay = table.Column<decimal>(type: "TEXT", nullable: false),
                PremiumPay = table.Column<decimal>(type: "TEXT", nullable: false),
                TNPpremiymPay = table.Column<decimal>(type: "TEXT", nullable: false),
                PaymentForGapInShift = table.Column<decimal>(type: "TEXT", nullable: false),
                PaymentForTheRegionalCoefficient = table.Column<decimal>(type: "TEXT", nullable: false),
                TotalSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                ProfessionalFees = table.Column<decimal>(type: "TEXT", nullable: false),
                Taxes = table.Column<decimal>(type: "TEXT", nullable: false),
                FinalSalary = table.Column<decimal>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Salary", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "TimeCards",
            columns: table => new
            {
                NumberTimeCard = table.Column<string>(type: "TEXT", nullable: false),
                WorkType = table.Column<int>(type: "INTEGER", nullable: false),
                DayofWeek = table.Column<int>(type: "INTEGER", nullable: false),
                StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TimeCards", x => new { x.NumberTimeCard, x.WorkType, x.DayofWeek });
            });

        migrationBuilder.CreateTable(
            name: "WorkedTimeCards",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                NumberTimeCard = table.Column<string>(type: "TEXT", nullable: false),
                WorkType = table.Column<int>(type: "INTEGER", nullable: false),
                DayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                WorkHours = table.Column<decimal>(type: "TEXT", nullable: false),
                EveningHours = table.Column<decimal>(type: "TEXT", nullable: false),
                NightHours = table.Column<decimal>(type: "TEXT", nullable: false),
                ShiftGap = table.Column<decimal>(type: "TEXT", nullable: false),
                HolidayHours = table.Column<decimal>(type: "TEXT", nullable: false),
                WorkDate = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkedTimeCards", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "DataForCalculation");

        migrationBuilder.DropTable(
            name: "Salary");

        migrationBuilder.DropTable(
            name: "TimeCards");

        migrationBuilder.DropTable(
            name: "WorkedTimeCards");
    }
}
