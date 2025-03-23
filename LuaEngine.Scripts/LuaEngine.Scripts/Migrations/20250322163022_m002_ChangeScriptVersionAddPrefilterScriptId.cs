using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuaEngine.Scripts.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class m002_ChangeScriptVersionAddPrefilterScriptId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_script_versions_prefilter_scripts_PrefilterScriptId",
                table: "script_versions");

            migrationBuilder.RenameColumn(
                name: "PrefilterScriptId",
                table: "script_versions",
                newName: "prefilter_script_id");

            migrationBuilder.RenameIndex(
                name: "IX_script_versions_PrefilterScriptId",
                table: "script_versions",
                newName: "IX_script_versions_prefilter_script_id");

            migrationBuilder.AddForeignKey(
                name: "FK_script_versions_prefilter_scripts_prefilter_script_id",
                table: "script_versions",
                column: "prefilter_script_id",
                principalTable: "prefilter_scripts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_script_versions_prefilter_scripts_prefilter_script_id",
                table: "script_versions");

            migrationBuilder.RenameColumn(
                name: "prefilter_script_id",
                table: "script_versions",
                newName: "PrefilterScriptId");

            migrationBuilder.RenameIndex(
                name: "IX_script_versions_prefilter_script_id",
                table: "script_versions",
                newName: "IX_script_versions_PrefilterScriptId");

            migrationBuilder.AddForeignKey(
                name: "FK_script_versions_prefilter_scripts_PrefilterScriptId",
                table: "script_versions",
                column: "PrefilterScriptId",
                principalTable: "prefilter_scripts",
                principalColumn: "id");
        }
    }
}
