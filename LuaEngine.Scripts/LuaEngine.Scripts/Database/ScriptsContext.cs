using Microsoft.EntityFrameworkCore;
using LuaEngine.Scripts.WebApi.Models;

namespace LuaEngine.Scripts.WebApi.Database;

public class ScriptsContext : DbContext
{
    public DbSet<ProcessScript> ProcessScripts { get; set; }

    public DbSet<RuleScript> RuleScripts { get; set; }

    public DbSet<ScriptVersion> ScriptVersions { get; set; }

    public ScriptsContext(DbContextOptions<ScriptsContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScriptsContext).Assembly);
    }
}
