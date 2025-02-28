using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LuaEngine.Scripts.WebApi.Models;
using static LuaEngine.Scripts.WebApi.Constants.DbConstants;

namespace LuaEngine.Scripts.WebApi.Database.Configurators;

public class ScriptVersionConfigurator : IEntityTypeConfiguration<ScriptVersion>
{
    public void Configure(EntityTypeBuilder<ScriptVersion> builder)
    {
        builder.ToTable(ScriptVersionTableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName(IdColumn).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Type).HasColumnName(TypeColumn).IsRequired();
        builder.Property(x => x.ProcessScriptId).HasColumnName(ProcessScriptIdColumn);
        builder.Property(x => x.RuleScriptId).HasColumnName(RuleScriptIdColumn);
        builder.Property(x => x.ParentId).HasColumnName(ParentIdColumn);
        builder.Property(x => x.Code).HasColumnName(CodeColumn);
        builder.Property(x => x.Version).HasColumnName(VersionColumn);
        builder.Property(x => x.Status).HasColumnName(StatusColumn);

        builder.HasIndex(x => x.Type);
    }
}
