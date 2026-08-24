using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PigFarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BatchCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BreedingRecords_Animals_BoarId",
                table: "BreedingRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_BreedingRecords_Animals_SowId",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "FarrowingDate",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "PigletsBornAlive",
                table: "BreedingRecords");

            migrationBuilder.DropColumn(
                name: "PigletsBornDead",
                table: "BreedingRecords");

            migrationBuilder.AddColumn<int>(
                name: "BatchSize",
                table: "Batches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Position",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Animals",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentWeight",
                table: "Animals",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Breed",
                table: "Animals",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "BirthWeight",
                table: "Animals",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "LitterId",
                table: "Animals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FarrowingRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SowId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BreedingRecordId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TotalBorn = table.Column<int>(type: "INTEGER", nullable: false),
                    BornAlive = table.Column<int>(type: "INTEGER", nullable: false),
                    StillBorn = table.Column<int>(type: "INTEGER", nullable: false),
                    FarrowingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    FarmId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarrowingRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarrowingRecord_Animals_SowId",
                        column: x => x.SowId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarrowingRecord_BreedingRecords_BreedingRecordId",
                        column: x => x.BreedingRecordId,
                        principalTable: "BreedingRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarrowingRecord_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Litter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LitterCode = table.Column<string>(type: "TEXT", nullable: false),
                    FarrowingRecordId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    FarmId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Litter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Litter_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Litter_FarrowingRecord_FarrowingRecordId",
                        column: x => x.FarrowingRecordId,
                        principalTable: "FarrowingRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_LitterId",
                table: "Animals",
                column: "LitterId");

            migrationBuilder.CreateIndex(
                name: "IX_FarrowingRecord_BreedingRecordId",
                table: "FarrowingRecord",
                column: "BreedingRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_FarrowingRecord_FarmId",
                table: "FarrowingRecord",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_FarrowingRecord_SowId",
                table: "FarrowingRecord",
                column: "SowId");

            migrationBuilder.CreateIndex(
                name: "IX_Litter_FarmId",
                table: "Litter",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Litter_FarrowingRecordId",
                table: "Litter",
                column: "FarrowingRecordId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Litter_LitterId",
                table: "Animals",
                column: "LitterId",
                principalTable: "Litter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BreedingRecords_Animals_BoarId",
                table: "BreedingRecords",
                column: "BoarId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BreedingRecords_Animals_SowId",
                table: "BreedingRecords",
                column: "SowId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Litter_LitterId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_BreedingRecords_Animals_BoarId",
                table: "BreedingRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_BreedingRecords_Animals_SowId",
                table: "BreedingRecords");

            migrationBuilder.DropTable(
                name: "Litter");

            migrationBuilder.DropTable(
                name: "FarrowingRecord");

            migrationBuilder.DropIndex(
                name: "IX_Animals_LitterId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "BatchSize",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "LitterId",
                table: "Animals");

            migrationBuilder.AddColumn<DateTime>(
                name: "FarrowingDate",
                table: "BreedingRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PigletsBornAlive",
                table: "BreedingRecords",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PigletsBornDead",
                table: "BreedingRecords",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentWeight",
                table: "Animals",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Breed",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BirthWeight",
                table: "Animals",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BreedingRecords_Animals_BoarId",
                table: "BreedingRecords",
                column: "BoarId",
                principalTable: "Animals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BreedingRecords_Animals_SowId",
                table: "BreedingRecords",
                column: "SowId",
                principalTable: "Animals",
                principalColumn: "Id");
        }
    }
}
