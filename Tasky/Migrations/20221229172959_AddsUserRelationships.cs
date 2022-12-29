using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasky.Migrations
{
    public partial class AddsUserRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_TaskList_taskListId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_taskListId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "TaskList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "taskListId1",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TaskList_userId",
                table: "TaskList",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_taskListId1",
                table: "Item",
                column: "taskListId1");

            migrationBuilder.CreateIndex(
                name: "IX_Category_userId",
                table: "Category",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_userId",
                table: "Category",
                column: "userId",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_TaskList_taskListId1",
                table: "Item",
                column: "taskListId1",
                principalTable: "TaskList",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_userId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_TaskList_taskListId1",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskList_User_userId",
                table: "TaskList");

            migrationBuilder.DropIndex(
                name: "IX_TaskList_userId",
                table: "TaskList");

            migrationBuilder.DropIndex(
                name: "IX_Item_taskListId1",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Category_userId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "name",
                table: "User");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "TaskList");

            migrationBuilder.DropColumn(
                name: "taskListId1",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Category");

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
        }
    }
}
