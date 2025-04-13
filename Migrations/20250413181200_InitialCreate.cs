using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Distance = table.Column<double>(type: "double precision", nullable: false),
                    AthleteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SportType = table.Column<string>(type: "text", nullable: false),
                    AthleteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workouts_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AthleteId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SportType = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MovingTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ElapsedTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    TotalElevationGain = table.Column<int>(type: "integer", nullable: false),
                    Distance = table.Column<double>(type: "double precision", nullable: false),
                    AverageSpeed = table.Column<double>(type: "double precision", nullable: false),
                    MaxSpeed = table.Column<double>(type: "double precision", nullable: false),
                    AverageHeartRate = table.Column<double>(type: "double precision", nullable: true),
                    MaxHeartRate = table.Column<int>(type: "integer", nullable: true),
                    AveragePower = table.Column<double>(type: "double precision", nullable: true),
                    MaxPower = table.Column<int>(type: "integer", nullable: true),
                    WorkoutId = table.Column<int>(type: "integer", nullable: true),
                    EquipmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activities_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkoutSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    TargetMinHeartRate = table.Column<int>(type: "integer", nullable: true),
                    TargetMaxHeartRate = table.Column<int>(type: "integer", nullable: true),
                    TargetMinPower = table.Column<int>(type: "integer", nullable: true),
                    TargetMaxPower = table.Column<int>(type: "integer", nullable: true),
                    WorkoutId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutSteps_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AthleteId",
                table: "Activities",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_EquipmentId",
                table: "Activities",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_WorkoutId",
                table: "Activities",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_AthleteId",
                table: "Equipments",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_AthleteId",
                table: "Workouts",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSteps_WorkoutId",
                table: "WorkoutSteps",
                column: "WorkoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "WorkoutSteps");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Athletes");
        }
    }
}
