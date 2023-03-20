#nullable disable

namespace WebAPI.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedExternalTableForPickupLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupLocation",
                table: "CarRentingRequests");

            migrationBuilder.AddColumn<string>(
                name: "PickupLocationId",
                table: "CarRentingRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PickupLocation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupLocation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarRentingRequests_PickupLocationId",
                table: "CarRentingRequests",
                column: "PickupLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupLocation_IsDeleted",
                table: "PickupLocation",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentingRequests_PickupLocation_PickupLocationId",
                table: "CarRentingRequests",
                column: "PickupLocationId",
                principalTable: "PickupLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentingRequests_PickupLocation_PickupLocationId",
                table: "CarRentingRequests");

            migrationBuilder.DropTable(
                name: "PickupLocation");

            migrationBuilder.DropIndex(
                name: "IX_CarRentingRequests_PickupLocationId",
                table: "CarRentingRequests");

            migrationBuilder.DropColumn(
                name: "PickupLocationId",
                table: "CarRentingRequests");

            migrationBuilder.AddColumn<string>(
                name: "PickupLocation",
                table: "CarRentingRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
