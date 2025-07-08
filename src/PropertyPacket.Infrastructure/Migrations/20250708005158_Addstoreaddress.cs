using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyPacket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addstoreaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressInfo_City",
                table: "StoreInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressInfo_Country",
                table: "StoreInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressInfo_Line1",
                table: "StoreInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressInfo_Line2",
                table: "StoreInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressInfo_PostCode",
                table: "StoreInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressInfo_City",
                table: "StoreInfos");

            migrationBuilder.DropColumn(
                name: "AddressInfo_Country",
                table: "StoreInfos");

            migrationBuilder.DropColumn(
                name: "AddressInfo_Line1",
                table: "StoreInfos");

            migrationBuilder.DropColumn(
                name: "AddressInfo_Line2",
                table: "StoreInfos");

            migrationBuilder.DropColumn(
                name: "AddressInfo_PostCode",
                table: "StoreInfos");
        }
    }
}
