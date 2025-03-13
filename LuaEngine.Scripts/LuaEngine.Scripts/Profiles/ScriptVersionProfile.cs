using AutoMapper;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.Models.ScriptVersion;

namespace LuaEngine.Scripts.WebApi.Profiles;

/// <summary>
/// Профиль автомаппера версий скриптов.
/// </summary>
public class ScriptVersionProfile : Profile
{
    public ScriptVersionProfile()
    {
        CreateMap<ScriptVersion, ScriptVersion>();
        CreateMap<ScriptVersion, ScriptVersionViewModel>();
        CreateMap<ScriptVersionPostViewModel, ScriptVersion>();
        CreateMap<ScriptVersionPutViewModel, ScriptVersion>();
        CreateMap<ScriptVersionFilterViewModel, ScriptVersionFilter>();
    }
}
