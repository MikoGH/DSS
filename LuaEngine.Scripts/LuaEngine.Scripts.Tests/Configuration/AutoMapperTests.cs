using AutoMapper;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace LuaEngine.Scripts.Tests.Configuration;

public class AutoMapperTests
{
    [Fact(DisplayName = "AutoMapper: Проверка привязки всех профилей.")]
    public void ShouldProperlyMapAllProfiles()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(Program)));

        // Act and Assert
        config.AssertConfigurationIsValid();
    }
}
