using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hierarchy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameHierarchyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HierarchyTableItemsEntity",
                table: "HierarchyTableItemsEntity");

            migrationBuilder.RenameTable(
                name: "HierarchyTableItemsEntity",
                newName: "Hierarchy Table");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hierarchy Table",
                table: "Hierarchy Table",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Hierarchy Table",
                table: "Hierarchy Table");

            migrationBuilder.RenameTable(
                name: "Hierarchy Table",
                newName: "HierarchyTableItemsEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HierarchyTableItemsEntity",
                table: "HierarchyTableItemsEntity",
                column: "Id");
        }
    }
}
