using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PigFarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorBuildingAndPenEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pens_Buildings_BuildingId",
                table: "Pens");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Buildings",
                newName: "LastPenSequence");

            migrationBuilder.AddColumn<int>(
                name: "DefaultPenCapacity",
                table: "Buildings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultPenType",
                table: "Buildings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Pens_Buildings_BuildingId",
                table: "Pens",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pens_Buildings_BuildingId",
                table: "Pens");

            migrationBuilder.DropColumn(
                name: "DefaultPenCapacity",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "DefaultPenType",
                table: "Buildings");

            migrationBuilder.RenameColumn(
                name: "LastPenSequence",
                table: "Buildings",
                newName: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Pens_Buildings_BuildingId",
                table: "Pens",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
