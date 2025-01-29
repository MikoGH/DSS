using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LuaEngine.Scripts.WebApi.Models;
using static LuaEngine.Scripts.WebApi.Constants.DbConstants;

namespace LuaEngine.Scripts.WebApi.Database.Configurators;

public class ScriptConfigurator : IEntityTypeConfiguration<Script>
{
    public void Configure(EntityTypeBuilder<Script> builder)
    {
        builder.ToTable(ScriptTableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName(IdColumn).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.SourceId).HasColumnName(SourceIdColumn).IsRequired();
        builder.Property(x => x.Name).HasColumnName(NameColumn).IsRequired().HasMaxLength(ScriptNameMaxLength);
        builder.Property(x => x.Description).HasColumnName(DescriptionColumn).HasMaxLength(ScriptDescriptionMaxLength);

        builder.HasIndex(x => x.SourceId);
    }
}
