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
                name: "prefilter_scripts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    source_id = table.Column<Guid>(type: "uuid", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prefilter_scripts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "process_scripts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    source_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_process_scripts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rule_scripts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    source_id = table.Column<Guid>(type: "uuid", nullable: false),
                    process_script_id = table.Column<Guid>(type: "uuid", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rule_scripts", x => x.id);
                    table.ForeignKey(
                        name: "FK_rule_scripts_process_scripts_process_script_id",
                        column: x => x.process_script_id,
                        principalTable: "process_scripts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "script_versions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<byte>(type: "smallint", nullable: false),
                    PrefilterScriptId = table.Column<Guid>(type: "uuid", nullable: true),
                    rule_script_id = table.Column<Guid>(type: "uuid", nullable: true),
                    process_script_id = table.Column<Guid>(type: "uuid", nullable: true),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    code = table.Column<string>(type: "text", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_script_versions", x => x.id);
                    table.ForeignKey(
                        name: "FK_script_versions_prefilter_scripts_PrefilterScriptId",
                        column: x => x.PrefilterScriptId,
                        principalTable: "prefilter_scripts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_script_versions_process_scripts_process_script_id",
                        column: x => x.process_script_id,
                        principalTable: "process_scripts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_script_versions_rule_scripts_rule_script_id",
                        column: x => x.rule_script_id,
                        principalTable: "rule_scripts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_script_versions_script_versions_parent_id",
                        column: x => x.parent_id,
                        principalTable: "script_versions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_prefilter_scripts_source_id",
                table: "prefilter_scripts",
                column: "source_id");

            migrationBuilder.CreateIndex(
                name: "IX_process_scripts_source_id",
                table: "process_scripts",
                column: "source_id");

            migrationBuilder.CreateIndex(
                name: "IX_rule_scripts_process_script_id",
                table: "rule_scripts",
                column: "process_script_id");

            migrationBuilder.CreateIndex(
                name: "IX_rule_scripts_source_id",
                table: "rule_scripts",
                column: "source_id");

            migrationBuilder.CreateIndex(
                name: "IX_script_versions_parent_id",
                table: "script_versions",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_script_versions_PrefilterScriptId",
                table: "script_versions",
                column: "PrefilterScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_script_versions_process_script_id",
                table: "script_versions",
                column: "process_script_id");

            migrationBuilder.CreateIndex(
                name: "IX_script_versions_rule_script_id",
                table: "script_versions",
                column: "rule_script_id");

            migrationBuilder.CreateIndex(
                name: "IX_script_versions_type",
                table: "script_versions",
                column: "type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "script_versions");

            migrationBuilder.DropTable(
                name: "prefilter_scripts");

            migrationBuilder.DropTable(
                name: "rule_scripts");

            migrationBuilder.DropTable(
                name: "process_scripts");
        }
    }
}
