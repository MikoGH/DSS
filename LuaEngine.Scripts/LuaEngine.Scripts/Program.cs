using LuaEngine.Scripts.WebApi.Extensions;
using LuaEngine.Scripts.WebApi.Middlewares;
using System.Text.Json;
using System.Text.Json.Serialization;
using static LuaEngine.Scripts.WebApi.Constants.AppConstants;

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
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    builder.Services.AddPostgresDbContext(builder.Configuration.GetConnectionString(PostgresConnectionStringSectionName));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddLuaEngineSwagger();
    builder.Services.AddLogging();
    builder.Services.AddServices();
    builder.Services.AddRepositories();
    builder.Services.AddRedis(builder.Configuration);

    var app = builder.Build();

    app.UseLuaEngineSwagger();
    app.UseCors(options => options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());
    app.UseRouting();
    app.MapControllers();
    app.UseMiddleware<ExceptionMiddleware>();
    await app.MigrateDatabaseAsync();

    app.Run();
}
catch (Exception exception)
{

}
