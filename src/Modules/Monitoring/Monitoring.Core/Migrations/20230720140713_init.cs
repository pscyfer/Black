using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Monitoring.Core.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Monitor");

            migrationBuilder.CreateTable(
                name: "Monitors",
                schema: "Monitor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    Timeout = table.Column<int>(type: "int", nullable: false),
                    IsPause = table.Column<bool>(type: "bit", nullable: false),
                    LastChecked = table.Column<long>(type: "bigint", nullable: false),
                    UpTimeFor = table.Column<long>(type: "bigint", nullable: false),
                    Http_StatusCode = table.Column<int>(type: "int", nullable: true),
                    Http_Method = table.Column<int>(type: "int", nullable: true),
                    Http_IsSslVerification = table.Column<bool>(type: "bit", nullable: true),
                    Http_IsDomainCheck = table.Column<bool>(type: "bit", nullable: true),
                    Http_DomainExpierDate = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerFullName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "Monitor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<long>(type: "bigint", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonitorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Monitors_MonitorId",
                        column: x => x.MonitorId,
                        principalSchema: "Monitor",
                        principalTable: "Monitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                schema: "Monitor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartAt = table.Column<long>(type: "bigint", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseHeader = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonitorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidents_Monitors_MonitorId",
                        column: x => x.MonitorId,
                        principalSchema: "Monitor",
                        principalTable: "Monitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitorResponsTime",
                schema: "Monitor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<long>(type: "bigint", nullable: false),
                    ResponsTime = table.Column<long>(type: "bigint", nullable: false),
                    MonitorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorResponsTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitorResponsTime_Monitors_MonitorId",
                        column: x => x.MonitorId,
                        principalSchema: "Monitor",
                        principalTable: "Monitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_MonitorId",
                schema: "Monitor",
                table: "Events",
                column: "MonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_MonitorId",
                schema: "Monitor",
                table: "Incidents",
                column: "MonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitorResponsTime_MonitorId",
                schema: "Monitor",
                table: "MonitorResponsTime",
                column: "MonitorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events",
                schema: "Monitor");

            migrationBuilder.DropTable(
                name: "Incidents",
                schema: "Monitor");

            migrationBuilder.DropTable(
                name: "MonitorResponsTime",
                schema: "Monitor");

            migrationBuilder.DropTable(
                name: "Monitors",
                schema: "Monitor");
        }
    }
}
