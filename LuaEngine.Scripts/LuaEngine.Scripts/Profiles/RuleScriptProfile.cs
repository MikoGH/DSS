using AutoMapper;
using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.Models.RuleScript;

namespace LuaEngine.Scripts.WebApi.Profiles;

/// <summary>
/// Автомаппер скриптов-правил.
/// </summary>
public class RuleScriptProfile : Profile
{
    public RuleScriptProfile()
    {
        CreateMap<RuleScript, RuleScript>();
        CreateMap<RuleScript, RuleScriptViewModel>();
        CreateMap<RuleScriptPostViewModel, RuleScript>();
        CreateMap<RuleScriptPutViewModel, RuleScript>();
        CreateMap<RuleScriptFilterViewModel, RuleScriptFilter>();
    }
}
