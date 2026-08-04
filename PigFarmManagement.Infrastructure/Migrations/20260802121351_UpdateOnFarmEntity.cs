using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PigFarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnFarmEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecord_Animals_AnimalId",
                table: "HealthRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecord_Batches_BatchId",
                table: "HealthRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecord_Farms_FarmId",
                table: "HealthRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthRecord",
                table: "HealthRecord");

            migrationBuilder.RenameTable(
                name: "HealthRecord",
                newName: "HealthRecords");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecord_FarmId",
                table: "HealthRecords",
                newName: "IX_HealthRecords_FarmId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecord_BatchId",
                table: "HealthRecords",
                newName: "IX_HealthRecords_BatchId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecord_AnimalId",
                table: "HealthRecords",
                newName: "IX_HealthRecords_AnimalId");

            migrationBuilder.AddColumn<int>(
                name: "LastAnimalSequence",
                table: "Farms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastBuildingSequence",
                table: "Farms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AnimalOrigin",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthRecords",
                table: "HealthRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_Animals_AnimalId",
                table: "HealthRecords",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_Batches_BatchId",
                table: "HealthRecords",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_Farms_FarmId",
                table: "HealthRecords",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_Animals_AnimalId",
                table: "HealthRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_Batches_BatchId",
                table: "HealthRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_Farms_FarmId",
                table: "HealthRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthRecords",
                table: "HealthRecords");

            migrationBuilder.DropColumn(
                name: "LastAnimalSequence",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "LastBuildingSequence",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "AnimalOrigin",
                table: "Animals");

            migrationBuilder.RenameTable(
                name: "HealthRecords",
                newName: "HealthRecord");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecords_FarmId",
                table: "HealthRecord",
                newName: "IX_HealthRecord_FarmId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecords_BatchId",
                table: "HealthRecord",
                newName: "IX_HealthRecord_BatchId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecords_AnimalId",
                table: "HealthRecord",
                newName: "IX_HealthRecord_AnimalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthRecord",
                table: "HealthRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecord_Animals_AnimalId",
                table: "HealthRecord",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecord_Batches_BatchId",
                table: "HealthRecord",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecord_Farms_FarmId",
                table: "HealthRecord",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
