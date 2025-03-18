using AutoMapper;
using LuaEngine.Scripts.Models.ProcessScript;
using LuaEngine.Scripts.WebApi.Models;
using LuaEngine.Scripts.WebApi.Models.Filters;

namespace LuaEngine.Scripts.WebApi.Profiles;

/// <summary>
/// Профиль автомаппера скриптов-обработчиков.
/// </summary>
public class ProcessScriptProfile : Profile
{
    public ProcessScriptProfile()
    {
        CreateMap<ProcessScript, ProcessScript>();
        CreateMap<ProcessScript, ProcessScriptViewModel>();
        CreateMap<ProcessScriptPostViewModel, ProcessScript>();
        CreateMap<ProcessScriptPutViewModel, ProcessScript>();
        CreateMap<ProcessScriptFilterViewModel, ProcessScriptFilter>();
    }
}
