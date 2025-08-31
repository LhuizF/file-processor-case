using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileProcessor.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CreateProcessedFileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessedFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AcquirerName = table.Column<string>(type: "text", nullable: true),
                    EstablishmentCode = table.Column<string>(type: "text", nullable: true),
                    ProcessingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PeriodStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PeriodEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SequenceNumber = table.Column<int>(type: "integer", nullable: true),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessedFiles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessedFiles");
        }
    }
}
