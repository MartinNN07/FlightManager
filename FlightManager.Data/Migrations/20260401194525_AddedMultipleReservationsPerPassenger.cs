using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMultipleReservationsPerPassenger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Reservations_ReservationId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_EGN",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_ReservationId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Passengers");

            migrationBuilder.CreateTable(
                name: "PassengerReservations",
                columns: table => new
                {
                    PassengersEGN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReservationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerReservations", x => new { x.PassengersEGN, x.ReservationsId });
                    table.ForeignKey(
                        name: "FK_PassengerReservations_Passengers_PassengersEGN",
                        column: x => x.PassengersEGN,
                        principalTable: "Passengers",
                        principalColumn: "EGN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassengerReservations_Reservations_ReservationsId",
                        column: x => x.ReservationsId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassengerReservations_ReservationsId",
                table: "PassengerReservations",
                column: "ReservationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassengerReservations");

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Passengers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_EGN",
                table: "Passengers",
                column: "EGN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_ReservationId",
                table: "Passengers",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Reservations_ReservationId",
                table: "Passengers",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
