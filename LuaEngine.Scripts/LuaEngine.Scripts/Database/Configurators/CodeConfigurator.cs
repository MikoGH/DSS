using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LuaEngine.Scripts.WebApi.Models;
using static LuaEngine.Scripts.WebApi.Constants.DbConstants;

namespace LuaEngine.Scripts.WebApi.Database.Configurators;

public class CodeConfigurator : IEntityTypeConfiguration<CodeVersion>
{
    public void Configure(EntityTypeBuilder<CodeVersion> builder)
    {
        builder.ToTable(CodeVersionTableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName(IdColumn).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Type).HasColumnName(TypeColumn).IsRequired().HasMaxLength(CodeVersionTypeMaxLength);
        builder.Property(x => x.ScriptId).HasColumnName(ScriptIdColumn);
        builder.Property(x => x.RuleId).HasColumnName(RuleIdColumn);
        builder.Property(x => x.ParentId).HasColumnName(ParentIdColumn);
        builder.Property(x => x.Code).HasColumnName(CodeColumn);
        builder.Property(x => x.Version).HasColumnName(VersionColumn);
        builder.Property(x => x.Status).HasColumnName(StatusColumn).HasMaxLength(CodeVersionStatusMaxLength);

        builder.HasIndex(x => x.Type);
    }
}
