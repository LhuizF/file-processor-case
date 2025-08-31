using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FileProcessor.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialLayoutSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Layouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AcquirerName = table.Column<string>(type: "text", nullable: false),
                    Identifier = table.Column<char>(type: "character(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LayoutFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    InitPosition = table.Column<int>(type: "integer", nullable: false),
                    Length = table.Column<int>(type: "integer", nullable: false),
                    DataType = table.Column<string>(type: "text", nullable: false),
                    LayoutId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LayoutFields_Layouts_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "Layouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Layouts",
                columns: new[] { "Id", "AcquirerName", "Identifier" },
                values: new object[,]
                {
                    { new Guid("4e79206b-3a83-42e6-8e8e-1b3c2e1f4d9b"), "UfCard", '0' },
                    { new Guid("8a6b1e3c-1b9d-4e2a-9f8c-3d4a5b6c7d8e"), "FagammonCard", '1' }
                });

            migrationBuilder.InsertData(
                table: "LayoutFields",
                columns: new[] { "Id", "DataType", "InitPosition", "LayoutId", "Length", "Name" },
                values: new object[,]
                {
                    { new Guid("a5f6e7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0"), "NUM", 36, new Guid("4e79206b-3a83-42e6-8e8e-1b3c2e1f4d9b"), 7, "Sequencia" },
                    { new Guid("b6a7f8c9-d0e1-f2a3-b4c5-d6e7f8a9b0c1"), "DATE_YYYYMMDD", 2, new Guid("8a6b1e3c-1b9d-4e2a-9f8c-3d4a5b6c7d8e"), 8, "DataProcessamento" },
                    { new Guid("c1b2a3d4-e5f6-a7b8-c9d0-e1f2a3b4c5d6"), "NUM", 2, new Guid("4e79206b-3a83-42e6-8e8e-1b3c2e1f4d9b"), 10, "Estabelecimento" },
                    { new Guid("c7b8a9d0-e1f2-a3b4-c5d6-e7f8a9b0c1d2"), "NUM", 10, new Guid("8a6b1e3c-1b9d-4e2a-9f8c-3d4a5b6c7d8e"), 8, "Estabelecimento" },
                    { new Guid("d2c3b4e5-f6a7-b8c9-d0e1-f2a3b4c5d6e7"), "DATE_YYYYMMDD", 12, new Guid("4e79206b-3a83-42e6-8e8e-1b3c2e1f4d9b"), 8, "DataProcessamento" },
                    { new Guid("d8c9b0e1-f2a3-b4c5-d6e7-f8a9b0c1d2e3"), "NUM", 30, new Guid("8a6b1e3c-1b9d-4e2a-9f8c-3d4a5b6c7d8e"), 7, "Sequencia" },
                    { new Guid("e3d4c5f6-a7b8-c9d0-e1f2-a3b4c5d6e7f8"), "DATE_YYYYMMDD", 20, new Guid("4e79206b-3a83-42e6-8e8e-1b3c2e1f4d9b"), 8, "PeriodoInicial" },
                    { new Guid("f4e5d6a7-b8c9-d0e1-f2a3-b4c5d6e7f8a9"), "DATE_YYYYMMDD", 28, new Guid("4e79206b-3a83-42e6-8e8e-1b3c2e1f4d9b"), 8, "PeriodoFinal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LayoutFields_LayoutId",
                table: "LayoutFields",
                column: "LayoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LayoutFields");

            migrationBuilder.DropTable(
                name: "Layouts");
        }
    }
}
