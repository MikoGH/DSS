using LuaEngine.Scripts.WebApi.Models;

namespace LuaEngine.Scripts.Tests.Helpers;

public static class ModelsGenerator
{
    public static IEnumerable<Script> GenerateScriptCollection(Random sporadic, int count)
    {
        var scriptCollection = new List<Script>(count);

        var nameCollection = RandomGenerator.GenerateCollectionsOfUniqueStrings(sporadic, count);
        var descriptionCollection = RandomGenerator.GenerateCollectionsOfUniqueStrings(sporadic, count);

        for (int i = 0; i < count; i++)
        {
            var script = new Script()
            {
                Id = RandomGenerator.GenerateGuid(sporadic),
                SourceId = RandomGenerator.GenerateGuid(sporadic),
                Name = RandomGenerator.PickOne(nameCollection, sporadic).Value,
                Description = RandomGenerator.PickOne(descriptionCollection, sporadic).Value
            };
            scriptCollection.Add(script);
        }

        return scriptCollection;
    }
}
