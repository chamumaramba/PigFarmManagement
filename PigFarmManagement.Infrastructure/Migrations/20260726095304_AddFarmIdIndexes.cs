using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PigFarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFarmIdIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Pens_PenId",
                table: "Batches");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Batches",
                newName: "FarmId");

            migrationBuilder.RenameColumn(
                name: "BatchNumber",
                table: "Batches",
                newName: "EndDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "WeightRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WeightRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "WeightRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WeightRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "WeightRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "WeightRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "VaccinationSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VaccinationSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "VaccinationSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VaccinationSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "VaccinationSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "VaccinationSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Treatments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Treatments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "Treatments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Treatments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Treatments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Treatments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PenCode",
                table: "Pens",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "HealthRecord",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "FeedTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "FeedPrograms",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "FeedAllocations",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "BuildingCode",
                table: "Buildings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPens",
                table: "Buildings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "BreedingRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PenId",
                table: "Batches",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "BatchCode",
                table: "Batches",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "Animals",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "AnimalMovements",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WeightRecords_FarmId",
                table: "WeightRecords",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationSchedules_FarmId",
                table: "VaccinationSchedules",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_FarmId",
                table: "Treatments",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecord_FarmId",
                table: "HealthRecord",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedTypes_FarmId",
                table: "FeedTypes",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedPrograms_FarmId",
                table: "FeedPrograms",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedAllocations_FarmId",
                table: "FeedAllocations",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingRecords_FarmId",
                table: "BreedingRecords",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_FarmId",
                table: "Batches",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_FarmId",
                table: "Animals",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalMovements_FarmId",
                table: "AnimalMovements",
                column: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalMovements_Farms_FarmId",
                table: "AnimalMovements",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Farms_FarmId",
                table: "Animals",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Farms_FarmId",
                table: "Batches",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Pens_PenId",
                table: "Batches",
                column: "PenId",
                principalTable: "Pens",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BreedingRecords_Farms_FarmId",
                table: "BreedingRecords",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedAllocations_Farms_FarmId",
                table: "FeedAllocations",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedPrograms_Farms_FarmId",
                table: "FeedPrograms",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedTypes_Farms_FarmId",
                table: "FeedTypes",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecord_Farms_FarmId",
                table: "HealthRecord",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Farms_FarmId",
                table: "Treatments",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationSchedules_Farms_FarmId",
                table: "VaccinationSchedules",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeightRecords_Farms_FarmId",
                table: "WeightRecords",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalMovements_Farms_FarmId",
                table: "AnimalMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Farms_FarmId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Farms_FarmId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Pens_PenId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_BreedingRecords_Farms_FarmId",
                table: "BreedingRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedAllocations_Farms_FarmId",
                table: "FeedAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedPrograms_Farms_FarmId",
                table: "FeedPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedTypes_Farms_FarmId",
                table: "FeedTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecord_Farms_FarmId",
                table: "HealthRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Farms_FarmId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationSchedules_Farms_FarmId",
                table: "VaccinationSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightRecords_Farms_FarmId",
                table: "WeightRecords");

            migrationBuilder.DropIndex(
                name: "IX_WeightRecords_FarmId",
                table: "WeightRecords");

            migrationBuilder.DropIndex(
                name: "IX_VaccinationSchedules_FarmId",
                table: "VaccinationSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_FarmId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_HealthRecord_FarmId",
                table: "HealthRecord");

            migrationBuilder.DropIndex(
                name: "IX_FeedTypes_FarmId",
                table: "FeedTypes");

            migrationBuilder.DropIndex(
                name: "IX_FeedPrograms_FarmId",
                table: "FeedPrograms");

            migrationBuilder.DropIndex(
                name: "IX_FeedAllocations_FarmId",
                table: "FeedAllocations");

            migrationBuilder.DropIndex(
                name: "IX_BreedingRecords_FarmId",
                table: "BreedingRecords");

            migrationBuilder.DropIndex(
                name: "IX_Batches_FarmId",
                table: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Animals_FarmId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_AnimalMovements_FarmId",
                table: "AnimalMovements");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "WeightRecords");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WeightRecords");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "WeightRecords");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WeightRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "WeightRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "WeightRecords");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "VaccinationSchedules");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VaccinationSchedules");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "VaccinationSchedules");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VaccinationSchedules");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "VaccinationSchedules");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "VaccinationSchedules");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "PenCode",
                table: "Pens");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "HealthRecord");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "FeedTypes");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "FeedPrograms");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "FeedAllocations");

            migrationBuilder.DropColumn(
                name: "BuildingCode",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "NumberOfPens",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "BatchCode",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "AnimalMovements");

            migrationBuilder.RenameColumn(
                name: "FarmId",
                table: "Batches",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Batches",
                newName: "BatchNumber");

            migrationBuilder.AlterColumn<Guid>(
                name: "PenId",
                table: "Batches",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Pens_PenId",
                table: "Batches",
                column: "PenId",
                principalTable: "Pens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
