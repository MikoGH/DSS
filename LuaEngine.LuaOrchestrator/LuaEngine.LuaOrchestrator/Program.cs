using LuaEngine.LuaOrchestrator.Extensions;
using LuaEngine.LuaOrchestrator.Hubs;
using LuaEngine.LuaOrchestrator.Middlewares;
using System.Text.Json;
using static LuaEngine.LuaOrchestrator.Constants.AppConstants;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCors();
    builder.Services.AddControllers()
        .AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            opt.JsonSerializerOptions.AllowTrailingCommas = true;
            opt.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddLuaEngineSwagger();
    builder.Services.AddRabbitMq(builder.Configuration, typeof(Program).Assembly);
    builder.Services.AddServices();
    builder.Services.AddSignalR();

    var app = builder.Build();

    app.UseLuaEngineSwagger();
    app.UseCors(options => options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());
    app.UseRouting();
    app.MapControllers();
    app.MapHub<OrchestratorHub>(HubPath);
    app.UseMiddleware<ExceptionMiddleware>();

    app.Run();
}
catch (Exception exception)
{

}