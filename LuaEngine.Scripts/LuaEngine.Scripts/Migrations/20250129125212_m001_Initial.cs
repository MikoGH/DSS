using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuaEngine.Scripts.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class m001_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    source_id = table.Column<Guid>(type: "uuid", nullable: false),
                    script_id = table.Column<Guid>(type: "uuid", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    prefilter = table.Column<bool>(type: "boolean", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scripts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    source_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scripts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "code_versions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    script_id = table.Column<Guid>(type: "uuid", nullable: true),
                    rule_id = table.Column<Guid>(type: "uuid", nullable: true),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    code = table.Column<string>(type: "text", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_code_versions", x => x.id);
                    table.ForeignKey(
                        name: "FK_code_versions_code_versions_parent_id",
                        column: x => x.parent_id,
                        principalTable: "code_versions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_code_versions_rules_rule_id",
                        column: x => x.rule_id,
                        principalTable: "rules",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_code_versions_scripts_script_id",
                        column: x => x.script_id,
                        principalTable: "scripts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_code_versions_parent_id",
                table: "code_versions",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_code_versions_rule_id",
                table: "code_versions",
                column: "rule_id");

            migrationBuilder.CreateIndex(
                name: "IX_code_versions_script_id",
                table: "code_versions",
                column: "script_id");

            migrationBuilder.CreateIndex(
                name: "IX_code_versions_type",
                table: "code_versions",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_rules_source_id",
                table: "rules",
                column: "source_id");

            migrationBuilder.CreateIndex(
                name: "IX_scripts_source_id",
                table: "scripts",
                column: "source_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "code_versions");

            migrationBuilder.DropTable(
                name: "rules");

            migrationBuilder.DropTable(
                name: "scripts");
        }
    }
}
