using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PigFarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTrackingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecordedBy",
                table: "HealthRecord",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Farms",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "RecordedBy",
                table: "AnimalMovements",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Pens",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Pens",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Pens",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pens",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Pens",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "HealthRecord",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HealthRecord",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "HealthRecord",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "HealthRecord",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FeedTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FeedTypes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FeedTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FeedTypes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "FeedTypes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FeedPrograms",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FeedPrograms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FeedPrograms",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FeedPrograms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "FeedPrograms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FeedAllocations",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FeedAllocations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FeedAllocations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FeedAllocations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "FeedAllocations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Farms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FarmCode",
                table: "Farms",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Farms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Farms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Buildings",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Buildings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Buildings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Buildings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Buildings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Buildings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Buildings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BreedingRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BreedingRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BreedingRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BreedingRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "BreedingRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Batches",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Batches",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Batches",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Batches",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Batches",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Animals",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Animals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Animals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AnimalMovements",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AnimalMovements",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AnimalMovements",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "AnimalMovements",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Pens");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Pens");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Pens");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Pens");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Pens");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HealthRecord");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HealthRecord");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "HealthRecord");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "HealthRecord");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FeedTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FeedTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FeedTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FeedTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "FeedTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FeedPrograms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FeedPrograms");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FeedPrograms");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FeedPrograms");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "FeedPrograms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FeedAllocations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FeedAllocations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FeedAllocations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FeedAllocations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "FeedAllocations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "FarmCode",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AnimalMovements");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AnimalMovements");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AnimalMovements");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AnimalMovements");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "HealthRecord",
                newName: "RecordedBy");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Farms",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AnimalMovements",
                newName: "RecordedBy");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Animals",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }
    }
}
