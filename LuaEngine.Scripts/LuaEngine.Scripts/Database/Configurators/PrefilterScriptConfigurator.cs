using LuaEngine.Scripts.WebApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using static LuaEngine.Scripts.WebApi.Constants.DbConstants;

namespace LuaEngine.Scripts.WebApi.Database.Configurators;

public class PrefilterScriptConfigurator : IEntityTypeConfiguration<PrefilterScript>
{
    public void Configure(EntityTypeBuilder<PrefilterScript> builder)
    {
        builder.ToTable(PrefilterScriptTableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName(IdColumn).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.SourceId).HasColumnName(SourceIdColumn).IsRequired();
        builder.Property(x => x.Enabled).HasColumnName(EnabledColumn).IsRequired();

        builder.HasIndex(x => x.SourceId);
    }
}
