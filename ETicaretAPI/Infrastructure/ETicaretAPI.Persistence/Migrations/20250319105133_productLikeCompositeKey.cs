using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class productLikeCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductLikes",
                table: "ProductLikes");

            // Bu satırı kaldırıyoruz çünkü indeks zaten mevcut değil
            // migrationBuilder.DropIndex(
            //     name: "IX_ProductLikes_UserId",
            //     table: "ProductLikes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductLikes",
                table: "ProductLikes",
                columns: new[] { "UserId", "ProductId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductLikes",
                table: "ProductLikes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductLikes",
                table: "ProductLikes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLikes_UserId",
                table: "ProductLikes",
                column: "UserId");
        }
    }
}
