using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyTenants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TPT12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "HasBackyard",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "HasElevator",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "HasPool",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "IncludesBreakfast",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "IsPetFriendly",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ListingType",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "NumberOfBedrooms",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "SquareFootage",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "StarRating",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "UniqueFeature",
                table: "Listings");

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    HasElevator = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartments_Listings_Id",
                        column: x => x.Id,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IncludesBreakfast = table.Column<bool>(type: "bit", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beds_Listings_Id",
                        column: x => x.Id,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuestHomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    SquareFootage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestHomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestHomes_Listings_Id",
                        column: x => x.Id,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasPool = table.Column<bool>(type: "bit", nullable: false),
                    StarRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Listings_Id",
                        column: x => x.Id,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasBackyard = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfBedrooms = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Houses_Listings_Id",
                        column: x => x.Id,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UniqueSpaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniqueFeature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPetFriendly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniqueSpaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniqueSpaces_Listings_Id",
                        column: x => x.Id,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropTable(
                name: "Beds");

            migrationBuilder.DropTable(
                name: "GuestHomes");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "UniqueSpaces");

            migrationBuilder.AddColumn<int>(
                name: "FloorNumber",
                table: "Listings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasBackyard",
                table: "Listings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasElevator",
                table: "Listings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPool",
                table: "Listings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncludesBreakfast",
                table: "Listings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPetFriendly",
                table: "Listings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Listings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListingType",
                table: "Listings",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBedrooms",
                table: "Listings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "Listings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SquareFootage",
                table: "Listings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StarRating",
                table: "Listings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueFeature",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
