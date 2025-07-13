using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyTenants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TPC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Listings_Id",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Beds_Listings_Id",
                table: "Beds");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestHomes_Listings_Id",
                table: "GuestHomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Listings_Id",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Listings_Id",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_UniqueSpaces_Listings_Id",
                table: "UniqueSpaces");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UniqueSpaces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "UniqueSpaces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNight",
                table: "UniqueSpaces",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "UniqueSpaces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UniqueSpaces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Houses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Houses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNight",
                table: "Houses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Houses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Hotels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNight",
                table: "Hotels",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Hotels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GuestHomes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "GuestHomes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNight",
                table: "GuestHomes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "GuestHomes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "GuestHomes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Beds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Beds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNight",
                table: "Beds",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Beds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Beds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Apartments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNight",
                table: "Apartments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Apartments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UniqueSpaces");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "UniqueSpaces");

            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "UniqueSpaces");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "UniqueSpaces");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UniqueSpaces");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GuestHomes");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "GuestHomes");

            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "GuestHomes");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "GuestHomes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "GuestHomes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Apartments");

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Listings_Id",
                table: "Apartments",
                column: "Id",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beds_Listings_Id",
                table: "Beds",
                column: "Id",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestHomes_Listings_Id",
                table: "GuestHomes",
                column: "Id",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Listings_Id",
                table: "Hotels",
                column: "Id",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Listings_Id",
                table: "Houses",
                column: "Id",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UniqueSpaces_Listings_Id",
                table: "UniqueSpaces",
                column: "Id",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
