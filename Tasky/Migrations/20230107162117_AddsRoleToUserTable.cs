using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasky.Migrations
{
    public partial class AddsRoleToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_TaskList_taskListId1",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_Category_categoryId",
                table: "TaskList");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_User_userId",
                table: "TaskList");

            migrationBuilder.DropIndex(
                name: "IX_Item_taskListId1",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Item_taskListId",
                table: "Item",
                column: "taskListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_TaskList_taskListId",
                table: "Item",
                column: "taskListId",
                principalTable: "TaskList",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskList_Category_categoryId",
                table: "TaskList",
                column: "categoryId",
                principalTable: "Category",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskList_User_userId",
                table: "TaskList",
                column: "userId",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_TaskList_taskListId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_Category_categoryId",
                table: "TaskList");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_User_userId",
                table: "TaskList");

            migrationBuilder.DropIndex(
                name: "IX_Item_taskListId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "role",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Item_taskListId1",
                table: "Item",
                column: "taskListId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_TaskList_taskListId1",
                table: "Item",
                column: "taskListId1",
                principalTable: "TaskList",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskList_Category_categoryId",
                table: "TaskList",
                column: "categoryId",
                principalTable: "Category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskList_User_userId",
                table: "TaskList",
                column: "userId",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
