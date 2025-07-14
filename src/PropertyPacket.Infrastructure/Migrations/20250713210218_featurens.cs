using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyTenants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class featurens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeatureGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeatureGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_FeatureGroups_FeatureGroupId",
                        column: x => x.FeatureGroupId,
                        principalTable: "FeatureGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    FeatureId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFeatures", x => x.Id);
                    table.UniqueConstraint("AK_PropertyFeatures_PropertyId_FeatureId", x => new { x.PropertyId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_PropertyFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyFeatures_Features_FeatureId1",
                        column: x => x.FeatureId1,
                        principalTable: "Features",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyFeatures_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Features_FeatureGroupId",
                table: "Features",
                column: "FeatureGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatures_FeatureId",
                table: "PropertyFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatures_FeatureId1",
                table: "PropertyFeatures",
                column: "FeatureId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyFeatures");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "FeatureGroups");
        }
    }
}
