using LuaEngine.Scripts.Models.PrefilterScript;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using AutoMapper;

namespace LuaEngine.Scripts.WebApi.Profiles;

/// <summary>
/// Автомаппер скриптов-префильтров.
/// </summary>
public class PrefilterScriptProfile : Profile
{
    public PrefilterScriptProfile()
    {
        CreateMap<PrefilterScript, PrefilterScript>();
        CreateMap<PrefilterScript, PrefilterScriptViewModel>();
        CreateMap<PrefilterScriptPostViewModel, PrefilterScript>();
        CreateMap<PrefilterScriptPutViewModel, PrefilterScript>();
        CreateMap<PrefilterScriptFilterViewModel, PrefilterScriptFilter>();
    }
}
