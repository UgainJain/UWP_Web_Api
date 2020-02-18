using Microsoft.EntityFrameworkCore.Migrations;

namespace webApi_test.Migrations
{
    public partial class addingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceTypeModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceTypeModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ResourceModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parentId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    TypeID = table.Column<int>(nullable: false),
                    Client = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceModel_ResourceTypeModel_TypeID",
                        column: x => x.TypeID,
                        principalTable: "ResourceTypeModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceModel_ResourceModel_parentId",
                        column: x => x.parentId,
                        principalTable: "ResourceModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceModel_TypeID",
                table: "ResourceModel",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceModel_parentId",
                table: "ResourceModel",
                column: "parentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceModel");

            migrationBuilder.DropTable(
                name: "ResourceTypeModel");
        }
    }
}
