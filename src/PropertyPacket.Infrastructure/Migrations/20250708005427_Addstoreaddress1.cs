using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyPacket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addstoreaddress1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressInfo_PostCode",
                table: "StoreInfos",
                newName: "PostCode");

            migrationBuilder.RenameColumn(
                name: "AddressInfo_Line2",
                table: "StoreInfos",
                newName: "Line2");

            migrationBuilder.RenameColumn(
                name: "AddressInfo_Line1",
                table: "StoreInfos",
                newName: "Line1");

            migrationBuilder.RenameColumn(
                name: "AddressInfo_Country",
                table: "StoreInfos",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "AddressInfo_City",
                table: "StoreInfos",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostCode",
                table: "StoreInfos",
                newName: "AddressInfo_PostCode");

            migrationBuilder.RenameColumn(
                name: "Line2",
                table: "StoreInfos",
                newName: "AddressInfo_Line2");

            migrationBuilder.RenameColumn(
                name: "Line1",
                table: "StoreInfos",
                newName: "AddressInfo_Line1");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "StoreInfos",
                newName: "AddressInfo_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "StoreInfos",
                newName: "AddressInfo_City");
        }
    }
}
