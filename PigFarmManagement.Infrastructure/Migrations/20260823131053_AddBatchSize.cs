using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PigFarmManagement.Infrastructure.Migrations
{
    public partial class AddBatchSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatchSize",
                table: "Batches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchSize",
                table: "Batches");
        }
    }
}
