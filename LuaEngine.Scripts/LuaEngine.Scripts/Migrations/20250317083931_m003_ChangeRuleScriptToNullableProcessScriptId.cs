using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuaEngine.Scripts.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class m003_ChangeRuleScriptToNullableProcessScriptId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "process_script_id",
                table: "rule_scripts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "process_script_id",
                table: "rule_scripts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
