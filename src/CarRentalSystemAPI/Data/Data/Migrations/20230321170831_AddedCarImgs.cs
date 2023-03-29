#nullable disable

namespace WebAPI.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedCarImgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentingRequests_PickupLocation_PickupLocationId",
                table: "CarRentingRequests");

            migrationBuilder.DropTable(
                name: "PickupLocation");

            migrationBuilder.AddColumn<string>(
                name: "TransmissionType",
                table: "CarAdvertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.CreateTable(
                name: "CarImage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarAdvertisementId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarImage_CarAdvertisements_CarAdvertisementId",
                        column: x => x.CarAdvertisementId,
                        principalTable: "CarAdvertisements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PickupLocations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupLocations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarImage_CarAdvertisementId",
                table: "CarImage",
                column: "CarAdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_CarImage_IsDeleted",
                table: "CarImage",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PickupLocations_IsDeleted",
                table: "PickupLocations",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentingRequests_PickupLocations_PickupLocationId",
                table: "CarRentingRequests",
                column: "PickupLocationId",
                principalTable: "PickupLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentingRequests_PickupLocations_PickupLocationId",
                table: "CarRentingRequests");

            migrationBuilder.DropTable(
                name: "CarImage");

            migrationBuilder.DropTable(
                name: "PickupLocations");

            migrationBuilder.DropColumn(
                name: "TransmissionType",
                table: "CarAdvertisements");

            migrationBuilder.CreateTable(
                name: "PickupLocation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupLocation", x => x.Id);
                });

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
    }
}
