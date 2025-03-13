using LuaEngine.LuaOrchestrator.Extensions;
using LuaEngine.LuaOrchestrator.Middlewares;
using System.Text.Json;

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

    var app = builder.Build();

    app.UseLuaEngineSwagger();
    app.UseCors(options => options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());
    app.UseRouting();
    app.MapControllers();
    app.UseMiddleware<ExceptionMiddleware>();

    app.Run();
}
catch (Exception exception)
{

}