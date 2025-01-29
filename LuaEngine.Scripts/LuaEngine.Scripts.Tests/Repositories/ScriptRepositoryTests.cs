﻿using AutoMapper;
using LuaEngine.Scripts.Tests.Configuration;
using LuaEngine.Scripts.Tests.Helpers;
using LuaEngine.Scripts.WebApi.Database;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Profiles;
using LuaEngine.Scripts.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Monq.Core.Paging.Models;

namespace LuaEngine.Scripts.Tests.Repositories;

public class ScriptRepositoryTests : IClassFixture<ScriptsTestsFixture>
{
    private readonly DbContextOptions<ScriptsContext> _options;
    private readonly MapperConfiguration _mapperConfiguration;

    public ScriptRepositoryTests(ScriptsTestsFixture fixture)
    {
        _options = fixture.Options;
        _mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ScriptProfile>();
        });
    }

    [Theory(DisplayName = "GetAllAsync возвращает ожидаемые результаты.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task GetAllAsync_ReturnsExpectedResult(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var mapper = new Mapper(_mapperConfiguration);

        var paging = new PagingModel();
        var filter = new ScriptFilter();

        var scripts = ModelsGenerator.GenerateScriptCollection(sporadic, 10);

        IEnumerable<Script> result;
        using (var context = new ScriptsContext(_options))
        {
            await context.Scripts.AddRangeAsync(scripts);
            await context.SaveChangesAsync();

            var repository = new ScriptRepository(context, mapper);

            // Act
            result = await repository.GetAllAsync(paging, filter, CancellationToken.None);
        }

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(scripts.Count(), result.Count());

        // Annihilate
        await EnsureDeleted();
    }

    [Theory(DisplayName = "GetAsync возвращает ожидаемые результаты.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task GetAsync_ReturnsExpectedResult(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var mapper = new Mapper(_mapperConfiguration);

        var paging = new PagingModel();
        var filter = new ScriptFilter();

        var scripts = ModelsGenerator.GenerateScriptCollection(sporadic, 10);

        var scriptId = RandomGenerator.PickOne(scripts, sporadic).Value.Id;

        Script? result;
        using (var context = new ScriptsContext(_options))
        {
            await context.Scripts.AddRangeAsync(scripts);
            await context.SaveChangesAsync();

            var repository = new ScriptRepository(context, mapper);

            // Act
            result = await repository.GetAsync(scriptId, CancellationToken.None);
        }

        // Assert
        Assert.NotNull(result);
        Assert.Equal(scriptId, result.Id);

        // Annihilate
        await EnsureDeleted();
    }

    private async Task EnsureDeleted()
    {
        using (var context = new ScriptsContext(_options))
        {
            await context.Database.EnsureDeletedAsync();
        }
    }
}
