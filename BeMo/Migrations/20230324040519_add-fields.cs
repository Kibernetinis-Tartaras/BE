using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeMo.Migrations
{
    /// <inheritdoc />
    public partial class addfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FastestKilometer",
                table: "Activity");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivitySync",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<double>(
                name: "ElevationGain",
                table: "Activity",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "Activity",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastActivitySync",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "ElevationGain",
                table: "Activity",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "Distance",
                table: "Activity",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FastestKilometer",
                table: "Activity",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
