using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumStandUpTracker_1.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    DeveloperId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.DeveloperId);
                });

            migrationBuilder.CreateTable(
                name: "StatusForms",
                columns: table => new
                {
                    StatusFormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeveloperId = table.Column<int>(type: "int", nullable: false),
                    DeveloperName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDetails = table.Column<string>(name: "Task Details", type: "nvarchar(200)", maxLength: 200, nullable: false),
                    YesterdayTask = table.Column<string>(name: "Yesterday Task", type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TodayTask = table.Column<string>(name: "Today Task", type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Blockers = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusForms", x => x.StatusFormId);
                    table.ForeignKey(
                        name: "FK_StatusForms_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "DeveloperId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatusForms_DeveloperId",
                table: "StatusForms",
                column: "DeveloperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusForms");

            migrationBuilder.DropTable(
                name: "Developers");
        }
    }
}
