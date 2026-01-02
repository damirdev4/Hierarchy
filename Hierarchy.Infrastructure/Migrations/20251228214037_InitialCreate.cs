using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hierarchy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HierarchyTableItemsEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HierarchyTableItemsEntity", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "HierarchyTableItemsEntity",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1L, "Layout", null },
                    { 2L, "Navigator", 1L },
                    { 3L, "TopBar", 1L },
                    { 4L, "SiteSettings", 1L },
                    { 5L, "Footer", 1L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HierarchyTableItemsEntity");
        }
    }
}
