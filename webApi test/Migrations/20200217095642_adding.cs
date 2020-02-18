using Microsoft.EntityFrameworkCore.Migrations;

namespace webApi_test.Migrations
{
    public partial class adding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceModel_ResourceTypeModel_TypeID",
                table: "ResourceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceModel_ResourceModel_parentId",
                table: "ResourceModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceTypeModel",
                table: "ResourceTypeModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceModel",
                table: "ResourceModel");

            migrationBuilder.RenameTable(
                name: "ResourceTypeModel",
                newName: "ResourceType");

            migrationBuilder.RenameTable(
                name: "ResourceModel",
                newName: "Resources");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceModel_parentId",
                table: "Resources",
                newName: "IX_Resources_parentId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceModel_TypeID",
                table: "Resources",
                newName: "IX_Resources_TypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceType",
                table: "ResourceType",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ResourceType_TypeID",
                table: "Resources",
                column: "TypeID",
                principalTable: "ResourceType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Resources_parentId",
                table: "Resources",
                column: "parentId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ResourceType_TypeID",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Resources_parentId",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceType",
                table: "ResourceType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.RenameTable(
                name: "ResourceType",
                newName: "ResourceTypeModel");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "ResourceModel");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_parentId",
                table: "ResourceModel",
                newName: "IX_ResourceModel_parentId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_TypeID",
                table: "ResourceModel",
                newName: "IX_ResourceModel_TypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceTypeModel",
                table: "ResourceTypeModel",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceModel",
                table: "ResourceModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceModel_ResourceTypeModel_TypeID",
                table: "ResourceModel",
                column: "TypeID",
                principalTable: "ResourceTypeModel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceModel_ResourceModel_parentId",
                table: "ResourceModel",
                column: "parentId",
                principalTable: "ResourceModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
