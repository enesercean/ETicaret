using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoleAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoleAudits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    ActionByUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoleAudits_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoleAudits_AspNetUsers_ActionByUserId",
                        column: x => x.ActionByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoleAudits_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleAudits_ActionByUserId",
                table: "UserRoleAudits",
                column: "ActionByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleAudits_RoleId",
                table: "UserRoleAudits",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleAudits_UserId",
                table: "UserRoleAudits",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleAudits_UserId_RoleId",
                table: "UserRoleAudits",
                columns: new[] { "UserId", "RoleId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoleAudits");
        }
    }
}
