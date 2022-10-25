using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Migrations
{
    public partial class ChangeApplicationTypeToUsedType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ApplicationType_ApplicationTypeId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ApplicationType");

            migrationBuilder.RenameColumn(
                name: "ApplicationTypeId",
                table: "Product",
                newName: "UsedTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ApplicationTypeId",
                table: "Product",
                newName: "IX_Product_UsedTypeId");

            migrationBuilder.CreateTable(
                name: "UsedType",
                columns: table => new
                {
                    UsedTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsedTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedType", x => x.UsedTypeId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UsedType_UsedTypeId",
                table: "Product",
                column: "UsedTypeId",
                principalTable: "UsedType",
                principalColumn: "UsedTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_UsedType_UsedTypeId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "UsedType");

            migrationBuilder.RenameColumn(
                name: "UsedTypeId",
                table: "Product",
                newName: "ApplicationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UsedTypeId",
                table: "Product",
                newName: "IX_Product_ApplicationTypeId");

            migrationBuilder.CreateTable(
                name: "ApplicationType",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationType", x => x.TypeId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ApplicationType_ApplicationTypeId",
                table: "Product",
                column: "ApplicationTypeId",
                principalTable: "ApplicationType",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
