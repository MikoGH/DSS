using Microsoft.EntityFrameworkCore;
using LuaEngine.Scripts.WebApi.Models;

namespace LuaEngine.Scripts.WebApi.Database;

public class ScriptsContext : DbContext
{
    public DbSet<Script> Scripts { get; set; }

    public DbSet<Rule> Rules { get; set; }

    public DbSet<CodeVersion> CodeVersions { get; set; }

    public ScriptsContext(DbContextOptions<ScriptsContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScriptsContext).Assembly);
    }
}
