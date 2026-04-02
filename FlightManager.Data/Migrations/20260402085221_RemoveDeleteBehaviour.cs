using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeleteBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_DepartureAirportIataCode",
                table: "Flights");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_DepartureAirportIataCode",
                table: "Flights",
                column: "DepartureAirportIataCode",
                principalTable: "Airports",
                principalColumn: "IataCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_DepartureAirportIataCode",
                table: "Flights");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_DepartureAirportIataCode",
                table: "Flights",
                column: "DepartureAirportIataCode",
                principalTable: "Airports",
                principalColumn: "IataCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
