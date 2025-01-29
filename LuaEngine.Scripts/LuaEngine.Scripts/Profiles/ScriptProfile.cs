using AutoMapper;
using LuaEngine.Scripts.Models.Script;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Models.Filters;

namespace LuaEngine.Scripts.WebApi.Profiles;

/// <summary>
/// Профиль автомаппера скриптов.
/// </summary>
public class ScriptProfile : Profile
{
    public ScriptProfile()
    {
        CreateMap<Script, Script>();
        CreateMap<Script, ScriptViewModel>();
        CreateMap<ScriptPostViewModel, Script>();
        CreateMap<ScriptPutViewModel, Script>();
        CreateMap<ScriptFilterViewModel, ScriptFilter>();
    }
}
