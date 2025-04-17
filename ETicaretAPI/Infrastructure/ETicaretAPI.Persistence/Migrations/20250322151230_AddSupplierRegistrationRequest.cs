using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierRegistrationRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierRegistrationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BusinessType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BusinessAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductCategories = table.Column<string>(type: "text", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AveragePrice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShippingMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    RejectionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    IsTransferred = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierRegistrationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierRegistrationRequests_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierRegistrationRequests_AspNetUsers_ReviewedById",
                        column: x => x.ReviewedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierRegistrationRequests_CreatedById",
                table: "SupplierRegistrationRequests",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierRegistrationRequests_ReviewedById",
                table: "SupplierRegistrationRequests",
                column: "ReviewedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierRegistrationRequests");
        }
    }
}
