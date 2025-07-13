using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyTenants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TPT11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_User_GuestId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_User_HostId",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ContactInfo_AddressId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo_Email",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo_Mobile",
                table: "Users",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo_PhoneNumber",
                table: "Users",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactInfo_AddressId",
                table: "Users",
                column: "ContactInfo_AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_GuestId",
                table: "Bookings",
                column: "GuestId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_HostId",
                table: "Properties",
                column: "HostId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_ContactInfo_AddressId",
                table: "Users",
                column: "ContactInfo_AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_GuestId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_HostId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_ContactInfo_AddressId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ContactInfo_AddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactInfo_AddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactInfo_Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactInfo_Mobile",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactInfo_PhoneNumber",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_User_GuestId",
                table: "Bookings",
                column: "GuestId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_User_HostId",
                table: "Properties",
                column: "HostId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
