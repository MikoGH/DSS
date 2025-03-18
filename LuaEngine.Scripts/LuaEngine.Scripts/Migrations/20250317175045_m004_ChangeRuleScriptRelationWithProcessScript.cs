using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuaEngine.Scripts.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class m004_ChangeRuleScriptRelationWithProcessScript : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_rule_scripts_process_script_id",
                table: "rule_scripts",
                column: "process_script_id");

            migrationBuilder.AddForeignKey(
                name: "FK_rule_scripts_process_scripts_process_script_id",
                table: "rule_scripts",
                column: "process_script_id",
                principalTable: "process_scripts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rule_scripts_process_scripts_process_script_id",
                table: "rule_scripts");

            migrationBuilder.DropIndex(
                name: "IX_rule_scripts_process_script_id",
                table: "rule_scripts");
        }
    }
}
