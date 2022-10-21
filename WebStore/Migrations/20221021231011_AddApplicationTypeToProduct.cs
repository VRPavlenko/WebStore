using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Migrations
{
    public partial class AddApplicationTypeToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_applicationType",
                table: "applicationType");

            migrationBuilder.RenameTable(
                name: "applicationType",
                newName: "ApplicationType");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationType",
                table: "ApplicationType",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ApplicationId",
                table: "Product",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ApplicationType_ApplicationId",
                table: "Product",
                column: "ApplicationId",
                principalTable: "ApplicationType",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ApplicationType_ApplicationId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ApplicationId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationType",
                table: "ApplicationType");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "ApplicationType",
                newName: "applicationType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_applicationType",
                table: "applicationType",
                column: "TypeId");
        }
    }
}
