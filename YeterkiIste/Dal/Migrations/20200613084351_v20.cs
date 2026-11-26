using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class v20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


 

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "MainComment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Brand",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MainComment_ProductId",
                table: "MainComment",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_CategoryId",
                table: "Brand",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Category_CategoryId",
                table: "Brand",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MainComment_Product_ProductId",
                table: "MainComment",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Category_CategoryId",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_MainComment_Product_ProductId",
                table: "MainComment");

            migrationBuilder.DropIndex(
                name: "IX_MainComment_ProductId",
                table: "MainComment");

            migrationBuilder.DropIndex(
                name: "IX_Brand_CategoryId",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "MainComment");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Brand");

  

        
        }
    }
}
