using LuaEngine.Scripts.WebApi.Database;
using Microsoft.EntityFrameworkCore;

namespace LuaEngine.Scripts.Tests.Configuration;

public class ScriptsTestsFixture
{
    // Свойство для настроек контекста базы данных
    private readonly DbContextOptions<ScriptsContext> _options;

    public ScriptsTestsFixture()
    {
        // Создание настроек контекста базы данных, использующего встроенную базу данных в памяти.
        _options = new DbContextOptionsBuilder<ScriptsContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase_" + Guid.NewGuid().ToString())
            .Options;
    }

    // Публичное свойство для доступа к настройкам контекста базы данных
    public DbContextOptions<ScriptsContext> Options => _options;
}
