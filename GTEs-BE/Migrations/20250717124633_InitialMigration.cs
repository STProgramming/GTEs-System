using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTEs_BE.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contatti",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Passenger = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SosContact = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CarOwner = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contatti", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notifiche",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Topic = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gravity = table.Column<int>(type: "int", nullable: false),
                    Read = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifiche", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Viaggi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TripName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OriginAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DestinationAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstimatedDistanceKm = table.Column<double>(type: "double", nullable: false),
                    EstimatedDuration = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    CurrentFuelRangeKm = table.Column<double>(type: "double", nullable: false),
                    EstimatedFuelCostEuro = table.Column<double>(type: "double", nullable: false),
                    EstimatedTollCostEuro = table.Column<double>(type: "double", nullable: false),
                    ScheduledDepartureTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IncludeSuggestedStops = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IncludeTouristSuggestions = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viaggi", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Abitudini",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    ActiveDays = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    RadiusMeters = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HabitType = table.Column<int>(type: "int", nullable: false),
                    RequiredWeather = table.Column<int>(type: "int", nullable: true),
                    AutoStartClimate = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TargetTemperature = table.Column<int>(type: "int", nullable: true),
                    PlaySpotify = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SpotifyPlaylistId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TriggerTripPlanner = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PreferredTripMode = table.Column<int>(type: "int", nullable: true),
                    IdTrip = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    AssociatedTripId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RaceChipSetting = table.Column<int>(type: "int", nullable: true),
                    MinBatteryPercent = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abitudini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abitudini_Viaggi_AssociatedTripId",
                        column: x => x.AssociatedTripId,
                        principalTable: "Viaggi",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Fermate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    ExpectedStopDuration = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    IsOptional = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ViaggioId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fermate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fermate_Viaggi_ViaggioId",
                        column: x => x.ViaggioId,
                        principalTable: "Viaggi",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Abitudini_AssociatedTripId",
                table: "Abitudini",
                column: "AssociatedTripId");

            migrationBuilder.CreateIndex(
                name: "IX_Fermate_ViaggioId",
                table: "Fermate",
                column: "ViaggioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abitudini");

            migrationBuilder.DropTable(
                name: "Contatti");

            migrationBuilder.DropTable(
                name: "Fermate");

            migrationBuilder.DropTable(
                name: "Notifiche");

            migrationBuilder.DropTable(
                name: "Viaggi");
        }
    }
}
