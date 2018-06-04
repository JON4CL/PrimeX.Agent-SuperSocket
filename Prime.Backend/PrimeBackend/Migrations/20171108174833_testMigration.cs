using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Prime.Backend.Migrations
{
    public partial class testMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComponentName = table.Column<string>(type: "TEXT", nullable: true),
                    DataBinary = table.Column<byte[]>(type: "BLOB", nullable: true),
                    DataString = table.Column<string>(type: "TEXT", nullable: true),
                    IsBinary = table.Column<bool>(type: "INTEGER", nullable: false),
                    MessageCommand = table.Column<int>(type: "INTEGER", nullable: false),
                    MessageTimestamp = table.Column<string>(type: "TEXT", nullable: true),
                    SourceIP = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageRecords");
        }
    }
}
