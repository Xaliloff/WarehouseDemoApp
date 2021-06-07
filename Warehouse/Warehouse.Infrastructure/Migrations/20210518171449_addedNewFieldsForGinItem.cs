using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.Infrastructure.Migrations
{
    public partial class addedNewFieldsForGinItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "GoodsReceiptNoteItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "GoodsReceiptNoteItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "GoodsIssueNoteItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptNoteItems_ProductId",
                table: "GoodsReceiptNoteItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueNoteItems_ItemId",
                table: "GoodsIssueNoteItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsIssueNoteItems_Item_ItemId",
                table: "GoodsIssueNoteItems",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceiptNoteItems_Products_ProductId",
                table: "GoodsReceiptNoteItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsIssueNoteItems_Item_ItemId",
                table: "GoodsIssueNoteItems");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceiptNoteItems_Products_ProductId",
                table: "GoodsReceiptNoteItems");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceiptNoteItems_ProductId",
                table: "GoodsReceiptNoteItems");

            migrationBuilder.DropIndex(
                name: "IX_GoodsIssueNoteItems_ItemId",
                table: "GoodsIssueNoteItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "GoodsReceiptNoteItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "GoodsReceiptNoteItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "GoodsIssueNoteItems");
        }
    }
}
