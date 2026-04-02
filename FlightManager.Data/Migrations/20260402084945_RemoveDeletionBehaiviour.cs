using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeletionBehaiviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_LandingAirportIataCode",
                table: "Flights");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_EGN",
                table: "Passengers",
                column: "EGN",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_LandingAirportIataCode",
                table: "Flights",
                column: "LandingAirportIataCode",
                principalTable: "Airports",
                principalColumn: "IataCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_LandingAirportIataCode",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_EGN",
                table: "Passengers");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_LandingAirportIataCode",
                table: "Flights",
                column: "LandingAirportIataCode",
                principalTable: "Airports",
                principalColumn: "IataCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
